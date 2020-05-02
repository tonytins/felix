// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at
// http://mozilla.org/MPL/2.0/.
using CommandLine;

namespace Workshop.Term
{
    public class BaseOptions
    {
        [Option('c', "chapter")]
        public string Chapter { get; set; }

        [Option('i', "input")]
        public string Input { get; set; }
    }

    [Verb("train")]
    public class TrainOption : BaseOptions { }

    [Verb("predict")]
    public class PredictOption : BaseOptions { }
}
