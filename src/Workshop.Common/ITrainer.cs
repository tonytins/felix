// This project is licensed under the MIT license.
// See the LICENSE file in the project root for more information.
namespace Workshop.Common
{
    public interface ITrainer
    {
        public string ModelPath { get; }
        public string TrainingFile { get; }
        public void Train();
    }
}
