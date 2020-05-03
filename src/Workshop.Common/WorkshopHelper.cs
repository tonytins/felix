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
        static readonly string _trainingPath = Path.Combine(AppContext.BaseDirectory, "data", "training");
        // static readonly string _predictionPath = Path.Combine(AppContext.BaseDirectory, "data", "prediction");

        public static string GetTrainingDataFile(params string[] file) => Path.Combine(_trainingPath, Path.Combine(file));

        // public static string GetPredictionDataFile(params string[] file) => Path.Combine(_predictionPath, Path.Combine(file));

        public static IDataView LoadTrainingData<T>(MLContext context, string file, char sepChar = ',', bool header = false)
        {
            try
            {
                var path = Path.Combine(_trainingPath, file);
                var trainingDataView = context.Data.LoadFromTextFile<T>(path, separatorChar: sepChar, hasHeader: header);
                return context.Data.ShuffleRows(trainingDataView);
            }
            catch (IOException err)
            {
                throw new IOException($"Failed to load training data.{Environment.NewLine}{err.StackTrace}");
            }
        }

        public static string GetModelPath(params string[] file)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "data", "models");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return Path.Combine(path, Path.Combine(file));
        }

        public static ITransformer GetModelData(MLContext context, params string[] file)
        {
            try
            {
                var path = GetModelPath(file);
                using var model = File.OpenRead(path);
                var mlModel = context.Model.Load(model, out _);

                return mlModel;

            } catch (IOException err)
            {
                throw new IOException($"Failed to load model{Environment.NewLine}{err.StackTrace}");
            }
        }
    }
}
