// This project is licensed under the MPL 2.0 license.
// See the LICENSE file in the project root for more information.
using System;
using Workshop.Common;
using Workshop.Models.Inventory;

namespace Workshop.ML.Chap31
{
    class Chap31Trainer : BaseML, ITrainer
    {
        public string ModelPath => throw new NotImplementedException();

        public string TrainingDataFile => throw new NotImplementedException();

        public void Train()
        {
            var trainingData = WorkshopHelper.GetTrainingDataFile("CarInventoryTest.csv");
            var trainingDataView = WorkshopHelper.GetTrainingData<CarInventory>(Context, trainingData);

            // Split sample data into training and test
            var dataSplit = Context.Data.TrainTestSplit(trainingDataView, testFraction: 0.2);

            //var processPipeline = Context.Transforms.Concatenate("Features",
            //    typeof(CarInventory).ToPropertyList<CarInventory>(nameof(CarInventory.Label)))
            //    .Append(Context.Transforms.NormalizeMeanVariance(inputColumnName: "Features", outputColumnName: "FeaturesNormalizedByMeanVar"));

            //var trainer = Context.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: nameof(RestaurantFeedback.Label),
            //    featureColumnName: "Features");

            //var trainingPipeline = processPipeline.Append(trainer);

            //var trainedModel = trainingPipeline.Fit(dataSplit.TrainSet);

            //Context.Model.Save(trainedModel, dataSplit.TrainSet.Schema, ModelPath);

            //var testSetTransform = trainedModel.Transform(dataSplit.TestSet);

            //var modelMetrics = Context.BinaryClassification.Evaluate(data: testSetTransform, labelColumnName: nameof(RestaurantFeedback.Label), nameof(RestaurantPrediction.Score));
        }
    }
}
