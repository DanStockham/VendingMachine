using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests.Tests
{
    /// <summary>
    /// Summary description for VendingMachineTests
    /// </summary>
    [TestClass]
    public class VendingMachineTests
    {
        public VendingMachineTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void VendingMachineTest()
        {
            VendingMachine testVM = new VendingMachine();

            Assert.AreEqual("Potato Crisps", testVM.Items["A1"].Name);
            Assert.AreEqual("Cowtales", testVM.Items["B2"].Name);
            Assert.AreEqual("Heavy", testVM.Items["C4"].Name);
        }

        [TestMethod]
        public void AddMoneyTest()
        {
            VendingMachine testVM = new VendingMachine();
            testVM.AddMoney(2.00M);
            Assert.AreEqual(5.00M, testVM.AddMoney(3.00M));
            Assert.AreEqual(10.00M, testVM.AddMoney(5.00M));
            Assert.AreEqual(19.00M, testVM.AddMoney(9.00M));
        }

        [TestMethod]
        public void DespenseMoneyTest()
        {
            VendingMachine testVM = new VendingMachine();
            testVM.AddMoney(1.00M);
            CollectionAssert.AreEqual(new List<int>() { 4, 0, 0 }
            , testVM.DespenseMoney());

            testVM.AddMoney(2.30M);
            CollectionAssert.AreEqual(new List<int>() { 9, 0, 1 }
            , testVM.DespenseMoney());

            testVM.AddMoney(5.25M);
            CollectionAssert.AreEqual(new List<int>() { 21, 0, 0 }
            , testVM.DespenseMoney());
        }

        [TestMethod]
        public void ProductValidityTest()
        {
            VendingMachine testVM = new VendingMachine();
            UserInterface testInterface = new UserInterface(testVM);

            Assert.AreEqual(true, testInterface.ProductValidity("A1"));
            //Assert.AreEqual(false, testInterface.ProductValidity("Z9"));
            Assert.AreEqual(true, testInterface.ProductValidity("C3"));

        }

        [TestMethod]
        public void InventoryFunctionTest()
        {
            VendingMachine testVM = new VendingMachine();
            UserInterface testInterface = new UserInterface(testVM);

            Assert.AreEqual(true, testInterface.ProductValidity("A1"));
            //Assert.AreEqual(false, testInterface.ProductValidity("Z9"));
            Assert.AreEqual(true, testInterface.ProductValidity("C3"));

        }

    }
}
