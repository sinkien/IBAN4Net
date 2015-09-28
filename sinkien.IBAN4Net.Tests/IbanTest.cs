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
    public class IbanTest
    {
        [TestMethod]
        public void IbanConstructionWithSupportedCountryShouldReturnIban ()
        {
            Iban iban = Iban.CreateInstance( "CZ6508000000192000145399" );
            Assert.AreEqual( iban.ToString(), "CZ6508000000192000145399" );
        }

        [TestMethod]
        public void IbansWithSameDataShouldBeEqual()
        {
            Iban iban1 = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "CZ" ) ).BankCode( "0800" ).AccountNumber( "0000192000145399" ).Build();
            Iban iban2 = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "CZ" ) ).BankCode( "0800" ).AccountNumber( "0000192000145399" ).Build();

            Assert.AreEqual( iban1, iban2 );
        }

        [TestMethod]
        public void IbansWithDifferentDataShouldNotBeEqual()
        {
            Iban iban1 = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "CZ" ) ).BankCode( "0800" ).AccountNumber( "0000192045654526" ).Build();
            Iban iban2 = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "CZ" ) ).BankCode( "0800" ).AccountNumber( "0000192000145399" ).Build();

            Assert.AreNotEqual( iban1, iban2 );
        }

        [TestMethod]
        public void IbansWithSameDataShouldHaveSameHashCode()
        {
            Iban iban1 = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "CZ" ) ).BankCode( "0800" ).AccountNumber( "0000192000145399" ).Build();
            Iban iban2 = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "CZ" ) ).BankCode( "0800" ).AccountNumber( "0000192000145399" ).Build();

            Assert.AreEqual( iban1.GetHashCode(), iban2.GetHashCode() );
        }

        [TestMethod]
        public void IbansWithDifferentDataShoouldHaveDifferenetHashCode()
        {
            Iban iban1 = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "CZ" ) ).BankCode( "0800" ).AccountNumber( "0000192045654526" ).Build();
            Iban iban2 = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "CZ" ) ).BankCode( "0800" ).AccountNumber( "0000192000145399" ).Build();

            Assert.AreNotEqual( iban1.GetHashCode(), iban2.GetHashCode() );
        }

        [TestMethod]
        public void IbanShouldReturnValidCountryCode()
        {
            Iban iban1 = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "CZ" ) ).BankCode( "0800" ).AccountNumber( "0000192045654526" ).Build();

            Assert.AreEqual( iban1.GetCountryCode(), CountryCode.GetCountryCode( "CZ" ) );
        }

        [TestMethod]
        public void IbanShouldReturnValidBankCode()
        {
            Iban iban1 = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "CZ" ) ).BankCode( "0800" ).AccountNumber( "0000192045654526" ).Build();
            Assert.AreEqual( iban1.GetBankCode(), "0800" );
        }

        [TestMethod]
        public void IbanShouldReturnValidAccountNumber()
        {
            Iban iban1 = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "CZ" ) ).BankCode( "0800" ).AccountNumber( "0000192045654526" ).Build();
            Assert.AreEqual( iban1.GetAccountNumber(), "0000192045654526" );                
        }

        [TestMethod]
        public void IbanShouldReturnValidBranchCode()
        {
            Iban iban = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "AD" ) ).BankCode( "0001" ).BranchCode( "2030" ).AccountNumber( "200359100100" ).Build();
            Assert.AreEqual( iban.GetBranchCode(), "2030" );
        }

        [TestMethod]
        public void IbanShouldReturnValidNationalCheckDigit()
        {
            Iban iban = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "AL" ) ).BankCode( "212" ).BranchCode( "1100" ).NationalCheckDigit( "9" ).AccountNumber( "0000000235698741" ).Build();
            Assert.AreEqual( iban.GetNationalCheckDigit(), "9" );                
        }

        [TestMethod]
        public void IbanShouldReturnValidAccountType()
        {
            Iban iban = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "BR" ) ).BankCode( "00360305" ).BranchCode( "00001" ).AccountNumber( "0009795493" ).AccountType( "P" ).OwnerAccountType( "1" ).Build();
            Assert.AreEqual( iban.GetAccountType(), "P" );
        }

        [TestMethod]
        public void IbanShouldReturnValidOwnerAccountType()
        {
            Iban iban = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "BR" ) ).BankCode( "00360305" ).BranchCode( "00001" ).AccountNumber( "0009795493" ).AccountType( "P" ).OwnerAccountType( "1" ).Build();
            Assert.AreEqual( iban.GetOwnerAccountType(), "1" );
        }

        [TestMethod]
        public void IbanShouldReturnValidIdentificationNumber()
        {
            Iban iban = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "IS" ) ).BankCode( "0159" ).BranchCode( "26" ).AccountNumber( "007654" ).IdentificationNumber( "5510730339" ).Build();
            Assert.AreEqual( iban.GetIdentificationNumber(), "5510730339" );
        }

        [TestMethod]
        public void IbanShouldReturnValidBBan()
        {
            Iban iban = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "CZ" ) ).BankCode( "0800" ).AccountNumber( "0000192045654526" ).Build();
            Assert.AreEqual( iban.GetBBan(), "08000000192045654526" );
        }

        [TestMethod]
        public void IbanShouldReturnValidCheckDigit()
        {            
            Iban iban = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "CZ" ) ).BankCode( "0800" ).AccountNumber( "0000192000145399" ).Build();
            Assert.AreEqual( iban.GetCheckDigit(), "65" );
        }

        [TestMethod]
        public void IbanToFormattedStringShouldHaveSpacesAfterEach4Character()
        {
            Iban iban = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "CZ" ) ).BankCode( "0800" ).AccountNumber( "0000192000145399" ).Build();
            Assert.AreEqual( iban.ToFormattedString(), "CZ65 0800 0000 1920 0014 5399" );
        }

        [TestMethod]
        public void IbanAccountNumberShouldPadItselfAccordingToBBANRule ()
        {
            Iban iban = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "CZ" ) ).BankCode( "0100" ).AccountNumber( "192000145399" ).Build();
            Assert.AreEqual( iban.GetAccountNumber(), "0000192000145399" );
        }

        [TestMethod]
        public void IbanBankCodeShoulPadItselfAccordingToBBANRule()
        {
            Iban iban = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "CZ" ) ).BankCode( "100" ).AccountNumber( "192000145399" ).Build();
            Assert.AreEqual( iban.GetBankCode(), "0100" );
        }

        [TestMethod]
        public void IbanTooLongAccountNumberShouldThrowException()
        {
            try
            {
                // Account number length rule for CZ is 16 digits
                Iban iban = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "CZ" ) ).BankCode( "0100" ).AccountNumber( "19200014539945687" ).Build();
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual( IbanFormatViolation.ACCOUNT_NUMBER_TOO_LONG, iex.FormatViolation );
            }
        }

        [TestMethod]
        public void IbanTooLongBankCodeShouldThrowException()
        {
            try
            {
                // Bank Code length rule for CZ is 4 digits
                Iban iban = new IbanBuilder().CountryCode( CountryCode.GetCountryCode( "CZ" ) ).BankCode( "00100" ).AccountNumber( "1920001453994568" ).Build();
            }
            catch (IbanFormatException iex)
            {
                Assert.AreEqual( IbanFormatViolation.BANK_CODE_TOO_LONG, iex.FormatViolation );
            }
        }

        

    }
}
