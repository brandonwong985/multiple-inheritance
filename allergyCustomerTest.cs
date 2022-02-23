// Brandon Wong
// 12/4/2021

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace P5
{
    [TestClass]
    public class allergyCustomerTest
    {
        [TestMethod]
        public void Test_DefaultConstructor_Valid()
        {
            //Arrange
            allergyCustomer obj = new allergyCustomer();
            //Act
            float balance = obj.getBalance();
            //Assert
            Assert.AreEqual(balance, customer_Global.DEFAULT_BALANCE);
        }

        [TestMethod]
        public void Test_ParameterConstructor_Valid()
        {
            //Arange
            allergyCustomer obj = new allergyCustomer(allergy_Global.DEFAULT_ALLERGY, allergy_Global.DEFAULT_SEVERELY_ALLERGIC);
            //Act          
            float balance = obj.getBalance();
            bool allergy = obj.isSeverelyAllergic();
            //Assert
            Assert.AreEqual(balance, customer_Global.DEFAULT_BALANCE);
            Assert.AreEqual(allergy, allergy_Global.DEFAULT_SEVERELY_ALLERGIC);
        }

        [TestMethod]
        public void Test_BuyOne_Success()
        {
            //Arrange
            allergyCustomer obj = new allergyCustomer("milk");
            Vendor vendor1 = new Vendor();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            //Act
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, "", "");
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
            allergyCustomer obj = new allergyCustomer("nuts");
            Vendor vendor1 = new Vendor();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            //Act
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, "nuts", "nuts");
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
            allergyCustomer obj = new allergyCustomer("milk");
            Vendor vendor1 = new Vendor();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            //Act
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, "", "");
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
            allergyCustomer obj = new allergyCustomer("nuts");
            Vendor vendor1 = new Vendor();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            //Act
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, "nuts", "nuts");
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
            allergyCustomer obj = new allergyCustomer("milk");
            Vendor vendor1 = new Vendor();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            //Act
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, "", "");
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
            allergyCustomer obj = new allergyCustomer("nuts");
            Vendor vendor1 = new Vendor();
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            //Act
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, "nuts", "nuts");
            vendor1.Load(newEntree, vendor_Global.NUM_ENTREES_IN_A_MEAL * vendor_Global.NUM_ENTREES_IN_A_MEAL);
            obj.addMoney(customer_Global.DEFAULT_AMOUNT);
            bool result = obj.canPurchase(vendor1, vendor1.getRandomEntree());
            //Assert
            Assert.IsFalse(result);
        }
    }
}
