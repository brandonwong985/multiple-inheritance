// Brandon Wong
// 12/4/2021

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace P5
{
    [TestClass]
    public class entreeTest
    {
        [TestMethod]
        public void Test_Constructor_ValidObj()
        {
            //Arrange
            Entree obj = new Entree();
            //Act
            string name = obj.getEntree();
            string ingredients = obj.getIngredients();
            string contains = obj.getContains();
            //Assert
            Assert.AreEqual(name, "");
            Assert.AreEqual(ingredients, "");
            Assert.AreEqual(contains, "");
        }

        [TestMethod]
        public void Test_CopyConstructor()
        {
            //Arrange
            Entree obj = new Entree();
            Entree copy = new Entree(obj);
            //Act
            string objName = obj.getEntree();
            string copyName = copy.getEntree();
            Assert.AreEqual(objName, copyName);
        }

        [TestMethod]
        public void Test_Constructor_Parameters()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree obj = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            //Act
            string name = obj.getEntree();
            string ingredients = obj.getIngredients();
            string contains = obj.getContains();
            //Assert
            Assert.AreEqual(name, entree_Global.DEFAULT_ENTREE);
            Assert.AreEqual(ingredients, entree_Global.DEFAULT_INGREDIENTS);
            Assert.AreEqual(contains, entree_Global.DEFAULT_CONTAINS);
        }

        [TestMethod]
        public void Test_getEntree_Valid()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree obj = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            //Act
            string name = obj.getEntree();
            //Assert
            Assert.AreEqual(name, entree_Global.DEFAULT_ENTREE);
        }

        [TestMethod]
        public void Test_getEntree_Empty()
        {
            //Arrange
            Entree obj = new Entree();
            //Act
            string name = obj.getEntree();
            //Asssert
            Assert.AreEqual(name, "");
        }

        [TestMethod]
        public void Test_getNutrionalFacts()
        {
            //Arrange
            Entree obj = new Entree();
            //Act
            bool status = true;
            int count = 0;
            while (count < entree_Global.NUM_NUTRITION_FACTS && status)
            {
                status = obj.getNutrionalFact(count++) >= 0 ? true : false;
            }
            //Assert
            Assert.IsTrue(status);
        }

        [TestMethod]
        public void Test_getIngredients_Empty()
        {
            //Arrange 
            Entree obj = new Entree();
            //Act
            string ingredients = obj.getIngredients();
            //Assert
            Assert.AreEqual(ingredients, "");
        }

        [TestMethod]
        public void Test_getIngredients_NonEmpty()
        {
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree obj = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            //Act
            string ingredients = obj.getIngredients();
            //Assert
            Assert.AreEqual(ingredients, entree_Global.DEFAULT_INGREDIENTS);
        }

        [TestMethod]
        public void Test_getContains_Empty()
        {
            //Arrange 
            Entree obj = new Entree();
            //Act
            string contains = obj.getContains();
            //Assert
            Assert.AreEqual(contains, "");
        }

        [TestMethod]
        public void Test_getContains_NonEmpty()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree obj = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            //Act
            string contains = obj.getContains();
            //Assert
            Assert.AreEqual(contains, entree_Global.DEFAULT_CONTAINS);
        }

        [TestMethod]
        public void Test_checkIngredient_True()
        {
            //Arrange
            Entree obj = new Entree();
            //Act
            bool status = obj.checkIngredient("");
            //Assert
            Assert.IsTrue(status);
        }

        [TestMethod]
        public void Test_checkIngredient_False()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree obj = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            //Act
            bool status = obj.checkIngredient("");
            //Assert
            Assert.IsFalse(status);
        }

        [TestMethod]
        public void Test_isExpired_False()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree obj = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            //Act
            bool status = obj.isExpired(entree_Global.SHELF_LIFE - 1);
            //Assert
            Assert.IsFalse(status);
        }

        [TestMethod]
        public void Test_isExpired_True()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree obj = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            //Act
            bool status = obj.isExpired(entree_Global.SHELF_LIFE + 1);
            //Assert
            Assert.IsTrue(status);
        }

        [TestMethod]
        public void Test_isSpoiled_False()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree obj = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, "", "");
            //Act
            bool status = obj.isSpoiled();
            //Assert
            Assert.IsFalse(status);
        }

        [TestMethod]
        public void Test_isSpoiled_True()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree obj = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.REFRIGERATE_INGREDIENTS[0], entree_Global.REFRIGERATE_INGREDIENTS[0]);
            //Act
            obj.setPower(false);
            bool status = obj.isSpoiled();
            //Assert
            Assert.IsTrue(status);
        }

        [TestMethod]
        public void Test_needsRefrigeration_False()
        {
            //Arrange
            Entree obj = new Entree();
            //Act
            bool status = obj.needsRefrigeration();
            //Assert
            Assert.IsFalse(status);
        }

        [TestMethod]
        public void Test_needsRefrigeration_True()
        {
            //Arrange
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree obj = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.REFRIGERATE_INGREDIENTS[0], entree_Global.REFRIGERATE_INGREDIENTS[0]);
            //Act
            bool status = obj.needsRefrigeration();
            //Assert
            Assert.IsTrue(status);
        }

        [TestMethod]
        public void Test_getPower_True()
        {
            //Arrange
            Entree obj = new Entree();
            //Act
            bool status = obj.getPower();
            //Assert
            Assert.IsTrue(status);
        }

        [TestMethod]
        public void Test_getPower_False()
        {
            //Arrange
            Entree obj = new Entree();
            //Act
            obj.setPower(false);
            bool status = obj.getPower();
            //Assert
            Assert.IsFalse(status);
        }
    }
}
