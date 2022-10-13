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
			_bbanStructures = new SortedDictionary<string, BBanStructure>
			{				
				// A
				{ "AD", new BBanStructure(BBanEntry.BankCode(4, "n"), BBanEntry.BranchCode(4, "n"), BBanEntry.AccountNumber(12, "c")) },
				{ "AE", new BBanStructure(BBanEntry.BankCode(3, "n"), BBanEntry.AccountNumber(16, "c")) },
				{ "AL", new BBanStructure(BBanEntry.BankCode(3, "n"), BBanEntry.BranchCode(4, "n"), BBanEntry.NationalCheckDigit(1, "n"), BBanEntry.AccountNumber(16, "c")) },
				{ "AT", new BBanStructure(BBanEntry.BankCode(5, "n"), BBanEntry.AccountNumber(11, "n")) },
				{ "AX", new BBanStructure(BBanEntry.BankCode(3, "n"), BBanEntry.BranchCode(3, "n"), BBanEntry.AccountNumber(7, "n"), BBanEntry.NationalCheckDigit(1, "n")) },
				{ "AZ", new BBanStructure(BBanEntry.BankCode(4, "a"), BBanEntry.AccountNumber(20, "c")) },
				// B
				{ "BA", new BBanStructure(BBanEntry.BankCode(3, "n"), BBanEntry.BranchCode(3, "n"), BBanEntry.AccountNumber(8, "n"), BBanEntry.NationalCheckDigit(2, "n")) },
				{ "BE", new BBanStructure(BBanEntry.BankCode(3, "n"), BBanEntry.AccountNumber(7, "n"), BBanEntry.NationalCheckDigit(2, "n")) },
				{ "BG", new BBanStructure(BBanEntry.BankCode(4, "a"), BBanEntry.BranchCode(4, "n"), BBanEntry.AccountType(2, "n"), BBanEntry.AccountNumber(8, "c")) },
				{ "BH", new BBanStructure(BBanEntry.BankCode(4, "a"), BBanEntry.AccountNumber(14, "c")) },
				{ "BI", new BBanStructure(BBanEntry.AccountNumber(12, "n")) },
				{ "BR", new BBanStructure(BBanEntry.BankCode(8, "n"), BBanEntry.BranchCode(5, "n"), BBanEntry.AccountNumber(10, "n"), BBanEntry.AccountType(1, "a"), BBanEntry.OwnerAccountNumber(1, "c")) },
				{ "BY", new BBanStructure(BBanEntry.BankCode(4, "c"), BBanEntry.BalanceAccountNumber(4, "n"), BBanEntry.AccountNumber(16, "c")) },
				// C 
				{ "CH", new BBanStructure(BBanEntry.BankCode(5, "n"), BBanEntry.AccountNumber(12, "c")) },
				{ "CR", new BBanStructure(BBanEntry.BankCode(4, "n"), BBanEntry.AccountNumber(14, "n")) },
				{ "CY", new BBanStructure(BBanEntry.BankCode(3, "n"), BBanEntry.BranchCode(5, "n"), BBanEntry.AccountNumber(16, "c")) },
				{ "CZ", new BBanStructure(BBanEntry.BankCode(4, "n"), BBanEntry.AccountNumberPrefix(6, "n"), BBanEntry.AccountNumber(10, "n")) },
				// D
				{ "DE", new BBanStructure(BBanEntry.BankCode(8, "n"), BBanEntry.AccountNumber(10, "n")) },
				{ "DJ", new BBanStructure(BBanEntry.BankCode(5, "n"), BBanEntry.BranchCode(5, "n"), BBanEntry.AccountNumber(13, "n")) }, // Djibouti
				{ "DK", new BBanStructure(BBanEntry.BankCode(4, "n"), BBanEntry.AccountNumber(10, "n")) },
				{ "DO", new BBanStructure(BBanEntry.BankCode(4, "c"), BBanEntry.AccountNumber(20, "n")) },
				// E
				{ "EE", new BBanStructure(BBanEntry.BankCode(2, "n"), BBanEntry.BranchCode(2, "n"), BBanEntry.AccountNumber(11, "n"), BBanEntry.NationalCheckDigit(1, "n")) },
				{ "EG", new BBanStructure(BBanEntry.BankCode(4, "n"), BBanEntry.BranchCode(4, "n"), BBanEntry.AccountNumber(17, "n")) },
				{ "ES", new BBanStructure(BBanEntry.BankCode(4, "n"), BBanEntry.BranchCode(4, "n"), BBanEntry.NationalCheckDigit(2, "n"), BBanEntry.AccountNumber(10, "n")) },								
				// F
				{ "FI", new BBanStructure(BBanEntry.BankCode(3, "n"), BBanEntry.BranchCode(3, "n"), BBanEntry.AccountNumber(7, "n"), BBanEntry.NationalCheckDigit(1, "n")) },
				{ "FO", new BBanStructure(BBanEntry.BankCode(4, "n"), BBanEntry.AccountNumber(9, "n"), BBanEntry.NationalCheckDigit(1, "n")) },
				{ "FR", new BBanStructure(BBanEntry.BankCode(5, "n"), BBanEntry.BranchCode(5, "n"), BBanEntry.AccountNumber(11, "c"), BBanEntry.NationalCheckDigit(2, "n")) },
				// G
				{ "GB", new BBanStructure(BBanEntry.BankCode(4, "a"), BBanEntry.BranchCode(6, "n"), BBanEntry.AccountNumber(8, "n")) },
				{ "GE", new BBanStructure(BBanEntry.BankCode(2, "a"), BBanEntry.AccountNumber(16, "n")) },
				{ "GI", new BBanStructure(BBanEntry.BankCode(4, "a"), BBanEntry.AccountNumber(15, "c")) },
				{ "GL", new BBanStructure(BBanEntry.BankCode(4, "n"), BBanEntry.AccountNumber(10, "n")) },
				{ "GR", new BBanStructure(BBanEntry.BankCode(3, "n"), BBanEntry.BranchCode(4, "n"), BBanEntry.AccountNumber(16, "c")) },
				{ "GT", new BBanStructure(BBanEntry.BankCode(4, "c"), BBanEntry.AccountNumber(20, "c")) },
				// H
				{ "HR", new BBanStructure(BBanEntry.BankCode(7, "n"), BBanEntry.AccountNumber(10, "n")) },
				{ "HU", new BBanStructure(BBanEntry.BankCode(3, "n"), BBanEntry.BranchCode(4, "n"), BBanEntry.AccountNumber(16, "n"), BBanEntry.NationalCheckDigit(1, "n")) },
				// I
				{ "IE", new BBanStructure(BBanEntry.BankCode(4, "a"), BBanEntry.BranchCode(6, "n"), BBanEntry.AccountNumber(8, "n")) },
				{ "IL", new BBanStructure(BBanEntry.BankCode(3, "n"), BBanEntry.BranchCode(3, "n"), BBanEntry.AccountNumber(13, "n")) },
				{ "IQ", new BBanStructure(BBanEntry.BankCode(4, "a"), BBanEntry.BranchCode(3, "n"), BBanEntry.AccountNumber(12, "n")) },
				{ "IS", new BBanStructure(BBanEntry.BankCode(4, "n"), BBanEntry.BranchCode(2, "n"), BBanEntry.AccountNumber(6, "n"), BBanEntry.IdentificationNumber(10, "n")) },
				{ "IT", new BBanStructure(BBanEntry.NationalCheckDigit(1, "a"), BBanEntry.BankCode(5, "n"), BBanEntry.BranchCode(5, "n"), BBanEntry.AccountNumber(12, "c")) },
				// J
				{ "JO", new BBanStructure(BBanEntry.BankCode(4, "a"), BBanEntry.BranchCode(4, "n"), BBanEntry.AccountNumber(18, "c")) },
				// K
				{ "KW", new BBanStructure(BBanEntry.BankCode(4, "a"), BBanEntry.AccountNumber(22, "c")) },
				{ "KZ", new BBanStructure(BBanEntry.BankCode(3, "n"), BBanEntry.AccountNumber(13, "c")) },
				// L
				{ "LB", new BBanStructure(BBanEntry.BankCode(4, "n"), BBanEntry.AccountNumber(20, "c")) },
				{ "LC", new BBanStructure(BBanEntry.BankCode(4, "a"), BBanEntry.AccountNumber(24, "c")) },
				{ "LI", new BBanStructure(BBanEntry.BankCode(5, "n"), BBanEntry.AccountNumber(12, "c")) },
				{ "LT", new BBanStructure(BBanEntry.BankCode(5, "n"), BBanEntry.AccountNumber(11, "n")) },
				{ "LU", new BBanStructure(BBanEntry.BankCode(3, "n"), BBanEntry.AccountNumber(13, "c")) },
				{ "LV", new BBanStructure(BBanEntry.BankCode(4, "a"), BBanEntry.AccountNumber(13, "c")) },
				{ "LY", new BBanStructure(BBanEntry.BankCode(3, "n"), BBanEntry.BranchCode(3, "n"), BBanEntry.AccountNumber(15, "n")) }, // Libya
				// M
				{ "MC", new BBanStructure(BBanEntry.BankCode(5, "n"), BBanEntry.BranchCode(5, "n"), BBanEntry.AccountNumber(11, "c"), BBanEntry.NationalCheckDigit(2, "n")) },
				{ "MD", new BBanStructure(BBanEntry.BankCode(2, "c"), BBanEntry.AccountNumber(18, "c")) },
				{ "ME", new BBanStructure(BBanEntry.BankCode(3, "n"), BBanEntry.AccountNumber(13, "n"), BBanEntry.NationalCheckDigit(2, "n")) },
				{ "MK", new BBanStructure(BBanEntry.BankCode(3, "n"), BBanEntry.AccountNumber(10, "c"), BBanEntry.NationalCheckDigit(2, "n")) },
				{ "MR", new BBanStructure(BBanEntry.BankCode(5, "n"), BBanEntry.BranchCode(5, "n"), BBanEntry.AccountNumber(11, "n"), BBanEntry.NationalCheckDigit(2, "n")) },
				{ "MT", new BBanStructure(BBanEntry.BankCode(4, "a"), BBanEntry.BranchCode(5, "n"), BBanEntry.AccountNumber(18, "c")) },
				{ "MU", new BBanStructure(BBanEntry.BankCode(6, "c"), BBanEntry.BranchCode(2, "n"), BBanEntry.AccountNumber(18, "c")) },
				// N
				{ "NL", new BBanStructure(BBanEntry.BankCode(4, "a"), BBanEntry.AccountNumber(10, "n")) },
				{ "NO", new BBanStructure(BBanEntry.BankCode(4, "n"), BBanEntry.AccountNumber(6, "n"), BBanEntry.NationalCheckDigit(1, "n")) },
				// P
				{ "PK", new BBanStructure(BBanEntry.BankCode(4, "c"), BBanEntry.AccountNumber(16, "n")) },
				{ "PL", new BBanStructure(BBanEntry.BranchCode(8, "n"), BBanEntry.AccountNumber(16, "n")) },
				{ "PS", new BBanStructure(BBanEntry.BankCode(4, "a"), BBanEntry.AccountNumber(21, "c")) },
				{ "PT", new BBanStructure(BBanEntry.BankCode(4, "n"), BBanEntry.BranchCode(4, "n"), BBanEntry.AccountNumber(11, "n"), BBanEntry.NationalCheckDigit(2, "n")) },
				// Q
				{ "QA", new BBanStructure(BBanEntry.BankCode(4, "a"), BBanEntry.AccountNumber(21, "c")) },
				// R
				{ "RO", new BBanStructure(BBanEntry.BankCode(4, "a"), BBanEntry.AccountNumber(16, "c")) },
				{ "RS", new BBanStructure(BBanEntry.BankCode(3, "n"), BBanEntry.AccountNumber(13, "n"), BBanEntry.NationalCheckDigit(2, "n")) },
				// S
				{ "SA", new BBanStructure(BBanEntry.BankCode(2, "n"), BBanEntry.AccountNumber(18, "c")) },
				{ "SC", new BBanStructure(BBanEntry.BankCode(6, "c"), BBanEntry.BranchCode(2, "n"), BBanEntry.AccountNumber(19, "c")) },
				{ "SE", new BBanStructure(BBanEntry.BankCode(3, "n"), BBanEntry.AccountNumber(17, "n")) },
				{ "SI", new BBanStructure(BBanEntry.BankCode(5, "n"), BBanEntry.BranchCode(0, "n"), BBanEntry.AccountNumber(8, "n"), BBanEntry.NationalCheckDigit(2, "n")) },
				{ "SK", new BBanStructure(BBanEntry.BankCode(4, "n"), BBanEntry.AccountNumberPrefix(6, "n"), BBanEntry.AccountNumber(10, "n")) },
				{ "SM", new BBanStructure(BBanEntry.NationalCheckDigit(1, "a"), BBanEntry.BankCode(5, "n"), BBanEntry.BranchCode(5, "n"), BBanEntry.AccountNumber(12, "c")) },
				{ "ST", new BBanStructure(BBanEntry.BankCode(4, "n"), BBanEntry.BranchCode(4, "n"), BBanEntry.AccountNumber(11, "n"), BBanEntry.NationalCheckDigit(2, "n")) },
				{ "SV", new BBanStructure(BBanEntry.BankCode(4, "a"), BBanEntry.AccountNumber(20, "n")) },				
				// T
				{ "TL", new BBanStructure(BBanEntry.BankCode(3, "n"), BBanEntry.AccountNumber(14, "n"), BBanEntry.NationalCheckDigit(2, "n")) },
				{ "TN", new BBanStructure(BBanEntry.BankCode(2, "n"), BBanEntry.BranchCode(3, "n"), BBanEntry.AccountNumber(15, "c")) },
				{ "TR", new BBanStructure(BBanEntry.BankCode(5, "n"), BBanEntry.NationalCheckDigit(1, "c"), BBanEntry.AccountNumber(16, "c")) },
				// U
				{ "UA", new BBanStructure(BBanEntry.BankCode(6, "n"), BBanEntry.AccountNumber(19, "n")) },
				// V
				{ "VA", new BBanStructure(BBanEntry.BankCode(3, "n"), BBanEntry.AccountNumber(15, "n")) },
				{ "VG", new BBanStructure(BBanEntry.BankCode(4, "c"), BBanEntry.AccountNumber(16, "n")) },				
				// X
				{ "XK", new BBanStructure(BBanEntry.BankCode(2, "n"), BBanEntry.BranchCode(2, "n"), BBanEntry.AccountNumber(10, "n"), BBanEntry.NationalCheckDigit(2, "n")) },
				
				// PARTIAL/EXPERIMENTAL Countries
				{ "AO", new BBanStructure(BBanEntry.AccountNumber(21, "n")) }, // Andola
				{ "BF", new BBanStructure(BBanEntry.BankCode(2, "c"), BBanEntry.AccountNumber(22, "n")) }, // Burkina Faso
				{ "BJ", new BBanStructure(BBanEntry.BankCode(2, "c"), BBanEntry.AccountNumber(22, "n")) }, // Benin
				{ "CF", new BBanStructure(BBanEntry.BankCode(5, "n"), BBanEntry.BranchCode(5, "n"), BBanEntry.AccountNumber(13, "n")) }, // Central African Republic
				{ "CG", new BBanStructure(BBanEntry.BankCode(5, "n"), BBanEntry.BranchCode(5, "n"), BBanEntry.AccountNumber(13, "n")) }, // Congo
				{ "CI", new BBanStructure(BBanEntry.BankCode(2, "c"), BBanEntry.AccountNumber(22, "n") ) }, // Ivory Coast (Cote Divoire)
				{ "CM", new BBanStructure(BBanEntry.AccountNumber(23, "n")) }, // Cameroon
				{ "CV", new BBanStructure(BBanEntry.AccountNumber(21, "n")) }, // Cape Verde
				{ "DZ", new BBanStructure(BBanEntry.AccountNumber(22, "n")) }, // Algeria
				{ "GA", new BBanStructure(BBanEntry.BankCode(5, "n"), BBanEntry.BranchCode(5, "n"), BBanEntry.AccountNumber(13, "c")) }, // Gabon
				{ "GQ", new BBanStructure(BBanEntry.BankCode(5, "n"), BBanEntry.BranchCode(5, "n"), BBanEntry.AccountNumber(13, "n")) }, // Equatorial Guinea
				{ "GW", new BBanStructure(BBanEntry.BankCode(4, "c"), BBanEntry.BranchCode(4, "n"), BBanEntry.AccountNumber(13, "n")) }, // Guinea-Bissau
				{ "HN", new BBanStructure(BBanEntry.BankCode(4, "c"), BBanEntry.AccountNumber(20, "n")) }, // Honduras
				{ "IR", new BBanStructure(BBanEntry.AccountNumber(22, "n")) }, // Iran
				{ "KM", new BBanStructure(BBanEntry.BankCode(5, "n"), BBanEntry.BranchCode(5, "n"), BBanEntry.AccountNumber(13, "c")) }, // Comoros
				{ "MA", new BBanStructure(BBanEntry.BankCode(3, "n"), BBanEntry.BranchCode(5, "n"), BBanEntry.AccountNumber(16, "n")) }, // Morocco
				{ "MG", new BBanStructure(BBanEntry.BankCode(5, "n"), BBanEntry.BranchCode(5, "n"), BBanEntry.AccountNumber(11, "c"), BBanEntry.NationalCheckDigit(2, "n")) }, // Madagascar
				{ "ML", new BBanStructure(BBanEntry.BankCode(2, "c"), BBanEntry.AccountNumber(22, "n")) }, // Mali
				{ "MN", new BBanStructure(BBanEntry.BankCode(2, "n"), BBanEntry.AccountNumber(14, "n")) }, // Mongolia
				{ "MZ", new BBanStructure(BBanEntry.AccountNumber(21, "n")) }, // Mozambique
				{ "NE", new BBanStructure(BBanEntry.BankCode(2, "c"), BBanEntry.AccountNumber(22, "n"))}, // Niger
				{ "NI", new BBanStructure(BBanEntry.BankCode(4, "c"), BBanEntry.AccountNumber(24, "n")) }, // Nicaragua
				{ "SN", new BBanStructure(BBanEntry.BankCode(2, "c"), BBanEntry.AccountNumber(22, "n")) }, // Senegal
				{ "TD", new BBanStructure(BBanEntry.BankCode(5, "n"), BBanEntry.BranchCode(5,"n"), BBanEntry.AccountNumber(13, "n")) }, // Chad
				{ "TG", new BBanStructure(BBanEntry.BankCode(2, "c"), BBanEntry.AccountNumber(22, "n")) }, // Togo
			};
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
