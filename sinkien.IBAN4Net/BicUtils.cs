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

namespace sinkien.IBAN4Net
{
    /// <summary>
    /// BIC Utility class
    /// </summary>
    public static class BicUtils
    {
        private const int _BIC8_LENGTH = 8;
        private const int _BIC11_LENGTH = 11;

        private const int _BANK_CODE_INDEX = 0;
        private const int _BANK_CODE_LENGTH = 4;
        private const int _COUNTRY_CODE_INDEX = _BANK_CODE_INDEX + _BANK_CODE_LENGTH;
        private const int _COUNTRY_CODE_LENGTH = 2;
        private const int _LOCATION_CODE_INDEX = _COUNTRY_CODE_INDEX + _COUNTRY_CODE_LENGTH;
        private const int _LOCATION_CODE_LENGTH = 2;
        private const int _BRANCH_CODE_INDEX = _LOCATION_CODE_INDEX + _LOCATION_CODE_LENGTH;
        private const int _BRANCH_CODE_LENGTH = 3;

        /// <summary>
        /// BIC validation
        /// </summary>
        /// <param name="bic">BIC to be validated.</param>
        /// <exception cref="BicFormatException">If BIC is invalid.</exception>
        /// <exception cref="UnsupportedCountryException">If BIC's country is not supported.</exception>
        public static void ValidateBIC(string bic)
        {
            try
            {
                validateEmpty( bic );
                validateLength( bic );
                validateCase( bic );
                validateBankCode( bic );
                validateCountryCode( bic );
                validateLocationCode( bic );

                if (HasBranchCode(bic))
                {
                    validateBranchCode( bic );
                }
            }
            catch (UnsupportedCountryException uce)
            {
                throw uce;
            }
            catch (BicFormatException bex)
            {
                throw bex;
            }
            catch (Exception ex)
            {
                throw new BicFormatException( ex.Message, BicFormatViolation.UNKNOWN, ex );
            }

        }        

        private static void validateEmpty (string bic)
        {
            if (string.IsNullOrEmpty(bic))
            {
                throw new BicFormatException( "Empty or null input string cannot be valid BIC", BicFormatViolation.BIC_NOT_EMPTY_OR_NULL );
            }
        }

        private static void validateLength (string bic)
        {
            if (bic.Length != _BIC8_LENGTH && bic.Length != _BIC11_LENGTH)
            {
                throw new BicFormatException( $"BIC length must be {_BIC8_LENGTH} or {_BIC11_LENGTH}", BicFormatViolation.BIC_LENGTH_8_OR_11 );
            }
        }

        private static void validateCase (string bic)
        {
            if (!bic.Equals(bic.ToUpper()))
            {
                throw new BicFormatException( "BIC must contain only upper case letters", BicFormatViolation.BIC_ONLY_UPPER_CASE_LETTERS );
            }
        }

        private static void validateBankCode (string bic)
        {
            string bankCode = GetBankCode( bic );
            foreach (char c in bankCode.ToCharArray())
            {
                if (!char.IsLetter(c))
                {
                    throw new BicFormatException( "Bank code must contain only letters", BicFormatViolation.BANK_CODE_ONLY_LETTERS );
                }
            }
            
        }

        private static void validateCountryCode (string bic)
        {
            string countryCode = GetCountryCode( bic );
            if (countryCode.Trim().Length <_COUNTRY_CODE_LENGTH || !countryCode.Equals(countryCode.ToUpper()) || !Char.IsLetter(countryCode[0]) || !Char.IsLetter(countryCode[1]))
            {
                throw new BicFormatException( "BIC country code must contain upper case letters", BicFormatViolation.COUNTRY_CODE_ONLY_UPPER_CASE_LETTERS );
            }

            if (CountryCode.GetCountryCode(countryCode) == null)
            {
                throw new UnsupportedCountryException( "Country code is not supported", countryCode );
            }
        }


        private static void validateLocationCode (string bic)
        {
            string locationCode = GetLocationCode( bic );
            foreach (char c in locationCode.ToCharArray())
            {
                if (!char.IsLetterOrDigit(c))
                {
                    throw new BicFormatException( "Location code must contain only letters or digits", BicFormatViolation.LOCATION_CODE_ONLY_LETTERS_OR_DIGITS );
                }
            }
        }

        private static void validateBranchCode (string bic)
        {
            string branchCode = GetBranchCode( bic );
            foreach (char c in branchCode.ToCharArray())
            {
                if (!char.IsLetterOrDigit(c))
                {
                    throw new BicFormatException( "Branch code must contain only letters or digits", BicFormatViolation.BRANCH_CODE_ONLY_LETTERS_OR_DIGITS );
                }
            }
        }

        public static string GetBankCode (string bic) => bic.Substring( _BANK_CODE_INDEX, _BANK_CODE_LENGTH );
        public static string GetCountryCode (string bic) => bic.Substring( _COUNTRY_CODE_INDEX, _COUNTRY_CODE_LENGTH );
        public static string GetLocationCode (string bic) => bic.Substring( _LOCATION_CODE_INDEX,  _LOCATION_CODE_LENGTH );
        public static bool HasBranchCode (string bic) => bic.Length == _BIC11_LENGTH;
        public static string GetBranchCode (string bic) => bic.Substring( _BRANCH_CODE_INDEX, _BRANCH_CODE_LENGTH );

    }
}
