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
                Assert.IsTrue(result == IbanFormatViolation.NO_VIOLATION, $"{iban}: {result}");
            }
        }
        
        [TestMethod]
        public void CentralAfricanRepublic_ValidIbanShouldPass()
        {
            IbanUtils.IsValid("CF4220001000010120069700160", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.NO_VIOLATION);
        }

        [TestMethod]
        public void CentralAfricanRepublic_InvalidCheckDigitShouldFailValidation()
        {
            IbanUtils.IsValid("CF0220001000010120069700160", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.IBAN_INVALID_CHECK_DIGIT_VALUE);
        }

        [TestMethod]
        public void Chad_ValidIbanShouldPass()
        {
            IbanUtils.IsValid("TD8960002000010271091600153", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.NO_VIOLATION);
        }

        [TestMethod]
        public void Chad_InvalidCheckDigitShouldFailValidation()
        {
            IbanUtils.IsValid("TD0260002000010271091600153", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.IBAN_INVALID_CHECK_DIGIT_VALUE);
        }

        [TestMethod]
        public void Comoros_ValidIbanShouldPass()
        {
            IbanUtils.IsValid("KM4600005000010010904400137", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.NO_VIOLATION);
        }

        [TestMethod]
        public void Comoros_InvalidCheckDigitShouldFailValidation()
        {
            IbanUtils.IsValid("KM4000005000010010904400137", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.IBAN_INVALID_CHECK_DIGIT_VALUE);
        }

        [TestMethod]
        public void Congo_ValidIbanShouldPass()
        {
            IbanUtils.IsValid("CG3930011000101013451300019", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.NO_VIOLATION);
        }

        [TestMethod]
        public void Congo_InvalidCheckDigitShouldFailValidation()
        {
            IbanUtils.IsValid("CG0930011000101013451300019", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.IBAN_INVALID_CHECK_DIGIT_VALUE);
        }
        
        [TestMethod]
        public void IvoryCoast_ValidIbanShouldPass()
        {            
            IbanUtils.IsValid("CI05A00060174100178530011852", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.NO_VIOLATION);
        }

        [TestMethod]
        public void IvoryCoast_InvalidCheckDigitShouldFailValidation()
        {
            IbanUtils.IsValid("CI97CI0080111301134291200589", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.IBAN_INVALID_CHECK_DIGIT_VALUE);
        }

        [TestMethod]
        public void Djibouti_ValidIbanShouldPass()
        {
            IbanUtils.IsValid("DJ2110002010010409943020008", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.NO_VIOLATION);
        }

        [TestMethod]
        public void Djibouti_InvalidCheckDigitShouldFailValidation()
        {
            IbanUtils.IsValid("DJ0110002010010409943020008", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.IBAN_INVALID_CHECK_DIGIT_VALUE);
        }

        [TestMethod]
        public void EquatorialGuinea_ValidIbanShouldPass()
        {
            IbanUtils.IsValid("GQ7050002001003715228190196", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.NO_VIOLATION);
        }

        [TestMethod]
        public void EquatorialGuinea_InvalidCheckDigitShouldFailValidation()
        {
            IbanUtils.IsValid("GQ7350002001003715228190196", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.IBAN_INVALID_CHECK_DIGIT_VALUE);
        }

        [TestMethod]
        public void Gabon_ValidIbanShouldPass()
        {
            IbanUtils.IsValid("GA2140021010032001890020126", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.NO_VIOLATION);
        }

        [TestMethod]
        public void Gabon_InvalidCheckDigitShouldFailValidation()
        {
            IbanUtils.IsValid("GA1140021010032001890020126", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.IBAN_INVALID_CHECK_DIGIT_VALUE);
        }

        [TestMethod]
        public void GuineaBissau_ValidIbanShouldPass()
        {
            IbanUtils.IsValid("GW04GW1430010181800637601", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.NO_VIOLATION);
        }

        [TestMethod]
        public void GuineaBissau_InvalidCheckDigitShouldFailValidation()
        {
            IbanUtils.IsValid("GW01GW1430010181800637601", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.IBAN_INVALID_CHECK_DIGIT_VALUE);
        }

        [TestMethod]
        public void Honduras_ValidIbanShouldPass()
        {
            IbanUtils.IsValid("HN54PISA00000000000000123124", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.NO_VIOLATION);
        }

        [TestMethod]
        public void Honduras_InvalidCheckDigitShouldFailValidation()
        {
            IbanUtils.IsValid("HN34PISA00000000000000123124", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.IBAN_INVALID_CHECK_DIGIT_VALUE);
        }

        [TestMethod]
        public void Morocco_ValidIbanShouldPass()
        {
            IbanUtils.IsValid("MA64181815211118602202000107", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.NO_VIOLATION);
        }

        [TestMethod]
        public void Morocco_InvalidCheckDigitShouldFailValidation()
        {
            IbanUtils.IsValid("MA04181815211118602202000107", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.IBAN_INVALID_CHECK_DIGIT_VALUE);
        }

        [TestMethod]
        public void Nicaragua_ValidIbanShouldPass()
        {
            IbanUtils.IsValid("NI92BAMC000000000000000003123123", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.NO_VIOLATION);
        }

        [TestMethod]
        public void Nicaragua_InvalidCheckDigitShouldFailValidation()
        {
            IbanUtils.IsValid("NI12BAMC000000000000000003123123", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.IBAN_INVALID_CHECK_DIGIT_VALUE);
        }

        [TestMethod]
        public void Niger_ValidIbanShouldPass()
        {
            IbanUtils.IsValid("NE58NE0380100100130305000268", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.NO_VIOLATION);
        }

        [TestMethod]
        public void Niger_InvalidCheckDigitShouldFailValidation()
        {
            IbanUtils.IsValid("NE28NE0380100100130305000268", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.IBAN_INVALID_CHECK_DIGIT_VALUE);
        }

        [TestMethod]
        public void Togo_ValidIbanShouldPass()
        {
            IbanUtils.IsValid("TG53TG0090604310346500400070", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.NO_VIOLATION);
        }

        [TestMethod]
        public void Togo_InvalidCheckDigitShouldFailValidation()
        {
            IbanUtils.IsValid("TG33TG0090604310346500400070", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.IBAN_INVALID_CHECK_DIGIT_VALUE);
        }

        [TestMethod]
        public void Libya_ValidIbanShouldPass()
        {
            IbanUtils.IsValid("LY83002048000020100120361", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.NO_VIOLATION);
        }

        [TestMethod]
        public void Libya_InvalidCheckDigitShouldFailValidation()
        {
            IbanUtils.IsValid("LY33002048000020100120361", out IbanFormatViolation result);
            Assert.IsTrue(result == IbanFormatViolation.IBAN_INVALID_CHECK_DIGIT_VALUE);
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
