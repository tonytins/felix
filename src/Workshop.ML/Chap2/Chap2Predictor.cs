// This project is licensed under the MIT license.
// See the LICENSE file in the project root for more information.
using System;
using System.IO;
using Microsoft.ML;
using Workshop.Common;
using Workshop.Models.Restaurant;

namespace Workshop.ML.Chap2
{
    public class Chap2Predictor : BaseML, IPredict
    {
        public Chap2Predictor(string input)
        {
            Input = input;
        }

        public string Input { get; set; }
        public string ModelPath => WorkshopHelper.GetModelPath("RestaurantFeedback.zip");

        public void Predict()
        {
            if (!File.Exists(ModelPath))
            {
                Console.WriteLine($"Could not located model at {ModelPath}");
                return;
            }

            ITransformer mlModel;

            using var stream = new FileStream(ModelPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            mlModel = MlContext.Model.Load(stream, out _);

            if (mlModel == null)
            {
                Console.WriteLine("Failed to load model");
                return;
            }

            var predictionEng = MlContext.Model.CreatePredictionEngine<RestaurantFeedback, RestaurantPrediction>(mlModel);

            var predict = predictionEng.Predict(new RestaurantFeedback { Text = Input });

            Console.WriteLine($"Based on \"{Input}\", the feedback is predicted to be:{Environment.NewLine}{(predict.Prediction ? "Negative" : "Positive")} at a {predict.Probability:P0} confidence");
        }

    }
}
