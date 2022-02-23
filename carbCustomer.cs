// Brandon Wong
// 12/4/2021
//
// Class invariant: The carbCustomer class demonstrates an inheritance relationship
// with the customer class. A carbCustomer Is-A customer, so it can do everything 
// a customer can with some restrictions on their carbohydrate intake. Each
// carbCustomer keeps track of basic things a normal customer can, like account
// number and balance, in addition to their their daily carbohydrate limit and current
// carbohydrate count. Error checking happens to make sure valid values are passed in
// when constructing a carbCustomer, if invalid will use the provided global default.
// A carbCustomer can choose to buy one specific entree or buy a meal of multiple entrees
// (if it meets their dietary restrictions and they have enough money for it). In addition
// to the P3 class, I added the support of an interface, CustomerI, to ensure we have a 
// consistent interface and can have a heterogenous collection of CustomerI in the P5 driver.
//
// Interface Invariant: The carbCustomer class has a consistent interface with the base
// customer class. The client can perform a number of methods on a carbCustomer object like
// getting account number, balance, and current carb count. Since a carbCustomer Is-A 
// Customer, it can also BuyOne and Buy entrees/meals as long as it meets their dietary restrictions.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5
{
    public static class carb_Global
    {
        public const float DEFAULT_DAILY_CARB_LIMIT = 30;
    }
    public class carbCustomer : Customer, CustomerI
    {
        protected float dailyCarbLimit;
        protected float currentCarbCount;
        public carbCustomer(float input = carb_Global.DEFAULT_DAILY_CARB_LIMIT)
        {
            if (input > 0) dailyCarbLimit = input;
            else input = carb_Global.DEFAULT_DAILY_CARB_LIMIT;
            currentCarbCount = 0;
            balance = 0;
        }
        public carbCustomer(carbCustomer c)
        {
            dailyCarbLimit = c.dailyCarbLimit;
            currentCarbCount = c.currentCarbCount;
            balance = c.balance;
            for(int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutritionInfo[i] = c.nutritionInfo[i];
            }
        }
        // Pre-condition: called on a valid carbCustomer
        // Post-condition: return their current carb count as float
        public float getCurrentCarbCount() { return currentCarbCount; }
        // Pre-condition: called on a valid carbCustomer, valid vendor passed in with string
        // Post-condition: return true if entree was successfully purchased, false if failed
        public override bool BuyOne(Vendor seller, string entreeName)
        {
            if (seller.isOpenForBusiness() && seller.isStocked(entreeName) && seller.getCarbs(entreeName) != -1)
            {
                float carbs = currentCarbCount + seller.getCarbs(entreeName);
                if (carbs <= dailyCarbLimit)
                {
                    if (base.BuyOne(seller, entreeName))
                    {
                        currentCarbCount = carbs;
                        return true;
                    }
                }
            }
            return false;
        }
        // Pre-condition: called on a valid carbCustomer, valid vendor passed in 
        // Post-condition: return true if meal was successfully purchased, false if failed
        public new bool Buy(Vendor seller)
        {
            if (seller.isOpenForBusiness() && seller.getNumTotalEntrees() >= vendor_Global.NUM_ENTREES_IN_A_MEAL)
            {
                string[] items = new string[vendor_Global.NUM_ENTREES_IN_A_MEAL];
                float[] carb = new float[vendor_Global.NUM_ENTREES_IN_A_MEAL];
                float carbs = 0;
                float totalPrice = 0;
                for (int i = 0; i < vendor_Global.NUM_ENTREES_IN_A_MEAL; i++)
                {
                    items[i] = seller.getRandomEntree();
                    carb[i] = seller.getCarbs(items[i]);
                    carbs += carb[i];
                    totalPrice += seller.getPrice(items[i]);
                }
                if (balance >= totalPrice && currentCarbCount + carbs <= dailyCarbLimit)
                {
                    bool result = true;
                    foreach (string meal in items)
                    {
                        if (!base.BuyOne(seller, meal)) result = false;
                    }
                    if (result)
                    {
                        currentCarbCount += carbs;
                        return true;
                    }
                }
            }
            return false;
        }
        // Pre-condition: called on a valid carbCustomer object, valid input provided
        // Post-condition: return true if entree is able to purchase, false if violates dietary restrictions
        public override bool canPurchase(Vendor v, string s)
        {
            bool result = true;
            v.CleanStock();
            float carbs = currentCarbCount + v.getCarbs(s);
            if (!v.isStocked(s) || carbs > dailyCarbLimit) result = false;

            return result;
        }
    }
}
// Implementation invariant: The carbCustomer class is implemented using an inheritance
// relationship with the base Customer class. A carbCustomer can be instantiated with
// passed in values, or use the global defaults if none/not enough are provided. Since
// a carbCustomer is-A customer, their information is very important and should be kept 
// private. Error checking is done throughout to make sure a carbCustomer cannot be 
// created in an invalid state. I also chose to support an interface with CustomerI so 
// so all carbCustomer objects will have a consistent interface with the base Customer
// class, ensuring we can have a heterogenous collection in the P5 driver.
