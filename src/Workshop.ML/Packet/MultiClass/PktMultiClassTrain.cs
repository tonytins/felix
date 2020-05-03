// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at
// http://mozilla.org/MPL/2.0/.
using System;
using Microsoft.ML;
using Workshop.Common;
using Workshop.Models.Inventory;
using Workshop.Models.SpamDetect;

namespace Workshop.ML.Packet.MultiClass
{
    public class PktMultiClassTrain : BaseML, ITrainer
    {
        public void Train()
        {
            var modelPath = WorkshopHelper.GetModelPath("RestaurantFeedback.zip");
            var trainingData = WorkshopHelper.GetTrainingDataFile("packt", "EmailTraining.csv");
            var trainingDataView = WorkshopHelper.LoadTrainingData<Email>(Context, trainingData);

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
