// Brandon Wong
// 12/4/2021
//
// Class Invariant: The P5 driver class shows the functionality of all the classes
// working together. Testing is done to show individual functionality of each class
// as well as showing all the interactions between all the classes. Error checking was
// done as described in the class invariants of each class. A random generator was also
// used to avoid hard coding and to allow for random test cases.
//
// Interface Invariant: Client can edit the global variables to suit their needs. Please
// make sure to change the directory and path of the fiels to make that of your local machine.
// Driver only consists of functions running the tests to ensure a functionally decomposed driver.
// Assumption that the files passed in follows the same format as the professor provided EntreesTabDelimited.txt
// file provided on Canvas.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5
{
    public static class Driver_Global
    {
        //Change directory to your local machine so dependency injection works as intended!
        public const string DIRECTORY = @"E:\Desktop\VS Code Folder\cpsc3200\P5\";
        public const string DEFAULT_FILE = DIRECTORY + "EntreesTabDelimited.txt";
        public const string ALTERNATE_FILE = DIRECTORY + "AlternateTabDelimited.txt";
        public const char FIRST_DELIMITER = '\t';
        public const char SECOND_DELIMITER = '$';
        public const int DEFAULT_NUM_TEST_CASES = 10;
        public const int NUM_TYPES_OF_CUSTOMERS = 5;
        public const int NUM_TYPES_OF_EMPLOYEES = 2;
    }
    class driver
    {
        static void Main(string[] args)
        {
            driver obj = new driver();

            Console.WriteLine("Entree Tests:");
            obj.EntreeTests();
            Console.Write("\n\n\n");

            Console.WriteLine("Customer Tests:");
            obj.CustomerTests();
            Console.Write("\n\n\n");

            Console.WriteLine("CarbCutomer Tests:");
            obj.CarbCustomerTests();
            Console.Write("\n\n\n");

            Console.WriteLine("DbetCustomer Tests:");
            obj.DbetCustomerTests();
            Console.Write("\n\n\n");

            Console.WriteLine("AllergyCustomer Tests:");
            obj.AllergyCustomerTests();
            Console.Write("\n\n\n");

            Console.WriteLine("Vendor Tests:");
            obj.VendorTests();
            Console.Write("\n\n\n");

            Console.WriteLine("Employee Tests:");
            obj.EmployeeTests();
            Console.Write("\n\n\n");

            Console.WriteLine("EmployeeCustomer Tests:");
            obj.EmployeeCustomerTests();
            Console.Write("\n\n\n");

            Console.WriteLine("All Classes Heterogenous Collection Tests:");
            obj.HeterogenousCollectionTests();
        }
        // Pre-condition: called on a valid P3 driver object
        // Post-condition: perform tests to show functionality of entree class 
        public void EntreeTests()
        {
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree item = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            Console.WriteLine("Entree: " + item.getEntree());
            Console.Write("Ingredients: " + item.getIngredients() + "\n");
            Console.Write("Contains: " + item.getContains() + "\n");
            if (item.needsRefrigeration())
            {
                Console.WriteLine(item.getEntree() + " needs refrigeration");
            }
            if (item.isExpired())
            {
                Console.WriteLine(item.getEntree() + " has expired");
            }
            if (item.isSpoiled())
            {
                Console.WriteLine(item.getEntree() + " has spoiled");
            }
        }
        // Pre-condition: called on a valid P3 driver object
        // Post-condition: perform tests to show functionality of customer class
        public void CustomerTests()
        {
            Customer Candice = new Customer();
            Console.WriteLine("Customer " + Candice.getAccountNum() + " has $" + Candice.getBalance());
            Candice.addMoney(customer_Global.DEFAULT_AMOUNT);
            Console.WriteLine("Customer " + Candice.getAccountNum() + " now has $" + Candice.getBalance());

            Customer Joe = new Customer(customer_Global.DEFAULT_AMOUNT);
            Console.WriteLine("Customer " + Joe.getAccountNum() + " started with $" + Joe.getBalance());
        }
        // Pre-condition: called on a valid P3 driver object
        // Post-condition: perform tests to show functionality of carbCustomer class
        public void CarbCustomerTests()
        {
            carbCustomer SeaOfThieves = new carbCustomer();
            Console.WriteLine("Customer " + SeaOfThieves.getAccountNum() + " has consumed " + SeaOfThieves.getCurrentCarbCount() + "g of carbohydrates");
            SeaOfThieves.addMoney(customer_Global.DEFAULT_AMOUNT);
            Vendor E3 = new Vendor(Driver_Global.DEFAULT_FILE);
            SeaOfThieves.BuyOne(E3, E3.getRandomEntree());
            Console.WriteLine("Customer " + SeaOfThieves.getAccountNum() + " has now consumed " + SeaOfThieves.getCurrentCarbCount() + "g of carbohydrates");

            carbCustomer Steve = new carbCustomer(customer_Global.DEFAULT_AMOUNT);
            Console.WriteLine("Customer " + Steve.getAccountNum() + " has consumed " + Steve.getCurrentCarbCount() + "g of carbohydrates");
            Steve.addMoney(customer_Global.DEFAULT_AMOUNT);
            Steve.Buy(E3);
            Console.WriteLine("Customer " + Steve.getAccountNum() + " has now consumed " + Steve.getCurrentCarbCount() + "g of carbohydrates");
        }
        // Pre-condition: called on a valid P3 driver object
        // Post-condition: perform tests to show functionality of dbetCustomer class
        public void DbetCustomerTests()
        {
            dbetCustomer Peter = new dbetCustomer();
            Console.WriteLine("Customer " + Peter.getAccountNum() + " has consumed " + Peter.getCurrentSugarCount() + "g of sugar");
            Peter.addMoney(customer_Global.DEFAULT_AMOUNT);
            Vendor TacoTruck = new Vendor(Driver_Global.DEFAULT_FILE);
            Peter.BuyOne(TacoTruck, TacoTruck.getRandomEntree());
            Console.WriteLine("Customer " + Peter.getAccountNum() + " has now consumed " + Peter.getCurrentSugarCount() + "g of sugar");

            dbetCustomer Stewie = new dbetCustomer(dbet_Global.DEFAULT_DAILY_SUGAR_LIMIT, dbet_Global.DEFAULT_ENTREE_SUGAR_LIMIT, dbet_Global.DEFAULT_MEAL_SUGAR_LIMIT);
            Stewie.addMoney(customer_Global.DEFAULT_AMOUNT);
            Console.WriteLine("Customer " + Stewie.getAccountNum() + " has consumed " + Stewie.getCurrentSugarCount() + "g of sugar");
            Stewie.Buy(TacoTruck);
            Console.WriteLine("Customer " + Stewie.getAccountNum() + " has now consumed " + Stewie.getCurrentSugarCount() + "g of sugar");
        }
        // Pre-condition: called on a valid P3 driver object
        // Post-condition: perform tests to show functionality of allergyCustomer class
        public void AllergyCustomerTests()
        {
            allergyCustomer Homer = new allergyCustomer();
            Console.WriteLine("Customer " + Homer.getAccountNum() + "'s allergies: " + Homer.getAllergy());
            Homer.addMoney(customer_Global.DEFAULT_AMOUNT);
            Vendor SushiBar = new Vendor(Driver_Global.DEFAULT_FILE);
            Homer.BuyOne(SushiBar, SushiBar.getRandomEntree());

            allergyCustomer Bart = new allergyCustomer(allergy_Global.ALTERNATE_ALLERGY, allergy_Global.DEFAULT_SEVERELY_ALLERGIC);
            Bart.addMoney(customer_Global.DEFAULT_AMOUNT);
            Console.WriteLine("Customer " + Bart.getAccountNum() + "  has $" + Bart.getBalance());
            Vendor DiningHall = new Vendor(Driver_Global.ALTERNATE_FILE);
            Bart.BuyOne(DiningHall, DiningHall.getRandomEntree());
            Console.WriteLine("Customer " + Bart.getAccountNum() + "  now has $" + Bart.getBalance());
        }
        // Pre-condition: called on a valid P3 driver object
        // Post-condition: perform tests to show functionality of vendor class
        public void VendorTests()
        {
            Vendor TakoEats = new Vendor();
            TakoEats.LoadMenu(Driver_Global.DEFAULT_FILE);
            string name = "";
            if (!TakoEats.isStocked(name)) Console.WriteLine(name + " is not in stock.");
            name = TakoEats.getRandomEntree();
            if (TakoEats.isStocked(name)) Console.WriteLine(name + " is in stock.");
            Console.WriteLine(name + " costs $" + TakoEats.getPrice(name));
            Console.WriteLine(name + " has " + TakoEats.getCarbs(name) + "g of carbohydrates");
            Console.WriteLine(name + " has " + TakoEats.getSugars(name) + "g of sugar");
            Console.WriteLine(name + " quantity: " + TakoEats.getQuantity(name));
            Console.WriteLine(name + "'s ingredients: " + TakoEats.getIngredients(name));
            Console.WriteLine(name + "'s contains: " + TakoEats.getContains(name));
            Console.WriteLine("This vendor has " + TakoEats.getNumTotalEntrees() + " total entrees.");
            Console.WriteLine("This vendor has " + TakoEats.getUniqueEntrees() + " unique entrees.");
            float[] nutrionFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
            for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
            {
                nutrionFacts[i] = entree_Global.DEFAULT_NUTFACT;
            }
            Entree newEntree = new Entree(entree_Global.DEFAULT_ENTREE, nutrionFacts, entree_Global.DEFAULT_INGREDIENTS, entree_Global.DEFAULT_CONTAINS);
            TakoEats.Load(newEntree, vendor_Global.DEFAULT_NUM_ENTREES);
            if (TakoEats.isStocked(entree_Global.DEFAULT_ENTREE)) Console.WriteLine(entree_Global.DEFAULT_ENTREE + " is in the stock.");

            Vendor CatShark = new Vendor(TakoEats);
            if (TakoEats.isStocked(entree_Global.DEFAULT_ENTREE)) Console.WriteLine(entree_Global.DEFAULT_ENTREE + " is in original stockstock.");
            else Console.WriteLine(entree_Global.DEFAULT_ENTREE + " is not in original stock.");
            if (CatShark.isStocked(entree_Global.DEFAULT_ENTREE)) Console.WriteLine(entree_Global.DEFAULT_ENTREE + " is in copy stock.");
            else Console.WriteLine(entree_Global.DEFAULT_ENTREE + " is not in copy stock.");
            Console.WriteLine("Power Outage and Clean Stock here");
            TakoEats.PowerOutage();
            TakoEats.CleanStock();
            if (TakoEats.isStocked(entree_Global.DEFAULT_ENTREE)) Console.WriteLine(entree_Global.DEFAULT_ENTREE + " is in original stockstock.");
            else Console.WriteLine(entree_Global.DEFAULT_ENTREE + " is not in original stock.");
            if (CatShark.isStocked(entree_Global.DEFAULT_ENTREE)) Console.WriteLine(entree_Global.DEFAULT_ENTREE + " is in copy stock.");
            else Console.WriteLine(entree_Global.DEFAULT_ENTREE + " is not in copy stock.");
        }
        // Pre-condition: called on a valid P3 driver object
        // Post-condition: perform tests to show functionality of Employee class
        public void EmployeeTests()
        {
            Vendor Employer = new Vendor();
            Vendor newEmployer = new Vendor();
            Employer.LoadMenu(Driver_Global.DEFAULT_FILE);
            Employee Candice = new Employee();
            Console.WriteLine("Employee pay level is " + Candice.checkPayLevel() + " with a weekly pay of $" + Candice.checkWeeklyPay());
            Candice.switchVendor(Employer);
            Candice.switchPayLevel(Employee_Global.PAY_LEVEL_THREE);
            Console.WriteLine("Employee pay level is " + Candice.checkPayLevel() + " with a weekly pay of $" + Candice.checkWeeklyPay());         
            Candice.switchVendor(newEmployer);
            Console.WriteLine("Employee pay level is " + Candice.checkPayLevel() + " with a weekly pay of $" + Candice.checkWeeklyPay());
        }
        // Pre-condition: called on a valid P3 driver object
        // Post-condition: perform tests to show functionality of EmployeeCustomer class
        public void EmployeeCustomerTests()
        {
            Random random = new Random();
            Vendor BossMan = new Vendor();
            Vendor BigBoss = new Vendor();
            BossMan.LoadMenu(Driver_Global.DEFAULT_FILE);
            BigBoss.LoadMenu(Driver_Global.ALTERNATE_FILE);

            Customer Jean = new Customer();
            Jean.addMoney(customer_Global.DEFAULT_AMOUNT);
            EmployeeCustomer WorkerJean = new EmployeeCustomer(Jean, BigBoss);
            Console.WriteLine("EmployeeCustomer " + WorkerJean.getAccountNum() + " has pay level " + WorkerJean.checkPayLevel() + " with a weekly pay of $" + WorkerJean.checkWeeklyPay()
                + " and current balance of $" + WorkerJean.getBalance());
            WorkerJean.depositPayCheck();
            Console.WriteLine("EmployeeCustomer " + WorkerJean.getAccountNum() + " has pay level " + WorkerJean.checkPayLevel() + " with a weekly pay of $" + WorkerJean.checkWeeklyPay()
                + " and current balance of $" + WorkerJean.getBalance());

            WorkerJean.addMoney(random.Next((int)customer_Global.DEFAULT_AMOUNT));
            Console.WriteLine("EmployeeCustomer " + WorkerJean.getAccountNum() + " has pay level " + WorkerJean.checkPayLevel() + " with a weekly pay of $" + WorkerJean.checkWeeklyPay()
                + " and current balance of $" + WorkerJean.getBalance());
            WorkerJean.switchVendor(BossMan);
            allergyCustomer allergicJean = new allergyCustomer();
            WorkerJean.switchCustomer(allergicJean);
            EmployeeCustomer[] employeeCustomers = new EmployeeCustomer[Driver_Global.DEFAULT_NUM_TEST_CASES];
            for(int i = 0; i < Driver_Global.DEFAULT_NUM_TEST_CASES; i++)
            {
                Customer inputCus;
                int r = random.Next(Driver_Global.NUM_TYPES_OF_CUSTOMERS - 1);
                int p = random.Next(Employee_Global.PAY_LEVEL_ONE, Employee_Global.PAY_LEVEL_THREE + 1);
                if (r == 0) inputCus = new Customer();
                else if (r == 1) inputCus = new carbCustomer();
                else if (r == 2) inputCus = new dbetCustomer();
                else inputCus = new allergyCustomer();
                employeeCustomers[i] = new EmployeeCustomer(inputCus, BossMan, p);

                Console.WriteLine("EmployeeCustomer " + employeeCustomers[i].getAccountNum() + " has pay level " + employeeCustomers[i].checkPayLevel() + " with a weekly pay of $" 
                    + employeeCustomers[i].checkWeeklyPay() + " and current balance of $" + employeeCustomers[i].getBalance());
                employeeCustomers[i].Buy(BossMan);
                employeeCustomers[i].depositPayCheck();
                Console.WriteLine("EmployeeCustomer " + employeeCustomers[i].getAccountNum() + " has pay level " + employeeCustomers[i].checkPayLevel() + " with a weekly pay of $"
                    + employeeCustomers[i].checkWeeklyPay() + " and current balance of $" + employeeCustomers[i].getBalance());
            }
        }
        // Pre-condition: called on a valid P3 driver object
        // Post-condition: perform tests to show functionality of all classes interacting with each other with 
        //                 heterogenous collection of employees and customers
        public void HeterogenousCollectionTests()
        {
            Random random = new Random();
            Vendor employeeBoss = new Vendor(Driver_Global.DEFAULT_FILE);
            EmployeeI[] employees = new EmployeeI[Driver_Global.DEFAULT_NUM_TEST_CASES];
            for (int i = 0; i < Driver_Global.DEFAULT_NUM_TEST_CASES; i++)
            {
                int r = random.Next(Driver_Global.NUM_TYPES_OF_EMPLOYEES);
                int p = random.Next(Employee_Global.PAY_LEVEL_ONE, Employee_Global.PAY_LEVEL_THREE + 1);
                employees[i] = getRandomEmployee(r, employeeBoss, p);
                Console.WriteLine("Employee " + (i + 1) + " with pay level of " + employees[i].checkPayLevel() + " has a balance of $" + employees[i].getBalance());
                employees[i].depositPayCheck();
                Console.WriteLine("Employee " + (i + 1) + " with pay level of " + employees[i].checkPayLevel() + " has a balance of $" + employees[i].getBalance());
            }

            Vendor seller = new Vendor(Driver_Global.DEFAULT_FILE);
            CustomerI[] customers = new CustomerI[Driver_Global.DEFAULT_NUM_TEST_CASES];
            for (int i = 0; i < Driver_Global.DEFAULT_NUM_TEST_CASES; i++)
            {
                int result = random.Next(Driver_Global.NUM_TYPES_OF_CUSTOMERS - 1);
                customers[i] = getRandomCustomer(random.Next(Driver_Global.NUM_TYPES_OF_CUSTOMERS), seller, result);
                customers[i].addMoney(customer_Global.DEFAULT_AMOUNT);
                Console.WriteLine("Customer " + customers[i].getAccountNum() + " has $" + customers[i].getBalance());
                customers[i].BuyOne(seller, seller.getRandomEntree());
                Console.WriteLine("Customer " + customers[i].getAccountNum() + " now has $" + customers[i].getBalance());
            }          
            CustomerI[] newCustomers = new CustomerI[Driver_Global.DEFAULT_NUM_TEST_CASES];
            for (int i = 0; i < Driver_Global.DEFAULT_NUM_TEST_CASES; i++)
            {
                int result = random.Next(Driver_Global.NUM_TYPES_OF_CUSTOMERS - 1);
                newCustomers[i] = getRandomCustomer(random.Next(Driver_Global.NUM_TYPES_OF_CUSTOMERS), seller, result);
                newCustomers[i].addMoney(customer_Global.DEFAULT_AMOUNT);
                Console.WriteLine("Customer " + newCustomers[i].getAccountNum() + " has $" + newCustomers[i].getBalance());
                newCustomers[i].Buy(seller);
                Console.WriteLine("Customer " + newCustomers[i].getAccountNum() + " now has $" + newCustomers[i].getBalance());
            }           
        }       
        // Pre-condition: called on a valid customer object
        // Post-condition: returns a variant of customer object based on input
        public CustomerI getRandomCustomer(int num, Vendor v, int result)
        {
            CustomerI obj;
            if (num == 0) obj = new Customer();
            else if (num == 1) obj = new carbCustomer();
            else if (num == 2) obj = new dbetCustomer();
            else if(num == 3) obj = new allergyCustomer();           
            else
            {
                Customer input;
                if (result == 0) input = new Customer();
                else if (result == 1) input = new carbCustomer();                 
                else if (result == 2) input = new dbetCustomer();
                else input = new allergyCustomer();
                obj = new EmployeeCustomer(input, v);               
            }
            return obj;
        }
        // Pre-condition: called on a valid employee object
        // Post-condition: returns a variant of employee object based on input
        public EmployeeI getRandomEmployee(int num, Vendor v, int p = Employee_Global.PAY_LEVEL_ONE)
        {
            EmployeeI obj;
            Random random = new Random();
            
            if (num == 0) obj = new Employee(v, p);
            else
            {
                Customer inputCus;              
                int r = random.Next(Driver_Global.NUM_TYPES_OF_CUSTOMERS - 1);              
                if (r == 0) inputCus = new Customer();
                else if (r == 1) inputCus = new carbCustomer();
                else if (r == 2) inputCus = new dbetCustomer();
                else inputCus = new allergyCustomer();
                obj = new EmployeeCustomer(inputCus, v, p);
            }
            return obj;
        }
    }
}
// Implementation Invariant: I implemented the driver with all the test functions
// in separate classes to show each functionality of the classes. This also shows 
// and ensures functional decomposition. Global variables are also declared at the
// top of the driver and each class to avoid hard coding. Each test function shows the 
// relationship they hold with the other classes and how they work/are related to each
// other.