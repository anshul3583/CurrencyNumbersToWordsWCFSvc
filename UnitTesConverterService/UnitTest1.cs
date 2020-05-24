using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTesConverterService.ConverterServiceReference;

namespace UnitTestCurrencyNumbersToWordsWCFService
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FiveDigitNumberTest()
        {
            //Arrange
            CurrencyNumbersToWords model = new CurrencyNumbersToWords();
            model.CurrencyNumber = "45 100";

            //Act
            UnitTesConverterService.ConverterServiceReference.ServiceClient svc = new UnitTesConverterService.ConverterServiceReference.ServiceClient();
            var output = svc.ConvertCurrencyNumbersToWords(model.CurrencyNumber);

            //Assertfourty-five thousand one hundred  dollars
            Assert.AreEqual(output.Result, "fourty-five thousand one hundred dollars");
        }

        [TestMethod]
        public void MaxAllowedNumberTest()
        {
            //Arrange
            CurrencyNumbersToWords model = new CurrencyNumbersToWords();
            model.CurrencyNumber = "999 999 999,99";

            //Act
            ServiceClient svc = new ServiceClient();
            var output = svc.ConvertCurrencyNumbersToWords(model.CurrencyNumber);

            //Assert
            Assert.AreEqual(output.Result, "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents");
        }
    }
}
