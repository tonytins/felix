// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at
// http://mozilla.org/MPL/2.0/.
using System;
using Workshop.Common;
using Workshop.Models.Inventory;
using Newtonsoft.Json;

namespace Workshop.ML.Chap31
{
    class Chap31Predictor : BaseML, IPredict
    {
        string JsonFile { get; set; }

        public Chap31Predictor(string file)
        {
            JsonFile = file;
        }

        public void Predict()
        {
            var modelFile = WorkshopHelper.GetModelPath("RestaurantFeedback.zip");
            var mlModel = WorkshopHelper.GetModelData(Context, modelFile);

            var predictionEng = Context.Model.CreatePredictionEngine<CarInventory, CarInventoryPrediction>(mlModel);

            var predict = predictionEng.Predict(JsonConvert.DeserializeObject<CarInventory>(JsonFile));

            Console.WriteLine(
                $"Based on input json:{Environment.NewLine}" +
                $"{JsonFile}{Environment.NewLine}" +
                $"The car price is a {(predict.PredictedLabel ? "good" : "bad")} " +
                $"deal, with a {predict.Probability:P0} confidence");
        }
    }
}
