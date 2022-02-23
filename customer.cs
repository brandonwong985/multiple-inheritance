// Brandon Wong
// 12/4/2021
//
// Class invariant: The Customer class is the base class for all the other variants of
// customers. Because of this, we want to have a consistent interface with all the other
// variants so we provded both Buy and BuyOne functions. A customer can perform basic 
// tasks that you would expect in the real world, like having an account number and 
// being able to get their balance. They are also able to purchase an entree or a meal of
// multiple entrees as long as they have enough money for it. Error checking is done to make
// sure a customer cannot be instantiated in an invalid state, which is very important since 
// this is our base class! In addition to the P3 class, I added the support of an interface,
// CustomerI, to ensure support for a consistent interface with allows for a heterogenous collection
// in the P5 driver.
//
// Interface Invariant: The client can perform a number of methods on each customer object,
// such as adding money to the customer's balance, getting the customer's balance, and getting
// the customer's account number. In addition, a customer can purchase an entree or a meal of 
// multiple entrees from a given vendor as long as they have enough money and the food is not
// expired or spoiled (error checking done on the vendor's side so they don't sell bad food).
// This class shows stable and consistent interface because the fields and methods are the same
// from previous legacy code from P1 and P2 with minor adjustments when switching between languages.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5
{
    public static class customer_Global
    {
        public static int NUM_CUSTOMERS = 0;
        public const float DEFAULT_BALANCE = 0;
        public const float DEFAULT_AMOUNT = 100;
    }
    interface CustomerI
    {
        void addMoney(float input);
        int getAccountNum();
        float getBalance();
        bool BuyOne(Vendor seller, string entreeName);
        bool Buy(Vendor seller);
        bool canPurchase(Vendor v, string s);
        void unsuccessfulPurchase(float f);
    }
    public class Customer : CustomerI
    {
        protected int accountNumber;
        protected float balance;
        protected float[] nutritionInfo = new float[entree_Global.NUM_NUTRITION_FACTS];
        public Customer(float input = customer_Global.DEFAULT_BALANCE)
        {
            accountNumber = ++customer_Global.NUM_CUSTOMERS;
            if (input >= 0) balance = input;
            else balance = customer_Global.DEFAULT_BALANCE;
            for(int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutritionInfo[i] = 0;
            }
        }
        public Customer(Customer c)
        {
            accountNumber = c.accountNumber;
            balance = c.balance;
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutritionInfo[i] = c.nutritionInfo[i];
            }
        }
        // Pre-condition: called on valid customer 
        // Post-condition: if valid input, add to balance, else add global default
        public void addMoney(float input)
        {
            if (input >= 0) balance += input;
            else balance += customer_Global.DEFAULT_AMOUNT;
        }
        // Pre-condition: called on valid customer
        // Post-condition: return account number as int
        public int getAccountNum() { return accountNumber; }
        // Pre-condition: called on valid customer
        // Post-condition: return balance as float
        public float getBalance() { return balance; }
        // Pre-condition: called on valid customer, valid vendor passed in
        // Post-condition: return true if buy success, false if failed
        public virtual bool BuyOne(Vendor seller, string entreeName)
        {
            if (seller.isOpenForBusiness() && seller.isStocked(entreeName))
            {
                if (seller.getPrice(entreeName) > 0 && balance >= seller.getPrice(entreeName))
                {
                    float soldPrice = seller.getPrice(entreeName);
                    float[] nutStorage = new float[entree_Global.NUM_NUTRITION_FACTS];
                    for(int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
                    {
                        nutStorage[i] = 0;
                        float currentFact = seller.getNutritionalFacts(entreeName, i);
                        if (currentFact != -1) nutStorage[i] += currentFact;
                    }
                    if (seller.Sell(this, entreeName))
                    {
                        balance -= soldPrice;
                        for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
                        {
                            nutritionInfo[i] = nutStorage[i];
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        // Pre-condition: called on valid customer, valid vendor passed in
        // Post-condition: return true if meal purchased successfully, false if failed
        public bool Buy(Vendor seller)
        {
            if (seller.isOpenForBusiness() && seller.getNumTotalEntrees() >= vendor_Global.NUM_ENTREES_IN_A_MEAL)
            {
                string[] items = new string[vendor_Global.NUM_ENTREES_IN_A_MEAL];
                float totalPrice = 0;
                for (int i = 0; i < vendor_Global.NUM_ENTREES_IN_A_MEAL; i++)
                {
                    items[i] = seller.getRandomEntree();
                    totalPrice += seller.getPrice(items[i]);
                }
                if (balance >= totalPrice)
                {
                    bool result = true;
                    foreach (string meal in items)
                    {
                        if (!BuyOne(seller, meal)) result = false;
                    }
                    return result;
                }
            }
            return false;
        }
        // Pre-condition: called on a valid customer object, valid input provided
        // Post-condition: return true if entree is able to purchase, false if violates vendor restrictions
        virtual public bool canPurchase(Vendor v, string s)
        {
            bool result = true;
            v.CleanStock();
            if (!v.isStocked(s)) result = false;
            return result;
        }
        // Pre-condition: called on a valid customer object, valid input provided
        // Post-condition: refunds money if the purchase was unsuccessful
        public void unsuccessfulPurchase(float f) { if ((balance - f) >= 0) balance -= f; }
    }
}
// Implementation Invariant: The customer class is the base class for all other 
// variants of customers so it was very important that we did error checking on
// instantiation so an invalid customer could not be created. If invalid values
// were passed in, the customer would use the global defaults provided at the top
// of the class file. In the real world, it is very important that a customer has 
// protected account numbers and balance, which is why they are protected here too.
// Having those data members as protected ensures the client cannot access them, but
// any inherited classes can access them (since they are an extension of a customer).
// After a customer purchases an entree, it was a design choice that they eat it 
// immediately so they do not have to store it. I also chose to support an interface with CustomerI so 
// so all Customer objects will have a consistent interface with the base Customer
// class, ensuring we can have a heterogenous collection in the P5 driver.