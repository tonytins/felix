// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at
// http://mozilla.org/MPL/2.0/.
using Microsoft.ML.Data;

namespace Workshop.Models.Employement
{
    public class EmploymentHistory
    {
        [LoadColumn(0)]
        public float DurationInMonths { get; set; }

        [LoadColumn(1)]
        public float IsMarried { get; set; }
        [LoadColumn(2)]
        public float BSDegree { get; set; }
        [LoadColumn(3)]
        public float MSDegree { get; set; }
        [LoadColumn(4)]
        public float YearsExperience { get; set; }
        [LoadColumn(5)]
        public float AgeAtHire { get; set; }
        [LoadColumn(6)]
        public float HasKids { get; set; }
        [LoadColumn(7)]
        public float WithinMonthOfVesting { get; set; }
        [LoadColumn(8)]
        public float DeskDecorations { get; set; }
        [LoadColumn(9)]
        public float LongCommute { get; set; }
    }
}
