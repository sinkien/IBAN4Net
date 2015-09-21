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
    [System.Runtime.InteropServices.Guid( "B3A09F00-BC91-4C53-87C1-30124ECD5EC6" )]
    public class BicUtilsTest
    {
        [TestMethod, ExpectedException(typeof(BicFormatException))]
        public void BicValidationWithEmptyhouldReturnThrowException ()
        {
            BicUtils.ValidateBIC( "" );
        }

        [TestMethod]
        public void BicValidationWithLessCharactersShouldThrowException()
        {
            try
            {
                BicUtils.ValidateBIC( "DEUTFF" );
            }
            catch (BicFormatException bex)
            {
                Assert.AreEqual( BicFormatViolation.BIC_LENGTH_8_OR_11, bex.FormatViolation );
            }
        }

        [TestMethod]
        public void BicValidationWithMoreCharactersShouldThrowException()
        {
            try
            {
                BicUtils.ValidateBIC( "DEUTFFDEUTFF" );
            }
            catch (BicFormatException bex)
            {
                Assert.AreEqual( BicFormatViolation.BIC_LENGTH_8_OR_11, bex.FormatViolation );
            }
        }

        [TestMethod]
        public void BicValidationWithlowercaseCharactersShouldThrowException()
        {
            try
            {
                BicUtils.ValidateBIC( "DEUTdeFF" );
            }
            catch (BicFormatException bex)
            {
                Assert.AreEqual( BicFormatViolation.BIC_ONLY_UPPER_CASE_LETTERS, bex.FormatViolation );
            }            
        }

        [TestMethod]
        public void BicValidationWithInvalidBankCodeShouldThrowException()
        {
            try
            {
                BicUtils.ValidateBIC( "DEU1DEFF" );
            }
            catch (BicFormatException bex)
            {
                Assert.AreEqual( BicFormatViolation.BANK_CODE_ONLY_LETTERS, bex.FormatViolation );
            }
        }

        [TestMethod, ExpectedException(typeof(UnsupportedCountryException))]
        public void BicValidationWithNonExistingCountryCodeShouldThrowException()
        {
            BicUtils.ValidateBIC( "DEUTXXFF" );
        }

        [TestMethod]
        public void BicValidationWithInvalidCountryCodeShouldThrowException()
        {
            try
            {
                BicUtils.ValidateBIC( "DEUT_1FF" );
            }
            catch (BicFormatException bex)
            {
                Assert.AreEqual( BicFormatViolation.COUNTRY_CODE_ONLY_UPPER_CASE_LETTERS, bex.FormatViolation );
            }
        }

        [TestMethod]
        public void BicValidationWithInvalidLocationCodeShouldThrowException()
        {
            try
            {
                BicUtils.ValidateBIC( "DEUTDEF " );
            }
            catch (BicFormatException bex)
            {
                Assert.AreEqual( BicFormatViolation.LOCATION_CODE_ONLY_LETTERS_OR_DIGITS, bex.FormatViolation );
            }
        }

        [TestMethod]
        public void BicValidationWithInvalidBranchCodeShouldThrowException()
        {
            try
            {
                BicUtils.ValidateBIC( "DEUTDEFF50_" );
            }
            catch (BicFormatException bex)
            {
                Assert.AreEqual( BicFormatViolation.BRANCH_CODE_ONLY_LETTERS_OR_DIGITS, bex.FormatViolation );
            }
        }

        [TestMethod]
        public void BicShouldReturnCountryCode()
        {
            string test = BicUtils.GetCountryCode( "DEUTDEFF500" );
            Assert.AreEqual( test, "DE" );
        }

        [TestMethod]
        public void BicShouldReturnBankCode()
        {
            string test = BicUtils.GetBankCode( "DEUTDEFF500" );
            Assert.AreEqual( test, "DEUT" );
        }
    }
}
