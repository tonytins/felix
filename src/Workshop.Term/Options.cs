// This project is licensed under the MIT license.
// See the LICENSE file in the project root for more information.
using CommandLine;

namespace Workshop.Term
{
    [Verb("train")]
    public class TrainOption
    {
        [Option('c', "chapter")]
        public string Chapter { get; set; }
    }

    [Verb("predict")]
    public class PredictOption
    {
        [Option('c', "chapter")]
        public string Chapter { get; set; }

        [Option('i', "input")]
        public string Input { get; set; }
    }
}
