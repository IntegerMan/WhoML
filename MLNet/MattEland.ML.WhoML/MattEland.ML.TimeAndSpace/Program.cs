using MattEland.ML.TimeAndSpace;
using MattEland.ML.TimeAndSpace.Core;
using Newtonsoft.Json;

// Train a regression model to predict episode scores based on historic episodes
DoctorWhoRegressionExperiment experiment = new();
experiment.Train(dataPath: "WhoDataSet.csv", secondsToTrain: 1);

// Generate a bunch of episodes, keeping only a season's worth of the best options
const int EpisodesToGenerate = 1000;
const int EpisodesToKeep = 3;
Console.WriteLine("Generating a season of " + EpisodesToKeep + " episodes from " + EpisodesToGenerate + " candidates");

List<TitledEpisode> episodes = new List<TitledEpisode>(EpisodesToKeep + 1); // Adding + 1 here because it will temporarily have one more before we prune it

for (int i = 0; i < EpisodesToGenerate; i++)
{
    // Build a random episode
    TitledEpisode ep = EpisodeBuilder.BuildRandomEpisode();

    // Generate a predicted score for that episode
    RegressionPrediction prediction = experiment.Predict(ep);
    ep.Rating = prediction.Score;

    // If this is one of the best episodes that we have, or the list isn't full yet, add it to the season
    if (episodes.Count < EpisodesToKeep)
    {
        episodes.Add(ep);
    } 
    else if (ep.Rating > episodes.Min(e => e.Rating))
    {
        episodes.Add(ep);
        episodes = episodes.OrderByDescending(e => e.Rating).Take(EpisodesToKeep).ToList();
    }
}

// Display the best episodes
int index = 0;
foreach (TitledEpisode episode in episodes)
{
    string json = JsonConvert.SerializeObject(episode, Formatting.Indented);

    Console.WriteLine($"Episode {++index}");
    Console.WriteLine(json);
    Console.WriteLine();
}

Console.WriteLine("And that's all we have! Allons-y!");