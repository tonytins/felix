// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at
// http://mozilla.org/MPL/2.0/.
using System;
using Workshop.Common;
using Workshop.Models.Inventory;
using Newtonsoft.Json;
using Workshop.Models.SpamDetect;

namespace Workshop.ML.Packet.MultiClass
{
    public class PktMultiClassPredict : BaseML, IPredict
    {
        string JsonFile { get; set; }

        public PktMultiClassPredict(string file)
        {
            JsonFile = file;
        }

        public void Predict()
        {
            var modelFile = WorkshopHelper.GetModelPath("packt", "RestaurantFeedback.zip");
            var mlModel = WorkshopHelper.GetModelData(Context, modelFile);

            var predictionEng = Context.Model.CreatePredictionEngine<Email, EmailPrediction>(mlModel);

            var emails = JsonConvert.DeserializeObject<Email[]>(JsonFile);
            

            foreach (var email in emails)
            {
                var predictions = predictionEng.Predict(email);
                Console.WriteLine(
                $"Based on input json:{Environment.NewLine}" +
                $"{JsonFile}{Environment.NewLine}" +
                $"The email is predicted to be a {predictions.Category}");
            }
            
        }
    }
}
