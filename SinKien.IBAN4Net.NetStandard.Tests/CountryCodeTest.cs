

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SinKien.IBAN4Net.NetStandard.Tests.Net45
{
    [TestClass]/*
 * IBAN4Net
 * Copyright 2020 Vaclav Beca [sinkien]
 *
 * Based on Artur Mkrtchyan's project IBAN4j (https://github.com/arturmkrtchyan/iban4j).
 *
 *
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
    public class CountryCodeTest
    {
        [TestMethod]
        public void GetCountryAndChangeShouldNotChangeOtherCountries()
        {
            string oldName;
            CountryCodeEntry entry = CountryCode.GetCountryCode("CY");
            Assert.IsNotNull(entry);
            oldName = entry.CountryName;
            entry.CountryName = "Change to this";
            CountryCodeEntry newEntry = CountryCode.GetCountryCode("CY");
            Assert.IsNotNull(newEntry);
            Assert.AreEqual(oldName, newEntry.CountryName);

        }

        [TestMethod]
        public void GetCountryCodeWithEmptyStringShouldReturnNullObject()
        {
            CountryCodeEntry entry = CountryCode.GetCountryCode(string.Empty);
            Assert.IsNull(entry);
        }

        [TestMethod]
        public void GetCountryCodeWith4LetterCodeShouldReturnNullObject()
        {
            CountryCodeEntry entry = CountryCode.GetCountryCode("XXXX");
            Assert.IsNull(entry);
        }

        [TestMethod]
        public void GetCountryCodeWithWrongAplha2CodeShouldReturnNull()
        {
            CountryCodeEntry entry = CountryCode.GetCountryCode("XX");
            Assert.IsNull(entry);
        }

        [TestMethod]
        public void GetCountryCodeWithWrongAlpha3CodeShouldReturnNull()
        {
            CountryCodeEntry entry = CountryCode.GetCountryCode("XXX");
            Assert.IsNull(entry);
        }

        [TestMethod]
        public void GetCountryCodeWithCZCodeShouldReturnCzechRepublic()
        {
            CountryCodeEntry entry = CountryCode.GetCountryCode("CZ");
            Assert.IsTrue(entry.CountryName.Contains("Czech Republic"));
        }

        [TestMethod]
        public void GetCountryCodeWithCZECodeShouldReturnCzechRepublic()
        {
            CountryCodeEntry entry = CountryCode.GetCountryCode("CZE");
            Assert.IsTrue(entry.CountryName.Contains("Czech Republic"));
        }

        [TestMethod]
        public void GetCountryCodeWithCZAplha2ShouldReturnCZEAsAplha3()
        {
            CountryCodeEntry entry = CountryCode.GetCountryCode("CZ");
            Assert.AreEqual("CZE", entry.Alpha3);
        }

        [TestMethod]
        public void GetCountryEqualsOnTwoDifferentObjectsShouldReturnFalse()
        {
            CountryCodeEntry entry1 = CountryCode.GetCountryCode("CZ");
            CountryCodeEntry entry2 = CountryCode.GetCountryCode("DE");

            Assert.IsFalse(entry1.Equals(entry2));
        }

        [TestMethod]
        public void GetCountryEqualsOnDifferentObjectShouldReturnFalse()
        {
            CountryCodeEntry entry = CountryCode.GetCountryCode("CZ");
            Assert.IsFalse(entry.Equals("Test"));
        }

        [TestMethod]
        public void GetCountryTwoDifferentObjectsShouldHaveDifferentHashcode()
        {
            CountryCodeEntry entry1 = CountryCode.GetCountryCode("CZ");
            CountryCodeEntry entry2 = CountryCode.GetCountryCode("DE");

            Assert.AreNotEqual(entry1.GetHashCode(), entry2.GetHashCode());
        }

        [TestMethod]
        public void GetCountryCodesShouldReturnAllOfThem()
        {
            IEnumerable<CountryCodeEntry> entries = CountryCode.GetCountryCodes();
            Assert.AreEqual(251, entries.Count());
        }
    }
}
