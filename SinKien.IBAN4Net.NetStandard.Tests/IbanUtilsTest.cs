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
    public class IbanUtilsTest
    {
        [TestMethod]
        public void IbanCountrySupportCheckWithNullShouldReturnFalse()
        {
            CountryCodeEntry entry = null;
            Assert.IsFalse(IbanUtils.IsSupportedCountry(entry));
        }

        [TestMethod]
        public void IbanCountrySupportCheckWithEmptyCodeShouldReturnFalse()
        {
            Assert.IsFalse(IbanUtils.IsSupportedCountry(""));
        }

        [TestMethod]
        public void IbanCountrySupportCheckWithUnsupportedCountryShouldReutnFalse_string()
        {
            Assert.IsFalse(IbanUtils.IsSupportedCountry("AM"));
        }

        [TestMethod]
        public void IbanCountrySupportCheckWithUnsupportedCountryShouldReutnFalse_object()
        {
            Assert.IsFalse(IbanUtils.IsSupportedCountry(CountryCode.GetCountryCode("AM")));
        }

        [TestMethod]
        public void IbanCountrySupportCheckWithSupportedCountryShouldRetrunTrue_object()
        {
            Assert.IsTrue(IbanUtils.IsSupportedCountry(CountryCode.GetCountryCode("CZ")));
        }

        [TestMethod]
        public void IbanCountrySupportCheckWithSupportedCountryShouldRetrunTrue_string()
        {
            Assert.IsTrue(IbanUtils.IsSupportedCountry("CZ"));
        }

        [TestMethod]
        [WorkItem(4)]
        public void IbanCountrySupportCheckWithIrelandShouldRetrunTrue_string()
        {
            Assert.IsTrue(IbanUtils.IsSupportedCountry("IE"));
        }

        [TestMethod]
        public void IbanGetLegnthShouldReturnValidLength()
        {
            string testIban = "CZ6508000000192000145399";
            Iban testObject = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0800").AccountNumberPrefix("000019").AccountNumber("2000145399").Build();
            Assert.AreEqual(testIban.Length, IbanUtils.GetIbanLength(testObject.GetCountryCode()));
        }

        [TestMethod]
        public void CheckDigitCalculationWithIbanObject()
        {
            string testIban = "CZ6508000000192000145399";
            Iban testObject = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("CZ")).BankCode("0800").AccountNumberPrefix("000019").AccountNumber("2000145399").Build();
            string checkDigit = IbanUtils.CalculateCheckDigit(testIban);
            Assert.AreEqual(testIban.Substring(2, 2), checkDigit);
        }

        [TestMethod]
        public void CheckDigitCalculationWithIbanString()
        {
            string checkDigit = IbanUtils.CalculateCheckDigit("CZ6508000000192000145399");
            Assert.AreEqual("65", checkDigit);
        }

        [TestMethod, ExpectedException(typeof(IbanFormatException))]
        public void CheckDigitCalculationWithInvalidBBANShouldThrowException()
        {
            IbanUtils.CalculateCheckDigit("AT000159260+076545510730339");
        }

        [TestMethod]
        public void IbanValidationWithEmptyIBANShouldThrowException()
        {
            try
            {
                IbanUtils.Validate("");
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.IBAN_NOT_EMPTY_OR_NULL, iex.FormatViolation);
            }
        }

        [TestMethod]
        public void IbanIsValidWithEmptyIBANShouldReturnFalse()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            Assert.IsFalse(IbanUtils.IsValid("", out validationResult));
        }

        [TestMethod]
        public void IbanIsValidWithEmptyIBANShouldReturnRightResult()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            IbanUtils.IsValid("", out validationResult);

            Assert.AreEqual(IbanFormatViolation.IBAN_NOT_EMPTY_OR_NULL, validationResult);
        }

        [TestMethod]
        public void IbanValidationWithOneCharStringSHouldThrowEception()
        {
            try
            {
                IbanUtils.Validate("A");
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.COUNTRY_CODE_TWO_LETTERS, iex.FormatViolation);
            }
        }

        [TestMethod]
        public void IbanIsValidWithOneCharStringShouldReturnFalse()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            Assert.IsFalse(IbanUtils.IsValid("A", out validationResult));
        }

        [TestMethod]
        public void IbanIsValidWithOneCharStringShouldReturnRightResult()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            IbanUtils.IsValid("A", out validationResult);

            Assert.AreEqual(IbanFormatViolation.COUNTRY_CODE_TWO_LETTERS, validationResult);
        }

        [TestMethod]
        public void IbanValidationWithCountryCodeOnlyShouldThrowException()
        {
            try
            {
                IbanUtils.Validate("CZ");
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.CHECK_DIGIT_TWO_DIGITS, iex.FormatViolation);
            }
        }

        [TestMethod]
        public void IbanIsValidWithCountryCodeOnlyShouldReturnFalse()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            Assert.IsFalse(IbanUtils.IsValid("CZ", out validationResult));
        }

        [TestMethod]
        public void IbanIsValidWithCountryCodeOnlyShouldReturnRightResult()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            IbanUtils.IsValid("CZ", out validationResult);

            Assert.AreEqual(IbanFormatViolation.CHECK_DIGIT_TWO_DIGITS, validationResult);
        }

        [TestMethod]
        public void IbanValidationWithNonDigitCheckDigitShouldThrowException()
        {
            try
            {
                IbanUtils.Validate("CZ4T");
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.CHECK_DIGIT_ONLY_DIGITS, iex.FormatViolation);
            }
        }

        [TestMethod]
        public void IbanIsValidWithNonDigitCheckDigitShouldReturnFalse()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            Assert.IsFalse(IbanUtils.IsValid("CZ4T", out validationResult));
        }

        [TestMethod]
        public void IbanIsValidWithNonDigitCheckDigitShouldReturnRightResult()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            IbanUtils.IsValid("CZ4T", out validationResult);

            Assert.AreEqual(IbanFormatViolation.CHECK_DIGIT_ONLY_DIGITS, validationResult);
        }

        [TestMethod]
        public void IbanValidationWithCountryCodeAndCheckDigitOnlyShouldThrowException()
        {
            try
            {
                IbanUtils.Validate("CZ48");
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.BBAN_LENGTH, iex.FormatViolation);
            }
        }

        [TestMethod]
        public void IbanIsValidWithCountryCodeAndCheckDigitOnlyShouldReturnFalse()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            Assert.IsFalse(IbanUtils.IsValid("CZ48", out validationResult));
        }

        [TestMethod]
        public void IbanIsValidWithCountryCodeAndCheckDigitOnlyShouldReturnRightResult()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            IbanUtils.IsValid("CZ48", out validationResult);

            Assert.AreEqual(IbanFormatViolation.BBAN_LENGTH, validationResult);

        }

        [TestMethod]
        public void IbanValidationWithLowercaseCountryCodeShouldThrowException()
        {
            try
            {
                IbanUtils.Validate("cz6508000000192000145399");
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.COUNTRY_CODE_UPPER_CASE_LETTERS, iex.FormatViolation);
            }
        }

        [TestMethod]
        public void IbanIsValidWithLowercaseCountryCodeShouldReturnFalse()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            Assert.IsFalse(IbanUtils.IsValid("cz6508000000192000145399", out validationResult));
        }

        [TestMethod]
        public void IbanIsValidWithLowercaseCountryCodeShouldReturnRightResult()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            IbanUtils.IsValid("cz6508000000192000145399", out validationResult);

            Assert.AreEqual(IbanFormatViolation.COUNTRY_CODE_UPPER_CASE_LETTERS, validationResult);
        }

        [TestMethod]
        public void IbanValidationWithEmptyCountryCodeShouldThrowException()
        {
            try
            {
                IbanUtils.Validate(" _6508000000192000145399");
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.COUNTRY_CODE_UPPER_CASE_LETTERS, iex.FormatViolation);
            }
        }

        [TestMethod]
        public void IbanIsValidWithEmptyCountryCodeShouldReturnFalse()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            Assert.IsFalse(IbanUtils.IsValid(" _6508000000192000145399", out validationResult));
        }

        [TestMethod]
        public void IbanIsValidWithEmptyCountryCodeShouldReturnRightResult()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            IbanUtils.IsValid(" _6508000000192000145399", out validationResult);

            Assert.AreEqual(IbanFormatViolation.COUNTRY_CODE_UPPER_CASE_LETTERS, validationResult);
        }

        [TestMethod, ExpectedException(typeof(UnsupportedCountryException))]
        public void IbanValidationWithUnsupportedCountryShouldThrowException()
        {
            IbanUtils.Validate("AM611904300234573201");
        }

        [TestMethod]
        public void IbanIsValidWithUnsupportedCountryShouldReturnFalse()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            Assert.IsFalse(IbanUtils.IsValid("AM611904300234573201", out validationResult));
        }

        [TestMethod]
        public void IbanIsValidWithUnsupportedCountryShouldReturnRightResult()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            IbanUtils.IsValid("AM611904300234573201", out validationResult);

            Assert.AreEqual(IbanFormatViolation.COUNTRY_CODE_UNSUPPORTED, validationResult);
        }

        [TestMethod]
        public void IbanValidationWithNonExistingCountryShouldThrowExeption()
        {
            try
            {
                IbanUtils.Validate("JJ611904300234573201");
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.COUNTRY_CODE_EXISTS, iex.FormatViolation);
            }
        }

        [TestMethod]
        public void IbanIsValidWithNonExistingCountryShouldReturnFalse()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            Assert.IsFalse(IbanUtils.IsValid("JJ611904300234573201", out validationResult));
        }

        [TestMethod]
        public void IbanIsValidWithNonExistingCountryShouldReturnRightResult()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            IbanUtils.IsValid("JJ611904300234573201", out validationResult);

            Assert.AreEqual(IbanFormatViolation.COUNTRY_CODE_EXISTS, validationResult);
        }

        [TestMethod, ExpectedException(typeof(InvalidCheckDigitException))]
        public void IbanValidationWithInvalidCheckDigitShouldThrowException()
        {
            IbanUtils.Validate("AT621904300234573201");
        }

        [TestMethod]
        public void IbanIsValidWithInvalidCheckDigitShouldReturnFalse()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            Assert.IsFalse(IbanUtils.IsValid("AT621904300234573201", out validationResult));
        }

        [TestMethod]
        public void IbanIsValidWithInvalidCheckDigitShouldReturnRightResult()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            IbanUtils.IsValid("AT621904300234573201", out validationResult);

            Assert.AreEqual(IbanFormatViolation.IBAN_INVALID_CHECK_DIGIT_VALUE, validationResult);
        }

        [TestMethod, ExpectedException(typeof(IbanFormatException))]
        public void IbanValidationWithInvalidLengthShouldThrowException()
        {
            IbanUtils.Validate("AT621904300");
        }

        [TestMethod]
        public void IbanIsValidWithInvalidLengthShouldReturnFalse()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            Assert.IsFalse(IbanUtils.IsValid("AT621904300", out validationResult));
        }

        [TestMethod]
        public void IbanIsValidWithInvalidLengthShouldReturnRightResult()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            IbanUtils.IsValid("AT621904300", out validationResult);

            Assert.AreEqual(IbanFormatViolation.BBAN_LENGTH, validationResult);
        }

        [TestMethod]
        public void IbanValidationWithInvalidBBANLengthShouldThrowException()
        {
            try
            {
                IbanUtils.Validate("AT61190430023457320");
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.BBAN_LENGTH, iex.FormatViolation);
            }
        }

        [TestMethod]
        public void IbanIsValidWithInvalidBBANLengthShouldReturnFalse()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            Assert.IsFalse(IbanUtils.IsValid("AT61190430023457320", out validationResult));
        }

        [TestMethod]
        public void IbanIsValidWithInvalidBBANLengthShouldReturnRightResult()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            IbanUtils.IsValid("AT61190430023457320", out validationResult);

            Assert.AreEqual(IbanFormatViolation.BBAN_LENGTH, validationResult);

        }

        [TestMethod]
        public void IbanValidationWithInvalidBankCodeShouldThrowException()
        {
            try
            {
                IbanUtils.Validate("AT611C04300234573201");
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.BBAN_ONLY_DIGITS, iex.FormatViolation);
            }
        }

        [TestMethod]
        public void IbanIsValidWithInvalidBankCodeShouldReturnFalse()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            Assert.IsFalse(IbanUtils.IsValid("AT611C04300234573201", out validationResult));
        }

        [TestMethod]
        public void IbanIsValidWithInvalidBankCodeShouldReturnRightResult()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            IbanUtils.IsValid("AT611C04300234573201", out validationResult);

            Assert.AreEqual(IbanFormatViolation.BBAN_ONLY_DIGITS, validationResult);
        }

        [TestMethod]
        public void IbanValidationWithInvalidAccountNumberShouldThrowException()
        {
            try
            {
                IbanUtils.Validate("DE8937040044053201300A");
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.BBAN_ONLY_DIGITS, iex.FormatViolation);
            }
        }

        [TestMethod]
        public void IbanIsValidWithInvalidAccountNumberShouldReturnFalse()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            Assert.IsFalse(IbanUtils.IsValid("DE8937040044053201300A", out validationResult));
        }

        [TestMethod]
        public void IbanIsValidWithInvalidAccountNumberShouldReturnRightResult()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            IbanUtils.IsValid("DE8937040044053201300A", out validationResult);

            Assert.AreEqual(IbanFormatViolation.BBAN_ONLY_DIGITS, validationResult);
        }

        [TestMethod]
        public void IbanValidationWithInvalidNationalCheckDigitShouldThrowException()
        {
            try
            {
                IbanUtils.Validate("IT6010542811101000000123456");
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.BBAN_ONLY_UPPER_CASE_LETTERS, iex.FormatViolation);
            }
        }

        [TestMethod]
        public void IbanIsValidWithInvalidNationalCheckDigitShouldReturnFalse()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            Assert.IsFalse(IbanUtils.IsValid("IT6010542811101000000123456", out validationResult));
        }

        [TestMethod]
        public void IbanIsValidWithInvalidNationalCheckDigitShouldReturnRightResult()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            IbanUtils.IsValid("IT6010542811101000000123456", out validationResult);

            Assert.AreEqual(IbanFormatViolation.BBAN_ONLY_UPPER_CASE_LETTERS, validationResult);
        }

        [TestMethod]
        public void IbanValidationWithValidIbanShouldNotThrowException()
        {
            IbanUtils.Validate("CZ6508000000192000145399");
        }

        [TestMethod]
        public void IbanIsValidWithValidIbanShouldReturnTrue()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            Assert.IsTrue(IbanUtils.IsValid("CZ6508000000192000145399", out validationResult));
        }

        [TestMethod]
        public void IbanIsValidWithValidIbanShouldReturnRightResult()
        {
            IbanFormatViolation validationResult = IbanFormatViolation.NO_VIOLATION;
            IbanUtils.IsValid("CZ6508000000192000145399", out validationResult);

            Assert.AreEqual(IbanFormatViolation.NO_VIOLATION, validationResult);
        }

        [TestMethod]
        public void IbanUtilGetAccountNumberShouldReturnAccountNumber()
        {
            string testIban = "CZ6508000000192000145399";
            Assert.AreEqual("2000145399", IbanUtils.GetAccountNumber(testIban));
        }

        [TestMethod]
        public void IbanUtilGetAccountNumberPrefixShouldReturnAccountPrefix()
        {
            string testIban = "CZ6508000000192000145399";
            Assert.AreEqual("000019", IbanUtils.GetAccountNumberPrefix(testIban));
        }

        [TestMethod]
        public void IbanUtilGetBankCodeShouldReturnBankCode()
        {
            string testIban = "CZ6508000000192000145399";
            Assert.AreEqual("0800", IbanUtils.GetBankCode(testIban));
        }

        [TestMethod]
        public void IbanUtilGetBBanShouldReturnBBan()
        {
            string testIban = "CZ6508000000192000145399";
            Assert.AreEqual("08000000192000145399", IbanUtils.GetBBan(testIban));
        }

        [TestMethod]
        public void IbanUtilChangeAccountNumber()
        {
            string iban = "CZ6508000000999999999999";
            string changed = IbanUtils.ChangeAccountNumber(iban, "2000145399");

            Assert.AreEqual("CZ5208000000992000145399", changed);
        }

        [TestMethod]
        public void IbanUtilChangeAccountNumberShouldPadItself()
        {
            string iban = "CZ6508000000999999999999";
            string changed = IbanUtils.ChangeAccountNumber(iban, "145399");

            Assert.AreEqual("CZ4508000000990000145399", changed);
        }

        [TestMethod]
        public void IbanUtilChangeAccountNumberWithTooLongNumberShouldThrowException()
        {
            try
            {
                string iban = "CZ6508000000999999999999";
                string changed = IbanUtils.ChangeAccountNumber(iban, "0000192000145399123");
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.BBAN_ENTRY_TOO_LONG, iex.FormatViolation);
            }
        }

        [TestMethod]
        public void IbanUtilChangeBankCode()
        {
            string iban = "CZ6511110000192000145399";
            string changed = IbanUtils.ChangeBankCode(iban, "0800");

            Assert.AreEqual("CZ6508000000192000145399", changed);
        }

        [TestMethod]
        public void IbanUtilChangeBankCodeShouldPadItself()
        {
            string iban = "CZ6511110000192000145399";
            string changed = IbanUtils.ChangeBankCode(iban, "800");

            Assert.AreEqual("CZ6508000000192000145399", changed);
        }

        [TestMethod]
        public void IbanUtilChangeBankCodeWithTooLongCodeShouldThrowException()
        {
            try
            {
                string iban = "CZ6511110000192000145399";
                string changed = IbanUtils.ChangeBankCode(iban, "11111");
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.BBAN_ENTRY_TOO_LONG, iex.FormatViolation);
            }
        }

        [TestMethod]
        public void IbanUtilChangeAccountPrefix()
        {
            string iban = "CZ6508000000999999999999";
            string changed = IbanUtils.ChangeAccountNumberPrefix(iban, "145388");

            Assert.AreEqual("CZ8308001453889999999999", changed);
        }

        [TestMethod]
        public void IbanUtilChangeAccountPrefixShouldPadItself()
        {
            string iban = "CZ6508000000999999999999";
            string changed = IbanUtils.ChangeAccountNumberPrefix(iban, "8");

            Assert.AreEqual("CZ1508000000089999999999", changed);
        }

        [TestMethod]
        public void IbanUtilChangeAccountPrefixWithTooLongNumberShouldThrowException()
        {
            try
            {
                string iban = "CZ6508000000999999999999";
                string changed = IbanUtils.ChangeAccountNumberPrefix(iban, "9999998");
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual(IbanFormatViolation.BBAN_ENTRY_TOO_LONG, iex.FormatViolation);
            }
        }
    }
}
