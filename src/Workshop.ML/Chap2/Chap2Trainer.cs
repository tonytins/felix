// This project is licensed under the MPL 2.0 license.
// See the LICENSE file in the project root for more information.
using System;
using Microsoft.ML;
using Workshop.Common;
using Workshop.Models.Restaurant;

namespace Workshop.ML.Chap2
{
    public class Chap2Trainer : BaseML, ITrainer
    {
        public string TrainingDataFile => WorkshopHelper.GetTrainingDataFile("RestaurantFeedbackTest.csv");

        public string ModelPath => WorkshopHelper.GetModelPath("RestaurantFeedback.zip");

        public void Train()
        {
            var trainingData = WorkshopHelper.GetTrainingDataFile("RestaurantFeedbackTraining.csv");
            var trainingDataView = WorkshopHelper.GetTrainingData<RestaurantFeedback>(Context, trainingData);

            // Split sample data into training and test
            var dataSplit = Context.Data.TrainTestSplit(trainingDataView, testFraction: 0.2);

            var processPipeline = Context.Transforms.Text.FeaturizeText(
                outputColumnName: "Features",
                inputColumnName: nameof(RestaurantFeedback.Text));

            var trainer = Context.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: nameof(RestaurantFeedback.Label),
                featureColumnName: "Features");

            var trainingPipeline = processPipeline.Append(trainer);

            var trainedModel = trainingPipeline.Fit(dataSplit.TrainSet);

            Context.Model.Save(trainedModel, dataSplit.TrainSet.Schema, ModelPath);

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
