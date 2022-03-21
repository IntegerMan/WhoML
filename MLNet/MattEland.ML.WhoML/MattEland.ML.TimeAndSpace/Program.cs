// See https://aka.ms/new-console-template for more information

using Microsoft.ML;

Console.WriteLine("Hello, Time and Space!");

// Regression - Predict the Rating of a Doctor Who episode

// Context
MLContext context = new();

// Load
IDataView data = context.Data.LoadFromTextFile<Episode>(
        path: "WhoDataSet.csv", 
        separatorChar: ',', 
        hasHeader: true, 
        allowQuoting: true, 
        trimWhitespace: true
    );

Console.WriteLine("Allons-y!");