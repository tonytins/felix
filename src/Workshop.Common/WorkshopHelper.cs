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
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.ML;

namespace Workshop.Common
{
    public static class WorkshopHelper
    {
        static readonly string _trainingPath = Path.Combine(AppContext.BaseDirectory, "data", "training");
        static readonly string _predictionPath = Path.Combine(AppContext.BaseDirectory, "data", "prediction");

        /// <summary>
        /// Gets the training file located in data/training/ of the application's directory.
        /// </summary>
        /// <param name="file">training file</param>
        /// <returns>training file with it's path</returns>
        public static string GetTrainingDataFile(params string[] file) => Path.Combine(_trainingPath, Path.Combine(file));

        /// <summary>
        /// Gets the prediction file located in data/prediction/ of the application's directory.
        /// </summary>
        /// <param name="file">prediction file</param>
        /// <returns>prediction file with it's path</returns>
        public static string GetPredictionDataFile(params string[] file) => Path.Combine(_predictionPath, Path.Combine(file));

        public static IEnumerable<T> GetCsvData<T>(bool header = true, params string[] file)
        {
            var path = Path.Combine(file);
            using var data = new StreamReader(path);
            var csConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = header
            };
            using var csv = new CsvReader(data, CultureInfo.InvariantCulture);
            return csv.GetRecords<T>();
        }

        /// <summary>
        /// Loads the training data 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="file"></param>
        /// <param name="sepChar"></param>
        /// <param name="header"></param>
        /// <param name="shuffle"></param>
        /// <returns></returns>
        public static IDataView LoadTrainingData<T>(MLContext context, string file, char sepChar = ',', bool header = false, bool shuffle = false)
        {
            try
            {
                var trainingDataView = context.Data.LoadFromTextFile<T>(file, separatorChar: sepChar, hasHeader: header);
                if (shuffle)
                    return context.Data.ShuffleRows(trainingDataView);
                else
                    return trainingDataView;
            }
            catch (IOException err)
            {
                throw new IOException($"Failed to load training data.{Environment.NewLine}{err.StackTrace}");
            }
        }

        /// <summary>
        /// Gets the training data located in data/models/ of the application's base directory.
        /// If the data/models doesn't exist, it'll be created.
        /// </summary>
        /// <param name="file">ML model</param>
        /// <returns></returns>
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

            }
            catch (IOException err)
            {
                throw new IOException($"Failed to load model{Environment.NewLine}{err.StackTrace}");
            }
        }
    }
}
