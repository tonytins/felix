// This project is licensed under the MPL 2.0 license.
// See the LICENSE file in the project root for more information.
namespace Workshop.Common
{
    public interface ITrainer
    {
        public string ModelPath { get; }
        public string TrainingDataFile { get; }
        public void Train();
    }
}
