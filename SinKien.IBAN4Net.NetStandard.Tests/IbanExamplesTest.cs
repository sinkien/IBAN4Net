using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SinKien.IBAN4Net.NetStandard.Tests.Net45
{
    [TestClass]
    public class IbanExamplesTest
    {

        [TestMethod]
        public void AllSampleValidIBANsShouldWork()
        {
            var IBANs = System.IO.File.ReadAllLines(System.IO.Path.Combine("SampleData", "SampleValidIBANs.txt"));
            foreach (var iban in IBANs)
            {
                IbanUtils.IsValid(iban, out IbanFormatViolation result);
                Assert.IsTrue(result == IbanFormatViolation.NO_VIOLATION, $"{iban}: {result.ToString()}");
            }
        }

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

        [TestMethod]
        public void Madagascar_ValidIbanShouldPass()
        {            
            IbanUtils.IsValid("MG4600005030071289421016045", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.NO_VIOLATION);
        }

        [TestMethod]
        public void Madagascar_IbanBuilderShouldBuildValidIban()
        {
            Iban madagascarIban = new IbanBuilder().CountryCode(CountryCode.GetCountryCode("MG"))                                
                .BankCode("00005")
                .BranchCode("03007")
                .AccountNumber("12894210160")
                .NationalCheckDigit("45")
                .Build();

            Assert.AreEqual("MG4600005030071289421016045", madagascarIban.ToString());
        }

        [TestMethod]
        public void Madagascar_InvalidCheckDigitShouldFailValidation()
        {
            IbanUtils.IsValid("MG4600005030071289421016095", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.IBAN_INVALID_CHECK_DIGIT_VALUE);
        }
    }
}
