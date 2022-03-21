using MattEland.ML.Common;
using Microsoft.ML;
using Microsoft.ML.AutoML;
using Microsoft.ML.Data;

// Welcome text
Console.WriteLine("Hello, Time and Space!");
Console.WriteLine();
Console.WriteLine("This app is intended to demonstrate ML.NET AutoML and is not intended for serious usage");
Console.WriteLine("Doctor Who and associated properties and trademarks are Copyright of the British Broadcasting Corporation (BBC)");
Console.WriteLine();

// Context
MLContext context = new();

// Load
IDataView rawData = context.Data.LoadCsv<Episode>(path: "WhoDataSet.csv");

// Split our data for validation
DataOperationsCatalog.TrainTestData trainTest = context.Data.TrainTestSplit(
    data: rawData, 
    testFraction: 0.2, 
    samplingKeyColumnName: null);

// Regression - Predict the Rating of a Doctor Who episode

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
ExperimentResult<RegressionMetrics> result = experiment.Execute(
    trainData: trainTest.TrainSet, 
    validationData: trainTest.TestSet, 
    labelColumnName: nameof(Episode.Rating),
    preFeaturizer: null, 
    progressHandler: new RegressionConsoleProgressHandler());

// Evaluate Results
Console.WriteLine($"Best algorithm: {result.BestRun.TrainerName}");
Console.WriteLine();
result.BestRun.ValidationMetrics.LogMetricsString();

// Build a Prediction Engine to predict new values
PredictionEngine<Episode, RatingPrediction> predictionEngine = 
    context.Model.CreatePredictionEngine<Episode, RatingPrediction>(
        transformer: result.BestRun.Model, 
        inputSchema: rawData.Schema
    );

// Predict Values from a sample episode
Episode sampleEpisode = new()
{
    AiredFriday = true,
    Has11 = true,
    HasTheMaster = true,
    HasAmy = true,
    HasRory = true,
    HasRiver = true,
    HasWarDoctor = true,
    HasGraham = true,
    HasSontaran = true,
    Producer = "Stephen Moffat",
    Writer = "Stephen Moffat",
    Music = "Murray Gold",
    IsFuture = true,
    IsSpace = true,
};

// Get a rating prediction
RatingPrediction prediction = predictionEngine.Predict(sampleEpisode);
Console.WriteLine($"This hypothetical episode would rate a {prediction.Score}");

// Tell the user we're done
Console.WriteLine();
Console.WriteLine("Allons-y!");