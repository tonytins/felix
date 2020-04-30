// This project is licensed under the MPL 2.0 license.
// See the LICENSE file in the project root for more information.
using Microsoft.ML.Data;

namespace Workshop.Models.Restaurant
{
    public class RestaurantPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        public float Probability { get; set; }

        public float Score { get; set; }
    }
}
