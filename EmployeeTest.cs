// Brandon Wong
// 12/4/2021

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace P5
{
    [TestClass]
    public class EmployeeTest
    {
        [TestMethod]
        public void Test_EmptyConstructor_Valid()
        {
            //Arrange
            Employee obj = new Employee();
            //Act
            int objPayLevel = obj.checkPayLevel();
            //Assert
            Assert.AreEqual(objPayLevel, Employee_Global.PAY_LEVEL_ZERO);
        }

        [TestMethod]
        public void Test_VendorConstructor_Valid()
        {
            //Arrange
            Vendor vendor1 = new Vendor();
            Employee obj = new Employee(vendor1);
            //Act
            bool result = obj.checkPayLevel() != 0 ? true : false;
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_VendorConstructor_Invalid()
        {
            //Arrange
            Vendor vendor1 = null;
            Employee obj = new Employee(vendor1);
            //Act
            bool result = obj.checkPayLevel() != 0 ? true : false;
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_VendorPayLevelConstructor_Valid()
        {
            //Arrange
            Vendor vendor1 = new Vendor();
            Employee obj = new Employee(vendor1, Employee_Global.PAY_LEVEL_THREE);
            //Act
            bool result = obj.checkPayLevel() == Employee_Global.PAY_LEVEL_THREE ? true : false;
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_VendorPayLevelConstructor_Invalid()
        {
            //Arrange
            Vendor vendor1 = null;
            Employee obj = new Employee(vendor1, Employee_Global.PAY_LEVEL_THREE);
            //Act
            bool result = obj.checkPayLevel() == Employee_Global.PAY_LEVEL_THREE ? true : false;
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_checkPayLevel()
        {
            //Arrange
            Vendor vendor1 = new Vendor();
            Employee obj = new Employee(vendor1, Employee_Global.PAY_LEVEL_THREE);
            //Act
            int result = obj.checkPayLevel();
            //Assert
            Assert.AreEqual(result, Employee_Global.PAY_LEVEL_THREE);
        }

        [TestMethod]
        public void Test_checkWeeklyPay()
        {
            //Arrange
            Vendor vendor1 = new Vendor();
            Employee obj = new Employee(vendor1, Employee_Global.PAY_LEVEL_THREE);
            //Act
            float result = obj.checkWeeklyPay();
            //Assert
            Assert.AreEqual(result, Employee_Global.PAY_LEVEL_THREE_AMOUNT);
        }

        [TestMethod]
        public void Test_switchVendor_True()
        {
            //Arrange
            Vendor vendor1 = new Vendor();
            Vendor vendor2 = new Vendor();
            Employee obj = new Employee(vendor1, Employee_Global.PAY_LEVEL_THREE);
            //Act
            bool result = obj.switchVendor(vendor2);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_switchVendor_False()
        {
            //Arrange
            Vendor vendor1 = new Vendor();
            Vendor vendor2 = null;
            Employee obj = new Employee(vendor1, Employee_Global.PAY_LEVEL_THREE);
            //Act
            bool result = obj.switchVendor(vendor2);
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_switchPayLevel_True()
        {
            //Arrange
            Vendor vendor1 = new Vendor();
            Employee obj = new Employee(vendor1, 1);
            //Act
            bool result = obj.switchPayLevel(Employee_Global.PAY_LEVEL_ONE);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_switchPayLevel_False()
        {
            //Arrange
            Vendor vendor1 = new Vendor();
            Employee obj = new Employee(vendor1, Employee_Global.PAY_LEVEL_THREE);
            //Act
            bool result = obj.switchPayLevel(Employee_Global.PAY_LEVEL_THREE + 1);
            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_getBalance_Zero()
        {
            //Arrange
            Employee obj = new Employee();
            //Act
            bool result = obj.getBalance() == 0 ? true : false;
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_getBalance_Nonzero()
        {
            //Arrange
            Vendor vendor1 = new Vendor();
            Employee obj = new Employee(vendor1);
            //Act
            obj.depositPayCheck();
            bool result = obj.getBalance() > 0 ? true : false;
            //Assert
            Assert.IsTrue(result);
        }
    }
}
