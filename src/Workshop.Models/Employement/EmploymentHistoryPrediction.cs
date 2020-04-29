// This project is licensed under the MIT license.
// See the LICENSE file in the project root for more information.
using Microsoft.ML.Data;

namespace Workshop.Models.Employement
{
    public class EmploymentHistoryPrediction
    {
        [ColumnName("Score")]
        public float DurationInMonths { get; set; }
    }
}
