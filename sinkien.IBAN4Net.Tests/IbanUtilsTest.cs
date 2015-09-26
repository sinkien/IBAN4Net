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
    public class IbanUtilsTest
    {
        [TestMethod]
        public void IbanCountrySupportCheckWithNullShouldReturnFalse ()
        {
            CountryCodeEntry entry = null;
            Assert.IsFalse( IbanUtils.IsSupportedCountry( entry ) );
        }

        [TestMethod]
        public void IbanCountrySupportCheckWithEmptyCodeShouldReturnFalse()
        {
            Assert.IsFalse( IbanUtils.IsSupportedCountry( "" ) );
        }

        [TestMethod]
        public void IbanCountrySupportCheckWithUnsupportedCountryShouldReutnFalse_string()
        {
            Assert.IsFalse( IbanUtils.IsSupportedCountry( "AM" ) );
        }

        [TestMethod]
        public void IbanCountrySupportCheckWithUnsupportedCountryShouldReutnFalse_object ()
        {
            Assert.IsFalse( IbanUtils.IsSupportedCountry( CountryCode.GetCountryCode( "AM" ) ) );
        }

        [TestMethod]
        public void IbanCountrySupportCheckWithSupportedCountryShouldRetrunTrue_object()
        {
            Assert.IsTrue( IbanUtils.IsSupportedCountry( CountryCode.GetCountryCode( "CZ" ) ) );
        }

        [TestMethod]
        public void IbanCountrySupportCheckWithSupportedCountryShouldRetrunTrue_string ()
        {
            Assert.IsTrue( IbanUtils.IsSupportedCountry( "CZ" ));
        }

        [TestMethod]
        public void IbanGetLegnthShouldReturnValidLength ()
        {
            string testIban = "CZ6508000000192000145399";
            Iban testObject = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "CZ" ) ).BankCode( "0800" ).AccountNumber( "0000192000145399" ).Build();            
            Assert.AreEqual( testIban.Length, IbanUtils.GetIbanLength(testObject.GetCountryCode() ));
        }

        [TestMethod]
        public void CheckDigitCalculationWithIbanObject()
        {
            string testIban = "CZ6508000000192000145399";
            Iban testObject = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "CZ" ) ).BankCode( "0800" ).AccountNumber( "0000192000145399" ).Build();
            string checkDigit = IbanUtils.CalculateCheckDigit( testIban );
            Assert.AreEqual( checkDigit, testIban.Substring( 2, 2 ) );            
        }

        [TestMethod]
        public void CheckDigitCalculationWithIbanString()
        {
            string checkDigit = IbanUtils.CalculateCheckDigit( "CZ6508000000192000145399" );
            Assert.AreEqual( checkDigit, "65" );
        }

        [TestMethod, ExpectedException(typeof(IbanFormatException))]
        public void CheckDigitCalculationWithInvalidBBANShouldThrowException()
        {
            IbanUtils.CalculateCheckDigit( "AT000159260+076545510730339" );
        }

        [TestMethod]
        public void IbanValidationWithEmptyIBANShouldThrowException()
        {
            try
            {
                IbanUtils.Validate( "" );
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual( IbanFormatViolation.IBAN_NOT_EMPTY_OR_NULL, iex.FormatViolation );
            }
        }

        [TestMethod]
        public void IbanValidationWithOneCharStringSHouldThrowEception()
        {
            try
            {
                IbanUtils.Validate( "A" );
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual( IbanFormatViolation.COUNTRY_CODE_TWO_LETTERS, iex.FormatViolation );
            }
        }

        [TestMethod]
        public void IbanValidationWithCountryCodeOnlyShouldThrowException()
        {
            try
            {
                IbanUtils.Validate( "CZ" );
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual( IbanFormatViolation.CHECK_DIGIT_TWO_DIGITS, iex.FormatViolation );
            }
        }

        [TestMethod]
        public void IbanValidationWithNonDigitCheckDigitShouldThrowException()
        {
            try
            {
                IbanUtils.Validate( "CZ4T" );
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual( IbanFormatViolation.CHECK_DIGIT_ONLY_DIGITS, iex.FormatViolation );
            }
        }

        [TestMethod]
        public void IbanValidationWithCountryCodeAndCheckDigitOnlyShouldThrowException()
        {
            try
            {
                IbanUtils.Validate( "CZ48" );
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual( IbanFormatViolation.BBAN_LENGTH, iex.FormatViolation );
            }
        }

        [TestMethod]
        public void IbanValidationWithLowercaseCountryCodeShouldThrowException()
        {
            try
            {
                IbanUtils.Validate( "cz6508000000192000145399" );
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual( IbanFormatViolation.COUNTRY_CODE_UPPER_CASE_LETTERS, iex.FormatViolation );
            }
        }

        [TestMethod]
        public void IbanValidationWithEmptyCountryCodeShouldThrowException()
        {
            try
            {
                IbanUtils.Validate( " _6508000000192000145399" );
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual( IbanFormatViolation.COUNTRY_CODE_UPPER_CASE_LETTERS, iex.FormatViolation );
            }
        }

        [TestMethod, ExpectedException(typeof(UnsupportedCountryException))]
        public void IbanValidationWithUnsupportedCountryShouldThrowException()
        {
            IbanUtils.Validate( "AM611904300234573201" );
        }

        [TestMethod]
        public void IbanValidationWithNonExistingCountryShouldThrowExeption()
        {
            try
            {
                IbanUtils.Validate( "JJ611904300234573201" );
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual( IbanFormatViolation.COUNTRY_CODE_EXISTS, iex.FormatViolation );
            }
        }

        [TestMethod, ExpectedException(typeof(InvalidCheckDigitException))]
        public void IbanValidationWithInvalidCheckDigitShouldThrowException()
        {            
            IbanUtils.Validate( "AT621904300234573201" );            
        }

        [TestMethod, ExpectedException(typeof(IbanFormatException))]
        public void IbanValidationWithInvalidLengthShouldThrowException()
        {           
            IbanUtils.Validate( "AT621904300" );            
        }

        [TestMethod]
        public void IbanValidationWithInvalidBBANLengthShouldThrowException()
        {
            try
            {
                IbanUtils.Validate( "AT61190430023457320" );
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual( IbanFormatViolation.BBAN_LENGTH, iex.FormatViolation );
            }
        }

        [TestMethod]
        public void IbanValidationWithInvalidBankCodeShouldThrowException()
        {
            try
            {
                IbanUtils.Validate( "AT611C04300234573201" );
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual( IbanFormatViolation.BBAN_ONLY_DIGITS, iex.FormatViolation );
            }
        }

        [TestMethod]
        public void IbanValidationWithInvalidAccountNumberShouldThrowException()
        {
            try
            {
                IbanUtils.Validate( "DE8937040044053201300A" );
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual( IbanFormatViolation.BBAN_ONLY_DIGITS, iex.FormatViolation );
            }
        }

        [TestMethod]
        public void IbanValidationWithInvalidNationalCheckDigitShouldThrowException()
        {
            try
            {
                IbanUtils.Validate( "IT6010542811101000000123456" );
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual( IbanFormatViolation.BBAN_ONLY_UPPER_CASE_LETTERS, iex.FormatViolation );
            }
        }

        [TestMethod]
        public void IbanValidationWithValidIbanShouldNotThrowException()
        {
            IbanUtils.Validate( "CZ6508000000192000145399" );            
        }

        [TestMethod]
        public void IbanUtilGetAccountNumberShouldReturnAccountNumber()
        {
            string testIban = "CZ6508000000192000145399";
            Assert.AreEqual( IbanUtils.GetAccountNumber( testIban ), "0000192000145399" );            
        }

        [TestMethod]
        public void IbanUtilGetBankCodeShouldReturnBankCode()
        {
            string testIban = "CZ6508000000192000145399";
            Assert.AreEqual( IbanUtils.GetBankCode( testIban ), "0800" );            
        }

        [TestMethod]
        public void IbanUtilGetBBanShouldReturnBBan()
        {
            string testIban = "CZ6508000000192000145399";
            Assert.AreEqual( IbanUtils.GetBBan( testIban ), "08000000192000145399" );
        }

        [TestMethod]
        public void IbanUtilChangeAccountNumber ()
        {
            string iban = "CZ6508000000999999999999";
            string changed = IbanUtils.ChangeAccountNumber( iban, "0000192000145399" );

            Assert.AreEqual( "CZ6508000000192000145399", changed );
        }

        [TestMethod]
        public void IbanUtilChangeAccountNumberShouldPadItself()
        {
            string iban = "CZ6508000000999999999999";
            string changed = IbanUtils.ChangeAccountNumber( iban, "192000145399" );

            Assert.AreEqual( "CZ6508000000192000145399", changed );
        }


        [TestMethod]
        public void IbanUtilChangeAccountNumberWithTooLongNumberShouldThrowException()
        {
            try
            {
                string iban = "CZ6508000000999999999999";
                string changed = IbanUtils.ChangeAccountNumber( iban, "0000192000145399123" );
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual( IbanFormatViolation.BBAN_ENTRY_TOO_LONG, iex.FormatViolation );
            }
        }

        [TestMethod]
        public void IbanUtilChangeBankCode()
        {
            string iban = "CZ6511110000192000145399";
            string changed = IbanUtils.ChangeBankCode( iban, "0800" );

            Assert.AreEqual( "CZ6508000000192000145399", changed );
        }

        [TestMethod]
        public void IbanUtilChangeBankCodeShouldPadItself()
        {
            string iban = "CZ6511110000192000145399";
            string changed = IbanUtils.ChangeBankCode( iban, "800" );

            Assert.AreEqual( "CZ6508000000192000145399", changed );
        }

        [TestMethod]
        public void IbanUtilChangeBankCodeWithTooLongCodeShouldThrowException()
        {
            try
            {
                string iban = "CZ6511110000192000145399";
                string changed = IbanUtils.ChangeBankCode( iban, "11111" );
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual( IbanFormatViolation.BBAN_ENTRY_TOO_LONG, iex.FormatViolation );
            }
        }
    }
}
