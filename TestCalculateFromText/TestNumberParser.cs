using CalculateFromText;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCalculateFromText
{
    [TestClass]
    public class TestNumberParser
    {
        [TestMethod]
        public void TestMethod1()
        {
            var calc = new NumberExtractor();
            var result = calc.GetNumberFromLine("08.03.2022 Kontraktssum 3 850 000,00");
            Assert.AreEqual(3850000, result);
        }

        [TestMethod]
        public void TestDateAndNumber2()
        {
            var calc = new NumberExtractor();
            var result = calc.GetNumberFromLine("05.05.2022 Betalt avregning IN-lån USBL 1 399,93");
            Assert.AreEqual(1399.93, result, 0.0001);
        }

        [TestMethod]
        public void TestDateAndNumber3()
        {
            var calc = new NumberExtractor();
            var result = calc.GetNumberFromLine("10. jan.	Visa 100021 Eventyrbrua 	13. jan.	1 350,00");
            Assert.AreEqual(1350.0, result, 0.0001);
        }
        
    }
}