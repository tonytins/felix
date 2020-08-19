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
using System;
using System.Collections.Generic;
using System.Linq;

namespace Workshop.Common
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Provides an easy method to return all of properties in a class
        /// except the label.
        /// </summary>
        /// <typeparam name="T">class</typeparam>
        /// <param name="objType">object type</param>
        /// <param name="labelName">label name</param>
        /// <returns>list of names</returns>
        public static IEnumerator<string> ToPropertyList<T>(this Type objType, string labelName) => objType.GetProperties()
            .Where(a => a.Name != labelName)
            .Select(a => a.Name)
            .GetEnumerator();
    }
}
