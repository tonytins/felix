// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at
// http://mozilla.org/MPL/2.0/.
using System;
using Microsoft.ML;
using Workshop.Common;
using Workshop.Models.Inventory;

namespace Workshop.ML.Packet.MultiClass
{
    class Chap31Trainer : BaseML, ITrainer
    {
        public void Train()
        {
            var trainingData = WorkshopHelper.GetTrainingDataFile("packet", "EmailTraining.csv");
            var trainingDataView = WorkshopHelper.LoadTrainingData<CarInventory>(Context, trainingData);

            // TBA
        }
    }
}
