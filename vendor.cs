// Brandon Wong
// 12/4/2021
//
// Class invariant: The vendor class demonstrates how a vendor can hold many
// entrees. They also immitate the real world and how even though different
// vendors may sell the same thing, they might sell at different prices.
// Each vendor holds item(s), which consists of an entree item, it's price, 
// the quantity. Each item can also be initialized with provided values and
// error checking is done to make sure it is not initialized as invalid. The
// vendor class also has control of state, since a vendor cannot sell entrees
// if they do not have any. This ensures that no customers can try to buy 
// when the vendor does not have any entrees in stock to sell.
//
// Interface Invariant: The client can call given member methods on the vendor class
// such as, but not limited to, loading a given entree, loading an entire menu of entrees,
// stimulating a power outage, cleaning out the stock for any expired or spoiled food, etc. There
// are also utility functions that help a vendor do it's job like checking the price of a
// given entree so they can return to the customer. This class shows stable and
// consistent interface because the fields and methods are the same from previous legacy
// code from P1 and P2 with minor adjustments when switching between languages.
//

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5
{
    public static class vendor_Global
    {
        public const int DEFAULT_NUM_ENTREES = 1;
        public const int CARB_INDEX = 7;
        public const int SUGAR_INDEX = 9;
        public const int NUM_ENTREES_IN_A_MEAL = 2;
        public const int PRICE_LOW = 5;
        public const int PRICE_HIGH = 25;
        public const int QUANTITY_LOW = 2;
        public const int QUANTITY_HIGH = 10;
    }
    public class Item
    {
        private static Random random = new Random();
        public Entree entreeItem;
        public float price;
        public int quantity;
        public Item next;
        public Item()
        {
            entreeItem = new Entree();
            price = 0;
            quantity = 0;
            next = null;
        }
        public Item(Entree inEntree, int inQuantity = vendor_Global.DEFAULT_NUM_ENTREES)
        {       
            entreeItem = new Entree(inEntree);
            price = random.Next(vendor_Global.PRICE_LOW, vendor_Global.PRICE_HIGH);
            if (inQuantity > 0) quantity = inQuantity;
            else quantity = vendor_Global.DEFAULT_NUM_ENTREES;
            next = null;
        }
        public Item(Entree inEntree, float inPrice, int inQuantity)
        {
            entreeItem = new Entree(inEntree);
            if(inPrice > 0) price = inPrice;
            else price = random.Next(vendor_Global.PRICE_LOW, vendor_Global.PRICE_HIGH);
            if (inQuantity > 0) quantity = inQuantity;
            else quantity = vendor_Global.DEFAULT_NUM_ENTREES;
            next = null;
        }
    }
    public class Vendor
    {
        private static Random random = new Random();
        private Item head;
        private bool openForBusiness;
        private int numUniqueEntrees;
        private int numTotalEntrees;
        // Pre-condition: called on valid vendor
        // Post-condition: if passed in path was valid, load all entrees to the vendor
        //                 if passed in path was invalid, use global default
        //                 if global default invalid, return false
        //                 if successfully loaded, then return true
        public bool LoadMenu(string path = Driver_Global.DEFAULT_FILE)
        {
            if (!File.Exists(path))
            {
                if (File.Exists(path)) path = Driver_Global.DEFAULT_FILE;
                else return false;
            }
            string[] lines = System.IO.File.ReadAllLines(path).Skip(1).ToArray();
            foreach (string line in lines)
            {
                int index = 0;
                string[] values = line.Split(Driver_Global.FIRST_DELIMITER);
                string entreeName = values[index++];
                float[] nutrionalFacts = new float[entree_Global.NUM_NUTRITION_FACTS];
                for (int i = 0; i < entree_Global.NUM_NUTRITION_FACTS; i++)
                {
                    nutrionalFacts[i] = float.Parse(values[index++]);
                }
                string ingredients = values[index++];
                string contains = values[index++];
                Entree newEntree = new Entree(entreeName, nutrionalFacts, ingredients, contains);
                int randomQuantity = random.Next(vendor_Global.QUANTITY_LOW, vendor_Global.QUANTITY_HIGH);
                Load(newEntree, randomQuantity);
            }
            return true;
        }
        public Vendor()
        {
            head = null;
            openForBusiness = false;
            numUniqueEntrees = 0;
            numTotalEntrees = 0;
        }
        public Vendor(string path = Driver_Global.DEFAULT_FILE)
        {
            head = null;
            openForBusiness = false;
            numUniqueEntrees = 0;
            numTotalEntrees = 0;
            if (File.Exists(path)) LoadMenu(path);
        }
        public Vendor(Vendor input)
        {
            Item current = input.head;
            Item newList = null;
            Item previous = null;
            while (current != null)
            {
                Item newItem = new Item(current.entreeItem, current.price, current.quantity);
                if (newList == null)
                {
                    newList = newItem;
                    previous = newList;
                    openForBusiness = true;
                }
                else
                {
                    previous.next = newItem;
                    previous = previous.next;
                }
                current = current.next;
            }
            head = newList;
            openForBusiness = input.openForBusiness;
            numUniqueEntrees = input.numUniqueEntrees;
            numTotalEntrees = input.numTotalEntrees;
        }
        // Pre-condition: called on valid vendor, valid entree passed in
        // Post-condition: load this entree into the vendor's list
        public void Load(Entree item, int inQuantity)
        {
            if (head == null)
            {
                Entree newEntree = new Entree(item);
                Item newItem = new Item(newEntree, inQuantity);
                head = newItem;
                openForBusiness = true;
                numUniqueEntrees++;
            }
            else
            {
                Item current = head;
                while (item.getEntree() != current.entreeItem.getEntree() && current.next != null)
                {
                    current = current.next;
                }
                if (item.getEntree() == current.entreeItem.getEntree()) current.quantity += inQuantity;
                else
                {
                    Entree newEntree = new Entree(item);
                    Item newItem = new Item(newEntree, inQuantity);
                    current.next = newItem;
                    newItem.next = null;
                    numUniqueEntrees++;
                }
            }
            numTotalEntrees += inQuantity;
        }       
        // Pre-condition: called on valid vendor
        // Post-condition: stimulate power outage, turning power off for all entrees
        public void PowerOutage()
        {
            Item current = head;
            while (current != null)
            {
                current.entreeItem.setPower(false);
                current = current.next;
            }
        }
        // Pre-condition: called on valid vendor
        // Post-condition: remove all spoiled and expired entrees from the vendor's stock
        public void CleanStock()
        {
            Item current = head;
            Item previous = null;
            while (current != null)
            {
                if (current.entreeItem.isExpired() || current.entreeItem.isSpoiled())
                {
                    numTotalEntrees -= current.quantity;
                    if (current == head)
                    {
                        current = current.next;
                        head = current;
                    }
                    else
                    {
                        Item temp = current;
                        current = current.next;
                        previous.next = current;
                    }
                    numUniqueEntrees--;
                }
                else
                {
                    previous = current;
                    current = current.next;
                }
                if (head == null) openForBusiness = false;
            }
        }
        // Pre-condition: called on valid vendor
        // Post-condition: return true if entree is in stock, false if not in stock
        public bool isStocked(string entreeName)
        {
            Item current = head;
            while (current != null)
            {
                if (current.entreeItem.getEntree() == entreeName) return true;
                current = current.next;
            }
            return false;
        }
        // Pre-condition: called on valid vendor
        // Post-condition: return price if found, return -1 if not found
        public float getPrice(string entreeName)
        {
            Item current = head;
            while (current != null)
            {
                if (current.entreeItem.getEntree() == entreeName) return current.price;
                current = current.next;
            }
            return -1;
        }
        // Pre-condition: called on valid vendor
        // Post-condition: remove that entree if found in the list
        private void removeEntree(string entreeName)
        {
            Item current = head;
            Item previous = null;
            while (current != null)
            {
                if (current.entreeItem.getEntree() == entreeName)
                {
                    numTotalEntrees -= current.quantity;
                    if (current == head)
                    {
                        current = current.next;
                        head = current;
                    }
                    else
                    {
                        Item temp = current;
                        current = current.next;
                        previous.next = current;
                    }
                    numUniqueEntrees--;
                }
                else
                {
                    previous = current;
                    current = current.next;
                }
                if (head == null) openForBusiness = false;
            }
        }
        // Pre-condition: called on valid vendor
        // Post-condition: return true if vendor is open for business, false if not
        public bool isOpenForBusiness() { return openForBusiness; }
        // Pre-condition: called on valid vendor
        // Post-condition: sell specified entree if not expired or spoiled and is in stock,
        // balance adjusted based on price of entree
        public bool Sell(Customer buyer, string entreeName)
        {
            if (openForBusiness && isStocked(entreeName))
            {
                Item current = head;
                while (current != null && current.entreeItem.getEntree() != entreeName)
                {
                    current = current.next;
                }
                if (current != null && /*buyer.getBalance() >= current.price &&*/ !current.entreeItem.isExpired() && !current.entreeItem.isSpoiled())
                {
                    if (current.quantity == 1) removeEntree(entreeName);
                    else
                    {
                        current.quantity--;
                        numTotalEntrees--;
                    }                
                    return true;
                }
            }
            return false;
        }
        // Pre-condition: called on valid vendor
        // Post-condition: return desired entree's carbs, -1 if not found
        public float getCarbs(string entreeName)
        {
            Item current = head;
            while (current != null && current.entreeItem.getEntree() != entreeName)
            {
                current = current.next;
            }
            if (current != null) return current.entreeItem.getNutrionalFact(vendor_Global.CARB_INDEX);
            return -1;
        }
        // Pre-condition: called on valid vendor
        // Post-condition: return desired entree's sugar, -1 if not found
        public float getSugars(string entreeName)
        {
            Item current = head;
            while (current != null && current.entreeItem.getEntree() != entreeName)
            {
                current = current.next;
            }
            if (current != null) return current.entreeItem.getNutrionalFact(vendor_Global.SUGAR_INDEX);
            return -1;
        }
        // Pre-condition: called on valid vendor
        // Post-condition: return desired entree's quantity, -1 if not found
        public int getQuantity(string entreeName)
        {
            Item current = head;
            while (current != null && current.entreeItem.getEntree() != entreeName)
            {
                current = current.next;
            }
            if (current != null) return current.quantity;
            return -1;
        }
        // Pre-condition: called on valid vendor
        // Post-condition: returns a random entree, empty string if no fresh entrees
        public string getRandomEntree()
        {
            CleanStock();
            int index = random.Next(numUniqueEntrees);
            int count = 0;
            Item current = head;
            string fresh = "";
            while (current != null && count <= index)
            {
                if (!current.entreeItem.isExpired() && !current.entreeItem.isSpoiled()) fresh = current.entreeItem.getEntree();
                current = current.next;
                count++;
            }
            return fresh;
        }
        // Pre-condition: called on valid vendor
        // Post-condition: return ingredients of specified entree if found, empty string if not found
        public string getIngredients(string entreeName)
        {
            Item current = head;
            string result = "";
            while (current != null && current.entreeItem.getEntree() != entreeName)
            {
                current = current.next;
            }
            if (current != null) result = current.entreeItem.getIngredients();
            return result;
        }
        // Pre-condition: called on valid vendor
        // Post-condition: return contains of specified entree if found, empty string if not found
        public string getContains(string entreeName)
        {
            Item current = head;
            string result = "";
            while (current != null && current.entreeItem.getEntree() != entreeName)
            {
                current = current.next;
            }
            if (current != null) result = current.entreeItem.getContains();
            return result;
        }
        // Pre-condition: called on valid vendor
        // Post-condition: return number of unique entrees
        public int getUniqueEntrees() { return numUniqueEntrees; }
        // Pre-condition: called on valid vendor
        // Post-condition: return total number of entrees
        public int getNumTotalEntrees() { return numTotalEntrees; }
        public float getNutritionalFacts(string entreeName, int index)
        {
            Item current = head;
            float result = -1;
            while(current != null && current.entreeItem.getEntree() != entreeName)
            {
                current = current.next;
            }
            if (current != null) result = current.entreeItem.getNutrionalFact(index);
            return result;
        }
    }
}
// Implementation Invariant: The vendor class is implemented with the composition of an entree
// inside the Item class. I also made sure the vendor class supports deep copying because it
// makes sense in the real world for different vendors to give their stock to another vendor
// to sell. Since this project was done in C# the only deep copying that was supported was through
// the copy constructor since we cannot overload the assignment operator. Also since we are in 
// C# we cannot utilize move semantics.