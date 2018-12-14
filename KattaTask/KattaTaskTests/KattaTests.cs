using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KattaTask;
using Moq;

namespace KattaTaskTests
{
    [TestClass]
    public class KattaTests
    {
        [TestMethod]
        public void calculator_take_0_numbers()
        {
            var log_mock = new Mock<ILogger>();
            var web_mock = new Mock<IWebservice>();
            Calculator calc = new Calculator(log_mock.Object,web_mock.Object);
             Assert.AreEqual(calc.Add(""), 0);
        }

        [TestMethod]
        public void calculator_take_1_numbers()
        {
            var log_mock = new Mock<ILogger>();
            var web_mock = new Mock<IWebservice>();
            Calculator calc = new Calculator(log_mock.Object, web_mock.Object);
            Assert.AreEqual(calc.Add("1"), 1);
        }

        [TestMethod]
        public void calculator_take_2_numbers()
        {
            var log_mock = new Mock<ILogger>();
            var web_mock = new Mock<IWebservice>();
            Calculator calc = new Calculator(log_mock.Object, web_mock.Object);
            Assert.AreEqual(calc.Add("1,2"), 3);
        }

        [TestMethod]
         public void calculator_take_an_unknown_amount_of_numbersnumbers()
        {
            var log_mock = new Mock<ILogger>();
            var web_mock = new Mock<IWebservice>();
            Calculator calc = new Calculator(log_mock.Object, web_mock.Object);
            Assert.AreEqual(calc.Add("1,2,3,20,4"), 30);
        }
        [TestMethod]
        public void calculator_handle_new_lines_between_numbers()
        {
            var log_mock = new Mock<ILogger>();
            var web_mock = new Mock<IWebservice>();
            Calculator calc = new Calculator(log_mock.Object, web_mock.Object);
            Assert.AreEqual(calc.Add("1\n2,3"), 6);
        }
        [TestMethod]
        public void two_delimiter_together()
        {
            var log_mock = new Mock<ILogger>();
            var web_mock = new Mock<IWebservice>();
            Calculator calc = new Calculator(log_mock.Object, web_mock.Object);
            try
            {
                calc.Add("1,\n");
                Assert.Fail();
            }
            catch (FormatException e)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void calculator_change_delimiters()
        {
            var log_mock = new Mock<ILogger>();
            var web_mock = new Mock<IWebservice>();
            Calculator calc = new Calculator(log_mock.Object, web_mock.Object);
            Assert.AreEqual(calc.Add("//;\n1;2"), 3);
        }

        [TestMethod]
        public void calculator_take_negative_numbers()
        {
            var log_mock = new Mock<ILogger>();
            var web_mock = new Mock<IWebservice>();
            Calculator calc = new Calculator(log_mock.Object, web_mock.Object);
            try
            {
                calc.Add("-1,2");
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                string expected = "negatives not allowed -1";
                Assert.AreEqual(expected, e.Message);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void calculator_take_numkbers_bigger_than_1000()
        {
            var log_mock = new Mock<ILogger>();
            var web_mock = new Mock<IWebservice>();
            Calculator calc = new Calculator(log_mock.Object, web_mock.Object);
            Assert.AreEqual(calc.Add("2,1001"), 2);
        }

        [TestMethod]
        public void calculator_take_delimiters_of_any_length()
        {
            var log_mock = new Mock<ILogger>();
            var web_mock = new Mock<IWebservice>();
            Calculator calc = new Calculator(log_mock.Object, web_mock.Object);
            Assert.AreEqual(calc.Add("//[***]\n1***2***3"), 6);
        }

        [TestMethod]
        public void calculator_take_multiple_delimiters()
        {
            var log_mock = new Mock<ILogger>();
            var web_mock = new Mock<IWebservice>();
            Calculator calc = new Calculator(log_mock.Object, web_mock.Object);
            Assert.AreEqual(calc.Add("//[*][%]\n1*2%3"), 6);
        }

        [TestMethod]
        public void Logger_throw_exception()
        {
            ILogger log = new BrokenLoger();
            var web_mock = new Mock<IWebservice>();
            Calculator calc = new Calculator(log, web_mock.Object);
            calc.Add("//[*][%]\n1*2%3");
            web_mock.Verify(x => x.show(), Times.Exactly(1));
        }


        [TestMethod]
        public void test_Write_function_expected_call_of_function_2()
        {
            var log_mock = new Mock<ILogger>();
            var web_mock = new Mock<IWebservice>();
            Calculator calc = new Calculator(log_mock.Object, web_mock.Object);
            calc.Add("//[*][%]\n1*2%3");
            log_mock.Verify(x => x.Write(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));

        }
    }
}
