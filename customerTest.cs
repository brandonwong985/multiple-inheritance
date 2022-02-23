// Brandon Wong
// 12/4/2021

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace P5
{
    [TestClass]
    public class customerTest
    {
        [TestMethod]
        public void Test_Constructor_Valid()
        {
            //Arrange
            Customer obj = new Customer();
            //Act
            float balance = obj.getBalance();
            //Assert
            Assert.AreEqual(balance, customer_Global.DEFAULT_BALANCE);
        }

        [TestMethod]
        public void Test_CopyConstructor_Valid()
        {
            //Arrange
            Customer obj = new Customer();
            //Act
            obj.addMoney(100);
            Customer copyOfObj = new Customer(obj);
            float objBal = obj.getBalance();
            float copyBal = copyOfObj.getBalance();
            //Assert
            Assert.AreEqual(objBal, copyBal);
        }

        [TestMethod]
        public void Test_AddMoney_Valid()
        {
            //Arrange
            Customer obj = new Customer();
            //Act
            float before = obj.getBalance();
            obj.addMoney(customer_Global.DEFAULT_AMOUNT);
            float after = obj.getBalance();
            Assert.AreEqual(before + customer_Global.DEFAULT_AMOUNT, after);
        }

        [TestMethod]
        public void Test_AddMoney_Invalid()
        {
            //Arrange
            Customer obj = new Customer();
            //Act
            float before = obj.getBalance();
            obj.addMoney(-10);
            float after = obj.getBalance();
            //Assert
            Assert.AreEqual(before + customer_Global.DEFAULT_AMOUNT, after);
        }

        [TestMethod]
        public void Test_getAccountNum_Valid()
        {
            //Arrange
            const int size = 10;
            Customer[] obj = new Customer[size];
            for (int i = 0; i < size; i++)
            {
                obj[i] = new Customer();
            }
            //Act
            int num = obj[size - 1].getAccountNum();
            //Assert
            Assert.AreEqual(num, customer_Global.NUM_CUSTOMERS);
        }

        [TestMethod]
        public void Test_getBalance_Valid()
        {
            //Arrange
            Customer obj = new Customer();
            //Act
            float num = obj.getBalance();
            //Assert
            Assert.AreEqual(num, customer_Global.DEFAULT_BALANCE);
        }

        [TestMethod]
        public void Test_BuyOne_Success()
        {
            //Arrange
            Customer obj = new Customer();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor vendor1 = new Vendor();
            //Act
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
            Customer obj = new Customer();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor vendor1 = new Vendor();
            //Act
            vendor1.Load(newEntree, vendor_Global.NUM_ENTREES_IN_A_MEAL * vendor_Global.NUM_ENTREES_IN_A_MEAL);
            bool result = obj.BuyOne(vendor1, vendor1.getRandomEntree());
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_Buy_SuccessfulPurchase()
        {
            //Arrange
            Customer obj = new Customer();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor vendor1 = new Vendor();
            //Act
            vendor1.Load(newEntree, vendor_Global.NUM_ENTREES_IN_A_MEAL * vendor_Global.NUM_ENTREES_IN_A_MEAL);
            obj.addMoney(customer_Global.DEFAULT_AMOUNT);
            bool result = obj.Buy(vendor1);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_Buy_UnsuccessfulPurchase()
        {
            //Arrange
            Customer obj = new Customer();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor vendor1 = new Vendor();
            //Act
            vendor1.Load(newEntree, vendor_Global.NUM_ENTREES_IN_A_MEAL * vendor_Global.NUM_ENTREES_IN_A_MEAL);
            bool result = obj.Buy(vendor1);
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CanPurchase_True()
        {
            //Arrange
            Customer obj = new Customer(vendor_Global.PRICE_HIGH);
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor vendor1 = new Vendor();
            //Act
            vendor1.Load(newEntree, vendor_Global.NUM_ENTREES_IN_A_MEAL * vendor_Global.NUM_ENTREES_IN_A_MEAL);
            obj.addMoney(vendor_Global.PRICE_HIGH);
            bool result = obj.canPurchase(vendor1, vendor1.getRandomEntree());
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_CanPurchase_False()
        {
            //Arrange
            Customer obj = new Customer(vendor_Global.PRICE_LOW/2);
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor vendor1 = new Vendor();
            //Act
            vendor1.Load(newEntree, vendor_Global.NUM_ENTREES_IN_A_MEAL * vendor_Global.NUM_ENTREES_IN_A_MEAL);
            bool result = obj.canPurchase(vendor1, vendor1.getRandomEntree());
            //Assert
            Assert.IsTrue(result);
        }
    }
}
