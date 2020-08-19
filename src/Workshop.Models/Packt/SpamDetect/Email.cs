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
using Microsoft.ML.Data;

namespace Workshop.Models.Packt.SpamDetect
{
    public class Email
    {
        [LoadColumn(0)]
        public string Subject { get; set; }
        [LoadColumn(1)]
        public string Body { get; set; }
        [LoadColumn(2)]
        public string Sender { get; set; }
        [LoadColumn(3)]
        public string Category { get; set; }


    }
}
