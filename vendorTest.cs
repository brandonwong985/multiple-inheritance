// Brandon Wong
// 12/4/2021

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace P5
{
    [TestClass]
    public class vendorTest
    {
        [TestMethod]
        public void Test_ItemConstructor_Empty()
        {
            //Arrange
            Item obj = new Item();
            //Act
            float price = obj.price;
            int quantity = obj.quantity;
            //Assert
            Assert.AreEqual(price, 0);
            Assert.AreEqual(quantity, 0);
        }

        [TestMethod]
        public void Test_ItemConstructor_PassByValue()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Item newItem = new Item(newEntree);
            //Act
            string name = newItem.entreeItem.getEntree();
            int quantity = newItem.quantity;
            //Assert
            Assert.AreEqual(name, entree_Global.DEFAULT_ENTREE);
            Assert.AreEqual(quantity, vendor_Global.DEFAULT_NUM_ENTREES);
        }

        [TestMethod]
        public void Test_VendorConstructor_Empty()
        {
            //Arrange             
            Vendor obj = new Vendor();
            //Act
            bool valid = obj.isOpenForBusiness();
            //Assert
            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void Test_VendorConstructor_InvalidFile()
        {
            //Arrange (has error checking, so if invalid file is passed in, will initialize as empty)
            //         this ensures an invalid obj cannot be made 
            Vendor obj = new Vendor("");
            //Act
            bool valid = obj.isOpenForBusiness();
            //Assert
            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void Test_LoadMenu_True()
        {
            //Arrange 
            Vendor obj = new Vendor();
            //Act (assumes the default file path was set up successfully)
            bool result = obj.LoadMenu(Driver_Global.DEFAULT_FILE);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_LoadMenu_False()
        {
            //Arrange
            Vendor obj = new Vendor();
            //Act
            bool result = obj.LoadMenu("");
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CopyConstructor_Valid()
        {
            //Arrange
            Vendor obj = new Vendor();
            Vendor copy = new Vendor(obj);
            //Act
            bool objOpen = obj.isOpenForBusiness();
            bool copyOpen = copy.isOpenForBusiness();
            string name = obj.getRandomEntree();
            bool objStocked = obj.isStocked(name);
            bool copyStocked = copy.isStocked(name);
            string notName = "";
            bool objNotStocked = obj.isStocked(notName);
            bool copyNotStocked = copy.isStocked(notName);
            //Assert
            Assert.AreEqual(objOpen, copyOpen);
            Assert.AreEqual(objStocked, copyStocked);
            Assert.AreEqual(objNotStocked, copyNotStocked);
        }

        [TestMethod]
        public void Test_Load_Valid()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            bool inStock = obj.isStocked(entree_Global.DEFAULT_ENTREE);
            //Assert
            Assert.IsTrue(inStock);
        }

        [TestMethod]
        public void Test_PowerOutageCleanStock()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.REFRIGERATE_INGREDIENTS[0], entree_Global.REFRIGERATE_INGREDIENTS[0]);
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            int before = obj.getUniqueEntrees();
            obj.PowerOutage();
            obj.CleanStock();
            int after = obj.getUniqueEntrees();
            //Assert
            Assert.AreNotEqual(before, after);
        }

        [TestMethod]
        public void Test_IsStocked_True()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.REFRIGERATE_INGREDIENTS[0], entree_Global.REFRIGERATE_INGREDIENTS[0]);
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            string name = obj.getRandomEntree();
            bool result = obj.isStocked(name);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_IsStocked_False()
        {
            //Arrange
            Vendor obj = new Vendor();
            //Act
            string name = "";
            bool result = obj.isStocked(name);
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_getPrice_Valid()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.REFRIGERATE_INGREDIENTS[0], entree_Global.REFRIGERATE_INGREDIENTS[0]);
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            string name = obj.getRandomEntree();
            float price = obj.getPrice(name);
            bool result = price >= 0 ? true : false;
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_getPrice_Invalid()
        {
            //Arrange
            Vendor obj = new Vendor();
            //Act
            string name = "";
            float price = obj.getPrice(name);
            bool result = price >= 0 ? true : false;
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_isOpenForBusiness_True()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.REFRIGERATE_INGREDIENTS[0], entree_Global.REFRIGERATE_INGREDIENTS[0]);
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            bool status = obj.isOpenForBusiness();
            //Assert
            Assert.IsTrue(status);
        }

        [TestMethod]
        public void Test_isOpenForBusiness_False()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.REFRIGERATE_INGREDIENTS[0], entree_Global.REFRIGERATE_INGREDIENTS[0]);
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            obj.PowerOutage();
            obj.CleanStock();
            bool status = obj.isOpenForBusiness();
            //Assert
            Assert.IsFalse(status);
        }

        [TestMethod]
        public void Test_Sell_Sucess()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor obj = new Vendor();
            Customer buyer = new Customer(vendor_Global.PRICE_HIGH * 2);
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            string name = obj.getRandomEntree();
            bool status = obj.Sell(buyer, name);
            //Assert
            Assert.IsTrue(status);
        }

        [TestMethod]
        public void Test_Sell_Fail()
        {
            //Arrange
            Vendor obj = new Vendor();
            Customer buyer = new Customer(vendor_Global.PRICE_LOW / 2);
            //Act
            string name = obj.getRandomEntree();
            bool status = obj.Sell(buyer, name);
            //Assert
            Assert.IsFalse(status);
        }

        [TestMethod]
        public void Test_getCarbs_True()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            string name = obj.getRandomEntree();
            float carbs = obj.getCarbs(name);
            bool status = carbs == entree_Global.DEFAULT_NUTFACT ? true : false;
            //Assert
            Assert.IsTrue(status);
        }

        [TestMethod]
        public void Test_getCarbs_False()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            string name = obj.getRandomEntree();
            float carbs = obj.getCarbs(name);
            bool status = carbs != entree_Global.DEFAULT_NUTFACT ? true : false;
            //Assert
            Assert.IsFalse(status);
        }

        [TestMethod]
        public void Test_getSugars_True()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            string name = obj.getRandomEntree();
            float sugars = obj.getSugars(name);
            bool status = sugars == entree_Global.DEFAULT_NUTFACT ? true : false;
            //Assert
            Assert.IsTrue(status);
        }

        [TestMethod]
        public void Test_getSugars_False()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            string name = obj.getRandomEntree();
            float sugars = obj.getSugars(name);
            bool status = sugars != entree_Global.DEFAULT_NUTFACT ? true : false;
            //Assert
            Assert.IsFalse(status);
        }

        [TestMethod]
        public void Test_getQuantity_True()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            string name = obj.getRandomEntree();
            int quantity = obj.getQuantity(name);
            bool status = quantity == vendor_Global.DEFAULT_NUM_ENTREES ? true : false;
            //Assert
            Assert.IsTrue(status);
        }

        [TestMethod]
        public void Test_getQuantity_False()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            string name = obj.getRandomEntree();
            int quantity = obj.getQuantity(name);
            bool status = quantity != vendor_Global.DEFAULT_NUM_ENTREES ? true : false;
            //Assert
            Assert.IsFalse(status);
        }

        [TestMethod]
        public void Test_getRandomEntree_Success()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree("Carrots", nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            string name = obj.getRandomEntree();
            //Assert
            Assert.AreEqual(name, "Carrots");
        }

        [TestMethod]
        public void Test_getRandomEntree_Fail()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree("Carrots", nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            string name = obj.getRandomEntree();
            //Assert
            Assert.AreNotEqual(name, "Cake");
        }

        [TestMethod]
        public void Test_getIngredients_Success()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            string name = obj.getRandomEntree();
            string ingredients = obj.getIngredients(name);
            //Assert
            Assert.AreEqual(ingredients, entree_Global.DEFAULT_INGREDIENTS);
        }

        [TestMethod]
        public void Test_getIngredients_Fail()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, "milk", entree_Global.DEFAULT_CONTAINS);
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            string name = obj.getRandomEntree();
            string ingredients = obj.getIngredients(name);
            //Assert
            Assert.AreNotEqual(ingredients, "flour");
        }

        [TestMethod]
        public void Test_getContains_True()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, "nuts");
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            string name = obj.getRandomEntree();
            string contains = obj.getContains(name);
            //Assert
            Assert.AreEqual(contains, "nuts");
        }

        [TestMethod]
        public void Test_getContains_False()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, "nuts");
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            string name = obj.getRandomEntree();
            string contains = obj.getContains(name);
            //Assert
            Assert.AreNotEqual(contains, "milk");
        }

        [TestMethod]
        public void Test_getUniqueEntrees_Nonempty()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            int num = obj.getUniqueEntrees();
            bool result = num > 0 ? true : false;
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_getUniqueEntrees_Empty()
        {
            //Arrange
            Vendor obj = new Vendor();
            //Act
            int num = obj.getUniqueEntrees();
            bool result = num > 0 ? true : false;
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_getNumTotalEntrees_Nonempty()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            int num = obj.getNumTotalEntrees();
            bool result = num > 0 ? true : false;
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_getNumTotalEntrees_Empty()
        {
            //Arrange
            Vendor obj = new Vendor();
            //Act
            int num = obj.getNumTotalEntrees();
            bool result = num > 0 ? true : false;
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_getNutritionalFacts_True()
        {
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            float testNutFact = obj.getNutritionalFacts(obj.getRandomEntree(), 0);
            //Assert
            Assert.AreEqual(testNutFact, entree_Global.DEFAULT_NUTFACT);
        }

        [TestMethod]
        public void Test_getNutritionalFacts_False()
        {
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT + i + 1;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Vendor obj = new Vendor();
            //Act
            obj.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            float testNutFact = obj.getNutritionalFacts(obj.getRandomEntree(), 0);
            //Assert
            Assert.AreNotEqual(testNutFact, entree_Global.DEFAULT_NUTFACT);
        }
    }
}
