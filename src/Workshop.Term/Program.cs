// This project is licensed under the MIT license.
// See the LICENSE file in the project root for more information.
using CommandLine;
using Workshop.ML.Chap2;

namespace Workshop.Term
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<TrainOption, PredictOption>(args)
                .WithParsed<TrainOption>(o =>
                {
                    switch (o.Chapter)
                    {
                        case Chapters.Chap2:
                            var chap2 = new Chap2Trainer();
                            chap2.Train();
                            break;
                    }
                })
                .WithParsed<PredictOption>(o =>
                {
                    switch (o.Chapter)
                    {
                        case Chapters.Chap2:
                            var chap2 = new Chap2Predictor(o.Input);
                            chap2.Predict();
                            break;
                    }
                });
        }
    }
}
