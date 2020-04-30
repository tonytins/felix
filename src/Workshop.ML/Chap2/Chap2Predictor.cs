// This project is licensed under the MIT license.
// See the LICENSE file in the project root for more information.
using System;
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
        public string ModelFile => WorkshopHelper.GetModelPath("RestaurantFeedback.zip");

        public void Predict()
        {
            var mlModel = WorkshopHelper.GetModelData(Context, ModelFile);

            var predictionEng = Context.Model.CreatePredictionEngine<RestaurantFeedback, RestaurantPrediction>(mlModel);

            var predict = predictionEng.Predict(new RestaurantFeedback { Text = Input });

            Console.WriteLine($"Based on \"{Input}\", the feedback is predicted to be:{Environment.NewLine}{(predict.Prediction ? "Negative" : "Positive")} at a {predict.Probability:P0} confidence");
        }

    }
}
