// This project is licensed under the MIT license.
// See the LICENSE file in the project root for more information.
namespace Workshop.Common
{
    public interface IPredict
    {
        public string ModelPath { get; }
        public void Predict();
    }
}
