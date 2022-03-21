using MattEland.ML.Common;
using MattEland.ML.TimeAndSpace;
using Microsoft.ML;

// Welcome text
Console.WriteLine("Hello, Time and Space!");
Console.WriteLine();
Console.WriteLine("This app is intended to demonstrate ML.NET AutoML and is not intended for serious usage");
Console.WriteLine("Doctor Who and associated properties and trademarks are Copyright of the British Broadcasting Corporation (BBC)");
Console.WriteLine();

// Context
MLContext context = new();

// Load our source data and split it for training
IDataView rawData = context.Data.LoadCsv<Episode>(path: "WhoDataSet.csv");
DataOperationsCatalog.TrainTestData trainTest = context.Data.TrainTestSplit(
    data: rawData,
    testFraction: 0.2,
    samplingKeyColumnName: null);

// Regression - Predict the Rating of a Doctor Who episode
PredictionEngine<Episode, RatingPrediction> predictionEngine = 
    context.TrainDoctorWhoRegressionPredictor(trainTest);

// Predict Values from a sample episode
Episode sampleEpisode = EpisodeBuilder.BuildSampleEpisode();

// Get a rating prediction
RatingPrediction prediction = predictionEngine.Predict(sampleEpisode);
Console.WriteLine($"This hypothetical episode would rate a {prediction.Score}");

// Tell the user we're done
Console.WriteLine();
Console.WriteLine("Allons-y!");