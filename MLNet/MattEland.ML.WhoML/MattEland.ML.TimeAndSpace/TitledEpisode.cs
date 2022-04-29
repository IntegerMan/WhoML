using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MattEland.ML.TimeAndSpace.Core;

namespace MattEland.ML.TimeAndSpace;

public class TitledEpisode : Episode
{
    public string Title { get; set; }
}