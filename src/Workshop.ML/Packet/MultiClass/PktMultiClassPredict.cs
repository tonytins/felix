// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at
// http://mozilla.org/MPL/2.0/.
using System;
using Workshop.Common;
using Workshop.Models.SpamDetect;

namespace Workshop.ML.Packet.MultiClass
{
    public class PktMultiClassPredict : BaseML, IPredict
    {
        string CsvFile { get; set; }

        public PktMultiClassPredict(string file)
        {
            CsvFile = file;
        }

        public void Predict()
        {
            var modelFile = WorkshopHelper.GetModelPath("Email.zip");
            var mlModel = WorkshopHelper.GetModelData(Context, modelFile);

            var predictionEng = Context.Model.CreatePredictionEngine<Email, EmailPrediction>(mlModel);

            var csvPath = WorkshopHelper.GetPredictionDataFile(CsvFile);
            var emails = WorkshopHelper.GetCsvData<Email>(file: csvPath);
            

            foreach (var email in emails)
            {
                var predictions = predictionEng.Predict(email);
                Console.WriteLine(
                $"Based on input json:{Environment.NewLine}" +
                $"{CsvFile}{Environment.NewLine}" +
                $"The email is predicted to be a {predictions.Category}");
            }
            
        }
    }
}
