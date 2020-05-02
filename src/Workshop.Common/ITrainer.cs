// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at
// http://mozilla.org/MPL/2.0/.
namespace Workshop.Common
{
    public interface ITrainer
    {
        public string ModelPath { get; }
        public string TrainingDataFile { get; }
        public void Train();
    }
}
