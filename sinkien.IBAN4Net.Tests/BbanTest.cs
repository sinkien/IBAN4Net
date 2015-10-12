using System;
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
        public void BbanGetStructureForCountryShouldReturnNullForUnsupportedCountry ()
        {
            BBanStructure structure = Bban.GetStructureForCountry( "XX" );
            Assert.IsNull( structure );
        }
    }
}
