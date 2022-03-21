using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MattEland.ML.Common;
using Microsoft.ML;
using Microsoft.ML.AutoML;
using Microsoft.ML.Data;
using Tensorflow.Contexts;

namespace MattEland.ML.TimeAndSpace;

public static class RegressionModelTrainer
{
    public static PredictionEngine<Episode, RatingPrediction> TrainDoctorWhoRegressionPredictor(
        this MLContext context,
        DataOperationsCatalog.TrainTestData trainTest)
    {
        // Configure experiment
        const uint secondsToTrain = 10;
        RegressionExperimentSettings settings = new()
        {
            MaxExperimentTimeInSeconds = secondsToTrain,
            OptimizingMetric = RegressionMetric.RSquared,
        };

        RegressionExperiment experiment = context.Auto().CreateRegressionExperiment(settings);

        // Train a model
        Console.WriteLine($"Training for {secondsToTrain} seconds...");

        RegressionConsoleProgressHandler progressHandler = new();
        ExperimentResult<RegressionMetrics> result = experiment.Execute(
            trainData: trainTest.TrainSet,
            validationData: trainTest.TestSet,
            labelColumnName: nameof(Episode.Rating),
            preFeaturizer: null,
            progressHandler: progressHandler);

        // Evaluate Results
        Console.WriteLine($"Best algorithm: {result.BestRun.TrainerName}");
        Console.WriteLine();
        result.BestRun.ValidationMetrics.LogMetricsString();

        // Build a Prediction Engine to predict new values
        PredictionEngine<Episode, RatingPrediction> predictionEngine =
            context.Model.CreatePredictionEngine<Episode, RatingPrediction>(
                transformer: result.BestRun.Model,
                inputSchema: trainTest.TestSet.Schema
            );


        return predictionEngine;
    }
}