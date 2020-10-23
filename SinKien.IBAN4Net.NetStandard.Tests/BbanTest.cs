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
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SinKien.IBAN4Net.NetStandard.Tests.Net45
{

    [TestClass]
    public class BbanTest
    {
        [TestMethod]
        public void GetBbanAndChangeShouldNotChangeOtherBban()
        {
            int entryCount = 0;
            BBanStructure entry = Bban.GetStructureForCountry("CY");
            Assert.IsNotNull(entry);
            entryCount = entry.Entries.Count();
            entry.Entries.RemoveAt(1);
            BBanStructure newEntry = Bban.GetStructureForCountry("CY");
            Assert.IsNotNull(newEntry);
            Assert.AreEqual(entryCount, newEntry.Entries.Count());

        }

        [TestMethod]
        public void BbanIsSupportedEntryShouldReturnFalseForUnsupportedEntryType()
        {
            bool result = Bban.IsBbanEntrySupported("CZ", BBanEntryType.BRANCH_CODE);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void BbanIsSupportedEntryShouldReturnTrueForSupportedEntryType()
        {
            bool result = Bban.IsBbanEntrySupported("CZ", BBanEntryType.ACCOUNT_NUMBER_PREFIX);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BbanIsSupportedEntryShouldReturnFalseForUnsupportedCountry()
        {
            bool result = Bban.IsBbanEntrySupported("XX", BBanEntryType.ACCOUNT_NUMBER);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void BbanGetStructureForCountryShouldReturnStructureForSupportedCountry()
        {
            BBanStructure structure = Bban.GetStructureForCountry("CZ");
            Assert.IsNotNull(structure);
        }

        [TestMethod]
        [WorkItem(3)]
        public void BbanGetStructureForCountryBulgariaShouldReturnStructureForBulgaria()
        {
            BBanStructure structure = Bban.GetStructureForCountry("BG");
            Assert.IsNotNull(structure);
            Assert.AreEqual(structure.Entries.Count, 4);

            BBanEntry bankCode = structure.Entries[0];
            BBanEntry branchCode = structure.Entries[1];
            BBanEntry accountType = structure.Entries[2];
            BBanEntry accountNumber = structure.Entries[3];

            Assert.AreEqual(bankCode.CharacterType, BBanEntryCharacterType.A);
            Assert.AreEqual(bankCode.EntryType, BBanEntryType.BANK_CODE);
            Assert.AreEqual(bankCode.Length, 4);

            Assert.AreEqual(branchCode.CharacterType, BBanEntryCharacterType.N);
            Assert.AreEqual(branchCode.EntryType, BBanEntryType.BRANCH_CODE);
            Assert.AreEqual(branchCode.Length, 4);

            Assert.AreEqual(accountType.CharacterType, BBanEntryCharacterType.N);
            Assert.AreEqual(accountType.EntryType, BBanEntryType.ACCOUNT_TYPE);
            Assert.AreEqual(accountType.Length, 2);

            Assert.AreEqual(accountNumber.CharacterType, BBanEntryCharacterType.C);
            Assert.AreEqual(accountNumber.EntryType, BBanEntryType.ACCOUNT_NUMBER);
            Assert.AreEqual(accountNumber.Length, 8);
        }

        [TestMethod]
        public void BbanGetStructureForCountryShouldReturnNullForUnsupportedCountry()
        {
            BBanStructure structure = Bban.GetStructureForCountry("XX");
            Assert.IsNull(structure);
        }
    }
}
