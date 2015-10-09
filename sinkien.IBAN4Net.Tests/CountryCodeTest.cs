/*
 * IBAN4Net
 * Copyright 2015 Vaclav Beca [sinkien]
 *
 * Based on Artur Mkrtchyan's project IBAN4j (https://github.com/arturmkrtchyan/iban4j).
 *
 *
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace sinkien.IBAN4Net.Tests
{
    [TestClass]
    public class CountryCodeTest
    {
        [TestMethod]
        public void GetCountryCodeWithEmptyStringShouldReturnNullObject ()
        {
            CountryCodeEntry entry = CountryCode.GetCountryCode( "" );
            Assert.IsNull( entry );
        }

        [TestMethod]
        public void GetCountryCodeWith4DigitCodeShouldReturnNullObject()
        {
            CountryCodeEntry entry = CountryCode.GetCountryCode( "XXXX" );
            Assert.IsNull( entry );
        }

        [TestMethod]
        public void GetCountryCodeWithWrongAplha2CodeShouldReturnNull()
        {
            CountryCodeEntry entry = CountryCode.GetCountryCode( "XX" );
            Assert.IsNull( entry );
        }

        [TestMethod]
        public void GetCountryCodeWithWrongAlpha3CodeShouldReturnNull()
        {
            CountryCodeEntry entry = CountryCode.GetCountryCode( "XXX" );
            Assert.IsNull( entry );
        }

        [TestMethod]
        public void GetCountryCodeWithCZCodeShouldReturnCzechRepublic()
        {
            CountryCodeEntry entry = CountryCode.GetCountryCode( "CZ" );
            Assert.IsTrue( entry.CountryName.Contains( "Czech Republic" ) );            
        }

        [TestMethod]
        public void GetCountryCodeWithCZECodeShouldReturnCzechRepublic ()
        {
            CountryCodeEntry entry = CountryCode.GetCountryCode( "CZE" );
            Assert.IsTrue( entry.CountryName.Contains( "Czech Republic" ) );
        }

        [TestMethod]
        public void GetCountryCodeWithCZAplha2ShouldReturnCZEAsAplha3()
        {
            CountryCodeEntry entry = CountryCode.GetCountryCode( "CZ" );
            Assert.AreEqual( "CZE", entry.Alpha3 );
        }

    }
}
