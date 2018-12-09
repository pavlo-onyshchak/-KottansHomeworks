using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KottansTasks.Tests
{
    [TestClass]
    public class KottansTests
    {
        [TestMethod]
        public void Test_GenerateNextCreditCard_function()
        {
            //arrange
            string number = "358999999999999992";
            CreditCard card = new CreditCard(number);
            
            string card_number;
            //act
            try
            {
                card_number = card.GenerateNext(number);
            }
            catch(Exception ex)
            {
                Assert.AreEqual("no more card numbers available for this vendor", ex.Message);
            }

            //assert
           
        }
        [TestMethod]
         
        public void Test_GetVendor_function()
        {
            //arrange
            string number = "4539145406914866";
            bool actual;
            bool expected = true;

            //act
            CreditCard card = new CreditCard(number);
            actual = card.IsValid(number);

            //assert
            Assert.AreEqual(actual, expected);
        }

    }
}
