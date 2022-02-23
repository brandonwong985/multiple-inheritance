// Brandon Wong
// 12/4/2021
//
// Class Invariant: The EmployeeCustomer class simulates multiple inheritance of the 
// Employee and Customer classes. An EmployeeCustomer Is-A Employee but not necessarily
// a Customer. Each EmployeeCustomer can perform all the basic functions an Employee can as
// expected since they both support the EmployeeI interface. Each EmployeeCustomer can also
// perform all basic functions of a Customer because of the support of an interface, CustomerI.
// The use of both CustomerI and EmployeeI interfaces ensures the each EmployeeCustomer has
// a consistent interface and ensures the support for a heterogenous collection in the P5 driver.
// Error checking is done throughout the class to ensure an invalid EmployeeCustomer cannot be 
// instantiated and that invalid vendor and customer objects cannot be passed in and assigned 
// to the EmployeeCustomer object. In addition to the base class, this class can buy entrees/meals
// from the vendor they work at, effectively becoming a customer, buy deducting from their weekly pay.
//
// Interface Invariant: The EmployeeCustomer class has a consistent interface with the base Employee Class
// and Customer classes. The client can perform a number of methods that are included in both the EmployeeI
// interface and CustomerI interface. The client can perform a number of methods and also create a 
// heterogeneous collection of CustomerI objects including the EmployeeCustomer class.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5
{
    public class EmployeeCustomer : Employee, EmployeeI, CustomerI
    {
        private CustomerI cus;
        public EmployeeCustomer()
        {
            cus = null;
        }
        public EmployeeCustomer(Vendor v)
        {
            if (v != null)
            {
                employer = v;
                payLevel = Employee_Global.PAY_LEVEL_ONE;
                weeklyPay = Employee_Global.PAY_LEVEL_ONE_AMOUNT;
            }
            else
            {
                payLevel = Employee_Global.PAY_LEVEL_ZERO;
                weeklyPay = Employee_Global.PAY_LEVEL_ZERO_AMOUNT;
                employer = null;
            }
            cus = null;
        }
        public EmployeeCustomer(Customer c)
        {
            if (c != null) cus = c;
            else cus = null;
        }
        public EmployeeCustomer(Customer c, Vendor v, int p = Employee_Global.PAY_LEVEL_ONE)
        {
            if (c != null) cus = c;
            else cus = null;

            if (v != null)
            {
                if (p == Employee_Global.PAY_LEVEL_ONE || p == Employee_Global.PAY_LEVEL_TWO || p == Employee_Global.PAY_LEVEL_THREE) payLevel = p;
                else payLevel = Employee_Global.PAY_LEVEL_ONE;


                if (payLevel == Employee_Global.PAY_LEVEL_ONE) weeklyPay = Employee_Global.PAY_LEVEL_ONE_AMOUNT;
                else if (payLevel == Employee_Global.PAY_LEVEL_TWO) weeklyPay = Employee_Global.PAY_LEVEL_TWO_AMOUNT;
                else weeklyPay = Employee_Global.PAY_LEVEL_THREE_AMOUNT;
                employer = v;
            }
            else
            {
                payLevel = Employee_Global.PAY_LEVEL_ZERO;
                weeklyPay = Employee_Global.PAY_LEVEL_ZERO_AMOUNT;
                employer = null;
            }
        }
        // Pre-condition: called on a valid EmployeeCustomer object, error checking for valid input
        // Post-condition: return true if customer was switched successfully, false if invalid customer input
        public bool switchCustomer(Customer c)
        {
            if (c == null) return false;
            cus = c;
            return true;
        }
        // Pre-condition: called on a valid EmployeeCustomer object, error checking for valid input
        // Post-condition: update employee customer's balance
        public void addMoney(float input)
        {
            if(cus != null) cus.addMoney(input);
        }
        // Pre-condition: called on a valid EmployeeCustomer object, error checking if valid customer is in place
        // Post-condition: return account number as int, return -1 if no customer instantiated
        public int getAccountNum() 
        { 
            if (cus != null) return cus.getAccountNum();
            return -1;
        }
        // Pre-condition: called on a valid EmployeeCustomer object, error checking if valid customer is in place
        // Post-condition: return balance as float, return -1 if no customer instantiated
        public override float getBalance() 
        {
            if(cus != null) return cus.getBalance();
            return -1;
        }
        // Pre-condition: called on a valid EmployeeCustomer object, error checking if valid customer is in place
        // Post-condition: deposit weekly pay and employee balance into customer balance
        public override void depositPayCheck()
        {
            if(cus != null)
            {
                cus.addMoney(empBalance);
                empBalance = 0;
                cus.addMoney(weeklyPay);
                if (payLevel == Employee_Global.PAY_LEVEL_ONE) weeklyPay = Employee_Global.PAY_LEVEL_ONE_AMOUNT;
                else if (payLevel == Employee_Global.PAY_LEVEL_TWO) weeklyPay = Employee_Global.PAY_LEVEL_TWO_AMOUNT;
                else weeklyPay = Employee_Global.PAY_LEVEL_THREE_AMOUNT;
            }          
        }
        // Pre-condition: called on a valid EmployeeCustomer object
        // Post-condition: return true if EmployeeCustomer successfully purchased entree from their specific vendor, false if entree purchase failed
        public bool BuyOne(Vendor seller, string entreeName)
        {        
            if (cus != null && employer != null && employer.isOpenForBusiness() && employer.isStocked(entreeName) && cus.canPurchase(employer, entreeName))
            {
                float buyPrice = employer.getPrice(entreeName);
                if(weeklyPay >= buyPrice)
                {
                    bool result = true;
                    cus.addMoney(buyPrice);
                    if (cus.BuyOne(employer, entreeName))
                    {
                        weeklyPay -= buyPrice;
                    }
                    else 
                    {
                        cus.unsuccessfulPurchase(buyPrice);
                        result = false;
                    }
                    return result;
                }            
            }           
            return false;
        }
        // Pre-condition: called on a valid EmployeeCustomer object
        // Post-condition: return true if EmployeeCustomer successfully purchased meal from their specific vendor, false if meal purchase failed
        public bool Buy(Vendor seller) 
        {
            if(cus != null && employer != null && employer.isOpenForBusiness() && employer.getNumTotalEntrees() >= vendor_Global.NUM_ENTREES_IN_A_MEAL)
            {
                string[] items = new string[vendor_Global.NUM_ENTREES_IN_A_MEAL];
                float totalPrice = 0;
                for (int i = 0; i < vendor_Global.NUM_ENTREES_IN_A_MEAL; i++)
                {
                    items[i] = employer.getRandomEntree();
                    totalPrice += employer.getPrice(items[i]);
                    if (!canPurchase(employer, items[i])) return false;
                }
                if (weeklyPay >= totalPrice)
                {
                    bool result = true;
                    foreach (string meal in items)
                    {
                        float buyPrice = employer.getPrice(meal);
                        cus.addMoney(buyPrice);
                        if (cus.BuyOne(employer, meal))
                        {
                            weeklyPay -= buyPrice;
                        } 
                        else
                        {
                            cus.unsuccessfulPurchase(buyPrice);
                            result = false; 
                        }
                    }
                    return result;
                }
            }          
            return false;
        }
        // Pre-condition: called on a valid EmployeeCustomer object, error checking to make sure valid vendor and customer in place
        // Post-condition: return true if EmployeeCustomer can purchase specified entree, false if cannot purchase
        public bool canPurchase(Vendor v, string s)
        {
            if(employer != null && cus != null)
            {
                employer.CleanStock();
                if (employer.isStocked(s) && weeklyPay >= employer.getPrice(s)) return cus.canPurchase(employer, s);
            }
            return false;
        }
        // Pre-condition: called on a valid EmployeeCustomer object
        // Post-condition: refund balance for an unsuccessful purchase
        public void unsuccessfulPurchase(float f)
        {
            cus.unsuccessfulPurchase(f);
        }
    }
}
// Implementation Invariant: The EmployeeCustomer class is implemented using single inheritance plus composition in order
// to simulate multiple inheritance. I chose this class design to maximize the upsides of multiple inheritance while minimizing
// the downsides of multiple inheritance. The EmployeeCustomer class inherits Employee and composes Customer because each
// EmployeeCustomer Is-A Employee, but does not necessarily have to be a customer. Also with the composition of Customer
// allows for it to be a polymorphic delegate which means any child of Customer can stand in for the Customer, i.e. an
// EmployeeCustomer object can be a normal Customer, an allergyCustomer, a carbCustomer, a dbetCustomer, or none. Error
// checking is done to ensure no invalid EmployeeCustomers can be created and also that no invalid Vendor and Customer 
// objects can be passed in to assignment inside the EmployeeCustomer class, which ensures that any EmployeeCustomer
// object will be in a valid state.