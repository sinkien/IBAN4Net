/*
 * IBAN4Net
 * Copyright 2020 Vaclav Beca [sinkien]
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
        private static SortedDictionary<string, BBanStructure> _bbanStructures = null;

        static Bban()
        {
            loadStructures();
        }

        /// <summary>
        /// Loads BBANs structures definitions
        /// </summary>
        private static void loadStructures()
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

            _bbanStructures.Add("BA", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.BranchCode(3, "n"),
                                                            BBanEntry.AccountNumber(8, "n"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));
            
            _bbanStructures.Add("BE", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.AccountNumber(7, "n"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));
            
            _bbanStructures.Add("BG", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.BranchCode(4, "n"),
                                                            BBanEntry.AccountType(2, "n"),
                                                            BBanEntry.AccountNumber(8, "c")));

            _bbanStructures.Add("BH", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.AccountNumber(14, "c")));

            _bbanStructures.Add("BR", new BBanStructure(BBanEntry.BankCode(8, "n"),
                                                            BBanEntry.BranchCode(5, "n"),
                                                            BBanEntry.AccountNumber(10, "n"),
                                                            BBanEntry.AccountType(1, "a"),
                                                            BBanEntry.OwnerAccountNumber(1, "c")));
                        
            _bbanStructures.Add("CH", new BBanStructure(BBanEntry.BankCode(5, "n"),
                                                           BBanEntry.AccountNumber(12, "c")));

            _bbanStructures.Add("CR", new BBanStructure(BBanEntry.BankCode(4, "n"),
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
            //Bank code is 3 and account number 10 + 1 check digit (11)
            _bbanStructures.Add("FI", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.BranchCode(3, "n"),
                                                            BBanEntry.AccountNumber(7, "n"),
                                                            BBanEntry.NationalCheckDigit(1, "n")));

            _bbanStructures.Add("FO", new BBanStructure(BBanEntry.BankCode(4, "n"),
                                                           BBanEntry.AccountNumber(9, "n"),
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
            
            _bbanStructures.Add("IE", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.BranchCode(6, "n"),
                                                            BBanEntry.AccountNumber(8, "n")));

            _bbanStructures.Add("IL", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.BranchCode(3, "n"),
                                                            BBanEntry.AccountNumber(13, "n")));

            //account number should be 18 and no brach code
            _bbanStructures.Add("IS", new BBanStructure(BBanEntry.BankCode(4, "n"),
                                                            BBanEntry.BranchCode(2, "n"),
                                                            BBanEntry.AccountNumber(6, "n"),
                                                            BBanEntry.IdentificationNumber(10, "n")));
           
            _bbanStructures.Add("IT", new BBanStructure(BBanEntry.NationalCheckDigit(1, "a"),
                                                            BBanEntry.BankCode(5, "n"),
                                                            BBanEntry.BranchCode(5, "n"),
                                                            BBanEntry.AccountNumber(12, "c")));

            _bbanStructures.Add("KW", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.AccountNumber(22, "c")));

            _bbanStructures.Add("KZ", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.AccountNumber(13, "c")));
                        
            _bbanStructures.Add("LB", new BBanStructure(BBanEntry.BankCode(4, "n"),
                                                            BBanEntry.AccountNumber(20, "c")));

            _bbanStructures.Add("LI", new BBanStructure(BBanEntry.BankCode(5, "n"),
                                                            BBanEntry.AccountNumber(12, "c")));

            _bbanStructures.Add("LT", new BBanStructure(BBanEntry.BankCode(5, "n"),
                                                            BBanEntry.AccountNumber(11, "n")));

            _bbanStructures.Add("LU", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.AccountNumber(13, "c")));

            _bbanStructures.Add("LV", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.AccountNumber(13, "c")));

            _bbanStructures.Add("MC", new BBanStructure(BBanEntry.BankCode(5, "n"),
                                                           BBanEntry.BranchCode(5, "n"),
                                                           BBanEntry.AccountNumber(11, "c"),
                                                           BBanEntry.NationalCheckDigit(2, "n")));

            _bbanStructures.Add("MD", new BBanStructure(BBanEntry.BankCode(2, "c"),
                                                        BBanEntry.AccountNumber(18, "c")));

            _bbanStructures.Add("ME", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.AccountNumber(13, "n"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));
                       

            _bbanStructures.Add("MK", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.AccountNumber(10, "c"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));
            
            _bbanStructures.Add("MR", new BBanStructure(BBanEntry.BankCode(5, "n"),
                                                            BBanEntry.BranchCode(5, "n"),
                                                            BBanEntry.AccountNumber(11, "n"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));

            _bbanStructures.Add("MT", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.BranchCode(5, "n"),
                                                            BBanEntry.AccountNumber(18, "c")));

            
            _bbanStructures.Add("MU", new BBanStructure(BBanEntry.BankCode(6, "c"),
                                                            BBanEntry.BranchCode(2, "n"),
                                                            BBanEntry.AccountNumber(18, "c")));           

            _bbanStructures.Add("NL", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.AccountNumber(10, "n")));

            _bbanStructures.Add("NO", new BBanStructure(BBanEntry.BankCode(4, "n"),
                                                            BBanEntry.AccountNumber(6, "n"),
                                                            BBanEntry.NationalCheckDigit(1, "n")));

            _bbanStructures.Add("PK", new BBanStructure(BBanEntry.BankCode(4, "c"),
                                                            BBanEntry.AccountNumber(16, "n")));
                        
            _bbanStructures.Add("PL", new BBanStructure(BBanEntry.BankCode(7, "n"),
                                                            BBanEntry.BranchCode(0, "n"),
                                                            BBanEntry.NationalCheckDigit(1, "n"),
                                                            BBanEntry.AccountNumber(16, "n")));

            _bbanStructures.Add("PS", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.AccountNumber(21, "c")));

            _bbanStructures.Add("PT", new BBanStructure(BBanEntry.BankCode(4, "n"),
                                                            BBanEntry.BranchCode(4, "n"),
                                                            BBanEntry.AccountNumber(11, "n"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));
                        
            _bbanStructures.Add("RO", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.AccountNumber(16, "c")));

            _bbanStructures.Add("RS", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.AccountNumber(13, "n"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));

            _bbanStructures.Add("SA", new BBanStructure(BBanEntry.BankCode(2, "n"),
                                                            BBanEntry.AccountNumber(18, "c")));

            _bbanStructures.Add("SE", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                           BBanEntry.AccountNumber(17, "n")));            

            _bbanStructures.Add("SI", new BBanStructure(BBanEntry.BankCode(5, "n"),
                                                            BBanEntry.BranchCode(0, "n"),
                                                            BBanEntry.AccountNumber(8, "n"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));

            _bbanStructures.Add("SK", new BBanStructure(BBanEntry.BankCode(4, "n"),
                                                            BBanEntry.AccountNumberPrefix(6, "n"),
                                                            BBanEntry.AccountNumber(10, "n")));

            _bbanStructures.Add("SM", new BBanStructure(BBanEntry.NationalCheckDigit(1, "a"),
                                                          BBanEntry.BankCode(5, "n"),
                                                          BBanEntry.BranchCode(5, "n"),
                                                          BBanEntry.AccountNumber(12, "c")));

            _bbanStructures.Add("TN", new BBanStructure(BBanEntry.BankCode(2, "n"),
                                                            BBanEntry.BranchCode(3, "n"),
                                                            BBanEntry.AccountNumber(15, "c")));

            _bbanStructures.Add("TR", new BBanStructure(BBanEntry.BankCode(5, "n"),
                                                            BBanEntry.NationalCheckDigit(1, "c"),
                                                            BBanEntry.AccountNumber(16, "c")));
           
            
            _bbanStructures.Add("VG", new BBanStructure(BBanEntry.BankCode(4, "c"),
                                                            BBanEntry.AccountNumber(16, "n")));
            
            //no branch code is defined
            _bbanStructures.Add("JO", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.BranchCode(4, "n"),
                                                            BBanEntry.AccountNumber(18, "c")));

            _bbanStructures.Add("QA", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                            BBanEntry.AccountNumber(21, "c")));
            //defined as account number 16 but starting at pos. 5 after checksum, i.e. 
            //swift account number definition includes the bank code (2) and branch code(2)
            _bbanStructures.Add("XK", new BBanStructure(BBanEntry.BankCode(2, "n"),
                                                            BBanEntry.BranchCode(2, "n"),
                                                            BBanEntry.AccountNumber(10, "n"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));
            
            _bbanStructures.Add("TL", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                           BBanEntry.AccountNumber(14, "n"),
                                                           BBanEntry.NationalCheckDigit(2, "n")));

            //defined as account number 24 (was 9)
            _bbanStructures.Add("LC", new BBanStructure(BBanEntry.BankCode(4, "a"),                                                           
                                                           BBanEntry.AccountNumber(24, "c")));


            _bbanStructures.Add("UA", new BBanStructure(BBanEntry.BankCode(6, "n"),
                                                            BBanEntry.AccountNumber(19, "n")));

            
            _bbanStructures.Add("ST", new BBanStructure(BBanEntry.BankCode(4, "n"),
                                                            BBanEntry.BranchCode(4, "n"),
                                                            BBanEntry.AccountNumber(13, "n")));
            
            _bbanStructures.Add("SC", new BBanStructure(BBanEntry.BankCode(6, "c"),
                                                           BBanEntry.BranchCode(2, "n"),
                                                           BBanEntry.AccountNumber(19, "c")));

            _bbanStructures.Add("IQ", new BBanStructure(BBanEntry.BankCode(4, "a"),
                                                           BBanEntry.BranchCode(3, "n"),
                                                           BBanEntry.AccountNumber(12, "n")));
            //defined as account number 20 was (9)
            _bbanStructures.Add("SV", new BBanStructure(BBanEntry.BankCode(4, "a"),                                                           
                                                           BBanEntry.AccountNumber(20, "n")));


            // since 2017
            _bbanStructures.Add("BY", new BBanStructure(BBanEntry.BankCode(4, "c"),
                                                            BBanEntry.BalanceAccountNumber(4, "n"),
                                                            BBanEntry.AccountNumber(16, "c")));
            
            _bbanStructures.Add("VA", new BBanStructure(BBanEntry.BankCode(3, "n"),                                                           
                                                           BBanEntry.AccountNumber(15, "n")));

            // MG is same as FR (according to Nordea's list)
            _bbanStructures.Add("MG", new BBanStructure(BBanEntry.BankCode(5, "n"),
                                                            BBanEntry.BranchCode(5, "n"),
                                                            BBanEntry.AccountNumber(11, "c"),
                                                            BBanEntry.NationalCheckDigit(2, "n")));

            /* NOT FOUND IN SWIFT */
            _bbanStructures.Add("IR", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.AccountNumber(19, "n")));

            _bbanStructures.Add("EG", new BBanStructure(BBanEntry.BankCode(4, "n"),
                                                            BBanEntry.BranchCode(4, "n"),
                                                            BBanEntry.AccountNumber(17, "n")));

            _bbanStructures.Add("AX", new BBanStructure(BBanEntry.BankCode(3, "n"),
                                                            BBanEntry.BranchCode(3, "n"),
                                                            BBanEntry.AccountNumber(7, "n"),
                                                            BBanEntry.NationalCheckDigit(1, "n")));
            
            //Algeria
            _bbanStructures.Add("DZ", new BBanStructure(BBanEntry.AccountNumber(20, "n")));
            
            //Angola
            _bbanStructures.Add("AO", new BBanStructure(BBanEntry.AccountNumber(21, "n")));
            
            //Benin
            _bbanStructures.Add("BJ", new BBanStructure(BBanEntry.BankCode(1, "a"),
                BBanEntry.AccountNumber(23, "n")));
            
            //Burkina Faso
            _bbanStructures.Add("BF", new BBanStructure(BBanEntry.AccountNumber(23, "n")));
            
            //Burundi
            _bbanStructures.Add("BI", new BBanStructure(BBanEntry.AccountNumber(12, "n")));
            
            //Cameroon
            _bbanStructures.Add("CM", new BBanStructure(BBanEntry.AccountNumber(23, "n")));
            
            //Cape Verde
            _bbanStructures.Add("CV", new BBanStructure(BBanEntry.AccountNumber(21, "n")));

            //Mali
            _bbanStructures.Add("ML", new BBanStructure(BBanEntry.BankCode(1, "a"),
                BBanEntry.AccountNumber(23, "n")));

            //Mozambique
            _bbanStructures.Add("MZ", new BBanStructure(BBanEntry.AccountNumber(21, "n")));

            //Senegal
            _bbanStructures.Add("SN", new BBanStructure(BBanEntry.BankCode(1, "a"),
                BBanEntry.AccountNumber(23, "n")));

            //Central African Republic
            _bbanStructures.Add("CF", new BBanStructure(
                BBanEntry.BankCode(5, "n"),
                BBanEntry.BranchCode(5, "n"),
                BBanEntry.AccountNumber(13, "n")
            ));

            //Chad
            _bbanStructures.Add("TD", new BBanStructure(
                BBanEntry.BankCode(5, "n"),
                BBanEntry.BranchCode(5, "n"),
                BBanEntry.AccountNumber(13, "n")
            ));

            //Comoros
            _bbanStructures.Add("KM", new BBanStructure(
                BBanEntry.BankCode(5, "n"),
                BBanEntry.BranchCode(5, "n"),
                BBanEntry.AccountNumber(13, "n")
            ));

            //Congo
            _bbanStructures.Add("CG", new BBanStructure(
                BBanEntry.BankCode(5, "n"),
                BBanEntry.BranchCode(5, "n"),
                BBanEntry.AccountNumber(13, "n")
            ));

            //Cote Divoire
            _bbanStructures.Add("CI", new BBanStructure(
                BBanEntry.NationalCheckDigit(1, "a"),
                BBanEntry.BankCode(5, "n"),
                BBanEntry.BranchCode(5, "n"),
                BBanEntry.AccountNumber(13, "n")
            ));

            //Djibouti
            _bbanStructures.Add("DJ", new BBanStructure(
                BBanEntry.BankCode(5, "n"),
                BBanEntry.BranchCode(5, "n"),
                BBanEntry.AccountNumber(13, "n")
            ));

            //Equatorial Guinea
            _bbanStructures.Add("GQ", new BBanStructure(
                BBanEntry.BankCode(5, "n"),
                BBanEntry.BranchCode(5, "n"),
                BBanEntry.AccountNumber(13, "n")
            ));

            //Gabon
            _bbanStructures.Add("GA", new BBanStructure(
                BBanEntry.BankCode(5, "n"),
                BBanEntry.BranchCode(5, "n"),
                BBanEntry.AccountNumber(13, "n")
            ));

            //Guinea-Bissau
            _bbanStructures.Add("GW", new BBanStructure(
                BBanEntry.BankCode(4, "c"), //2chars and 2numbers actually
                BBanEntry.BranchCode(4, "n"),
                BBanEntry.AccountNumber(13, "n")
            ));

            //Honduras
            _bbanStructures.Add("HN", new BBanStructure(
                BBanEntry.BankCode(4, "c"),
                BBanEntry.AccountNumber(20, "n")
            ));
        }

        /// <summary>
        /// Search for BBAN structure of specified country
        /// </summary>
        /// <param name="countryCode">Country code object</param>
        /// <returns>BBAN structure of defined country, or null if given country code is unsupported</returns>
        public static BBanStructure GetStructureForCountry(CountryCodeEntry countryCode)
        {           
            BBanStructure result = null;

            if (countryCode != null)
            {
                
                if (_bbanStructures.ContainsKey(countryCode.Alpha2))
                {
                    result = _bbanStructures[countryCode.Alpha2].Clone();
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
            BBanStructure result = null;

            if (alpha2Code?.Length == 2)
            {                
                if (_bbanStructures.ContainsKey(alpha2Code.ToUpper()))
                {
                    result = _bbanStructures[alpha2Code].Clone();
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

        public BBanStructure Clone()
        {
            BBanStructure result = new BBanStructure();
            foreach (var item in Entries)
            {
                result.Entries.Add(new BBanEntry(item.EntryType, item.CharacterType, item.Length));

            }
            return result;
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

        internal BBanEntry(BBanEntryType entryType, BBanEntryCharacterType characterType, int length)
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
