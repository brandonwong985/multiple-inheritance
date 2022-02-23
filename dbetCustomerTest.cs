// Brandon Wong
// 12/4/2021

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace P5
{
    [TestClass]
    public class dbetCustomerTest
    {
        [TestMethod]
        public void Test_DefaultConstructor_Valid()
        {
            //Arrange
            dbetCustomer obj = new dbetCustomer();
            //Act
            float sugar = obj.getCurrentSugarCount();
            float balance = obj.getBalance();
            //Assert
            Assert.AreEqual(sugar, 0);
            Assert.AreEqual(balance, customer_Global.DEFAULT_BALANCE);
        }

        [TestMethod]
        public void Test_ParameterConstructor_Valid()
        {
            //Arange
            dbetCustomer obj = new dbetCustomer(dbet_Global.DEFAULT_DAILY_SUGAR_LIMIT,
                dbet_Global.DEFAULT_ENTREE_SUGAR_LIMIT, dbet_Global.DEFAULT_MEAL_SUGAR_LIMIT);
            //Act
            float sugar = obj.getCurrentSugarCount();
            float balance = obj.getBalance();
            //Assert
            Assert.AreEqual(sugar, 0);
            Assert.AreEqual(balance, customer_Global.DEFAULT_BALANCE);
        }

        [TestMethod]
        public void Test_getCurrentSugarCount_Initial()
        {
            //Arrange 
            dbetCustomer obj = new dbetCustomer();
            //Act
            float sugar = obj.getCurrentSugarCount();
            //Assert
            Assert.AreEqual(sugar, 0);
        }

        [TestMethod]
        public void Test_getCurrentSugarCount_AfterPurchase()
        {
            //Arrange
            dbetCustomer obj = new dbetCustomer();
            Vendor vendor1 = new Vendor();
            //Act
            obj.addMoney(customer_Global.DEFAULT_AMOUNT);
            float before = obj.getCurrentSugarCount();
            obj.BuyOne(vendor1, vendor1.getRandomEntree());
            float after = obj.getCurrentSugarCount();
            bool result = after >= before ? true : false;
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_BuyOne_Success()
        {
            //Arrange
            dbetCustomer obj = new dbetCustomer(10);
            Vendor vendor1 = new Vendor();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = 0;
            }
            //Act
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            vendor1.Load(newEntree, vendor_Global.NUM_ENTREES_IN_A_MEAL * vendor_Global.NUM_ENTREES_IN_A_MEAL);
            obj.addMoney(customer_Global.DEFAULT_AMOUNT);
            bool result = obj.BuyOne(vendor1, vendor1.getRandomEntree());
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_BuyOne_Fail()
        {
            //Arrange
            dbetCustomer obj = new dbetCustomer(10);
            Vendor vendor1 = new Vendor();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = 1000;
            }
            //Act
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            vendor1.Load(newEntree, vendor_Global.NUM_ENTREES_IN_A_MEAL * vendor_Global.NUM_ENTREES_IN_A_MEAL);
            obj.addMoney(customer_Global.DEFAULT_AMOUNT);
            bool result = obj.BuyOne(vendor1, vendor1.getRandomEntree());
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_Buy_Success()
        {
            //Arrange
            dbetCustomer obj = new dbetCustomer(10);
            Vendor vendor1 = new Vendor();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = 0;
            }
            //Act
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            vendor1.Load(newEntree, vendor_Global.NUM_ENTREES_IN_A_MEAL * vendor_Global.NUM_ENTREES_IN_A_MEAL);
            obj.addMoney(customer_Global.DEFAULT_AMOUNT);
            bool result = obj.Buy(vendor1);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_Buy_Fail()
        {
            //Arrange
            dbetCustomer obj = new dbetCustomer(10);
            Vendor vendor1 = new Vendor();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = 1000;
            }
            //Act
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            vendor1.Load(newEntree, vendor_Global.NUM_ENTREES_IN_A_MEAL * vendor_Global.NUM_ENTREES_IN_A_MEAL);
            obj.addMoney(customer_Global.DEFAULT_AMOUNT);
            bool result = obj.Buy(vendor1);
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CanPurchase_True()
        {
            //Arrange
            dbetCustomer obj = new dbetCustomer(10);
            Vendor vendor1 = new Vendor();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = 0;
            }
            //Act
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            vendor1.Load(newEntree, vendor_Global.NUM_ENTREES_IN_A_MEAL * vendor_Global.NUM_ENTREES_IN_A_MEAL);
            obj.addMoney(customer_Global.DEFAULT_AMOUNT);
            bool result = obj.canPurchase(vendor1, vendor1.getRandomEntree());
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_CanPurchase_False()
        {
            //Arrange
            dbetCustomer obj = new dbetCustomer(10);
            Vendor vendor1 = new Vendor();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = 1000;
            }
            //Act
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            vendor1.Load(newEntree, vendor_Global.NUM_ENTREES_IN_A_MEAL * vendor_Global.NUM_ENTREES_IN_A_MEAL);
            obj.addMoney(customer_Global.DEFAULT_AMOUNT);
            bool result = obj.canPurchase(vendor1, vendor1.getRandomEntree());
            //Assert
            Assert.IsFalse(result);
        }
    }
}
