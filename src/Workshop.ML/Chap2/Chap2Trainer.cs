// This project is licensed under the MIT license.
// See the LICENSE file in the project root for more information.
using System;
using System.IO;
using Microsoft.ML;
using Workshop.Common;
using Workshop.Models.Restaurant;

namespace Workshop.ML.Chap2
{
    public class Chap2Trainer : BaseML, ITrainer
    {
        public string TrainingFile => WorkshopHelper.GetTrainingData("RestaurantFeedbackTest.csv");

        public string ModelPath => WorkshopHelper.GetModelPath("RestaurantFeedback.zip");

        public void Train()
        {
            if (!File.Exists(TrainingFile))
            {
                Console.WriteLine($"Failed to locate {TrainingFile}");
                return;
            }

            var trainingDataView = MlContext.Data.LoadFromTextFile<RestaurantFeedback>(TrainingFile);

            var dataSplit = MlContext.Data.TrainTestSplit(trainingDataView, testFraction: 0.2);

            var processPipeline = MlContext.Transforms.Text.FeaturizeText(
                outputColumnName: "Features",
                inputColumnName: nameof(RestaurantFeedback.Text));

            var trainer = MlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: nameof(RestaurantFeedback.Label),
                featureColumnName: "Features");

            var trainingPipeline = processPipeline.Append(trainer);

            var trainedModel = trainingPipeline.Fit(dataSplit.TrainSet);

            MlContext.Model.Save(trainedModel, dataSplit.TrainSet.Schema, ModelPath);

            var testSetTransform = trainedModel.Transform(dataSplit.TestSet);

            var modelMetrics = MlContext.BinaryClassification.Evaluate(data: testSetTransform, labelColumnName: nameof(RestaurantFeedback.Label), nameof(RestaurantPrediction.Score));

            Console.WriteLine($"Area Under Curve: {modelMetrics.AreaUnderRocCurve:P2}{Environment.NewLine}" +
                              $"Area Under Precision Recall Curve: {modelMetrics.AreaUnderPrecisionRecallCurve:P2}{Environment.NewLine}" +
                              $"Accuracy: {modelMetrics.Accuracy:P2}{Environment.NewLine}" +
                              $"F1Score: {modelMetrics.F1Score:P2}{Environment.NewLine}" +
                              $"Positive Recall: {modelMetrics.PositiveRecall:#.##}{Environment.NewLine}" +
                              $"Negative Recall: {modelMetrics.NegativeRecall:#.##}{Environment.NewLine}");
        }
    }
}
