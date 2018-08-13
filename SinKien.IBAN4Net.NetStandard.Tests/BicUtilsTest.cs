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
    public class BicUtilsTest
    {
        [TestMethod, ExpectedException(typeof(BicFormatException))]
        public void BicValidationWithEmptyShouldReturnThrowException()
        {
            BicUtils.ValidateBIC("");
        }

        [TestMethod]
        public void BicIsValidWithEmptyStringShouldReturnFalse()
        {
            BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
            Assert.IsFalse(BicUtils.IsValid("", out validationResult));
        }

        [TestMethod]
        public void BicIsValidWithEmptyStringShouldReturnRightResult()
        {
            BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
            BicUtils.IsValid("", out validationResult);

            Assert.AreEqual(BicFormatViolation.BIC_NOT_EMPTY_OR_NULL, validationResult);
        }

        [TestMethod]
        public void BicValidationWithLessCharactersShouldThrowException()
        {
            try
            {
                BicUtils.ValidateBIC("DEUTFF");
            }
            catch (BicFormatException bex)
            {
                Assert.AreEqual(BicFormatViolation.BIC_LENGTH_8_OR_11, bex.FormatViolation);
            }
        }

        [TestMethod]
        public void BicIsValidWithLessCharactersShouldReturnFalse()
        {
            BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
            Assert.IsFalse(BicUtils.IsValid("DEUTFF", out validationResult));
        }

        [TestMethod]
        public void BicIsValidWithLessCharactersShouldReturnRightResult()
        {
            BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
            BicUtils.IsValid("DEUTFF", out validationResult);

            Assert.AreEqual(BicFormatViolation.BIC_LENGTH_8_OR_11, validationResult);
        }

        [TestMethod]
        public void BicValidationWithMoreCharactersShouldThrowException()
        {
            try
            {
                BicUtils.ValidateBIC("DEUTFFDEUTFF");
            }
            catch (BicFormatException bex)
            {
                Assert.AreEqual(BicFormatViolation.BIC_LENGTH_8_OR_11, bex.FormatViolation);
            }
        }

        [TestMethod]
        public void BicIsValidWithMoreCharactersShouldReturnFalse()
        {
            BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
            Assert.IsFalse(BicUtils.IsValid("DEUTFFDEUTFF", out validationResult));
        }

        [TestMethod]
        public void BicIsValidWithMoreCharactersShouldReturnRightResult()
        {
            BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
            BicUtils.IsValid("DEUTFFDEUTFF", out validationResult);

            Assert.AreEqual(BicFormatViolation.BIC_LENGTH_8_OR_11, validationResult);
        }

        [TestMethod]
        public void BicValidationWithLowercaseCharactersShouldThrowException()
        {
            try
            {
                BicUtils.ValidateBIC("DEUTdeFF");
            }
            catch (BicFormatException bex)
            {
                Assert.AreEqual(BicFormatViolation.BIC_ONLY_UPPER_CASE_LETTERS, bex.FormatViolation);
            }
        }

        [TestMethod]
        public void BicIsValidWithLowercaseCharactersShouldReturnFalse()
        {
            BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
            Assert.IsFalse(BicUtils.IsValid("DEUTdeFF", out validationResult));
        }

        [TestMethod]
        public void BicIsValidWithLowercaseCharactersShouldReturnRightResult()
        {
            BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
            BicUtils.IsValid("DEUTdeFF", out validationResult);

            Assert.AreEqual(BicFormatViolation.BIC_ONLY_UPPER_CASE_LETTERS, validationResult);
        }

        [TestMethod]
        public void BicValidationWithInvalidBankCodeShouldThrowException()
        {
            try
            {
                BicUtils.ValidateBIC("DEU1DEFF");
            }
            catch (BicFormatException bex)
            {
                Assert.AreEqual(BicFormatViolation.BANK_CODE_ONLY_LETTERS, bex.FormatViolation);
            }
        }

        [TestMethod]
        public void BicIsValidWithInvalidBankCodeShouldReturnFalse()
        {
            BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
            Assert.IsFalse(BicUtils.IsValid("DEU1DEFF", out validationResult));
        }

        [TestMethod]
        public void BicIsValidWithInvalidBankCodeShouldReturnRightResult()
        {
            BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
            BicUtils.IsValid("DEU1DEFF", out validationResult);

            Assert.AreEqual(BicFormatViolation.BANK_CODE_ONLY_LETTERS, validationResult);
        }

        [TestMethod, ExpectedException(typeof(UnsupportedCountryException))]
        public void BicValidationWithNonExistingCountryCodeShouldThrowException()
        {
            BicUtils.ValidateBIC("DEUTXXFF");
        }

        [TestMethod]
        public void BicValidationWithInvalidCountryCodeShouldThrowException()
        {
            try
            {
                BicUtils.ValidateBIC("DEUT_1FF");
            }
            catch (BicFormatException bex)
            {
                Assert.AreEqual(BicFormatViolation.COUNTRY_CODE_ONLY_UPPER_CASE_LETTERS, bex.FormatViolation);
            }
        }

        [TestMethod]
        public void BicIsValidWithInvalidCountryCodeShouldReturnFalse()
        {
            BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
            Assert.IsFalse(BicUtils.IsValid("DEUT_1FF", out validationResult));
        }

        [TestMethod]
        public void BicIsValidWithInvalidCountryCodeShouldReturnRightResult()
        {
            BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
            BicUtils.IsValid("DEUT_1FF", out validationResult);

            Assert.AreEqual(BicFormatViolation.COUNTRY_CODE_ONLY_UPPER_CASE_LETTERS, validationResult);
        }

        [TestMethod]
        public void BicIsValidWithUnsupportedCountryShouldReturnFalse()
        {
            BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
            Assert.IsFalse(BicUtils.IsValid("DEUTXXFF", out validationResult));
        }

        [TestMethod]
        public void BicIsValidWithUnsupportedCountryShouldReturnRightResult()
        {
            BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
            BicUtils.IsValid("DEUTXXFF", out validationResult);

            Assert.AreEqual(BicFormatViolation.COUNTRY_CODE_UNSUPPORTED, validationResult);
        }

        [TestMethod]
        public void BicValidationWithInvalidLocationCodeShouldThrowException()
        {
            try
            {
                BicUtils.ValidateBIC("DEUTDEF ");
            }
            catch (BicFormatException bex)
            {
                Assert.AreEqual(BicFormatViolation.LOCATION_CODE_ONLY_LETTERS_OR_DIGITS, bex.FormatViolation);
            }
        }

        [TestMethod]
        public void BicIsValidWithInvalidLocationCodeShouldReturnFalse()
        {
            BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
            Assert.IsFalse(BicUtils.IsValid("DEUTDEF ", out validationResult));
        }

        [TestMethod]
        public void BicIsValidWithInvalidLocationCodeShouldReturnRightResult()
        {
            BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
            BicUtils.IsValid("DEUTDEF ", out validationResult);

            Assert.AreEqual(BicFormatViolation.LOCATION_CODE_ONLY_LETTERS_OR_DIGITS, validationResult);
        }

        [TestMethod]
        public void BicValidationWithInvalidBranchCodeShouldThrowException()
        {
            try
            {
                BicUtils.ValidateBIC("DEUTDEFF50_");
            }
            catch (BicFormatException bex)
            {
                Assert.AreEqual(BicFormatViolation.BRANCH_CODE_ONLY_LETTERS_OR_DIGITS, bex.FormatViolation);
            }
        }

        [TestMethod]
        public void BicIsValidWithInvalidBranchCodeShouldReturnFalse()
        {
            BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
            Assert.IsFalse(BicUtils.IsValid("DEUTDEFF50_", out validationResult));
        }

        [TestMethod]
        public void BicIsValidWithInvalidBranchCodeShouldReturnRightResult()
        {
            BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
            BicUtils.IsValid("DEUTDEFF50_", out validationResult);

            Assert.AreEqual(BicFormatViolation.BRANCH_CODE_ONLY_LETTERS_OR_DIGITS, validationResult);
        }


        [TestMethod]
        public void BicValidationWithValidBicShouldNotThrowException()
        {
            BicUtils.ValidateBIC("DEUTDEFF500");
        }

        [TestMethod]
        public void BicIsValidWithValidBicShouldReturnTrue()
        {
            BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
            Assert.IsTrue(BicUtils.IsValid("DEUTDEFF500", out validationResult));
        }

        [TestMethod]
        public void BicIsValidWithValidBicShouldReturnRightResult()
        {
            BicFormatViolation validationResult = BicFormatViolation.NO_VIOLATION;
            BicUtils.IsValid("DEUTDEFF500", out validationResult);

            Assert.AreEqual(BicFormatViolation.NO_VIOLATION, validationResult);
        }

        [TestMethod]
        public void BicShouldReturnCountryCode()
        {
            string test = BicUtils.GetCountryCode("DEUTDEFF500");
            Assert.AreEqual("DE", test);
        }

        [TestMethod]
        public void BicShouldReturnBankCode()
        {
            string test = BicUtils.GetBankCode("DEUTDEFF500");
            Assert.AreEqual("DEUT", test);
        }
    }
}
