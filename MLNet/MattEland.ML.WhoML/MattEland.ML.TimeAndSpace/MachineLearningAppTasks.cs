using MattEland.ML.Common;
using Microsoft.ML;

namespace MattEland.ML.TimeAndSpace;

public class MachineLearningAppTasks
{
    public void PerformRegression()
    {
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

    }
}