using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace ContosoExpenses.Tests
{
    [TestClass]
    public class UITests
    {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        private const string WpfAppId = @"ContosoExpenses_e627vcndsd2rc!App";

        protected static WindowsDriver<WindowsElement> session;
        protected static WindowsDriver<WindowsElement> DesktopSession;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            if (session == null)
            {
                var appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("app", WpfAppId);
                appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");
                DesktopSession = null;
                try
                {
                    Console.WriteLine("Trying to Launch App");
                    DesktopSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);
                }
                catch
                {
                    Console.WriteLine("Failed to attach to app session (expected).");
                }

                appiumOptions.AddAdditionalCapability("app", "Root");
                DesktopSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);
                var mainWindow = DesktopSession.FindElementByAccessibilityId("ContosoExpenses_MainWindow");
                Console.WriteLine("Getting Window Handle");
                var mainWindowHandle = mainWindow.GetAttribute("NativeWindowHandle");
                mainWindowHandle = (int.Parse(mainWindowHandle)).ToString("x"); // Convert to Hex
                appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("appTopLevelWindow", mainWindowHandle);
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);
            }
        }

        [TestMethod]
        [Obsolete]
        public void SelectEmployee()
        {
            var grid = session.FindElementByAccessibilityId("CustomersGrid");
            DesktopSession.Keyboard.SendKeys(Keys.Tab);
            grid.Click();
            var expensesList = DesktopSession.FindElementByAccessibilityId("ContosoExpenses_ExpensesList");
            Assert.IsTrue(expensesList.Displayed);
            DesktopSession.Keyboard.SendKeys(Keys.Alt + Keys.F4 + Keys.Alt);

        }

        [TestMethod]
        [Obsolete]
        public void AddNewExpense()
        {
            Thread.Sleep(3000);
            var grid = session.FindElementByAccessibilityId("CustomersGrid");
            DesktopSession.Keyboard.SendKeys(Keys.Tab);
            grid.Click();
            Thread.Sleep(2000);
            var newExpense = DesktopSession.FindElementByName("Add new expense");
            newExpense.Click();
            Assert.IsTrue(newExpense.Displayed);
            Thread.Sleep(1000);
            var type = DesktopSession.FindElementByAccessibilityId("txtType");
            type.SendKeys("Invoice - Food");
            var description = DesktopSession.FindElementByAccessibilityId("txtDescription");
            description.SendKeys("A lot of chicken nuggets!!");
            var amount = DesktopSession.FindElementByAccessibilityId("txtAmount");
            amount.SendKeys("148.32");
            var location = DesktopSession.FindElementByAccessibilityId("txtLocation");
            location.SendKeys("Wendy's");
            Thread.Sleep(1000);
            var city = DesktopSession.FindElementByAccessibilityId("txtCity");
            city.SendKeys("Bellevue");
            var Date = DesktopSession.FindElementByAccessibilityId("txtDate");
            Date.SendKeys("10/31/2019");
            DesktopSession.FindElementByName("Save").Click();
            DesktopSession.Keyboard.SendKeys(Keys.Alt + Keys.F4 + Keys.Alt);
            //TODO: add assert to confirm successfull entry.
        }

        [ClassCleanup]
        [Obsolete]
        public static void Cleanup()
        {
            if (session != null)
            {
                DesktopSession.Keyboard.SendKeys(Keys.Alt + Keys.F4 + Keys.Alt);
                session.CloseApp();
                session.Quit();
            }
        }
    }
}
