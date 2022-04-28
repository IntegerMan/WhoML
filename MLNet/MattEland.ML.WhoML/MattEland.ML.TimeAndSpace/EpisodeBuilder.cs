using MattEland.ML.TimeAndSpace.Core;

namespace MattEland.ML.TimeAndSpace;

public static class EpisodeBuilder
{
    public static Episode BuildSampleEpisode() =>
        new()
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
}