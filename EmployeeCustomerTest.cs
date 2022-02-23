// Brandon Wong
// 12/4/2021

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace P5
{
    [TestClass]
    public class EmployeeCustomerTest
    {
        [TestMethod]
        public void Test_EmptyConstructor_Valid()
        {
            //Arrange
            EmployeeCustomer obj = new EmployeeCustomer();
            //Act
            int objPayLevel = obj.checkPayLevel();
            //Assert
            Assert.AreEqual(objPayLevel, Employee_Global.PAY_LEVEL_ZERO);
        }
        
        [TestMethod]
        public void Test_VendorConstructor_Valid()
        {
            //Arrange
            Vendor vendor1 = new Vendor();
            EmployeeCustomer obj = new EmployeeCustomer(vendor1);
            //Act
            bool result = obj.checkPayLevel() != 0 ? true : false;
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_CustomerConstructor_Valid()
        {
            //Arrange
            Customer customer1 = new Customer();
            EmployeeCustomer obj = new EmployeeCustomer(customer1);
            //Act
            bool result = obj.getAccountNum() != -1 ? true : false;
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_CustomerConstructor_Invalid()
        {
            //Arrange
            Customer customer1 = null;
            EmployeeCustomer obj = new EmployeeCustomer(customer1);
            //Act
            bool result = obj.getAccountNum() != -1 ? true : false;
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CustomerVendorPayLevelConstructor_Valid()
        {
            //Arrange
            Customer customer1 = new Customer();
            Vendor vendor1 = new Vendor();
            EmployeeCustomer obj = new EmployeeCustomer(customer1, vendor1, Employee_Global.PAY_LEVEL_ONE);
            //Act
            bool result = obj.checkPayLevel() == Employee_Global.PAY_LEVEL_ONE ? true : false;
            bool result2 = obj.getAccountNum() != -1 ? true : false;
            //Assert
            Assert.IsTrue(result);
            Assert.IsTrue(result2);
        }

        [TestMethod]
        public void Test_CustomerVendorPayLevelConstructor_Invalid()
        {
            //Arrange
            Customer customer1 = null;
            Vendor vendor1 = null;
            EmployeeCustomer obj = new EmployeeCustomer(customer1, vendor1, Employee_Global.PAY_LEVEL_ONE);
            //Act
            bool result = obj.checkPayLevel() == Employee_Global.PAY_LEVEL_ONE ? true : false;
            bool result2 = obj.getAccountNum() != -1 ? true : false;
            //Assert
            Assert.IsFalse(result);
            Assert.IsFalse(result2);
        }

        [TestMethod]
        public void Test_switchCustomer_True()
        {
            //Arrange
            Customer customer1 = new Customer();
            Customer customer2 = new Customer();
            EmployeeCustomer obj = new EmployeeCustomer(customer1);
            //Act
            bool result = obj.switchCustomer(customer2);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_switchCustomer_False()
        {
            //Arrange
            Customer customer1 = new Customer();
            Customer customer2 = null;
            EmployeeCustomer obj = new EmployeeCustomer(customer1);
            //Act
            bool result = obj.switchCustomer(customer2);
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_getAccountNum_True()
        {
            //Arrange
            Customer customer1 = new Customer();
            EmployeeCustomer obj = new EmployeeCustomer(customer1);
            //Act
            bool result = obj.getAccountNum() != -1 ? true : false;
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_getAccountNum_False()
        {
            //Arrange
            Customer customer1 = null;
            EmployeeCustomer obj = new EmployeeCustomer(customer1);
            //Act
            bool result = obj.getAccountNum() != -1 ? true : false;
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_getBalance_Valid()
        {
            //Arrange
            Customer customer1 = new Customer();
            EmployeeCustomer obj = new EmployeeCustomer(customer1);
            //Act
            bool result = obj.getBalance() != -1 ? true : false;
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_getBalance_Invalid()
        {
            //Arrange
            Customer customer1 = null;
            EmployeeCustomer obj = new EmployeeCustomer(customer1);
            //Act
            bool result = obj.getBalance() != -1 ? true : false;
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_BuyOne_True()
        {
            //Arrange
            Customer customer1 = new Customer();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor vendor1 = new Vendor();         
            EmployeeCustomer obj = new EmployeeCustomer(customer1, vendor1);
            //Act
            vendor1.Load(newEntree, vendor_Global.NUM_ENTREES_IN_A_MEAL * vendor_Global.NUM_ENTREES_IN_A_MEAL);
            obj.addMoney(customer_Global.DEFAULT_AMOUNT);
            bool result = obj.BuyOne(vendor1, vendor1.getRandomEntree());
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_BuyOne_False()
        {
            //Arrange
            Customer customer1 = new Customer();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor vendor1 = new Vendor();
            EmployeeCustomer obj = new EmployeeCustomer(customer1);
            //Act
            vendor1.Load(newEntree, vendor_Global.NUM_ENTREES_IN_A_MEAL * vendor_Global.NUM_ENTREES_IN_A_MEAL);
            bool result = obj.BuyOne(vendor1, vendor1.getRandomEntree());
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_Buy_True()
        {
            //Arrange
            Customer customer1 = new Customer();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor vendor1 = new Vendor();
            EmployeeCustomer obj = new EmployeeCustomer(customer1, vendor1);
            //Act
            vendor1.Load(newEntree, vendor_Global.NUM_ENTREES_IN_A_MEAL * vendor_Global.NUM_ENTREES_IN_A_MEAL);
            obj.addMoney(customer_Global.DEFAULT_AMOUNT);
            bool result = obj.Buy(vendor1);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_Buy_False()
        {
            //Arrange
            Customer customer1 = new Customer();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor vendor1 = new Vendor();
            EmployeeCustomer obj = new EmployeeCustomer(customer1);
            //Act
            vendor1.Load(newEntree, vendor_Global.NUM_ENTREES_IN_A_MEAL * vendor_Global.NUM_ENTREES_IN_A_MEAL);
            bool result = obj.Buy(vendor1);
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_canPurchase_True()
        {
            //Arrange
            Customer customer1 = new Customer();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor vendor1 = new Vendor();
            EmployeeCustomer obj = new EmployeeCustomer(customer1, vendor1);
            //Act
            vendor1.Load(newEntree, vendor_Global.NUM_ENTREES_IN_A_MEAL * vendor_Global.NUM_ENTREES_IN_A_MEAL);
            obj.addMoney(customer_Global.DEFAULT_AMOUNT);
            bool result = obj.canPurchase(vendor1, vendor1.getRandomEntree());
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_canPurchase_False()
        {
            //Arrange
            Customer customer1 = new Customer();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor vendor1 = new Vendor();
            EmployeeCustomer obj = new EmployeeCustomer(customer1);
            //Act
            vendor1.Load(newEntree, vendor_Global.NUM_ENTREES_IN_A_MEAL * vendor_Global.NUM_ENTREES_IN_A_MEAL);
            bool result = obj.canPurchase(vendor1, vendor1.getRandomEntree());
            //Assert
            Assert.IsFalse(result);
        }
    }
}
