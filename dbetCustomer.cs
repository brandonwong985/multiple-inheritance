// Brandon Wong
// 12/4/2021
//
// Class invariant: The dbetCustomer class demonstrates an inheritance relationship
// with the customer class. A dbetCustomer Is-A customer, so it can do everything 
// a customer can with some restrictions on their sugar intake. Each dbetCustomer
// keeps track of basic things a normal customer can, like account number and balance,
// in addition to their their daily sugar limit and current sugar intake. In addition to 
// this, a dbetCustomer has extra restrictions on the amount of sugar an entree can have
// and the amount of sugar a meal can have before they can purchase it. Error checking
// happens to make sure valid values are passed in when constructing a dbetCustomer, 
// if invalid will use the provided global default. A dbetCustomer can choose to
// buy one specific entree or buy a meal of multiple entrees. (if it meets their dietary
// restrictions). In addition to the P3 class, I added the support of an interface, CustomerI,
// which ensures support of a heterogenous collection in the P5 driver.
//
// Interface Invariant: The dbetCustomer class has a consistent interface with the base
// customer class. The client can perform a number of methods on a dbetCustomer object like
// getting account number, balance, and current sugar count. Since a dbetCustomer Is-A 
// Customer, it can also BuyOne and Buy entrees/meals as long as it meets their dietary restrictions.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5
{
    public static class dbet_Global
    {
        public const float DEFAULT_DAILY_SUGAR_LIMIT = 50;
        public const float DEFAULT_ENTREE_SUGAR_LIMIT = 10;
        public const float DEFAULT_MEAL_SUGAR_LIMIT = 25;
    }
    public class dbetCustomer : Customer, CustomerI
    {
        protected float dailySugarLimit;
        protected float entreeSugarLimit;
        protected float mealSugarLimit;
        protected float currentSugarCount;
        public dbetCustomer(float dailySugar = dbet_Global.DEFAULT_DAILY_SUGAR_LIMIT, float entreeSugar = dbet_Global.DEFAULT_ENTREE_SUGAR_LIMIT, float mealSugar = dbet_Global.DEFAULT_MEAL_SUGAR_LIMIT)
        {
            if (dailySugar > entreeSugar && dailySugar > mealSugar && mealSugar > entreeSugar)
            {
                dailySugarLimit = dailySugar;
                entreeSugarLimit = entreeSugar;
                mealSugarLimit = mealSugar;
            }
            else
            {
                dailySugarLimit = dbet_Global.DEFAULT_DAILY_SUGAR_LIMIT;
                entreeSugarLimit = dbet_Global.DEFAULT_ENTREE_SUGAR_LIMIT;
                mealSugarLimit = dbet_Global.DEFAULT_MEAL_SUGAR_LIMIT;
            }
            currentSugarCount = 0;
            balance = 0;
        }
        public dbetCustomer(dbetCustomer dc)
        {
            dailySugarLimit = dc.dailySugarLimit;
            entreeSugarLimit = dc.entreeSugarLimit;
            mealSugarLimit = dc.mealSugarLimit;
            currentSugarCount = dc.currentSugarCount;
            balance = dc.balance;
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutritionInfo[i] = dc.nutritionInfo[i];
            }
        }
        // Pre-condition: called on a valid dbetCustomer
        // Post-condition: return their current sugar count as float
        public float getCurrentSugarCount() { return currentSugarCount; }
        // Pre-condition: called on a valid dbetCustomer, valid vendor passed in with string
        // Post-condition: return true if entree was successfully purchased, false if failed
        public override bool BuyOne(Vendor seller, string entreeName)
        {
            if (seller.isOpenForBusiness() && seller.isStocked(entreeName) && seller.getSugars(entreeName) != -1)
            {
                float sugars = currentSugarCount + seller.getSugars(entreeName);
                if (sugars <= entreeSugarLimit)
                {
                    if (base.BuyOne(seller, entreeName))
                    {
                        currentSugarCount = sugars;
                        return true;
                    }
                }
            }
            return false;
        }
        // Pre-condition: called on a valid dbetCustomer, valid vendor passed in 
        // Post-condition: return true if meal was successfully purchased, false if failed
        public new bool Buy(Vendor seller)
        {
            if (seller.isOpenForBusiness() && seller.getNumTotalEntrees() >= vendor_Global.NUM_ENTREES_IN_A_MEAL)
            {
                string[] items = new string[vendor_Global.NUM_ENTREES_IN_A_MEAL];
                float[] sugar = new float[vendor_Global.NUM_ENTREES_IN_A_MEAL];
                float sugars = 0;
                float totalPrice = 0;
                for (int i = 0; i < vendor_Global.NUM_ENTREES_IN_A_MEAL; i++)
                {
                    items[i] = seller.getRandomEntree();
                    sugar[i] = seller.getSugars(items[i]);
                    sugars += sugar[i];
                    totalPrice += seller.getPrice(items[i]);
                }
                if (balance >= totalPrice && currentSugarCount + sugars <= dailySugarLimit && sugars <= mealSugarLimit)
                {
                    bool result = true;
                    foreach (string meal in items)
                    {
                        if (!base.BuyOne(seller, meal)) result = false;
                    }
                    if (result)
                    {
                        currentSugarCount += sugars;
                        return true;
                    }
                }
            }
            return false;
        }
        // Pre-condition: called on a valid dbetCustomer object, valid input provided
        // Post-condition: return true if entree is able to purchase, false if violates dietary restrictions
        public override bool canPurchase(Vendor v, string s)
        {
            bool result = true;
            v.CleanStock();
            float sugars = currentSugarCount + v.getSugars(s);
            if (!v.isStocked(s) || sugars > dailySugarLimit) result = false;

            return result;
        }
    }
}
// Implementation Invariant: The dbetCustomer class is implemented using an inheritance
// relationship with the base Customer class. A dbetCustomer can be instantiated with
// passed in values, or use the global defaults if none/not enough are provided. Since
// a dbetcustomer is-A customer, their information is very important and should be kept 
// private. Error checking is done throughout to make sure a dbetcustomer cannot be 
// created in an invalid state. I also chose to support an interface with CustomerI so 
// so all dbetCustomer objects will have a consistent interface with the base Customer
// class, ensuring we can have a heterogenous collection in the P5 driver.
