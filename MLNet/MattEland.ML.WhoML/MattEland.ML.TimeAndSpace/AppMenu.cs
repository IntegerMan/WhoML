using MattEland.ML.TimeAndSpace.Core;

namespace MattEland.ML.TimeAndSpace;

internal class AppMenu
{
    private const string DataFilePath = "WhoDataSet.csv";

    private string GetMainMenuChoice()
    {
        Console.WriteLine();
        Console.WriteLine("The available options are: ");
        Console.WriteLine();
        Console.WriteLine("1) Regression");
        Console.WriteLine("2) Binary Classification");
        Console.WriteLine("3) Multi-Class Classification");
        Console.WriteLine("4) Recommendation");
        Console.WriteLine("5) Ranking");
        Console.WriteLine("Q) Quit");

        Console.WriteLine();
        Console.WriteLine("What do you want to do?");

        return Console.ReadLine()!;
    }

    public void RunMainMenu()
    {

        ShowWelcome();

        bool keepGoing = true;
        while (keepGoing)
        {
            string choice = GetMainMenuChoice();
            Console.WriteLine();

            switch (choice.ToUpperInvariant())
            {
                case "1": // Regression
                    DoctorWhoRegressionExperiment regressionExperiment = new();
                    regressionExperiment.PerformRegression(DataFilePath);
                    break;

                case "2": // Binary Classification
                    DoctorWhoClassificationExperiment binaryExperiment = new();
                    binaryExperiment.PerformBinaryClassification(DataFilePath);
                    break;

                case "3": // Multi-Class Classification
                    Console.WriteLine("Multi-Class Classification is not implemented");
                    break;

                case "4": // Recommendation
                    Console.WriteLine("Recommendation is not implemented");
                    break;

                case "5": // Ranking
                    Console.WriteLine("Ranking is not implemented");
                    break;

                case "Q":
                    Console.WriteLine("Thanks for using the application! Allons-y!");
                    keepGoing = false;
                    break;

                default:
                    Console.WriteLine("That is not a supported input");
                    break;
            }
        }
    }

    private void ShowWelcome()
    {
        // Welcome text
        Console.WriteLine("Hello, Time and Space!");
        Console.WriteLine();
        Console.WriteLine("This app is intended to demonstrate ML.NET AutoML and is not intended for serious usage");
        Console.WriteLine("Doctor Who and associated properties and trademarks are Copyright of the British Broadcasting Corporation (BBC)");
        Console.WriteLine();
    }
}