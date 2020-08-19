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
using System.Data.SqlClient;
using Microsoft.ML;
using Microsoft.ML.Data;
using Workshop.Common;
using Workshop.Models.MS.BikeRental;

namespace Workshop.ML.MS.BikeRental
{
    class MSBikeTrain : BaseML, ITrainer
    {
        public MSBikeTrain(string trainingFile = "DailyDemand.mdf", string modelFile = "MLModel.zip") : base(trainingFile, modelFile)
        {
        }

        public void Train()
        {
            // Define paths and initial variables
            var modelPath = WorkshopHelper.GetModelPath(ModelFile);
            var trainingData = WorkshopHelper.GetTrainingDataFile("ms", TrainingFile);
            var connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={trainingData};Integrated Security=True;Connect Timeout=30;";

            // Load the data
            var loader = Context.Data.CreateDatabaseLoader<BikeInput>();
            var query = "SELECT RentalDate, CAST(Year as REAL) as Year, CAST(TotalRentals as REAL) as TotalRentals FROM Rentals";
            var dbSource = new DatabaseSource(SqlClientFactory.Instance, connectionString, query);
            var dataView = loader.Load(dbSource);
            var firstYear = Context.Data.FilterRowsByColumn(dataView, "Year", upperBound: 1);
            var secondYear = Context.Data.FilterRowsByColumn(dataView, "Year", upperBound: 2);

            // Define analysis pipeline
            var forecastPipeline = Context.Forecasting.ForecastBySsa(
                outputColumnName: "ForecastedRentals",
                inputColumnName: "TotalRentals",
                windowSize: 7,
                seriesLength: 30,
                trainSize: 365,
                horizon: 7,
                confidenceLevel: 0.95f,
                confidenceLowerBoundColumn: "LowerBoundRentals",
                confidenceUpperBoundColumn: "UpperBoundRentals");
            var forecaster = forecastPipeline.Fit(firstYear);
        }
    }
}
