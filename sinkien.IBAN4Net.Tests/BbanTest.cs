using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace sinkien.IBAN4Net.Tests
{
    [TestClass]
    public class BbanTest
    {
        [TestMethod]
        public void BbanIsSupportedEntryShouldReturnFalseForUnsupportedEntryType()
        {
            bool result = Bban.IsBbanEntrySupported( "CZ", BBanEntryType.BRANCH_CODE );
            Assert.IsFalse( result );
        }

        [TestMethod]
        public void BbanIsSupportedEntryShouldReturnTrueForSupportedEntryType()
        {
            bool result = Bban.IsBbanEntrySupported( "CZ", BBanEntryType.ACCOUNT_NUMBER_PREFIX );
            Assert.IsTrue( result );
        }

        [TestMethod]
        public void BbanIsSupportedEntryShouldReturnFalseForUnsupportedCountry()
        {
            bool result = Bban.IsBbanEntrySupported( "XX", BBanEntryType.ACCOUNT_NUMBER );
            Assert.IsFalse( result );
        }

        [TestMethod]
        public void BbanGetStructureForCountryShouldReturnStructureForSupportedCountry()
        {
            BBanStructure structure = Bban.GetStructureForCountry( "CZ" );
            Assert.IsNotNull( structure );
        }

        [TestMethod]
        [WorkItem(3)]
        public void IbanCountrySupportCheckWithBulgariaShouldRetrunTrue_string()
        {
            BBanStructure structure = Bban.GetStructureForCountry( "BG" );
            Assert.IsNotNull(structure);
            Assert.AreEqual(structure.Entries.Count, 4);

            BBanEntry bankCode = structure.Entries[0];
            BBanEntry branchCode = structure.Entries[1];
            BBanEntry accountType = structure.Entries[2];
            BBanEntry accountNumber = structure.Entries[3];

            Assert.AreEqual( bankCode.CharacterType, BBanEntryCharacterType.A );
            Assert.AreEqual( bankCode.EntryType, BBanEntryType.BANK_CODE );
            Assert.AreEqual( bankCode.Length, 4 );

            Assert.AreEqual( branchCode.CharacterType, BBanEntryCharacterType.N );
            Assert.AreEqual( branchCode.EntryType, BBanEntryType.BRANCH_CODE );
            Assert.AreEqual( branchCode.Length, 4 );

            Assert.AreEqual( accountType.CharacterType, BBanEntryCharacterType.N );
            Assert.AreEqual( accountType.EntryType, BBanEntryType.ACCOUNT_TYPE );
            Assert.AreEqual( accountType.Length, 2 );

            Assert.AreEqual( accountNumber.CharacterType, BBanEntryCharacterType.C );
            Assert.AreEqual( accountNumber.EntryType, BBanEntryType.ACCOUNT_NUMBER );
            Assert.AreEqual( accountNumber.Length, 8 );
        }

        [TestMethod]
        public void BbanGetStructureForCountryShouldReturnNullForUnsupportedCountry ()
        {
            BBanStructure structure = Bban.GetStructureForCountry( "XX" );
            Assert.IsNull( structure );
        }
    }
}
