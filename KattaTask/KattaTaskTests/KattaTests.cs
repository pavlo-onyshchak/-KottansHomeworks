using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KattaTask;

namespace KattaTaskTests
{
    [TestClass]
    public class KattaTests
    {
        [TestMethod]
        public void calculator_take_0_numbers()
        {
             Assert.AreEqual(Calculator.Add(""), 0);
        }

        [TestMethod]
        public void calculator_take_1_numbers()
        {
            Assert.AreEqual(Calculator.Add("1"), 1);
        }

        [TestMethod]
        public void calculator_take_2_numbers()
        {
            Assert.AreEqual(Calculator.Add("1,2"), 3);
        }

        [TestMethod]
         public void calculator_take_an_unknown_amount_of_numbersnumbers()
        {
            Assert.AreEqual(Calculator.Add("1,2,3,20,4"), 30);
        }
        [TestMethod]
        public void calculator_handle_new_lines_between_numbers()
        {
            Assert.AreEqual(Calculator.Add("1\n2,3"), 6);
        }
        [TestMethod]
        public void two_delimiter_together()
        {
            try
            {
                Calculator.Add("1,\n");
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
            Assert.AreEqual(Calculator.Add("//;\n1;2"), 3);
        }

        [TestMethod]
        public void calculator_take_negative_numbers()
        {
            try
            {
                Calculator.Add("-1,2");
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
            Assert.AreEqual(Calculator.Add("2,1001"), 2);
        }

        [TestMethod]
        public void calculator_take_delimiters_of_any_length()
        {
            Assert.AreEqual(Calculator.Add("//[***]\n1***2***3"), 6);
        }

        [TestMethod]
        public void calculator_take_multiple_delimiters()
        {
            Assert.AreEqual(Calculator.Add("//[*][%]\n1*2%3"), 6);
        }
    }
}
