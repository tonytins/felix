// This project is licensed under the MIT license.
// See the LICENSE file in the project root for more information.
using CommandLine;
using Workshop.ML.Chap2;

namespace Workshop.Term
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<TrainOption, PredictOption>(args)
                .WithParsed<TrainOption>(o =>
                {
                    Trainer(o.Chapter);
                })
                .WithParsed<PredictOption>(o =>
                {
                    Predictor(o.Chapter, o.Input);
                });

        }

        static void Trainer(string chap)
        {
            switch (chap)
            {
                case Chapters.Chap2:
                    var chap2 = new Chap2Trainer();
                    chap2.Train();
                    break;
                case Chapters.Chap31:
                    break;
                case Chapters.Chap32:
                    break;
            }
        }

        static void Predictor(string chap, string input)
        {
            switch (chap)
            {
                case Chapters.Chap2:
                    var chap2 = new Chap2Predictor(input);
                    chap2.Predict();
                    break;
                case Chapters.Chap31:
                    break;
                case Chapters.Chap32:
                    break;
            }
        }
    }
}
