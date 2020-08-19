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
using Workshop.Models.Packt.Restaurant;

namespace Workshop.ML.Packt.LogisticRegression
{
    public class PktLogRegTrain : BaseML, ITrainer
    {
        public PktLogRegTrain(string file = "RestaurantFeedbackTraining.csv", string modelFile = "RestaurantFeedback.zip") : base(file, modelFile)
        {
        }

        public void Train()
        {
            var modelPath = WorkshopHelper.GetModelPath(ModelFile);
            var trainingData = WorkshopHelper.GetTrainingDataFile("packt", TrainingFile);
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
