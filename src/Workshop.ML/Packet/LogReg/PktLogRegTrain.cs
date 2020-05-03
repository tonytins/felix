// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at
// http://mozilla.org/MPL/2.0/.
using System;
using Microsoft.ML;
using Workshop.Common;
using Workshop.Models.Restaurant;

namespace Workshop.ML.Packet.LogisticRegression
{
    public class PktLogRegTrain : BaseML, ITrainer
    {
        public void Train()
        {
            var modelPath = WorkshopHelper.GetModelPath("RestaurantFeedback.zip"); 
            var trainingData = WorkshopHelper.GetTrainingDataFile("packet", "packt", "RestaurantFeedbackTraining.csv");
            var trainingDataView = WorkshopHelper.LoadTrainingData<RestaurantFeedback>(Context, trainingData);

            // Split sample data into training and test
            var dataSplit = Context.Data.TrainTestSplit(trainingDataView, testFraction: 0.2);

            var processPipeline = Context.Transforms.Text.FeaturizeText(
                outputColumnName: "Features",
                inputColumnName: nameof(RestaurantFeedback.Text));

            var trainer = Context.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: nameof(RestaurantFeedback.Label),
                featureColumnName: "Features")
                .AppendCacheCheckpoint(Context);

            var trainingPipeline = processPipeline.Append(trainer);

            var trainedModel = trainingPipeline.Fit(dataSplit.TrainSet);

            Context.Model.Save(trainedModel, dataSplit.TrainSet.Schema, modelPath);

            var testSetTransform = trainedModel.Transform(dataSplit.TestSet);

            var modelMetrics = Context.BinaryClassification.Evaluate(data: testSetTransform, labelColumnName: nameof(RestaurantFeedback.Label), nameof(RestaurantPrediction.Score));

            Console.WriteLine($"Area Under Curve: {modelMetrics.AreaUnderRocCurve:P2}{Environment.NewLine}" +
                              $"Area Under Precision Recall Curve: {modelMetrics.AreaUnderPrecisionRecallCurve:P2}{Environment.NewLine}" +
                              $"Accuracy: {modelMetrics.Accuracy:P2}{Environment.NewLine}" +
                              $"F1Score: {modelMetrics.F1Score:P2}{Environment.NewLine}" +
                              $"Positive Recall: {modelMetrics.PositiveRecall:#.##}{Environment.NewLine}" +
                              $"Negative Recall: {modelMetrics.NegativeRecall:#.##}{Environment.NewLine}");
        }
    }
}
