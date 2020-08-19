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
using Microsoft.ML;

namespace Workshop.Common
{
    public class BaseML
    {
        protected MLContext Context { get; private set; }
        protected string TrainingFile { get; set; }
        protected string TestingFile { get; set; }
        protected string ModelFile { get; set; }

        public BaseML()
        {
            Context = new MLContext(MLConsts.SEED);
        }

        public BaseML(string trainingFile, string modelFile)
        {
            TrainingFile = trainingFile;
            ModelFile = modelFile;
            Context = new MLContext(MLConsts.SEED);
        }

        public BaseML(string testingFile, string trainingFile, string modelFile)
        {
            TrainingFile = trainingFile;
            TestingFile = testingFile;
            ModelFile = modelFile;
            Context = new MLContext(MLConsts.SEED);
        }

        public BaseML(string modelFile)
        {
            ModelFile = modelFile;
            Context = new MLContext(MLConsts.SEED);
        }

    }
}
