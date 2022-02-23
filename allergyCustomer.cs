// Brandon Wong
// 12/4/2021
//
// Class invariant: The allergyCustomer class demonstrates an inheritance relationship
// with the customer class. An allergyCustomer Is-A customer, so it can do everything 
// a customer can with some restrictions based on their allergies. Each allergyCustomer
// keeps track of basic things a normal customer can, like account number and balance,
// in addition to their allergies and if they are serverly allergic or not. Error checking
// happens to make sure valid values are passed in when constructing an allergyCustomer, 
// if invalid will use the provided global default. An allergyCustomer can choose to
// buy one specific entree or buy a meal of multiple entrees. (if it meets their allergic
// restrictions). In addition to the previous P3, this allergyCustomer class supports an
// interface, CustomerI, which allows for a heterogenous collection of Customers in the P5 driver.
//
// Interface Invariant: The allergyCustomer class has a consistent interface with the base
// customer class. The client can perform a number of methods on an allergyCustomer object like
// getting account number, balance, and printing their allergies. Since an allergyCustomer Is-A 
// Customer, it can also BuyOne and Buy entrees/meals as long as it meets their allergy restrictions.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5
{
    public static class allergy_Global
    {
        public const string DEFAULT_ALLERGY = "nuts$milk";
        public const string ALTERNATE_ALLERGY = "eggs";
        public const bool DEFAULT_SEVERELY_ALLERGIC = true;
    }
    public class allergyCustomer : Customer, CustomerI
    {
        protected string allergy;
        protected bool severelyAllergic;

        public allergyCustomer(string input = allergy_Global.DEFAULT_ALLERGY, bool status = allergy_Global.DEFAULT_SEVERELY_ALLERGIC)
        {
            if (input == "") allergy = allergy_Global.DEFAULT_ALLERGY;
            else allergy = input;           
            severelyAllergic = status;
        }
        public allergyCustomer(allergyCustomer a)
        {
            allergy = a.allergy;
            severelyAllergic = a.severelyAllergic;
            balance = a.balance;
            for(int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutritionInfo[i] = a.nutritionInfo[i];
            }
        }
        // Pre-condition: called on valid allergyCustomer
        // Post-condition: returns allergy as a string
        public string getAllergy() { return allergy; }
        // Pre-condition: called on valid allergyCustomer
        // Post-condition: return true if severely allergic, false if not
        public bool isSeverelyAllergic() { return severelyAllergic; }
        // Pre-condition: called on a valid allergyCustomer, valid vendor passed in with string
        // Post-condition: return true if entree was successfully purchased, false if failed
        public override bool BuyOne(Vendor seller, string entreeName)
        {
            if (seller.isOpenForBusiness() && seller.isStocked(entreeName))
            {
                string[] ingredients = seller.getIngredients(entreeName).Split(Driver_Global.SECOND_DELIMITER);
                string[] contains = seller.getContains(entreeName).Split(Driver_Global.SECOND_DELIMITER);
                string[] myAllergy = allergy.Split(Driver_Global.SECOND_DELIMITER);
                bool safeToBuy = true;
                if (severelyAllergic)
                {
                    
                    foreach (string contain in contains)
                    {
                        foreach (string allergic in myAllergy)
                        {
                            if (allergic == contain) safeToBuy = false;
                        }
                    }
                }
                foreach (string ingredient in ingredients)
                {
                    foreach (string allergic in myAllergy)
                    {
                        if (allergic == ingredient) safeToBuy = false;
                    }
                }
                if (safeToBuy)
                {
                    if (base.BuyOne(seller, entreeName)) return true;
                }
            }
            return false;
        }
        // Pre-condition: called on a valid allergyCustomer, valid vendor passed in 
        // Post-condition: return true if meal was successfully purchased, false if failed
        public new bool Buy(Vendor seller)
        {
            if (seller.isOpenForBusiness() && seller.getNumTotalEntrees() >= vendor_Global.NUM_ENTREES_IN_A_MEAL)
            {
                string[] items = new string[vendor_Global.NUM_ENTREES_IN_A_MEAL];
                string[] allerIngredients = new string[vendor_Global.NUM_ENTREES_IN_A_MEAL];
                string[] allerContains = new string[vendor_Global.NUM_ENTREES_IN_A_MEAL];
                string[] myAllergy = allergy.Split(Driver_Global.SECOND_DELIMITER);
                bool safeToBuy = true;
                float totalPrice = 0;
                for (int i = 0; i < vendor_Global.NUM_ENTREES_IN_A_MEAL; i++)
                {
                    items[i] = seller.getRandomEntree();
                    allerIngredients[i] = seller.getIngredients(items[i]);
                    allerContains[i] = seller.getContains(items[i]);
                    totalPrice += seller.getPrice(items[i]);
                }
                if (severelyAllergic)
                {
                    foreach (string contains in allerContains)
                    {
                        string[] contain = contains.Split(Driver_Global.SECOND_DELIMITER);
                        foreach (string current in contain)
                        {
                            foreach (string allergic in myAllergy)
                            {
                                if (allergic == current) safeToBuy = false;
                            }
                        }
                    }
                }
                foreach (string ingredients in allerIngredients)
                {
                    string[] ingredient = ingredients.Split(Driver_Global.SECOND_DELIMITER);
                    foreach (string current in ingredient)
                    {
                        foreach (string allergic in myAllergy)
                        {
                            if (allergic == current) safeToBuy = false;
                        }
                    }
                }
                if (balance >= totalPrice && safeToBuy)
                {
                    bool result = true;
                    foreach (string meal in items)
                    {
                        if (!base.BuyOne(seller, meal)) result = false;
                    }
                    return result;
                }
            }
            return false;
        }
        // Pre-condition: called on a valid allergyCustomer object, valid input provided
        // Post-condition: return true if entree is able to purchase, false if violates dietary restrictions
        public override bool canPurchase(Vendor v, string s)
        {
            bool result = true;
            v.CleanStock();
            if (!v.isStocked(s)) result = false;
            string[] ingredients = v.getIngredients(s).Split(Driver_Global.SECOND_DELIMITER);
            string[] contains = v.getContains(s).Split(Driver_Global.SECOND_DELIMITER);
            string[] myAllergy = allergy.Split(Driver_Global.SECOND_DELIMITER);
            if (severelyAllergic)
            {

                foreach (string contain in contains)
                {
                    foreach (string allergic in myAllergy)
                    {
                        if (allergic == contain) result = false;
                    }
                }
            }
            foreach (string ingredient in ingredients)
            {
                foreach (string allergic in myAllergy)
                {
                    if (allergic == ingredient) result = false;
                }
            }           
            return result;
        }
    }
}
// Implementation Invariant: The allergyCustomer class is implemented using an inheritance
// relationship with the base Customer class. An allergyCustomer can be instantiated with
// passed in values, or use the global defaults if none/not enough are provided. Since
// an allergyCustomer is-A customer, their information is very important and should be kept 
// private. Error checking is done throughout to make sure an allergyCustomer cannot be 
// created in an invalid state. I also chose to support an interface with CustomerI so 
// so all allergyCustomer objects will have a consistent interface with the base Customer
// class, ensuring we can have a heterogenous collection in the P5 driver.