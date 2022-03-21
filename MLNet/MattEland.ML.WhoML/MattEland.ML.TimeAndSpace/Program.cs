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
IDataView rawData = context.Data.LoadFromTextFile<Episode>(
        path: "WhoDataSet.csv", 
        separatorChar: ',', 
        hasHeader: true, 
        allowQuoting: true, 
        trimWhitespace: true
    );

// Split our data for validation
DataOperationsCatalog.TrainTestData trainTest = context.Data.TrainTestSplit(
    data: rawData, 
    testFraction: 0.3, 
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
Console.WriteLine("Beginning Training");
ExperimentResult<RegressionMetrics> result = experiment.Execute(
    trainData: trainTest.TrainSet, 
    validationData: trainTest.TestSet, 
    labelColumnName: nameof(Episode.Rating),
    preFeaturizer: null, 
    progressHandler: null);

// Evaluate Results
Console.WriteLine($"Best algorithm: {result.BestRun.TrainerName}");

Console.WriteLine(result.BestRun.ValidationMetrics.BuildMetricsString());

// Tell the user we're done
Console.WriteLine();
Console.WriteLine("Allons-y!");