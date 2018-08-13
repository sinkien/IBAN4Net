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
    public class IbanTest
    {
        
        [TestMethod]
        public void IbanConstructionWithSupportedCountryShouldReturnIban()
        {
            Iban iban = Iban.CreateInstance("CZ6508000000192000145399");
            Assert.AreEqual("CZ6508000000192000145399", iban.ToString());
        }

        [TestMethod]
        public void IbansWithSameDataShouldBeEqual()
        {
            Iban iban1 = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0800").AccountNumberPrefix("000019").AccountNumber("2000145399").Build();
            Iban iban2 = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0800").AccountNumberPrefix("000019").AccountNumber("2000145399").Build();

            Assert.AreEqual(iban1, iban2);
        }

        [TestMethod]
        public void IbansWithDifferentDataShouldNotBeEqual()
        {
            Iban iban1 = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0800").AccountNumberPrefix("000019").AccountNumber("2045654526").Build();
            Iban iban2 = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0800").AccountNumberPrefix("000019").AccountNumber("2000145399").Build();

            Assert.AreNotEqual(iban1, iban2);
        }

        [TestMethod]
        public void IbansWithSameDataShouldHaveSameHashCode()
        {
            Iban iban1 = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0800").AccountNumberPrefix("000019").AccountNumber("2000145399").Build();
            Iban iban2 = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0800").AccountNumberPrefix("000019").AccountNumber("2000145399").Build();

            Assert.AreEqual(iban1.GetHashCode(), iban2.GetHashCode());
        }

        [TestMethod]
        public void IbansWithDifferentDataShoouldHaveDifferenetHashCode()
        {
            Iban iban1 = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0800").AccountNumberPrefix("000019").AccountNumber("2045654526").Build();
            Iban iban2 = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0800").AccountNumberPrefix("000019").AccountNumber("2000145399").Build();

            Assert.AreNotEqual(iban1.GetHashCode(), iban2.GetHashCode());
        }

        [TestMethod]
        public void IbanShouldReturnValidCountryCode()
        {
            Iban iban1 = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0800").AccountNumberPrefix("000019").AccountNumber("2045654526").Build();

            Assert.AreEqual(CountryCode.GetCountryCode("CZ"), iban1.GetCountryCode());
        }

        [TestMethod]
        public void IbanShouldReturnValidBankCode()
        {
            Iban iban1 = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0800").AccountNumberPrefix("000019").AccountNumber("2045654526").Build();
            Assert.AreEqual("0800", iban1.GetBankCode());
        }

        [TestMethod]
        public void IbanShouldReturnValidAccountNumber()
        {
            Iban iban1 = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0800").AccountNumberPrefix("000019").AccountNumber("2045654526").Build();
            Assert.AreEqual("2045654526", iban1.GetAccountNumber());
        }

        [TestMethod]
        public void IbanShouldReturnValidBranchCode()
        {
            Iban iban = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("AD")).BankCode("0001").BranchCode("2030").AccountNumber("200359100100").Build();
            Assert.AreEqual("2030", iban.GetBranchCode());
        }

        [TestMethod]
        public void IbanShouldReturnValidNationalCheckDigit()
        {
            Iban iban = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("AL")).BankCode("212").BranchCode("1100").NationalCheckDigit("9").AccountNumber("0000000235698741").Build();
            Assert.AreEqual("9", iban.GetNationalCheckDigit());
        }

        [TestMethod]
        public void IbanShouldReturnValidAccountType()
        {
            Iban iban = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("BR")).BankCode("00360305").BranchCode("00001").AccountNumber("0009795493").AccountType("P").OwnerAccountType("1").Build();
            Assert.AreEqual("P", iban.GetAccountType());
        }

        [TestMethod]
        public void IbanShouldReturnValidOwnerAccountType()
        {
            Iban iban = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("BR")).BankCode("00360305").BranchCode("00001").AccountNumber("0009795493").AccountType("P").OwnerAccountType("1").Build();
            Assert.AreEqual("1", iban.GetOwnerAccountType());
        }

        [TestMethod]
        public void IbanShouldReturnValidIdentificationNumber()
        {
            Iban iban = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("IS")).BankCode("0159").BranchCode("26").AccountNumber("007654").IdentificationNumber("5510730339").Build();
            Assert.AreEqual("5510730339", iban.GetIdentificationNumber());
        }

        [TestMethod]
        public void IbanShouldReturnValidBBan()
        {
            Iban iban = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0800").AccountNumberPrefix("000019").AccountNumber("2045654526").Build();
            Assert.AreEqual("08000000192045654526", iban.GetBBan());
        }

        [TestMethod]
        public void IbanShouldReturnValidCheckDigit()
        {
            Iban iban = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0800").AccountNumberPrefix("000019").AccountNumber("2000145399").Build();
            Assert.AreEqual("65", iban.GetCheckDigit());
        }

        [TestMethod]
        public void IbanToFormattedStringShouldHaveSpacesAfterEach4Character()
        {
            Iban iban = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0800").AccountNumberPrefix("000019").AccountNumber("2000145399").Build();
            Assert.AreEqual("CZ65 0800 0000 1920 0014 5399", iban.ToFormattedString());
        }

        [TestMethod]
        public void IbanAccountNumberShouldPadItselfAccordingToBBANRule()
        {
            Iban iban = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0100").AccountNumberPrefix("000019").AccountNumber("145399").Build();
            Assert.AreEqual("0000145399", iban.GetAccountNumber());
        }

        [TestMethod]
        public void IbanBankCodeShoulPadItselfAccordingToBBANRule()
        {
            Iban iban = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("100").AccountNumberPrefix("000019").AccountNumber("2000145399").Build();
            Assert.AreEqual("0100", iban.GetBankCode());
        }

        [TestMethod]
        public void IbanAccountNumberPrefixShouldPadItselfAccordingToBBANRule()
        {
            Iban iban = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("100").AccountNumberPrefix("19").AccountNumber("2000145399").Build();
            Assert.AreEqual("000019", iban.GetAccountNumberPrefix());
        }

        [TestMethod]
        public void IbanCreationFromPartsShouldSucceed()
        {
            // all parts will autopad itselves
            Iban iban = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ"))
                .BankCode("2010")
                .AccountNumber("2300121591")
                .Build();

            Assert.AreEqual("CZ2820100000002300121591", iban.ToString());
        }

        [TestMethod]
        public void IbanTooLongAccountNumberShouldThrowException()
        {
            try
            {
                // Account number length rule for CZ is 10 digits
                Iban iban = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0100").AccountNumber("14539945687").Build();
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.ACCOUNT_NUMBER_TOO_LONG, iex.FormatViolation);
            }
        }

        [TestMethod]
        public void IbanTooLongBankCodeShouldThrowException()
        {
            try
            {
                // Bank Code length rule for CZ is 4 digits
                Iban iban = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("00100").AccountNumber("1920001453994568").Build();
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.BANK_CODE_TOO_LONG, iex.FormatViolation);
            }
        }

        [TestMethod]
        public void IbanTooLongAccountNumberPrefixShouldThrowException()
        {
            try
            {
                // Account number prefix length rule for CZ is 6 digits
                Iban iban = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0010").AccountNumberPrefix("1234567").AccountNumber("1453994568").Build();
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.ACCOUNT_NUMBER_PREFIX_TOO_LONG, iex.FormatViolation);
            }
        }

        [TestMethod]
        public void IbanShortAccountNumberWithoutAutoPaddingshouldThrowException()
        {
            try
            {
                Iban iban = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0010").AccountNumberPrefix("123456").AccountNumber("4568").Build(true, false);
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.ACCOUNT_NUMBER_TOO_SHORT, iex.FormatViolation);
            }
        }

        [TestMethod]
        public void IbanShortAccountNumberPrefixWithoutAutoPaddingShouldThrowException()
        {
            try
            {
                Iban iban = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0100").AccountNumberPrefix("12").AccountNumber("1234567890").Build(true, false);
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.ACCOUNT_NUMBER_PREFIX_TOO_SHORT, iex.FormatViolation);
            }
        }

        [TestMethod]
        public void IbanShortBankCodeWithoutAutoPaddingShouldThrowException()
        {
            try
            {
                Iban iban = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("10").AccountNumberPrefix("123456").AccountNumber("1234567890").Build(true, false);
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.BANK_CODE_TOO_SHORT, iex.FormatViolation);
            }
        }
    }
}
