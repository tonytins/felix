// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at
// http://mozilla.org/MPL/2.0/.
using System;
using Workshop.Common;
using Workshop.Models.Restaurant;

namespace Workshop.ML.Packet.LogisticRegression
{
    public class PktLogRegPredict : BaseML, IPredict
    {
        public PktLogRegPredict(string input)
        {
            Input = input;
        }

        public string Input { get; set; }

        public void Predict()
        {
            var modelFile = WorkshopHelper.GetModelPath("RestaurantFeedback.zip");
            var mlModel = WorkshopHelper.GetModelData(Context, modelFile);

            var predictionEng = Context.Model.CreatePredictionEngine<RestaurantFeedback, RestaurantPrediction>(mlModel);

            var predict = predictionEng.Predict(new RestaurantFeedback { Text = Input });

            Console.WriteLine($"Based on \"{Input}\", the feedback is predicted to be:{Environment.NewLine}{(predict.Prediction ? "Negative" : "Positive")} at a {predict.Probability:P0} confidence");
        }

    }
}
