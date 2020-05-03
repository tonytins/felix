// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at
// http://mozilla.org/MPL/2.0/.
using System;
using ConsoleTables;
using CommandLine;
using Workshop.ML.Packet.LogisticRegression;

namespace Workshop.Term
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<TrainOption, PredictOption>(args)
                .WithParsed<TrainOption>(o =>
                {
                    Trainer(o.Chapter, o.Input);
                })
                .WithParsed<PredictOption>(o =>
                {
                    Predictor(o.Chapter, o.Input);
                });

        }

        static void PrintTable()
        {
            var cmdTable = new ConsoleTable("Command", "Description");

            cmdTable.AddRow("pkt-logreg", "Restaurant reviews");
            cmdTable.Write(Format.Minimal);

            Console.WriteLine("Usage: mlworkshop [train|predict] -c [command] -i [input]");
        }

        static void Trainer(string chap, string input)
        {
            switch (chap)
            {
                case Courses.Chap2:
                    var chap2 = new Chap2Trainer();
                    chap2.Train();
                    break;
                case Courses.Chap31:
                    break;
                default:
                    PrintTable();
                    break;
            }
        }

        static void Predictor(string chap, string input)
        {
            switch (chap)
            {
                case Courses.Chap2:
                    var chap2 = new Chap2Predictor(input);
                    chap2.Predict();
                    break;
                case Courses.Chap31:
                    break;
                default:
                    PrintTable();
                    break;
            }
        }
    }
}
