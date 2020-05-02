// This project is licensed under the MPL 2.0 license.
// See the LICENSE file in the project root for more information.
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
