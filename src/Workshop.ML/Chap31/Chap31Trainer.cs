// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at
// http://mozilla.org/MPL/2.0/.
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
            var trainingDataView = WorkshopHelper.LoadTrainingData<CarInventory>(Context, trainingData);

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
