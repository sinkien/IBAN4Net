/*
 * IBAN4Net
 * Copyright 2018 Vaclav Beca [sinkien]
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

using SinKien.IBAN4Net.Exceptions;
using System;
using System.Text;

namespace SinKien.IBAN4Net
{
    /// <summary>
    /// IBAN Utilities
    /// </summary>
    public class IbanUtils
    {        
        /// <summary>
        /// Validation of IBAN string
        /// </summary>
        /// <param name="iban">IBAN string</param>
        /// <exception cref="IbanFormatException">Thrown when IBAN is invalid</exception>
        /// <exception cref="UnsupportedCountryException">Thrown when INAB's country code is not supported</exception>
        /// <exception cref="InvalidCheckDigitException">Thrown when IBAN string contains invalid check digit</exception>
        public static void Validate(string iban)
        {
            try
            {
                validateEmpty(iban);
                validateCountryCode(iban);
                validateCheckDigitPresence(iban);

                BBanStructure structure = getBbanStructure(iban);
                validateBbanLength(iban, structure);
                validateBbanEntries(iban, structure);

                validateCheckDigit(iban);
            }
            catch (InvalidCheckDigitException icex)
            {
                throw icex;
            }
            catch (IbanFormatException iex)
            {
                throw iex;
            }
            catch (UnsupportedCountryException ucex)
            {
                throw ucex;
            }
            catch (Exception ex)
            {
                throw new IbanFormatException(ex.Message, IbanFormatViolation.UNKNOWN, ex);
            }
        }

        /// <summary>
        /// Validation of IBAN string
        /// </summary>
        /// <param name="iban">Iban string</param>
        /// <param name="validationResult">Validation result</param>
        /// <returns>True if IBAN string is valid, false if it encounters any problem</returns>
        public static bool IsValid(string iban, out IbanFormatViolation validationResult)
        {
            bool result = false;
            validationResult = IbanFormatViolation.NO_VIOLATION;

            if (!string.IsNullOrEmpty(iban))
            {
                if (hasValidCountryCode(iban, out validationResult))
                {
                    if (hasValidCheckDigit(iban, out validationResult))
                    {
                        BBanStructure structure = getBbanStructure(iban);
                        if (hasValidBbanLength(iban, structure, out validationResult))
                        {
                            if (hasValidBbanEntries(iban, structure, out validationResult))
                            {
                                if (hasValidCheckDigitValue(iban, out validationResult))
                                {
                                    result = true;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                validationResult = IbanFormatViolation.IBAN_NOT_EMPTY_OR_NULL;
            }

            return result;
        }

        /// <summary>
        /// Checks whether country is supported.
        /// It is checked by trying to find the country code in defined BBAN structures.
        /// </summary>
        /// <param name="countryCode">Country code object</param>
        /// <returns>True if country code is supported, othewise false</returns>
        public static bool IsSupportedCountry(CountryCodeEntry countryCode) => (CountryCode.GetCountryCode(countryCode?.Alpha2) != null) && (Bban.GetStructureForCountry(countryCode) != null);


        /// <summary>
        /// Checks whether country is supported.
        /// It is checked by trying to find the country code in defined BBAN structures.
        /// </summary>
        /// <param name="alpha2Code">Alpha2 code for country</param>
        /// <returns>True if country code is supported, othewise false</returns>
        public static bool IsSupportedCountry(string alpha2Code) => (CountryCode.GetCountryCode(alpha2Code) != null) && (Bban.GetStructureForCountry(alpha2Code) != null);

        /// <summary>
        /// Returns IBAN length for the specified country
        /// </summary>
        /// <param name="countryCode">Country code object</param>
        /// <returns>The length of IBAN for the specified country</returns>
        public static int GetIbanLength(CountryCodeEntry countryCode)
        {
            int result = 0;
            BBanStructure structure = getBbanStructure(countryCode);

            if (structure != null)
            {
                result = Consts.IBAN_COUNTRY_CODE_LENGTH + Consts.IBAN_CHECK_DIGIT_LENGTH + structure.GetBBanLength();
            }

            return result;
        }

        /// <summary>
        /// Calculates IBAN's check digit.
        /// ISO13616
        /// </summary>
        /// <param name="iban">Iban string value</param>
        /// <returns>Check digit as string</returns>
        /// <exception cref="IbanFormatException">Thrown if supplied iban string contains invalid chracters</exception>
        public static string CalculateCheckDigit(string iban)
        {
            string reformattedIban = ReplaceCheckDigit(iban, Consts.IBAN_DEFAULT_CHECK_DIGIT);
            int modResult = calculateMod(reformattedIban);
            int checkDigitValue = (98 - modResult);
            string checkDigit = checkDigitValue.ToString();

            return checkDigitValue > 9 ? checkDigit : "0" + checkDigit;
        }

        /// <summary>
        /// Calculates IBAN's check digit.
        /// ISO13616
        /// </summary>
        /// <param name="iban">IBAN object</param>
        /// <returns>Check digit as string</returns>
        /// <exception cref="IbanFormatException">Thrown if supplied iban string contains invalid chracters</exception>
        public static string CalculateCheckDigit(Iban iban) => CalculateCheckDigit(iban.ToString());

        /// <summary>
        /// Returns IBAN's check digit
        /// </summary>
        /// <param name="iban">IBAN string value</param>
        /// <returns>Check digit string</returns>
        public static string GetCheckDigit(string iban) => iban.Substring(Consts.IBAN_CHECK_DIGIT_INDEX, Consts.IBAN_CHECK_DIGIT_LENGTH);


        /// <summary>
        /// Returns IBAN's country code
        /// </summary>
        /// <param name="iban">IBAN string value</param>
        /// <returns>IBAN's country code string</returns>
        public static string GetCountryCode(string iban) => iban.Substring(Consts.IBAN_COUNTRY_CODE_INDEX, Consts.IBAN_COUNTRY_CODE_LENGTH);

        /// <summary>
        /// Returns IBAN'S country code and check digit
        /// </summary>
        /// <param name="iban">IBAN string vlaue</param>
        /// <returns>IBAN's country code and check digit string</returns>
        public static string GetCountryCodeAndCheckDigit(string iban) => iban.Substring(Consts.IBAN_COUNTRY_CODE_INDEX, 
            Consts.IBAN_COUNTRY_CODE_LENGTH + Consts.IBAN_CHECK_DIGIT_LENGTH);

        /// <summary>
        /// Returns IBAN's BBAN code 
        /// (all what is left without country code and check digit).
        /// </summary>
        /// <param name="iban">Iban string value</param>
        /// <returns>BBAN string</returns>
        public static string GetBBan(string iban) => iban.Substring(Consts.IBAN_BBAN_INDEX);

        /// <summary>
        /// Returns IBAN's account number
        /// </summary>
        /// <param name="iban">IBAN string value</param>
        /// <returns>IBAN's account number as string</returns>
        public static string GetAccountNumber(string iban) => extractBbanEntry(iban, BBanEntryType.ACCOUNT_NUMBER);


        /// <summary>
        /// Returns IBAN's account number prefix      
        /// </summary>
        /// <param name="iban">IBAN string value</param>
        /// <returns>IBAN's account number as string (if it is present)</returns>
        public static string GetAccountNumberPrefix(string iban) => extractBbanEntry(iban, BBanEntryType.ACCOUNT_NUMBER_PREFIX);

        /// <summary>
        /// Returns a new IBAN string with changed account number
        /// Automatically adds zeros to the beginning in order to maintain the length specified by the BBAN rule
        /// </summary>
        /// <param name="iban">Original IBAN</param>
        /// <param name="newAccountNumber">The new account number</param>
        /// <returns>IBAN with changed account number and recalculated check digit</returns>
        /// <exception cref="IbanFormatException">Thrown when new account number is longer, than that is specified in BBAN rules</exception>
        public static string ChangeAccountNumber(string iban, string newAccountNumber) => changeBbanEntry(iban, newAccountNumber, BBanEntryType.ACCOUNT_NUMBER);

        /// <summary>
        /// Returns a new IBAN string with changed account number prefix
        /// Automatically adds zeros to the beginning in order to maintain the length specified by the BBAN rule
        /// </summary>
        /// <param name="iban">Original IBAN</param>
        /// <param name="newAccountNumberPrefix">The new account number prefix</param>
        /// <returns>IBAN with changed account number prefix and recalculated check digit</returns>
        /// <exception cref="IbanFormatException">Thrown when new account number is longer, than that is specified in BBAN rules</exception>
        public static string ChangeAccountNumberPrefix(string iban, string newAccountNumberPrefix) => changeBbanEntry(iban, newAccountNumberPrefix, BBanEntryType.ACCOUNT_NUMBER_PREFIX);

        /// <summary>
        /// Returns IBAN'S bank code
        /// </summary>
        /// <param name="iban">Iban string value</param>
        /// <returns>IBAN's bank code</returns>
        public static string GetBankCode(string iban) => extractBbanEntry(iban, BBanEntryType.BANK_CODE);

        /// <summary>
        /// Return a new IBAN string with changed bank code
        /// Automatically adds zeros to the beginning in order to maintain the length specified by the BBAN rule
        /// </summary>
        /// <param name="iban">Original IBAN</param>
        /// <param name="newBankCode">The new bank code</param>
        /// <returns>IBAN with changed bank code and recalculated check digit</returns>
        /// <exception cref="IbanFormatException">Thrown when new bank code is longer, than that is specified in BBAN rules</exception>
        public static string ChangeBankCode(string iban, string newBankCode) => changeBbanEntry(iban, newBankCode, BBanEntryType.BANK_CODE);

        /// <summary>
        /// Returns IBAN's branch code
        /// </summary>
        /// <param name="iban">Iban string value</param>
        /// <returns>IBAN's branch code string</returns>
        public static string GetBranchCode(string iban) => extractBbanEntry(iban, BBanEntryType.BRANCH_CODE);

        /// <summary>
        /// Returns IBAN's national check digit
        /// </summary>
        /// <param name="iban">Iban value string</param>
        /// <returns>IBAN's national check digit string</returns>
        public static string GetNationalCheckDigit(string iban) => extractBbanEntry(iban, BBanEntryType.NATIONAL_CHECK_DIGIT);

        /// <summary>
        /// Returns IBAN's account type
        /// </summary>
        /// <param name="iban">Iban string value</param>
        /// <returns>IBAN's account type string</returns>
        public static string GetAccountType(string iban) => extractBbanEntry(iban, BBanEntryType.ACCOUNT_TYPE);

        /// <summary>
        /// Returns IBAN'S owner account type
        /// </summary>
        /// <param name="iban">Iban string value</param>
        /// <returns>IBAN's owner account type string</returns>
        public static string GetOwnerAccountType(string iban) => extractBbanEntry(iban, BBanEntryType.OWNER_ACCOUNT_NUMBER);

        /// <summary>
        /// Returns IBAN's identification number
        /// </summary>
        /// <param name="iban">Iban string value</param>
        /// <returns>IBAN's identifcation number string</returns>
        public static string GetIdentificationNumber(string iban) => extractBbanEntry(iban, BBanEntryType.IDENTIFICATION_NUMBER);



        /// <summary>
        /// Returns an iban with replaced check digit
        /// </summary>
        /// <param name="iban">Iban string value</param>
        /// <param name="checkDigit">A check digit which will be placed to IBAN string</param>
        /// <returns>IBAN string with replaced check digit</returns>
        public static string ReplaceCheckDigit(string iban, string checkDigit) => GetCountryCode(iban) + checkDigit + GetBBan(iban);



        private static void validateCheckDigit(string iban)
        {
            if (calculateMod(iban) != 1)
            {
                string checkDigit = GetCheckDigit(iban);
                string expectedCheckDigit = CalculateCheckDigit(iban);

                throw new InvalidCheckDigitException($"{iban} has invalid check digit {checkDigit}. Expected check digit is {expectedCheckDigit}", expectedCheckDigit, checkDigit);
            }
        }

        private static bool hasValidCheckDigitValue(string iban, out IbanFormatViolation validationResult)
        {
            validationResult = IbanFormatViolation.NO_VIOLATION;

            if (calculateMod(iban) != 1)
            {
                validationResult = IbanFormatViolation.IBAN_INVALID_CHECK_DIGIT_VALUE;
            }

            return (validationResult == IbanFormatViolation.NO_VIOLATION);
        }

        private static void validateEmpty(string iban)
        {
            if (string.IsNullOrEmpty(iban))
            {
                throw new IbanFormatException("Empty or null input cannot be a valid IBAN", IbanFormatViolation.IBAN_NOT_EMPTY_OR_NULL);
            }
        }

        private static void validateCountryCode(string iban)
        {
            if (iban.Length < Consts.IBAN_COUNTRY_CODE_LENGTH)
            {
                throw new IbanFormatException("Input must contain 2 letters for country code", IbanFormatViolation.COUNTRY_CODE_TWO_LETTERS, iban);
            }

            string countryCode = GetCountryCode(iban);

            if (!countryCode.Equals(countryCode.ToUpper()) || !char.IsLetter(iban[0]) || !char.IsLetter(iban[1]))
            {
                throw new IbanFormatException("IBAN's country code must contain upper case letters", IbanFormatViolation.COUNTRY_CODE_UPPER_CASE_LETTERS, iban);
            }

            CountryCodeEntry countryEntry = CountryCode.GetCountryCode(countryCode);

            if (countryEntry == null)
            {
                throw new IbanFormatException("IBAN contains non existing country code", IbanFormatViolation.COUNTRY_CODE_EXISTS, iban);
            }

            BBanStructure structure = Bban.GetStructureForCountry(countryEntry);
            if (structure == null)
            {
                throw new UnsupportedCountryException("IBAN contains not supported country code", countryCode);
            }
        }

        private static bool hasValidCountryCode(string iban, out IbanFormatViolation validationResult)
        {
            validationResult = IbanFormatViolation.NO_VIOLATION;

            if (iban.Length < Consts.IBAN_COUNTRY_CODE_LENGTH)
            {
                validationResult = IbanFormatViolation.COUNTRY_CODE_TWO_LETTERS;
            }
            else
            {
                string countryCode = GetCountryCode(iban);
                if (!countryCode.Equals(countryCode.ToUpper()) || !char.IsLetter(iban[0]) || !char.IsLetter(iban[1]))
                {
                    validationResult = IbanFormatViolation.COUNTRY_CODE_UPPER_CASE_LETTERS;
                }
                else
                {
                    CountryCodeEntry countryEntry = CountryCode.GetCountryCode(countryCode);
                    if (countryEntry == null)
                    {
                        validationResult = IbanFormatViolation.COUNTRY_CODE_EXISTS;
                    }
                    else
                    {
                        BBanStructure structure = Bban.GetStructureForCountry(countryEntry);
                        if (structure == null)
                        {
                            validationResult = IbanFormatViolation.COUNTRY_CODE_UNSUPPORTED;
                        }
                    }
                }
            }

            return (validationResult == IbanFormatViolation.NO_VIOLATION);
        }

        private static void validateCheckDigitPresence(string iban)
        {
            if (iban.Length < (Consts.IBAN_COUNTRY_CODE_LENGTH + Consts.IBAN_CHECK_DIGIT_LENGTH))
            {
                throw new IbanFormatException("IBAN must contain 2 digit check digit", IbanFormatViolation.CHECK_DIGIT_TWO_DIGITS, 
                    iban.Substring(Consts.IBAN_COUNTRY_CODE_LENGTH));
            }

            string checkDigit = GetCheckDigit(iban);
            if (!char.IsDigit(checkDigit[0]) || !char.IsDigit(checkDigit[1]))
            {
                throw new IbanFormatException("IBAN's check digit should contain only digits", IbanFormatViolation.CHECK_DIGIT_ONLY_DIGITS, checkDigit);
            }
        }

        private static bool hasValidCheckDigit(string iban, out IbanFormatViolation validationResult)
        {
            validationResult = IbanFormatViolation.NO_VIOLATION;

            if ((iban.Length < (Consts.IBAN_COUNTRY_CODE_LENGTH + Consts.IBAN_CHECK_DIGIT_LENGTH)))
            {
                validationResult = IbanFormatViolation.CHECK_DIGIT_TWO_DIGITS;
            }
            else
            {
                string checkDigit = GetCheckDigit(iban);
                if (!char.IsDigit(checkDigit[0]) || !char.IsDigit(checkDigit[1]))
                {
                    validationResult = IbanFormatViolation.CHECK_DIGIT_ONLY_DIGITS;
                }
            }

            return (validationResult == IbanFormatViolation.NO_VIOLATION);
        }

        private static void validateBbanLength(string iban, BBanStructure structure)
        {
            int expectedBbanLength = structure.GetBBanLength();
            string bban = GetBBan(iban);
            int bbanLength = bban.Length;

            if (expectedBbanLength != bbanLength)
            {
                throw new IbanFormatException($"BBAN '{bban}' length is {bbanLength}, expected is {expectedBbanLength}",
                                               IbanFormatViolation.BBAN_LENGTH, bbanLength, expectedBbanLength);
            }
        }

        private static bool hasValidBbanLength(string iban, BBanStructure structure, out IbanFormatViolation validationResult)
        {
            validationResult = IbanFormatViolation.NO_VIOLATION;

            int expectedBbanLength = structure.GetBBanLength();
            string bban = GetBBan(iban);
            int bbanLength = bban.Length;

            if (expectedBbanLength != bbanLength)
            {
                validationResult = IbanFormatViolation.BBAN_LENGTH;
            }

            return (validationResult == IbanFormatViolation.NO_VIOLATION);
        }

        private static void validateBbanEntries(string iban, BBanStructure structure)
        {
            string bban = GetBBan(iban);
            int bbanOffset = 0;

            foreach (BBanEntry entry in structure.Entries)
            {
                int entryLength = entry.Length;
                string entryValue = bban.Substring(bbanOffset, entryLength);

                bbanOffset += entryLength;

                validateBbanEntryCharacterType(entry, entryValue);
            }
        }

        private static bool hasValidBbanEntries(string iban, BBanStructure structure, out IbanFormatViolation validationResult)
        {
            validationResult = IbanFormatViolation.NO_VIOLATION;

            string bban = GetBBan(iban);
            int bbanOffset = 0;

            foreach (BBanEntry entry in structure.Entries)
            {
                int entryLength = entry.Length;
                string entryValue = bban.Substring(bbanOffset, entryLength);

                bbanOffset += entryLength;

                if (!hasValidBbanEntryCharacterType(entry, entryValue, out validationResult))
                {
                    break;
                }
            }

            return (validationResult == IbanFormatViolation.NO_VIOLATION);
        }

        private static void validateBbanEntryCharacterType(BBanEntry entry, string entryValue)
        {
            switch (entry.CharacterType)
            {
                case BBanEntryCharacterType.A:
                    foreach (char c in entryValue.ToCharArray())
                    {
                        if (!char.IsUpper(c))
                        {
                            throw new IbanFormatException($"'{entryValue}' must contain only upper case letters",
                                                           IbanFormatViolation.BBAN_ONLY_UPPER_CASE_LETTERS, c, entry.EntryType, entryValue);
                        }
                    }
                    break;
                case BBanEntryCharacterType.C:
                    foreach (char c in entryValue.ToCharArray())
                    {
                        if (!char.IsLetterOrDigit(c))
                        {
                            throw new IbanFormatException($"'{entryValue}' must contain only letters or digits",
                                                           IbanFormatViolation.BBAN_ONLY_DIGITS_OR_LETTERS, c, entry.EntryType, entryValue);
                        }
                    }
                    break;
                case BBanEntryCharacterType.N:
                    foreach (char c in entryValue.ToCharArray())
                    {
                        if (!char.IsDigit(c))
                        {
                            throw new IbanFormatException($"'{entryValue}' must contain only digits",
                                                           IbanFormatViolation.BBAN_ONLY_DIGITS, c, entry.EntryType, entryValue);
                        }
                    }
                    break;
            }
        }

        private static bool hasValidBbanEntryCharacterType(BBanEntry entry, string entryValue, out IbanFormatViolation validationResult)
        {
            validationResult = IbanFormatViolation.NO_VIOLATION;

            switch (entry.CharacterType)
            {
                case BBanEntryCharacterType.A:
                    foreach (char c in entryValue.ToCharArray())
                    {
                        if (!char.IsUpper(c))
                        {
                            validationResult = IbanFormatViolation.BBAN_ONLY_UPPER_CASE_LETTERS;
                            break;
                        }
                    }
                    break;
                case BBanEntryCharacterType.C:
                    foreach (char c in entryValue.ToCharArray())
                    {
                        if (!char.IsLetterOrDigit(c))
                        {
                            validationResult = IbanFormatViolation.BBAN_ONLY_DIGITS_OR_LETTERS;
                            break;
                        }
                    }
                    break;
                case BBanEntryCharacterType.N:
                    foreach (char c in entryValue.ToCharArray())
                    {
                        if (!char.IsDigit(c))
                        {
                            validationResult = IbanFormatViolation.BBAN_ONLY_DIGITS;
                            break;
                        }
                    }
                    break;
            }

            return (validationResult == IbanFormatViolation.NO_VIOLATION);
        }

        private static int calculateMod(string iban)
        {
            string reformattedIban = GetBBan(iban) + GetCountryCodeAndCheckDigit(iban);
            double total = 0;

            // a little java's workaround ;)
            char[] letters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            for (int i = 0; i < reformattedIban.Length; i++)
            {
                double numericValue = char.IsLetter(reformattedIban[i]) ? (10 + Array.IndexOf(letters, reformattedIban[i])) : char.GetNumericValue(reformattedIban[i]);
                if (numericValue < 0 || numericValue > 35)
                {
                    throw new IbanFormatException($"Invalid character on position {i} = {numericValue}", IbanFormatViolation.IBAN_VALID_CHARACTERS, reformattedIban[i]);
                }

                total = (numericValue > 9 ? total * 100 : total * 10) + numericValue;

                if (total > Consts.IBAN_MAX)
                {
                    total = (total % Consts.IBAN_MOD);
                }
            }

            return (int)(total % Consts.IBAN_MOD);
        }

        private static BBanStructure getBbanStructure(string iban)
        {
            string countryCode = GetCountryCode(iban);
            return getBbanStructure(CountryCode.GetCountryCode(countryCode));
        }

        private static BBanStructure getBbanStructure(CountryCodeEntry countryCode) => Bban.GetStructureForCountry(countryCode);

        private static string extractBbanEntry(string iban, BBanEntryType entryType)
        {
            string result = "";

            string bban = GetBBan(iban);
            BBanStructure structure = getBbanStructure(iban);
            int bbanOffset = 0;

            foreach (BBanEntry entry in structure.Entries)
            {
                int entryLength = entry.Length;
                string entryValue = bban.Substring(bbanOffset, entryLength);

                bbanOffset += entryLength;

                if (entry.EntryType == entryType)
                {
                    result = entryValue;
                    break;
                }
            }

            return result;
        }

        private static string changeBbanEntry(string iban, string newValue, BBanEntryType entryType)
        {

            string bban = GetBBan(iban);
            string newIban = GetCountryCode(iban) + Consts.IBAN_DEFAULT_CHECK_DIGIT;

            BBanStructure structure = getBbanStructure(iban);
            int bbanOffset = 0;
            StringBuilder sb = new StringBuilder(bban);

            foreach (BBanEntry entry in structure.Entries)
            {
                if (entry.EntryType == entryType)
                {

                    if (newValue.Length > entry.Length)
                    {
                        throw new IbanFormatException($"New value for {Enum.GetName(typeof(BBanEntryType), entry.EntryType)} is too long.", IbanFormatViolation.BBAN_ENTRY_TOO_LONG);
                    }

                    sb.Remove(bbanOffset, entry.Length);
                    sb.Insert(bbanOffset, newValue.PadLeft(entry.Length, '0'));
                    break;
                }

                bbanOffset += entry.Length;
            }

            sb.Insert(0, newIban);
            newIban = sb.ToString();

            string newCheckDigit = CalculateCheckDigit(newIban);
            string result = ReplaceCheckDigit(newIban, newCheckDigit);

            return result;

        }
    }
}
