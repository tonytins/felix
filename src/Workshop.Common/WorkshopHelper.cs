// This project is licensed under the MIT license.
// See the LICENSE file in the project root for more information.
using System;
using System.IO;

namespace Workshop.Common
{
    public static class WorkshopHelper
    {
        public static string GetTrainingData(string file)
        {
            return Path.Combine(AppContext.BaseDirectory, "training", file);
        }

        public static string GetPredictionData(string file)
        {
            return Path.Combine(AppContext.BaseDirectory, "prediction", file);
        }

        public static string GetModelPath(string file)
        {
            var modelDir = Path.Combine(AppContext.BaseDirectory, "models");

            if (!Directory.Exists(modelDir))
                Directory.CreateDirectory(modelDir);

            return Path.Combine(modelDir, file);
        }
    }
}
