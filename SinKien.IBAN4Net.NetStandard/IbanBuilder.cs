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
using System.Text;

namespace SinKien.IBAN4Net
{
    /// <summary>
    /// IBAN Builder class
    /// </summary>
    public class IbanBuilder
    {
        private CountryCodeEntry _countryCodeEntry = null;
        private string _bankCode = "";
        private string _branchCode = "";
        private string _nationalCheckDigit = "";
        private string _accountType = "";
        private string _accountNumberPrefix = "";
        private string _accountNumber = "";
        private string _ownerAccountType = "";
        private string _identificationNumber = "";

        private bool _enableAutopadding = false;

        /// <summary>
        /// Creates an Iban Builder instance
        /// For chaining.
        /// </summary>
        public IbanBuilder()
        { }

        /// <summary>
        /// Sets iban's Country code 
        /// </summary>
        /// <param name="countryCode">Country code entry</param>
        /// <returns>Builder instance</returns>
        public IbanBuilder CountryCode(CountryCodeEntry countryCode)
        {
            this._countryCodeEntry = countryCode;
            return this;
        }

        /// <summary>
        /// Sets iban's bank code
        /// During building of IBAN string, if this feature is enabled, the number is left-padded with zeroes based on BBAN Account Number policy for
        /// specified country.
        /// </summary>
        /// <param name="bankCode">Bank code string</param>
        /// <returns>Builder instance</returns>
        public IbanBuilder BankCode(string bankCode)
        {
            this._bankCode = bankCode;
            return this;
        }

        /// <summary>
        /// Sets iban's branch code
        /// </summary>
        /// <param name="branchCode"> ranch code string</param>
        /// <returns>Builder instance</returns>
        public IbanBuilder BranchCode(string branchCode)
        {
            this._branchCode = branchCode;
            return this;
        }

        /// <summary>
        /// Set iban's account prefix number
        /// During building of IBAN string, if this feature is enabled, the number is left-padded with zeroes based on BBAN Account Number policy for
        /// specified country.
        /// </summary>
        /// <param name="accountNumberPrefix">Account number prefix string</param>
        /// <returns>Builder instance</returns>
        public IbanBuilder AccountNumberPrefix(string accountNumberPrefix)
        {
            this._accountNumberPrefix = accountNumberPrefix;
            return this;
        }

        /// <summary>
        /// Sets iban's account number
        /// During building of IBAN string, if this feature is enabled, the number is left-padded with zeroes based on BBAN Account Number policy for
        /// specified country.
        /// </summary>
        /// <param name="accountNumber">Account number string</param>
        /// <returns>Builder instance</returns>
        public IbanBuilder AccountNumber(string accountNumber)
        {
            this._accountNumber = accountNumber;
            return this;
        }

        /// <summary>
        /// Sets iban's national check digit
        /// </summary>
        /// <param name="nationalCheckDigit">National check string</param>
        /// <returns>Builder instance</returns>
        public IbanBuilder NationalCheckDigit(string nationalCheckDigit)
        {
            this._nationalCheckDigit = nationalCheckDigit;
            return this;
        }

        /// <summary>
        /// Sets iban's account type
        /// </summary>
        /// <param name="accountType">Account type string</param>
        /// <returns>Builder instance</returns>
        public IbanBuilder AccountType(string accountType)
        {
            this._accountType = accountType;
            return this;
        }

        /// <summary>
        /// Sets iban's owner account type       
        /// </summary>
        /// <param name="ownerAccountType">Owner account type string</param>
        /// <returns>Builder instance</returns>
        public IbanBuilder OwnerAccountType(string ownerAccountType)
        {
            this._ownerAccountType = ownerAccountType;
            return this;
        }

        /// <summary>
        /// Sets iban's identification number
        /// </summary>
        /// <param name="identificationNumber">Identification number string</param>
        /// <returns>Builder instance</returns>
        public IbanBuilder IdentificationNumber(string identificationNumber)
        {
            this._identificationNumber = identificationNumber;
            return this;
        }

        /// <summary>
        /// Builds new IBAN instance. By default, Bank code, Account number prefix and Account number will be auto left-padded if they are shorter than 
        /// has to be (according to BBAN rules) and then the new generated IBAN will be validated .
        /// </summary>
        /// <returns>New IBAN instance</returns>
        public Iban Build()
        {
            return Build(true, true);
        }

        /// <summary>
        /// Builds new IBAN instance
        /// </summary>
        /// <param name="validate">True if the generated IBAN will be validated after generation</param>
        /// <returns>New IBAN instance</returns>
        /// <exception cref="IbanFormatException">If values doesn't meet requirements for valid IBAN.</exception>
        /// <exception cref="UnsupportedCountryException">If specified country code is not supported.</exception>
        public Iban Build(bool validate)
        {
            require(_countryCodeEntry, _bankCode, _accountNumber);

            string formattedIban = formatIban();
            string checkDigit = IbanUtils.CalculateCheckDigit(formattedIban);
            string ibanValue = IbanUtils.ReplaceCheckDigit(formattedIban, checkDigit);

            if (validate)
            {
                IbanUtils.Validate(ibanValue);
            }

            return Iban.CreateInstance(ibanValue);
        }

        /// <summary>
        /// Builds an IBAN instance
        /// </summary>
        /// <param name="validate">True if the generated IBAN will be validated after generation</param>
        /// <param name="autopadding">True if Bank code, Account number prefix and Account number can be auto left-padded with zeroes if they are shorter than specified in BBAN rules</param>
        /// <returns>New IBAN instance</returns>
        /// <exception cref="IbanFormatException">If values doesn't meet requirements for valid IBAN.</exception>
        /// <exception cref="UnsupportedCountryException">If specified country code is not supported.</exception>
        public Iban Build(bool validate, bool autopadding)
        {
            _enableAutopadding = autopadding;
            return Build(validate);
        }

        public Iban ParseFromNationalFormat(string nationalFormat, string countryCode)
        {
            // TODO
            return null;
        }

        /// <summary>
        /// Format IBAN string with deafult check digit
        /// </summary>
        /// <returns>IBAN string</returns>
        private string formatIban()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(_countryCodeEntry.Alpha2);
            sb.Append(Consts.IBAN_DEFAULT_CHECK_DIGIT);
            sb.Append(formatBban());

            return sb.ToString();
        }

        /// <summary>
        /// Format BBAN string
        /// </summary>
        /// <returns>BBAN string</returns>
        private string formatBban()
        {
            StringBuilder sb = new StringBuilder();
            BBanStructure bbanStructure = Bban.GetStructureForCountry(_countryCodeEntry);

            if (bbanStructure == null)
            {
                throw new UnsupportedCountryException("Country code is not supported", _countryCodeEntry.Alpha2);
            }

            foreach (BBanEntry entry in bbanStructure.Entries)
            {
                switch (entry.EntryType)
                {
                    case BBanEntryType.BANK_CODE:
                        reformatBankCode(entry.Length);
                        sb.Append(_bankCode);
                        break;
                    case BBanEntryType.BRANCH_CODE:
                        sb.Append(_branchCode);
                        break;
                    case BBanEntryType.ACCOUNT_NUMBER_PREFIX:
                        reformatAccountNumberPrefix(entry.Length);
                        sb.Append(_accountNumberPrefix);
                        break;
                    case BBanEntryType.ACCOUNT_NUMBER:
                        reformatAccountNumber(entry.Length);
                        sb.Append(_accountNumber);
                        break;
                    case BBanEntryType.NATIONAL_CHECK_DIGIT:
                        sb.Append(_nationalCheckDigit);
                        break;
                    case BBanEntryType.ACCOUNT_TYPE:
                        sb.Append(_accountType);
                        break;
                    case BBanEntryType.OWNER_ACCOUNT_NUMBER:
                        sb.Append(_ownerAccountType);
                        break;
                    case BBanEntryType.IDENTIFICATION_NUMBER:
                        sb.Append(_identificationNumber);
                        break;
                }
            }

            return sb.ToString();
        }


        /// <summary>
        /// Check for required fields.
        /// Every broken rule throws exception
        /// </summary>
        /// <param name="countryCodeEntry">Country code Entry</param>
        /// <param name="bankCode">Bank code</param>
        /// <param name="accountNumber">Account number</param>
        /// <exception cref="IbanFormatException">Thrown when one of the parameters is not supplied</exception>
        private void require(CountryCodeEntry countryCodeEntry, string bankCode, string accountNumber)
        {
            if (_countryCodeEntry == null)
            {
                throw new IbanFormatException("Country code is required, it cannot be null.", IbanFormatViolation.COUNTRY_CODE_NOT_NULL);
            }

            if (string.IsNullOrEmpty(bankCode))
            {
                throw new IbanFormatException("Bank code is required, it cannot be empty.", IbanFormatViolation.BANK_CODE_NOT_NULL);
            }

            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new IbanFormatException("Account number is required, it cannot be empty.", IbanFormatViolation.ACCOUNT_NUMBER_NOT_NULL);
            }
        }

        private void reformatBankCode(int requiredLength)
        {
            if (_bankCode.Length > requiredLength)
            {
                throw new IbanFormatException("Bank Code is too long for the specified country", IbanFormatViolation.BANK_CODE_TOO_LONG,
                                                $"Actual length: {_bankCode.Length}", $"Expected length: {requiredLength}");
            }
            else if (_bankCode.Length < requiredLength && !_enableAutopadding)
            {
                throw new IbanFormatException("Bank Code is too short for the specified country and the autopadding feature is disabled", IbanFormatViolation.BANK_CODE_TOO_SHORT,
                                                $"Actual length: {_bankCode.Length}", $"Expected length: {requiredLength}");
            }
            else
            {
                _bankCode = _bankCode.PadLeft(requiredLength, '0');
            }
        }

        private void reformatAccountNumberPrefix(int requiredLength)
        {
            if (_accountNumberPrefix.Length > requiredLength)
            {
                throw new IbanFormatException("Account number prefix is too long for the specified country", IbanFormatViolation.ACCOUNT_NUMBER_PREFIX_TOO_LONG,
                    $"Actual length: {_accountNumberPrefix.Length}", $"Expected length: {requiredLength}");
            }
            else if (_accountNumberPrefix.Length < requiredLength && !_enableAutopadding)
            {
                throw new IbanFormatException("Account number prefix is too short for the specified country and the autopadding feature is disabled", IbanFormatViolation.ACCOUNT_NUMBER_PREFIX_TOO_SHORT,
                                                $"Actual length: {_bankCode.Length}", $"Expected length: {requiredLength}");
            }
            else
            {
                _accountNumberPrefix = _accountNumberPrefix.PadLeft(requiredLength, '0');
            }
        }

        private void reformatAccountNumber(int requiredLength)
        {
            if (_accountNumber.Length > requiredLength)
            {
                throw new IbanFormatException("Account number is too long for the specified country", IbanFormatViolation.ACCOUNT_NUMBER_TOO_LONG,
                                                $"Actual length: {_accountNumber.Length}", $"Expected length: {requiredLength}");
            }
            else if (_accountNumber.Length < requiredLength && !_enableAutopadding)
            {
                throw new IbanFormatException("Account number is too short for the specified country and the autopadding feature is disabled", IbanFormatViolation.ACCOUNT_NUMBER_TOO_SHORT,
                                                $"Actual length: {_bankCode.Length}", $"Expected length: {requiredLength}");
            }
            else
            {
                _accountNumber = _accountNumber.PadLeft(requiredLength, '0');
            }
        }
    }
}
