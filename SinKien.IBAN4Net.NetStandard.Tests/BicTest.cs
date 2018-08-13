/*
 * IBAN4Net
 * Copyright 2018 Vaclav Beca[sinkien]
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

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SinKien.IBAN4Net.Exceptions;

namespace SinKien.IBAN4Net.NetStandard.Tests.Net45
{
    [TestClass]
    public class BicTest
    {
        [TestMethod, ExpectedException(typeof(UnsupportedCountryException))]
        public void BicInstanceCreationWithInvalidCountryCodeShouldThrowException()
        {
            Bic testBic = Bic.CreateInstance("DEUTAAFF500");
        }

        [TestMethod]
        public void BicWithSameDataShouldBeEqual()
        {
            Bic bic1 = Bic.CreateInstance("DEUTDEFF500");
            Bic bic2 = Bic.CreateInstance("DEUTDEFF500");

            Assert.AreEqual(bic1, bic2);
        }

        [TestMethod]
        public void BicWithDifferentDataShouldNotBeEqual()
        {
            Bic bic1 = Bic.CreateInstance("DEUTDEFF500");
            Bic bic2 = Bic.CreateInstance("DEUTDEFF501");

            Assert.AreNotEqual(bic1, bic2);
        }

        [TestMethod]
        public void BicWithStringValueAndBicShouldNotBeEqual()
        {
            Bic bic = Bic.CreateInstance("DEUTDEFF500");

            Assert.AreNotEqual(bic, "DEUTDEFF500");
        }

        [TestMethod]
        public void BicsWithSameDataShouldHaveSameHashCode()
        {
            Bic bic1 = Bic.CreateInstance("DEUTDEFF500");
            Bic bic2 = Bic.CreateInstance("DEUTDEFF500");

            Assert.AreEqual(bic1.GetHashCode(), bic2.GetHashCode());
        }

        [TestMethod]
        public void BicsWithDifferentDataShouldHaveDifferentHashCodes()
        {
            Bic bic1 = Bic.CreateInstance("DEUTDEFF500");
            Bic bic2 = Bic.CreateInstance("DEUTDEFF501");

            Assert.AreNotEqual(bic1.GetHashCode(), bic2.GetHashCode());
        }

        [TestMethod]
        public void BicShouldReturnBankCode()
        {
            Bic bic = Bic.CreateInstance("DEUTDEFF500");
            Assert.AreEqual("DEUT", bic.BankCode);
        }

        [TestMethod]
        public void BicShouldReturnCountryCode()
        {
            Bic bic = Bic.CreateInstance("DEUTDEFF500");

            Assert.AreEqual("DE", bic.GetCountryCode().Alpha2);
        }

        [TestMethod]
        public void BicShouldReturnBranchCode()
        {
            Bic bic = Bic.CreateInstance("DEUTDEFF500");
            Assert.AreEqual("500", bic.GetBranchCode());
        }

        [TestMethod]
        public void BicWithoutBranchCodeShoulRetunEmpty()
        {
            Bic bic = Bic.CreateInstance("DEUTDEFF");
            Assert.AreEqual(string.Empty, bic.GetBranchCode());
        }

        [TestMethod]
        public void BicShouldReturnLocationCode()
        {
            Bic bic = Bic.CreateInstance("DEUTDEFF500");
            Assert.AreEqual("FF", bic.LocationCode);
        }

        [TestMethod]
        public void BicToStringShoulReturnString()
        {
            Bic bic = Bic.CreateInstance("DEUTDEFF500");
            Assert.AreEqual("DEUTDEFF500", bic.ToString());
        }
    }
}
