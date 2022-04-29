using MattEland.ML.TimeAndSpace;
using MattEland.ML.TimeAndSpace.Core;
using Newtonsoft.Json;

// Train a regression model to predict episode scores based on historic episodes
const uint secondsToTrain = 10;
DoctorWhoRegressionExperiment experiment = new();
experiment.Train(dataPath: "WhoDataSet.csv", secondsToTrain: secondsToTrain);

// Generate a bunch of episodes, keeping only a season's worth of the best options
const int episodesToGenerate = 1000;
const int episodesToKeep = 12;
Console.WriteLine("Generating a season of " + episodesToKeep + " episodes from " + episodesToGenerate + " candidates");

List<TitledEpisode> episodes = new List<TitledEpisode>(episodesToKeep + 1); // Adding + 1 here because it will temporarily have one more before we prune it

for (int i = 0; i < episodesToGenerate; i++)
{
    // Build a random episode
    TitledEpisode ep = EpisodeBuilder.BuildRandomEpisode();

    // Generate a predicted score for that episode
    RegressionPrediction prediction = experiment.Predict(ep);
    ep.Rating = prediction.Score;

    // If this is one of the best episodes that we have, or the list isn't full yet, add it to the season
    if (episodes.Count < episodesToKeep)
    {
        episodes.Add(ep);
    } 
    else if (ep.Rating > episodes.Min(e => e.Rating))
    {
        episodes.Add(ep);
        episodes = episodes.OrderByDescending(e => e.Rating).Take(episodesToKeep).ToList();
    }
}

// Display the best episodes
Console.WriteLine("The best episode involves...");

TitledEpisode bestEpisode = episodes.OrderByDescending(e => e.Rating).First();
Console.WriteLine(JsonConvert.SerializeObject(bestEpisode, Formatting.Indented));

// Serialize the full season to disk
File.WriteAllText("season.json", JsonConvert.SerializeObject(episodes, Formatting.Indented));
Console.WriteLine();
Console.WriteLine("Saved full season to season.json for inspection");

// Closing
Console.WriteLine();
Console.WriteLine("And that's all we have! Allons-y!");