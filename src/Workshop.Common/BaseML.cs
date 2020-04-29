// This project is licensed under the MIT license.
// See the LICENSE file in the project root for more information.
using Microsoft.ML;

namespace Workshop.Common
{
    public class BaseML
    {
        protected MLContext MlContext { get; set; }

        protected BaseML()
        {
            MlContext = new MLContext(2020);
        }
    }
}