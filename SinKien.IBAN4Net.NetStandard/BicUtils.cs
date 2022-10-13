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

using SinKien.IBAN4Net.Exceptions;
using System;

namespace SinKien.IBAN4Net
{
	/// <summary>
	/// BIC Utility class
	/// </summary>
	public class BicUtils
	{        
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
				validateEmpty(bic);
				validateLength(bic);
				validateCase(bic);
				validateBankCode(bic);
				validateCountryCode(bic);
				validateLocationCode(bic);

				if (HasBranchCode(bic))
				{
					validateBranchCode(bic);
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
				throw new BicFormatException(ex.Message, BicFormatViolation.UNKNOWN, ex);
			}

		}

		/// <summary>
		/// Validation of BIC string
		/// </summary>
		/// <param name="bic">BIC to be validated</param>
		/// <param name="validationResult">Validation result</param>
		/// <returns>True if BIC is valid, false if it encounters any problem</returns>
		public static bool IsValid(string bic, out BicFormatViolation validationResult)
		{
			bool result = false;
			validationResult = BicFormatViolation.NO_VIOLATION;

			if (!string.IsNullOrEmpty(bic))
			{
				if (hasValidLength(bic, out validationResult))
				{
					if (hasValidCase(bic, out validationResult))
					{
						if (hasValidBankCode(bic, out validationResult))
						{
							if (hasValidCountryCode(bic, out validationResult))
							{
								if (hasValidLocationCode(bic, out validationResult))
								{
									if (HasBranchCode(bic))
									{
										if (hasValidBranchCode(bic, out validationResult))
										{
											result = true;
										}
									}
									else
									{
										result = true;
									}
								}
							}
						}
					}

				}
			}
			else
			{
				validationResult = BicFormatViolation.BIC_NOT_EMPTY_OR_NULL;
			}

			return result;
		}

		private static void validateEmpty(string bic)
		{
			if (string.IsNullOrEmpty(bic))
			{
				throw new BicFormatException("Empty or null input string cannot be valid BIC", BicFormatViolation.BIC_NOT_EMPTY_OR_NULL);
			}
		}

		private static void validateLength(string bic)
		{
			if (bic.Length != Consts.BIC_BIC8_LENGTH && bic.Length != Consts.BIC_BIC11_LENGTH)
			{
				throw new BicFormatException($"BIC length must be {Consts.BIC_BIC8_LENGTH} or {Consts.BIC_BIC11_LENGTH}", BicFormatViolation.BIC_LENGTH_8_OR_11);
			}
		}

		private static bool hasValidLength(string bic, out BicFormatViolation validationResult)
		{
			validationResult = BicFormatViolation.NO_VIOLATION;

			if (bic.Length != Consts.BIC_BIC8_LENGTH && bic.Length != Consts.BIC_BIC11_LENGTH)
			{
				validationResult = BicFormatViolation.BIC_LENGTH_8_OR_11;
			}

			return (validationResult == BicFormatViolation.NO_VIOLATION);
		}

		private static void validateCase(string bic)
		{
			if (!bic.Equals(bic.ToUpper()))
			{
				throw new BicFormatException("BIC must contain only upper case letters", BicFormatViolation.BIC_ONLY_UPPER_CASE_LETTERS);
			}
		}

		private static bool hasValidCase(string bic, out BicFormatViolation validationResult)
		{
			validationResult = BicFormatViolation.NO_VIOLATION;

			if (!bic.Equals(bic.ToUpper()))
			{
				validationResult = BicFormatViolation.BIC_ONLY_UPPER_CASE_LETTERS;
			}

			return (validationResult == BicFormatViolation.NO_VIOLATION);
		}

		private static void validateBankCode(string bic)
		{
			string bankCode = GetBankCode(bic);
			foreach (char c in bankCode.ToCharArray())
			{
				if (!char.IsLetter(c))
				{
					throw new BicFormatException("Bank code must contain only letters", BicFormatViolation.BANK_CODE_ONLY_LETTERS);
				}
			}

		}

		private static bool hasValidBankCode(string bic, out BicFormatViolation validationResult)
		{
			validationResult = BicFormatViolation.NO_VIOLATION;

			string bankCode = GetBankCode(bic);
			foreach (char c in bankCode.ToCharArray())
			{
				if (!char.IsLetter(c))
				{
					validationResult = BicFormatViolation.BANK_CODE_ONLY_LETTERS;
					break;
				}
			}

			return (validationResult == BicFormatViolation.NO_VIOLATION);
		}

		private static void validateCountryCode(string bic)
		{
			string countryCode = GetCountryCode(bic);
			if (countryCode.Trim().Length < Consts.BIC_COUNTRY_CODE_LENGTH || !countryCode.Equals(countryCode.ToUpper()) || !Char.IsLetter(countryCode[0]) || !Char.IsLetter(countryCode[1]))
			{
				throw new BicFormatException("BIC country code must contain upper case letters", BicFormatViolation.COUNTRY_CODE_ONLY_UPPER_CASE_LETTERS);
			}

			if (CountryCode.GetCountryCode(countryCode) == null)
			{
				throw new UnsupportedCountryException("Country code is not supported", countryCode);
			}
		}

		private static bool hasValidCountryCode(string bic, out BicFormatViolation validationResult)
		{
			validationResult = BicFormatViolation.NO_VIOLATION;

			string countryCode = GetCountryCode(bic);
			if (countryCode.Trim().Length < Consts.BIC_COUNTRY_CODE_LENGTH || !countryCode.Equals(countryCode.ToUpper()) || !Char.IsLetter(countryCode[0]) || !Char.IsLetter(countryCode[1]))
			{
				validationResult = BicFormatViolation.COUNTRY_CODE_ONLY_UPPER_CASE_LETTERS;
			}
			else
			{
				if (CountryCode.GetCountryCode(countryCode) == null)
				{
					validationResult = BicFormatViolation.COUNTRY_CODE_UNSUPPORTED;
				}
			}

			return (validationResult == BicFormatViolation.NO_VIOLATION);
		}

		private static void validateLocationCode(string bic)
		{
			string locationCode = GetLocationCode(bic);
			foreach (char c in locationCode.ToCharArray())
			{
				if (!char.IsLetterOrDigit(c))
				{
					throw new BicFormatException("Location code must contain only letters or digits", BicFormatViolation.LOCATION_CODE_ONLY_LETTERS_OR_DIGITS);
				}
			}
		}

		private static bool hasValidLocationCode(string bic, out BicFormatViolation validationResult)
		{
			validationResult = BicFormatViolation.NO_VIOLATION;

			string locationCode = GetLocationCode(bic);
			foreach (char c in locationCode.ToCharArray())
			{
				if (!char.IsLetterOrDigit(c))
				{
					validationResult = BicFormatViolation.LOCATION_CODE_ONLY_LETTERS_OR_DIGITS;
					break;
				}
			}

			return (validationResult == BicFormatViolation.NO_VIOLATION);
		}

		private static void validateBranchCode(string bic)
		{
			string branchCode = GetBranchCode(bic);
			foreach (char c in branchCode.ToCharArray())
			{
				if (!char.IsLetterOrDigit(c))
				{
					throw new BicFormatException("Branch code must contain only letters or digits", BicFormatViolation.BRANCH_CODE_ONLY_LETTERS_OR_DIGITS);
				}
			}
		}

		private static bool hasValidBranchCode(string bic, out BicFormatViolation validationResult)
		{
			validationResult = BicFormatViolation.NO_VIOLATION;

			string branchCode = GetBranchCode(bic);
			foreach (char c in branchCode.ToCharArray())
			{
				if (!char.IsLetterOrDigit(c))
				{
					validationResult = BicFormatViolation.BRANCH_CODE_ONLY_LETTERS_OR_DIGITS;
					break;
				}
			}

			return (validationResult == BicFormatViolation.NO_VIOLATION);
		}

		public static string GetBankCode(string bic) => bic.Substring(Consts.BIC_BANK_CODE_INDEX, Consts.BIC_BANK_CODE_LENGTH);
		public static string GetCountryCode(string bic) => bic.Substring(Consts.BIC_COUNTRY_CODE_INDEX, Consts.BIC_COUNTRY_CODE_LENGTH);
		public static string GetLocationCode(string bic) => bic.Substring(Consts.BIC_LOCATION_CODE_INDEX, Consts.BIC_LOCATION_CODE_LENGTH);
		public static bool HasBranchCode(string bic) => bic.Length == Consts.BIC_BIC11_LENGTH;
		public static string GetBranchCode(string bic) => bic.Substring(Consts.BIC_BRANCH_CODE_INDEX, Consts.BIC_BRANCH_CODE_LENGTH);

	}
}
