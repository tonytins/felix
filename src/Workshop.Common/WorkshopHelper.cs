// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at
// http://mozilla.org/MPL/2.0/.
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

        public static IDataView LoadTrainingData<T>(MLContext context, string file, char sepChar = ',', bool header = false)
        {
            var path = Path.Combine(_trainingPath, file);
            var trainingDataView = context.Data.LoadFromTextFile<T>(path, separatorChar: sepChar, hasHeader: header);
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
            using var model = File.OpenRead(path);
            var mlModel = context.Model.Load(model, out _);

            if (mlModel == null)
                Console.WriteLine("Failed to load model");

            return mlModel;
        }
    }
}
