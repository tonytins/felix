// This project is licensed under the MIT license.
// See the LICENSE file in the project root for more information.
using System;
using System.IO;
using Microsoft.ML;

namespace Workshop.Common
{
    public static class WorkshopHelper
    {
        static readonly string _trainingPath = Path.Combine(AppContext.BaseDirectory, "training");
        static readonly string _predictionPath = Path.Combine(AppContext.BaseDirectory, "prediction");

        public static string GetTrainingDataFile(string file) => Path.Combine(_trainingPath, file);

        public static string GetPredictionDataFile(string file) => Path.Combine(_predictionPath, file);

        public static IDataView GetTrainingData<T>(MLContext context, string file, char sepChar = ',')
        {
            var path = Path.Combine(_trainingPath, file);
            var trainingDataView = context.Data.LoadFromTextFile<T>(path, separatorChar: sepChar);
            return context.Data.ShuffleRows(trainingDataView);
        }

        public static string GetModelPath(string file)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "models");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return Path.Combine(path, file);
        }

        public static ITransformer GetModelData(MLContext context, string file)
        {
            var path = GetModelPath(file);
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            var mlModel = context.Model.Load(stream, out _);

            if (mlModel == null)
                Console.WriteLine("Failed to load model");

            return mlModel;
        }
    }
}
