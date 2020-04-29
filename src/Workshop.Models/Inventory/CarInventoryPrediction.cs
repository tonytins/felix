// This project is licensed under the MIT license.
// See the LICENSE file in the project root for more information.
namespace Workshop.Models.Inventory
{
    public class CarInventoryPrediction
    {
        public bool Label { get; set; }

        public bool PredictedLabel { get; set; }

        public float Score { get; set; }

        public float Probability { get; set; }
    }
}