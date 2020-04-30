using System;
using Workshop.Common;
using Workshop.Models.Inventory;
using Newtonsoft.Json;

namespace Workshop.ML.Chap31
{
    class Chap31Predictor : BaseML, IPredict
    {
        public string ModelFile => throw new NotImplementedException();

        string JsonFile { get; set; }

        public Chap31Predictor(string file)
        {
            JsonFile = file;
        }

        public void Predict()
        {
            var mlModel = WorkshopHelper.GetModelData(Context, ModelFile);

            var predictionEng = Context.Model.CreatePredictionEngine<CarInventory, CarInventoryPrediction>(mlModel);

            var predict = predictionEng.Predict(JsonConvert.DeserializeObject<CarInventory>(JsonFile));

            Console.WriteLine(
                $"Based on input json:{Environment.NewLine}" +
                $"{JsonFile}{Environment.NewLine}" +
                $"The car price is a {(predict.PredictedLabel ? "good" : "bad")} deal, with a {predict.Probability:P0} confidence");
        }
    }
}
