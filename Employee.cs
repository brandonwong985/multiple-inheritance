// Brandon Wong
// 12/4/2021
//
// Class Invariant: The Employee is the base class for all Employee type
// objects (namely for EmployeeCustomer). Because of this, we want to have 
// a consistent interface which is why I also chose to support an interface,
// EmployeeI. This ensures a consistent interface with all necessary methods 
// and will allow for a heterogenous collection of EmployeeI objects in the 
// P5 driver. An Employee can perform basic functions like viewing their
// pay level, weekly pay check, balance, and depositing their pay check. An
// Employee also works for a specific vendor, and if they are not working for a 
// vendor, they will have a weekly pay of $0. There are 3 pay levels (not including
// pay level 0), which are shown in the globals below. Error checking is done within
// the class to ensure things like weekly pay and balance cannot go under 0, and that an
// invalid Employee cannot be created. Error checking is also done to make sure the vendor
// is inputted correctly and anything related to the vendor is checked before performing
// operations to make sure object is always in a valid state.
//
// Interface Invariant: The client can perform a number of methods on each Employee
// object, such as checking their pay level, weekly pay, balance, and depositing pay
// check. Since this is the base class, not much else is required of this class
// specifically, but is later supported in the child class that will inherit this class,
// EmployeeCustomer. 
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5
{
    public static class Employee_Global
    {
        public const int PAY_LEVEL_ZERO = 0;
        public const int PAY_LEVEL_ZERO_AMOUNT = 0;

        public const int PAY_LEVEL_ONE = 1;
        public const int PAY_LEVEL_TWO = 2;
        public const int PAY_LEVEL_THREE = 3;
        public const uint PAY_LEVEL_ONE_AMOUNT = 100;
        public const uint PAY_LEVEL_TWO_AMOUNT = 200;
        public const uint PAY_LEVEL_THREE_AMOUNT = 300;
        public const int NUM_PAY_LEVELS = 3;
    }
    interface EmployeeI
    {
        int checkPayLevel();
        float checkWeeklyPay();
        float getBalance();
        void depositPayCheck();
    }
    public class Employee : EmployeeI
    {
        protected int payLevel;
        protected float weeklyPay;
        protected float empBalance;
        protected Vendor employer;
        public Employee()
        {
            payLevel = Employee_Global.PAY_LEVEL_ZERO;
            weeklyPay = Employee_Global.PAY_LEVEL_ZERO_AMOUNT;
            empBalance = 0;
            employer = null;
        }
        public Employee(Vendor v)
        {
            if (v != null)
            {
                employer = v;
                payLevel = Employee_Global.PAY_LEVEL_ONE;
                weeklyPay = Employee_Global.PAY_LEVEL_ONE_AMOUNT;
                empBalance = 0;
            }
            else
            {
                payLevel = Employee_Global.PAY_LEVEL_ZERO;
                weeklyPay = Employee_Global.PAY_LEVEL_ZERO_AMOUNT;
                empBalance = 0;
                employer = null;
            }
        }
        public Employee(Vendor v, int p = Employee_Global.PAY_LEVEL_ONE)
        {
            if (v != null)
            {
                if (p == Employee_Global.PAY_LEVEL_ONE || p == Employee_Global.PAY_LEVEL_TWO || p == Employee_Global.PAY_LEVEL_THREE) payLevel = p;
                else payLevel = Employee_Global.PAY_LEVEL_ONE;


                if (payLevel == Employee_Global.PAY_LEVEL_ONE) weeklyPay = Employee_Global.PAY_LEVEL_ONE_AMOUNT;
                else if (payLevel == Employee_Global.PAY_LEVEL_TWO) weeklyPay = Employee_Global.PAY_LEVEL_TWO_AMOUNT;
                else weeklyPay = Employee_Global.PAY_LEVEL_THREE_AMOUNT;
                empBalance = 0;
                employer = v;
            }
            else
            {
                payLevel = Employee_Global.PAY_LEVEL_ZERO;
                weeklyPay = Employee_Global.PAY_LEVEL_ZERO_AMOUNT;
                empBalance = 0;
                employer = null;
            }
        }
        // Pre-condition: called on a valid employee object
        // Post-condition: return employee pay level as an int
        public int checkPayLevel() { return payLevel; }
        // Pre-condition: called on a valid employee object
        // Post-condition: return employee weekly pay as a float
        public float checkWeeklyPay() { return weeklyPay; }
        // Pre-condition: called on a valid employee object, error checking for valid vendor input
        // Post-condition: return true if vendor was switched successfully, false if vendor was invalid
        public bool switchVendor(Vendor v)
        {
            if (v == null) return false;
            employer = v;
            payLevel = Employee_Global.PAY_LEVEL_ONE;
            weeklyPay = Employee_Global.PAY_LEVEL_ONE_AMOUNT;
            return true;
        }
        // Pre-condition: called on a valid employee object, error checking for valid pay level input
        // Post-condition: return true if pay level was switched successfully, false if pay level input was invalid
        public bool switchPayLevel(int p)
        {
            bool result = true;
            if (employer == null) result = false;
            else
            {
                if (p == Employee_Global.PAY_LEVEL_ONE || p == Employee_Global.PAY_LEVEL_TWO || p == Employee_Global.PAY_LEVEL_THREE)
                {
                    payLevel = p;
                    result = true;
                }
                else
                {
                    payLevel = Employee_Global.PAY_LEVEL_ONE;
                    result = false;
                }
                if (payLevel == Employee_Global.PAY_LEVEL_ONE) weeklyPay = Employee_Global.PAY_LEVEL_ONE_AMOUNT;
                else if (payLevel == Employee_Global.PAY_LEVEL_TWO) weeklyPay = Employee_Global.PAY_LEVEL_TWO_AMOUNT;
                else weeklyPay = Employee_Global.PAY_LEVEL_THREE_AMOUNT;
            }
            return result;
        }
        // Pre-condition: called on a valid employee object
        // Post-condition: return employee balance as a float
        public virtual float getBalance() { return empBalance; }
        // Pre-condition: called on a valid employee object
        // Post-condition: deposit the employee's pay check (weekly pay) into their balance
        public virtual void depositPayCheck()
        {
            empBalance += weeklyPay;
            if (payLevel == Employee_Global.PAY_LEVEL_ONE) weeklyPay = Employee_Global.PAY_LEVEL_ONE_AMOUNT;
            else if (payLevel == Employee_Global.PAY_LEVEL_TWO) weeklyPay = Employee_Global.PAY_LEVEL_TWO_AMOUNT;
            else weeklyPay = Employee_Global.PAY_LEVEL_THREE_AMOUNT;
        }
    }
}
// Implementation Invariant: The employee class is the base class for all the other variants
// of an Employee (most importantly EmployeeCustomer for this assignment). Since this is the 
// base class, extra caution is taken for error checking to ensure an invalid object cannot
// be instantiated and to ensure invalid vendor objects cannot be set for the employee. If invalid
// values are passed in, the class will use the global defaults. In the real world, an employee's 
// personal information is highly important which is why we have a protected level of protection so 
// the information is not publically available to the client, but is accessible to the children of the 
// class via inheritance. Since an Employee does not interact with Vendor in any other way, no extra functions
// we implemented since this class serves as a base for the EmployeeCustomer class that has many more
// interactions with a customer-vendor relationship.