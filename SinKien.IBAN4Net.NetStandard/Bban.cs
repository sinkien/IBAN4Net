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

using System;
using System.Collections.Generic;
using System.Linq;

namespace SinKien.IBAN4Net
{
    /// <summary>
    /// Basic Bank Account Number.
    /// Class that holds a list of BBAN structures (sets of rules for BBAN string construction) for supported countries.
    /// </summary>
    public class Bban
    {                
        private SortedDictionary<string, BBanStructure> _bbanStructures = null;

        public Bban()
        {
            loadStructures();
        }

        /// <summary>
        /// Loads BBANs structures definitions
        /// </summary>
        private void loadStructures()
        {
            _bbanStructures = new SortedDictionary<string, BBanStructure>();


            _bbanStructures.Add("AD", new BBanStructure(BBanEntry.BankCode(4, "n"),
                                                            BBanEntry.BranchCode(4, "n"),
                                                            BBanEntry.AccountNumber(12, "c")));

            _bbanStructures.Add("AE", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.AccountNumber(16, "c")));

            _bbanStructures.Add("AL", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.BranchCode(4, "n"),
                                                            BBanEntry.NationalCheckDigit(1, "n"),
                                                            BBanEntry.AccountNumber(16, "c")));

            _bbanStructures.Add("AT", new BBanStructure(BBanEntry.BankCode(5, "n"),
                                                            BBanEntry.AccountNumber(11, "n")));

            _bbanStructures.Add("AZ", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.AccountNumber(20, "c")));

            _bbanStructures.Add("BH", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.AccountNumber(14, "c")));

            _bbanStructures.Add("BE", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.AccountNumber(7, "n"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));

            _bbanStructures.Add("BA", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.BranchCode(3, "n"),
                                                            BBanEntry.AccountNumber(8, "n"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));

            _bbanStructures.Add("BR", new BBanStructure(BBanEntry.BankCode(8, "n"),
                                                            BBanEntry.BranchCode(5, "n"),
                                                            BBanEntry.AccountNumber(10, "n"),
                                                            BBanEntry.AccountType(1, "a"),
                                                            BBanEntry.OwnerAccountNumber(1, "c")));

            _bbanStructures.Add("BG", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.BranchCode(4, "n"),
                                                            BBanEntry.AccountType(2, "n"),
                                                            BBanEntry.AccountNumber(8, "c")));

            // since 2017
            _bbanStructures.Add("BY", new BBanStructure(BBanEntry.BankCode(4, "c"),
                                                            BBanEntry.BalanceAccountNumber(4, "n"),
                                                            BBanEntry.AccountNumber(16, "n")));

            _bbanStructures.Add("CR", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.AccountNumber(14, "n")));

            _bbanStructures.Add("CY", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.BranchCode(5, "n"),
                                                            BBanEntry.AccountNumber(16, "c")));

            _bbanStructures.Add("CZ", new BBanStructure(BBanEntry.BankCode(4, "n"),
                                                            BBanEntry.AccountNumberPrefix(6, "n"),
                                                            BBanEntry.AccountNumber(10, "n")));

            _bbanStructures.Add("DE", new BBanStructure(BBanEntry.BankCode(8, "n"),
                                                            BBanEntry.AccountNumber(10, "n")));

            _bbanStructures.Add("DK", new BBanStructure(BBanEntry.BankCode(4, "n"),
                                                            BBanEntry.AccountNumber(10, "n")));

            _bbanStructures.Add("DO", new BBanStructure(BBanEntry.BankCode(4, "c"),
                                                            BBanEntry.AccountNumber(20, "n")));

            _bbanStructures.Add("EE", new BBanStructure(BBanEntry.BankCode(2, "n"),
                                                            BBanEntry.BranchCode(2, "n"),
                                                            BBanEntry.AccountNumber(11, "n"),
                                                            BBanEntry.NationalCheckDigit(1, "n")));

            _bbanStructures.Add("ES", new BBanStructure(BBanEntry.BankCode(4, "n"),
                                                            BBanEntry.BranchCode(4, "n"),
                                                            BBanEntry.NationalCheckDigit(2, "n"),
                                                            BBanEntry.AccountNumber(10, "n")));

            _bbanStructures.Add("FO", new BBanStructure(BBanEntry.BankCode(4, "n"),
                                                            BBanEntry.AccountNumber(9, "n"),
                                                            BBanEntry.NationalCheckDigit(1, "n")));

            _bbanStructures.Add("FI", new BBanStructure(BBanEntry.BankCode(6, "n"),
                                                            BBanEntry.AccountNumber(7, "n"),
                                                            BBanEntry.NationalCheckDigit(1, "n")));

            _bbanStructures.Add("FR", new BBanStructure(BBanEntry.BankCode(5, "n"),
                                                            BBanEntry.BranchCode(5, "n"),
                                                            BBanEntry.AccountNumber(11, "c"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));

            _bbanStructures.Add("GB", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.BranchCode(6, "n"),
                                                            BBanEntry.AccountNumber(8, "n")));

            _bbanStructures.Add("GE", new BBanStructure(BBanEntry.BankCode(2, "a"),
                                                            BBanEntry.AccountNumber(16, "n")));

            _bbanStructures.Add("GI", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.AccountNumber(15, "c")));

            _bbanStructures.Add("GL", new BBanStructure(BBanEntry.BankCode(4, "n"),
                                                            BBanEntry.AccountNumber(10, "n")));

            _bbanStructures.Add("GR", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.BranchCode(4, "n"),
                                                            BBanEntry.AccountNumber(16, "c")));

            _bbanStructures.Add("GT", new BBanStructure(BBanEntry.BankCode(4, "c"),
                                                            BBanEntry.AccountNumber(20, "c")));

            _bbanStructures.Add("HR", new BBanStructure(BBanEntry.BankCode(7, "n"),
                                                            BBanEntry.AccountNumber(10, "n")));

            _bbanStructures.Add("HU", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.BranchCode(4, "n"),
                                                            BBanEntry.AccountNumber(16, "n"),
                                                            BBanEntry.NationalCheckDigit(1, "n")));

            _bbanStructures.Add("CH", new BBanStructure(BBanEntry.BankCode(5, "n"),
                                                            BBanEntry.AccountNumber(12, "c")));

            _bbanStructures.Add("IS", new BBanStructure(BBanEntry.BankCode(4, "n"),
                                                            BBanEntry.BranchCode(2, "n"),
                                                            BBanEntry.AccountNumber(6, "n"),
                                                            BBanEntry.IdentificationNumber(10, "n")));

            _bbanStructures.Add("IE", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.BranchCode(6, "n"),
                                                            BBanEntry.AccountNumber(8, "n")));

            _bbanStructures.Add("IL", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.BranchCode(3, "n"),
                                                            BBanEntry.AccountNumber(13, "n")));

            _bbanStructures.Add("IR", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.AccountNumber(19, "n")));

            _bbanStructures.Add("IT", new BBanStructure(BBanEntry.NationalCheckDigit(1, "a"),
                                                            BBanEntry.BankCode(5, "n"),
                                                            BBanEntry.BranchCode(5, "n"),
                                                            BBanEntry.AccountNumber(12, "c")));

            _bbanStructures.Add("JO", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.BranchCode(4, "n"),
                                                            BBanEntry.AccountNumber(18, "c")));

            _bbanStructures.Add("KZ", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.AccountNumber(13, "c")));

            _bbanStructures.Add("KW", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.AccountNumber(22, "c")));

            _bbanStructures.Add("LV", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.AccountNumber(13, "c")));

            _bbanStructures.Add("LB", new BBanStructure(BBanEntry.BankCode(4, "n"),
                                                            BBanEntry.AccountNumber(20, "c")));

            _bbanStructures.Add("LI", new BBanStructure(BBanEntry.BankCode(5, "n"),
                                                            BBanEntry.AccountNumber(12, "c")));

            _bbanStructures.Add("LT", new BBanStructure(BBanEntry.BankCode(5, "n"),
                                                            BBanEntry.AccountNumber(11, "n")));

            _bbanStructures.Add("LU", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.AccountNumber(13, "c")));

            // MG is same as FR (according to Nordea's list)
            _bbanStructures.Add("MG", new BBanStructure(BBanEntry.BankCode(5, "n"),
                                                            BBanEntry.BranchCode(5, "n"),
                                                            BBanEntry.AccountNumber(11, "c"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));

            _bbanStructures.Add("MK", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.AccountNumber(10, "c"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));

            _bbanStructures.Add("MT", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.BranchCode(5, "n"),
                                                            BBanEntry.AccountNumber(18, "c")));

            _bbanStructures.Add("MR", new BBanStructure(BBanEntry.BankCode(5, "n"),
                                                            BBanEntry.BranchCode(5, "n"),
                                                            BBanEntry.AccountNumber(11, "n"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));

            _bbanStructures.Add("MU", new BBanStructure(BBanEntry.BankCode(6, "c"),
                                                            BBanEntry.BranchCode(2, "n"),
                                                            BBanEntry.AccountNumber(18, "c")));

            _bbanStructures.Add("MD", new BBanStructure(BBanEntry.BankCode(2, "c"),
                                                        BBanEntry.AccountNumber(18, "c")));

            _bbanStructures.Add("MC", new BBanStructure(BBanEntry.BankCode(5, "n"),
                                                            BBanEntry.BranchCode(5, "n"),
                                                            BBanEntry.AccountNumber(11, "c"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));

            _bbanStructures.Add("ME", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.AccountNumber(13, "n"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));

            _bbanStructures.Add("NL", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.AccountNumber(10, "n")));

            _bbanStructures.Add("NO", new BBanStructure(BBanEntry.BankCode(4, "n"),
                                                            BBanEntry.AccountNumber(6, "n"),
                                                            BBanEntry.NationalCheckDigit(1, "n")));

            _bbanStructures.Add("PK", new BBanStructure(BBanEntry.BankCode(4, "c"),
                                                            BBanEntry.AccountNumber(16, "n")));

            _bbanStructures.Add("PS", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.AccountNumber(21, "c")));

            _bbanStructures.Add("PL", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.BranchCode(4, "n"),
                                                            BBanEntry.NationalCheckDigit(1, "n"),
                                                            BBanEntry.AccountNumber(16, "n")));

            _bbanStructures.Add("PT", new BBanStructure(BBanEntry.BankCode(4, "n"),
                                                            BBanEntry.BranchCode(4, "n"),
                                                            BBanEntry.AccountNumber(11, "n"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));

            _bbanStructures.Add("QA", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.AccountNumber(21, "c")));

            _bbanStructures.Add("RS", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.AccountNumber(13, "n"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));
            _bbanStructures.Add("RO", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.AccountNumber(16, "c")));

            _bbanStructures.Add("TL", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.AccountNumber(14, "n"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));

            _bbanStructures.Add("TN", new BBanStructure(BBanEntry.BankCode(2, "n"),
                                                            BBanEntry.BranchCode(3, "n"),
                                                            BBanEntry.AccountNumber(15, "c")));

            _bbanStructures.Add("TR", new BBanStructure(BBanEntry.BankCode(5, "n"),
                                                            BBanEntry.NationalCheckDigit(1, "c"),
                                                            BBanEntry.AccountNumber(16, "c")));

            _bbanStructures.Add("SM", new BBanStructure(BBanEntry.NationalCheckDigit(1, "a"),
                                                            BBanEntry.BankCode(5, "n"),
                                                            BBanEntry.BranchCode(5, "n"),
                                                            BBanEntry.AccountNumber(12, "c")));

            _bbanStructures.Add("SA", new BBanStructure(BBanEntry.BankCode(2, "n"),
                                                            BBanEntry.AccountNumber(18, "c")));

            _bbanStructures.Add("SK", new BBanStructure(BBanEntry.BankCode(4, "n"),
                                                            BBanEntry.AccountNumberPrefix(6, "n"),
                                                            BBanEntry.AccountNumber(10, "n")));

            _bbanStructures.Add("SI", new BBanStructure(BBanEntry.BankCode(2, "n"),
                                                            BBanEntry.BranchCode(3, "n"),
                                                            BBanEntry.AccountNumber(8, "n"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));

            _bbanStructures.Add("SE", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.AccountNumber(17, "n")));

            _bbanStructures.Add("UA", new BBanStructure(BBanEntry.BankCode(6, "n"),
                                                            BBanEntry.AccountNumber(19, "n")));

            _bbanStructures.Add("VG", new BBanStructure(BBanEntry.BankCode(4, "c"),
                                                            BBanEntry.AccountNumber(16, "n")));

            _bbanStructures.Add("XK", new BBanStructure(BBanEntry.BankCode(2, "n"),
                                                            BBanEntry.BranchCode(2, "n"),
                                                            BBanEntry.AccountNumber(10, "n"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));
        }

        /// <summary>
        /// Search for BBAN structure of specified country
        /// </summary>
        /// <param name="countryCode">Country code object</param>
        /// <returns>BBAN structure of defined country, or null if given country code is unsupported</returns>
        public static BBanStructure GetStructureForCountry(CountryCodeEntry countryCode)
        {
            Bban bban = new Bban();
            BBanStructure result = null;

            if (countryCode != null)
            {
                if (bban._bbanStructures != null)
                {
                    if (bban._bbanStructures.ContainsKey(countryCode.Alpha2))
                    {
                        result = bban._bbanStructures[countryCode.Alpha2];
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Search for BBAN structure of specified country
        /// </summary>
        /// <param name="alpha2Code">Alpha2 Country code</param>
        /// <returns>BBAN structure of defined country, or null if given country code is unsupported</returns>
        public static BBanStructure GetStructureForCountry(string alpha2Code)
        {
            Bban bban = new Bban();
            BBanStructure result = null;

            if (!string.IsNullOrEmpty(alpha2Code) && alpha2Code.Length == 2)
            {
                if (bban._bbanStructures != null)
                {
                    if (bban._bbanStructures.ContainsKey(alpha2Code.ToUpper()))
                    {
                        result = bban._bbanStructures[alpha2Code];
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Checks if specified BBAN entry is supported for given country code.
        /// </summary>
        /// <param name="alpha2Code">Alpha2 Country code</param>
        /// <param name="entryType">BBAN entry type</param>
        /// <returns>True if given country contains rule for specified entry</returns>
        public static bool IsBbanEntrySupported(string alpha2Code, BBanEntryType entryType)
        {
            Bban bban = new Bban();
            BBanStructure structure = GetStructureForCountry(alpha2Code);
            bool result = false;

            if (structure != null)
            {
                result = structure.Entries.Any(x => x.EntryType == entryType);
            }

            return result;
        }

    }

    /// <summary>
    /// BBAN structure representation
    /// </summary>
    public class BBanStructure
    {
        /// <summary>
        /// List of BBAN's rules
        /// </summary>
        public List<BBanEntry> Entries { get; private set; }

        public BBanStructure(params BBanEntry[] entries)
        {
            Entries = new List<BBanEntry>(entries);
        }

        /// <summary>
        /// Length of BBAN string
        /// </summary>
        /// <returns>A number representating a length of BBAN string</returns>
        public int GetBBanLength()
        {
            int length = 0;
            foreach (BBanEntry entry in Entries)
            {
                length += entry.Length;
            }

            return length;
        }
    }

    /// <summary>
    /// Representation of BBan structure's entry (the rule)
    /// </summary>
    public class BBanEntry
    {
        public BBanEntryType EntryType { get; private set; }
        public BBanEntryCharacterType CharacterType { get; private set; }
        public int Length { get; private set; } = 0;

        private BBanEntry(BBanEntryType entryType, BBanEntryCharacterType characterType, int length)
        {
            EntryType = entryType;
            CharacterType = characterType;
            Length = length;
        }

        public static BBanEntry BankCode(int length, string characterType) => new BBanEntry(BBanEntryType.BANK_CODE, getCharacterType(characterType), length);

        public static BBanEntry BranchCode(int length, string characterType) => new BBanEntry(BBanEntryType.BRANCH_CODE, getCharacterType(characterType), length);

        public static BBanEntry AccountNumberPrefix(int length, string characterType) => new BBanEntry(BBanEntryType.ACCOUNT_NUMBER_PREFIX, getCharacterType(characterType), length);

        public static BBanEntry AccountNumber(int length, string characterType) => new BBanEntry(BBanEntryType.ACCOUNT_NUMBER, getCharacterType(characterType), length);

        public static BBanEntry NationalCheckDigit(int length, string characterType) => new BBanEntry(BBanEntryType.NATIONAL_CHECK_DIGIT, getCharacterType(characterType), length);

        public static BBanEntry AccountType(int length, string characterType) => new BBanEntry(BBanEntryType.ACCOUNT_TYPE, getCharacterType(characterType), length);

        public static BBanEntry OwnerAccountNumber(int length, string characterType) => new BBanEntry(BBanEntryType.OWNER_ACCOUNT_NUMBER, getCharacterType(characterType), length);

        public static BBanEntry IdentificationNumber(int length, string characterType) => new BBanEntry(BBanEntryType.IDENTIFICATION_NUMBER, getCharacterType(characterType), length);

        public static BBanEntry BalanceAccountNumber(int length, string characterType) => new BBanEntry(BBanEntryType.BALANCE_ACCOUNT_NUMBER, getCharacterType(characterType), length);

        private static BBanEntryCharacterType getCharacterType(string character)
        {
            if (string.IsNullOrEmpty(character) || character.Length != 1)
            {
                throw new ArgumentException($"Value of 'character' parameter is invalid ! [{character}] ");
            }

            return (BBanEntryCharacterType)Enum.Parse(typeof(BBanEntryCharacterType), character, true);

        }
    }
}
