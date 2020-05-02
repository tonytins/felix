// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at
// http://mozilla.org/MPL/2.0/.
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
