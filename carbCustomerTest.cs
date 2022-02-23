// Brandon Wong
// 12/4/2021

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace P5
{
    [TestClass]
    public class carbCustomerTest
    {
        [TestMethod]
        public void Test_DefaultConstructor_Valid()
        {
            //Arrange
            carbCustomer obj = new carbCustomer();
            //Act
            float carb = obj.getCurrentCarbCount();
            float balance = obj.getBalance();
            //Assert
            Assert.AreEqual(carb, 0);
            Assert.AreEqual(balance, customer_Global.DEFAULT_BALANCE);
        }

        [TestMethod]
        public void Test_ParameterConstructor_Valid()
        {
            //Arange
            carbCustomer obj = new carbCustomer(carb_Global.DEFAULT_DAILY_CARB_LIMIT);
            //Act
            float carb = obj.getCurrentCarbCount();
            float balance = obj.getBalance();
            //Assert
            Assert.AreEqual(carb, 0);
            Assert.AreEqual(balance, customer_Global.DEFAULT_BALANCE);
        }

        [TestMethod]
        public void Test_getCurrentCarbCount_Initial()
        {
            //Arrange 
            carbCustomer obj = new carbCustomer();
            //Act
            float carb = obj.getCurrentCarbCount();
            //Assert
            Assert.AreEqual(carb, 0);
        }

        [TestMethod]
        public void Test_getCurrentCarbCount_AfterPurchase()
        {
            //Arrange
            carbCustomer obj = new carbCustomer();
            Vendor vendor1 = new Vendor();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = 0;
            }
            //Act
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            vendor1.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            obj.addMoney(customer_Global.DEFAULT_AMOUNT);
            float before = obj.getCurrentCarbCount();
            obj.BuyOne(vendor1, vendor1.getRandomEntree());
            float after = obj.getCurrentCarbCount();
            bool result = after >= before ? true : false;
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_BuyOne_Success()
        {
            //Arrange
            carbCustomer obj = new carbCustomer(1000);
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
            carbCustomer obj = new carbCustomer(10);
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
            carbCustomer obj = new carbCustomer(1000);
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
            carbCustomer obj = new carbCustomer(10);
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
            carbCustomer obj = new carbCustomer(1000);
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
            carbCustomer obj = new carbCustomer(10);
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
