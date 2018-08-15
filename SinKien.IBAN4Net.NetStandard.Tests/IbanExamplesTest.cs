using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SinKien.IBAN4Net.NetStandard.Tests.Net45
{
    [TestClass]
    public class IbanExamplesTest
    {
        [TestMethod]
        public void Belarus_ValidIbanShouldPass()
        {
            //Iban iban = Iban.CreateInstance("BY86AKBB10100000002966000000");
            IbanUtils.IsValid("BY86AKBB10100000002966000000", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.NO_VIOLATION);
        }

        [TestMethod]
        public void Belarus_InvalidCheckDigitShouldFailValidation()
        {
            IbanUtils.IsValid("BY06AKBB10100000002966000000", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.IBAN_INVALID_CHECK_DIGIT_VALUE);
        }

        [TestMethod]
        public void Belarus_IbanBuilderShouldBuildValidIban()
        {
            Iban belarusIban = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("BY"))
                .AccountNumber("2966000000")
                .BankCode("AKBB")
                .BalanceAccountNumber("1010")
                .Build();

            Assert.AreEqual("BY86AKBB10100000002966000000", belarusIban.ToString());
        }
    }
}
