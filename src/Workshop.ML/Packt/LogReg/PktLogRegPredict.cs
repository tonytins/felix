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
using Workshop.Common;
using Workshop.Models.Packt.Restaurant;

namespace Workshop.ML.Packt.LogisticRegression
{
    public class PktLogRegPredict : BaseML, IPredict
    {
        public PktLogRegPredict(string file, string modelFile = "RestaurantFeedback.zip") : base(file, modelFile)
        {
        }

        public void Predict()
        {
            var modelFile = WorkshopHelper.GetModelPath(ModelFile);
            var mlModel = WorkshopHelper.GetModelData(Context, modelFile);

            var predictionEng = Context.Model.CreatePredictionEngine<RestaurantFeedback, RestaurantPrediction>(mlModel);

            Console.WriteLine($"Write down random feedback.");

            while (true)
            {
                var input = Terminal.ReadLine;
                var predict = predictionEng.Predict(new RestaurantFeedback { Text = input });

                if (input == "exit".ToLowerInvariant())
                    break;

                Console.WriteLine($"Based on \"{input}\", the feedback is predicted to be:{Environment.NewLine}{(predict.Prediction ? "Negative" : "Positive")} at a {predict.Probability:P0} confidence");

            }

        }

    }
}
