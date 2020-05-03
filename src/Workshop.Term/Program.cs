// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at
// http://mozilla.org/MPL/2.0/.
using System;
using ConsoleTables;
using CommandLine;
using Workshop.ML.Packet.LogisticRegression;
using Workshop.ML.Packet.MultiClass;
using Workshop.Common;
using System.Text.Json;
using Workshop.Models.SpamDetect;

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
                case Courses.PktLogReg:
                    var chap2 = new PktLogRegTrain();
                    chap2.Train();
                    break;
                case Courses.PktMultiClass:
                    var pktMultiTrain = new PktMultiClassTrain();
                    pktMultiTrain.Train();
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
                case Courses.PktLogReg:
                    var chap2 = new PktLogRegPredict(input);
                    chap2.Predict();
                    break;
                case Courses.PktMultiClass:
                    var emailJson = WorkshopHelper.GetPredictionDataFile(input);
                    var pktMultiPredict = new PktMultiClassPredict(emailJson);
                    pktMultiPredict.Predict();
                    break;
                default:
                    PrintTable();
                    break;
            }
        }
    }
}
