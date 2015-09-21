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

namespace sinkien.IBAN4Net
{
    /// <summary>
    /// Basic Bank Account Number Entry types
    /// </summary>
    public enum BBanEntryType
    {
        BANK_CODE,
        BRANCH_CODE,
        ACCOUNT_NUMBER,
        NATIONAL_CHECK_DIGIT,
        ACCOUNT_TYPE,
        OWNER_ACCOUNT_NUMBER,
        IDENTIFICATION_NUMBER
    }

    /// <summary>
    /// Character type for BBAN rule entry
    /// </summary>
    public enum BBanEntryCharacterType
    {
        N = 'n',    // Digits (numbers 0-9)
        A = 'a',    // Upper case letters (A-Z)
        C = 'c'    // Alphanumeric (a-z, A-Z, 0-9)        
    }

    /// <summary>
    /// Types of BIC's format violation
    /// </summary>
    public enum BicFormatViolation
    {
        UNKNOWN,
        BIC_NOT_EMPTY_OR_NULL,        
        BIC_LENGTH_8_OR_11,
        BIC_ONLY_UPPER_CASE_LETTERS,
        BRANCH_CODE_ONLY_LETTERS_OR_DIGITS,
        LOCATION_CODE_ONLY_LETTERS_OR_DIGITS,
        BANK_CODE_ONLY_LETTERS,
        COUNTRY_CODE_ONLY_UPPER_CASE_LETTERS
    }

    /// <summary>
    /// Types of IBAN's format violation 
    /// </summary>
    public enum IbanFormatViolation
    {
        UNKNOWN,
        IBAN_NOT_EMPTY_OR_NULL,
        IBAN_VALID_CHARACTERS,
        CHECK_DIGIT_ONLY_DIGITS,
        CHECK_DIGIT_TWO_DIGITS,
        COUNTRY_CODE_TWO_LETTERS,
        COUNTRY_CODE_UPPER_CASE_LETTERS,
        COUNTRY_CODE_EXISTS,
        COUNTRY_CODE_NOT_NULL,
        BBAN_LENGTH,
        BBAN_ONLY_DIGITS,
        BBAN_ONLY_UPPER_CASE_LETTERS,
        BBAN_ONLY_DIGITS_OR_LETTERS,
        BANK_CODE_NOT_NULL,
        BANK_CODE_TOO_LONG,
        ACCOUNT_NUMBER_NOT_NULL,
        ACCOUNT_NUMBER_TOO_LONG
    }
}
