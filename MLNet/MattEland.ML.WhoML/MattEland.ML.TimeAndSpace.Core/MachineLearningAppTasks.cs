using MattEland.ML.Common;
using Microsoft.ML;
using Microsoft.ML.AutoML;
using Microsoft.ML.Data;

namespace MattEland.ML.TimeAndSpace.Core;

public class MachineLearningAppTasks
{
    const uint SecondsToTrain = 10;

    private readonly MLContext _context = new();

    public void PerformRegression()
    {
        // Load our source data and split it for training
        DataOperationsCatalog.TrainTestData trainTest = LoadTrainTestData();

        // Regression - Predict the Rating of a Doctor Who episode
        // Configure experiment
        RegressionExperimentSettings settings = new()
        {
            MaxExperimentTimeInSeconds = SecondsToTrain,
            OptimizingMetric = RegressionMetric.RSquared,
        };

        RegressionExperiment experiment = _context.Auto().CreateRegressionExperiment(settings);

        // Train a model
        Console.WriteLine($"Training for {SecondsToTrain} seconds...");

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
            _context.Model.CreatePredictionEngine<Episode, RatingPrediction>(
                transformer: result.BestRun.Model,
                inputSchema: trainTest.TestSet.Schema
            );

        // Predict Values from a sample episode
        Episode sampleEpisode = EpisodeBuilder.BuildSampleEpisode();

        // Get a rating prediction
        RatingPrediction prediction = predictionEngine.Predict(sampleEpisode);
        Console.WriteLine($"This hypothetical episode would rate a {prediction.Score}");
    }

    public void PerformBinaryClassification()
    {
        // Load our source data and split it for training
        DataOperationsCatalog.TrainTestData trainTest = LoadTrainTestData();

        // Binary Classification - Predict if an episode takes place in the present
        // Configure experiment
        BinaryExperimentSettings settings = new()
        {
            MaxExperimentTimeInSeconds = SecondsToTrain,
            OptimizingMetric = BinaryClassificationMetric.F1Score,
        };

        BinaryClassificationExperiment experiment = _context.Auto().CreateBinaryClassificationExperiment(settings);

        // Train a model
        Console.WriteLine($"Training for {SecondsToTrain} seconds...");

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
            _context.Model.CreatePredictionEngine<Episode, BinaryPrediction>(
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

    private DataOperationsCatalog.TrainTestData LoadTrainTestData()
    {
        IDataView rawData = _context.Data.LoadCsv<Episode>(path: "WhoDataSet.csv");
        DataOperationsCatalog.TrainTestData trainTest = _context.Data.TrainTestSplit(
            data: rawData,
            testFraction: 0.2,
            samplingKeyColumnName: null);

        return trainTest;
    }
}