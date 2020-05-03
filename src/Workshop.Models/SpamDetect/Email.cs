// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at
// http://mozilla.org/MPL/2.0/.
using Microsoft.ML.Data;

namespace Workshop.Models.SpamDetect
{
    public class Email
    {
        [LoadColumn(0)]
        public string Subject { get; set; }
        [LoadColumn(1)]
        public string Body { get; set; }
        [LoadColumn(2)]
        public string Sender { get; set; }
        [LoadColumn(3)]
        public string Category { get; set; }


    }
}
