// This project is licensed under the MIT license.
// See the LICENSE file in the project root for more information.
using System;
using System.IO;
using Microsoft.ML;

namespace Workshop.Common
{
    public class BaseML
    {
        protected string ModelPath => Path.Combine(AppContext.BaseDirectory, "training");
        protected MLContext MlContext { get; set; }

        protected BaseML()
        {
            MlContext = new MLContext(2020);
        }

    }
}