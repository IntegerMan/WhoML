using MattEland.ML.Common;
using Microsoft.ML;
using Microsoft.ML.AutoML;
using Microsoft.ML.Data;

namespace MattEland.ML.TimeAndSpace.Core;

public class DoctorWhoClassificationExperiment : DoctorWhoExperimentBase
{
    public void PerformBinaryClassification(string dataPath, uint secondsToTrain = 30)
    {
        // Load our source data and split it for training
        DataOperationsCatalog.TrainTestData trainTest = LoadTrainTestData(dataPath);

        // Binary Classification - Predict if an episode takes place in the present
        // Configure experiment
        BinaryExperimentSettings settings = new()
        {
            MaxExperimentTimeInSeconds = secondsToTrain,
            OptimizingMetric = BinaryClassificationMetric.F1Score,
        };

        BinaryClassificationExperiment experiment = Context.Auto().CreateBinaryClassificationExperiment(settings);

        // Train a model
        Console.WriteLine($"Training for {secondsToTrain} seconds...");

        ExperimentResult<BinaryClassificationMetrics> result = experiment.Execute(
            trainData: trainTest.TrainSet,
            validationData: trainTest.TestSet,
            labelColumnName: nameof(Episode.IsPresent),
            preFeaturizer: null,
            progressHandler: new BinaryClassificationConsoleProgressHandler());

        // Evaluate Results
        Console.WriteLine($"Best algorithm: {result.BestRun.TrainerName}{Environment.NewLine}");
        //result.BestRun.ValidationMetrics.LogMetricsString();

        // Build a Prediction Engine to predict new values
        PredictionEngine<Episode, BinaryPrediction> predictionEngine =
            Context.Model.CreatePredictionEngine<Episode, BinaryPrediction>(
                transformer: result.BestRun.Model,
                inputSchema: trainTest.TestSet.Schema
            );

        // Predict Values from a sample episode
        Episode sampleEpisode = EpisodeBuilder.BuildSampleEpisode();

        // Get a rating prediction
        BinaryPrediction prediction = predictionEngine.Predict(sampleEpisode);

        Console.WriteLine(prediction.Value
            ? $"This hypothetical episode WOULD take place on earth with a score of {prediction.Confidence}"
            : $"This hypothetical episode would NOT take place on earth with a score of {prediction.Confidence}");
    }
}