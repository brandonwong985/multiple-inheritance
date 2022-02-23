// Brandon Wong
// 12/4/2021
//
// Class invariant: The entree class encapsulates an entree and it's data. Each
// entree has a name, nutrional facts, ingredients and contains. Error checking is done
// to make sure an entree cannot be intialized as invalid, as entrees will be intialized 
// as empty if invalid.
//
// Interface Invariant: The client can perform expected on entree objects like the real 
// world such as, but not limited to, checking if the entree has expired or spoiled, 
// getting nutrional values, ingredients, contains, etc. This class shows stable and
// consistent interface because the fields and methods are the same from previous legacy
// code from P1 and P2 with minor adjustments when switching between languages.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5
{
    public static class entree_Global
    {
        public const int NUM_NUTRITION_FACTS = 11;
        public const int SHELF_LIFE = 3;
        public const int CURRENT_DAY = 2;
        public const int ALTERNATE_DAY = 5;
        public static string[] REFRIGERATE_INGREDIENTS = { "milk", "eggs", "meat" };

        public const string DEFAULT_ENTREE = "Cake";
        public const float DEFAULT_NUTFACT = 10;
        public const string DEFAULT_INGREDIENTS = "flour$milk$eggs$vanilla extract";
        public const string DEFAULT_CONTAINS = "milk$eggs";
    }
    public class Entree
    {
        private string entree;
        private float[] nutritionFacts;
        private string ingredients;
        private string contains;
        private DateTime expirationDate;
        private bool inRefrigerator;
        private bool powered;
        private bool spoiled;

        public Entree()
        {
            entree = "";
            nutritionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            foreach (int i in nutritionFacts)
            {
                nutritionFacts[i] = 0;
            }
            ingredients = "";
            contains = "";
            expirationDate = DateTime.Now.AddDays(entree_Global.SHELF_LIFE);
            inRefrigerator = false;
            powered = true;
            spoiled = false;
        }
        public Entree(Entree input)
        {
            entree = input.entree;
            nutritionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutritionFacts[i] = input.nutritionFacts[i];
            }
            ingredients = input.ingredients;
            contains = input.contains;
            expirationDate = input.expirationDate;
            inRefrigerator = input.inRefrigerator;
            powered = input.powered;
            spoiled = input.spoiled;
        }
        public Entree(string inEntree, float[] inNutFacts, string inIngredients, string inContains)
        {
            entree = inEntree;
            expirationDate = DateTime.Now.AddDays(entree_Global.SHELF_LIFE);
            inRefrigerator = false;
            powered = true;
            spoiled = false;
            nutritionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            if (inNutFacts == null)
            {
                for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++) nutritionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            else
            {
                for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++) nutritionFacts[i] = inNutFacts[i];
            }
            ingredients = inIngredients;
            string[] ingredient = inIngredients.Split(Driver_Global.SECOND_DELIMITER);
            foreach (string current in ingredient)
            {
                foreach (string check in entree_Global.REFRIGERATE_INGREDIENTS)
                {
                    if (current == check) inRefrigerator = true;
                }
            }
            contains = inContains;
            string[] contain = inContains.Split(Driver_Global.SECOND_DELIMITER);
            foreach (string current in contain)
            {
                foreach (string check in entree_Global.REFRIGERATE_INGREDIENTS)
                {
                    if (current == check) inRefrigerator = true;
                }
            }
        }
        // Pre-condition: called on a valid entree
        // Post-condition: return the name of the entree
        public string getEntree() { return entree; }
        // Pre-condition: called on a valid entree
        // Post-condition: return the desired nutrional fact
        public float getNutrionalFact(int num) { return nutritionFacts[num]; }
        // Pre-condition: called on a valid entree
        // Post-condition: return ingredients as a string
        public string getIngredients() { return ingredients; }
        // Pre-condition: called on a valid entree
        // Post-condition: return contains as a string
        public string getContains() { return contains; }       
        // Pre-condition: called on a valid entree
        // Post-condition: return true if ingredient was found within the entree,
        //                 return false if ingredient was not found within the entree
        public bool checkIngredient(string input)
        {
            string tempIngredient = ingredients;
            string[] ingredient = tempIngredient.Split(Driver_Global.SECOND_DELIMITER);
            foreach (string current in ingredient)
            {
                if (current == input) return true;
            }
            string tempContain = contains;
            string[] contain = tempContain.Split(Driver_Global.SECOND_DELIMITER);
            foreach (string current in contain)
            {
                if (current == input) return true;
            }
            return false;
        }
        // Pre-condition: called on a valid entree
        // Post-condition: return true if entree is expired, false if not expired
        public bool isExpired(int inCurrent = entree_Global.CURRENT_DAY)
        {
            DateTime current = DateTime.Now.AddDays(inCurrent);
            if (current < expirationDate) return false;
            else return true;
        }
        // Pre-condition: called on a valid entree
        // Post-condition: return true if entree is spoiled, false if not spoiled
        public bool isSpoiled(int inCurrent = entree_Global.CURRENT_DAY)
        {
            if (!isExpired(inCurrent))
            {
                if (inRefrigerator && !powered) spoiled = true;
                else spoiled = false;
            }
            else spoiled = true;
            return spoiled;
        }
        // Pre-condition: called on a valid entree
        // Post-condition: returns true if entree needs refrigeration, false if doesn't need refrigeration
        public bool needsRefrigeration() { return inRefrigerator; }
        // Pre-condition: called on a valid entree
        // Post-condition: sets the power of the entree based on input 
        public void setPower(bool input)
        {
            powered = input;
            if (inRefrigerator && !powered) spoiled = true;
        }
        // Pre-condition: called on a valid entree
        // Post-condition: returns true if powered, false if not powered
        public bool getPower() { return powered; }
    }
}
// Implementation Invariant: This class demonstrates the standard class implementation with
// private and public memebrs. Deep copying is supported with the class (through copy ctor since
// no overloaded assignment operator). Control of state is maintained because cannot create an
// invalid object because they are at the very least checked and made with empty strings and 0s.