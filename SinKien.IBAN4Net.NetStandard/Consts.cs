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


namespace SinKien.IBAN4Net
{
    /// <summary>
    /// Const values for BIC and IBAN Utility methods
    /// </summary>
    public static class Consts
    {

        //-- BIC
        public const int BIC_BIC8_LENGTH = 8;
        public const int BIC_BIC11_LENGTH = 11;
        public const int BIC_BANK_CODE_INDEX = 0;
        public const int BIC_BANK_CODE_LENGTH = 4;
        public const int BIC_COUNTRY_CODE_INDEX = BIC_BANK_CODE_INDEX + BIC_BANK_CODE_LENGTH;
        public const int BIC_COUNTRY_CODE_LENGTH = 2;
        public const int BIC_LOCATION_CODE_INDEX = BIC_COUNTRY_CODE_INDEX + BIC_COUNTRY_CODE_LENGTH;
        public const int BIC_LOCATION_CODE_LENGTH = 2;
        public const int BIC_BRANCH_CODE_INDEX = BIC_LOCATION_CODE_INDEX + BIC_LOCATION_CODE_LENGTH;
        public const int BIC_BRANCH_CODE_LENGTH = 3;

        //-- IBAN
        public const string IBAN_DEFAULT_CHECK_DIGIT = "00";
        public const int IBAN_MOD = 97;
        public const long IBAN_MAX = 999999999;

        public const int IBAN_COUNTRY_CODE_INDEX = 0;
        public const int IBAN_COUNTRY_CODE_LENGTH = 2;
        public const int IBAN_CHECK_DIGIT_INDEX = IBAN_COUNTRY_CODE_LENGTH;
        public const int IBAN_CHECK_DIGIT_LENGTH = 2;
        public const int IBAN_BBAN_INDEX = IBAN_CHECK_DIGIT_INDEX + IBAN_CHECK_DIGIT_LENGTH;
    }
}
