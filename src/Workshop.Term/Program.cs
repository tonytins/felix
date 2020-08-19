/*
 *   Copyright 2020 Anthony Leland
 *
 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
 */
using System;
using CommandLine;
using ConsoleTables;
using Workshop.Common;
using Workshop.ML.Packt.LogisticRegression;
using Workshop.ML.Packt.MultiClass;

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
