// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at
// http://mozilla.org/MPL/2.0/.
using Microsoft.ML;

namespace Workshop.Common
{
    public class BaseML
    {
        protected MLContext Context { get; private set; }

        protected BaseML(int seed = 2020)
        {
            Context = new MLContext(seed);
        }
    }
}