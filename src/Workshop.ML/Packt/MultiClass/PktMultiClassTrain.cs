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
using Microsoft.ML;
using Workshop.Common;
using Workshop.Models.Packt.SpamDetect;

namespace Workshop.ML.Packt.MultiClass
{
    public class PktMultiClassTrain : BaseML, ITrainer
    {
        public PktMultiClassTrain(string trainingFile = "EmailTraining.csv", string modelFile = "Email.zip") : base(trainingFile, modelFile)
        {
        }

        public void Train()
        {
            var modelPath = WorkshopHelper.GetModelPath(ModelFile);
            var trainingData = WorkshopHelper.GetTrainingDataFile("packt", TrainingFile);
            var trainingDataView = WorkshopHelper.LoadTrainingData<Email>(Context, trainingData, shuffle: true);

            var dataProcessingPipeline = Context.Transforms.Conversion.MapValueToKey(inputColumnName: nameof(Email.Category), outputColumnName: "Label")
                .Append(Context.Transforms.Text.FeaturizeText(inputColumnName: nameof(Email.Subject), outputColumnName: "SubjectFeaturized"))
                .Append(Context.Transforms.Text.FeaturizeText(inputColumnName: nameof(Email.Body), outputColumnName: "BodyFeaturized"))
                .Append(Context.Transforms.Text.FeaturizeText(inputColumnName: nameof(Email.Sender), outputColumnName: "SenderFeaturized"))
                .Append(Context.Transforms.Concatenate("Features",
                "SubjectFeaturized", "BodyFeaturized", "SenderFeaturized"))
                .AppendCacheCheckpoint(Context);

            var trainingPipeline = dataProcessingPipeline.Append(Context.MulticlassClassification.Trainers
                .SdcaMaximumEntropy("Label", "Features"))
                .Append(Context.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var testData = WorkshopHelper.GetTrainingDataFile("packt", "EmailTest.csv");
            var testDataView = WorkshopHelper.LoadTrainingData<Email>(Context, testData);

            var trainedModel = trainingPipeline.Fit(trainingDataView);

            Context.Model.Save(trainedModel, trainingDataView.Schema, modelPath);

            var modelMetrics = Context.MulticlassClassification.Evaluate(trainedModel.Transform(testDataView));

            Console.WriteLine($"MicroAccuracy: {modelMetrics.MicroAccuracy:0.###}");
            Console.WriteLine($"MacroAccuracy: {modelMetrics.MacroAccuracy:0.###}");
            Console.WriteLine($"LogLoss: {modelMetrics.LogLoss:#.###}");
            Console.WriteLine($"LogLossReduction: {modelMetrics.LogLossReduction:#.###}");
        }
    }
}
