using System.Text;
using Microsoft.ML.Data;

namespace MattEland.ML.Common
{
    public static class MetricsFormatter
    {
        public static string BuildMetricsString(this RegressionMetrics metrics)
        {
            StringBuilder sb = new();
            sb.AppendLine($"R Squared (Coefficient of Determination): {metrics.RSquared}");
            sb.AppendLine($"Mean Absolute Error (MAE): {metrics.MeanAbsoluteError}");
            sb.AppendLine($"Mean Squared Error (MSE): {metrics.MeanSquaredError}");
            sb.AppendLine($"Root Mean Squared Error (RMSE): {metrics.RootMeanSquaredError}");
            sb.AppendLine($"Loss Function: {metrics.LossFunction}");

            return sb.ToString();
        }
    }
}