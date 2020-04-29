// This project is licensed under the MIT license.
// See the LICENSE file in the project root for more information.
using Microsoft.ML.Data;

namespace Workshop.Models.Restaurant
{
    public class RestaurantFeedback
    {
        [LoadColumn(0)]
        public bool Label { get; set; }

        [LoadColumn(1)]
        public string Text { get; set; }
    }
}
