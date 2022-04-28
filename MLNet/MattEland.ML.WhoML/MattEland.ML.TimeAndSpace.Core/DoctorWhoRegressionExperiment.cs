using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MattEland.ML.Common;
using Microsoft.ML;
using Microsoft.ML.AutoML;
using Microsoft.ML.Data;

namespace MattEland.ML.TimeAndSpace.Core;

public class DoctorWhoRegressionExperiment : DoctorWhoExperimentBase
{
    public void PerformRegression(string dataPath, uint secondsToTrain=30)
    {
        // Load our source data and split it for training
        DataOperationsCatalog.TrainTestData trainTest = LoadTrainTestData(dataPath);

        // Regression - Predict the Rating of a Doctor Who episode
        // Configure experiment
        RegressionExperimentSettings settings = new()
        {
            MaxExperimentTimeInSeconds = secondsToTrain,
            OptimizingMetric = RegressionMetric.RSquared,
        };

        RegressionExperiment experiment = Context.Auto().CreateRegressionExperiment(settings);

        // Train a model
        Console.WriteLine($"Training for {secondsToTrain} seconds...");

        ExperimentResult<RegressionMetrics> result = experiment.Execute(
            trainData: trainTest.TrainSet,
            validationData: trainTest.TestSet,
            labelColumnName: nameof(Episode.Rating),
            preFeaturizer: null,
            progressHandler: new RegressionConsoleProgressHandler());

        // Evaluate Results
        Console.WriteLine($"Best algorithm: {result.BestRun.TrainerName}{Environment.NewLine}");
        result.BestRun.ValidationMetrics.LogMetricsString();

        // Build a Prediction Engine to predict new values
        PredictionEngine<Episode, RatingPrediction> predictionEngine =
            Context.Model.CreatePredictionEngine<Episode, RatingPrediction>(
                transformer: result.BestRun.Model,
                inputSchema: trainTest.TestSet.Schema
            );

        // Predict Values from a sample episode
        Episode sampleEpisode = EpisodeBuilder.BuildSampleEpisode();

        // Get a rating prediction
        RatingPrediction prediction = predictionEngine.Predict(sampleEpisode);
        Console.WriteLine($"This hypothetical episode would rate a {prediction.Score}");
    }
}