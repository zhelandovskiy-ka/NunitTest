using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest;

[TestFixture]
public class MainPageTest
{
    private IWebDriver _driver;
    private MainPage _mainPage;
    private LoginPage _loginPage;

    [SetUp]
    public void SetUp()
    {
        _driver = new ChromeDriver();
        _driver.Manage().Window.Maximize();
        _driver.Navigate().GoToUrl("https://www.saucedemo.com/");

        _mainPage = new MainPage(_driver);
        _loginPage = new LoginPage(_driver);
    }

    [TearDown]
    public void Quit()
    {
        _driver.Quit();
    }

    private void Login()
    {
        _loginPage.Login(LoginData.standard_user, LoginData.secret_sauce);
    }

    [Test]
    public void AddToCart()
    {
        Login();

        _mainPage.AddToCart();

        Assert.AreEqual(3, _mainPage.ShoppingCartCount());
    }

    [Test]
    public void RemoveFromCart()
    {
        Login();

        _mainPage.AddToCart();

        _mainPage.ShoppingCart.Click();

        _mainPage.RemoveFromCart();

        Assert.AreEqual(0, _mainPage.ShoppingCartCount());
    }

    [Test]
    public void MakeOrder()
    {
        Login();

        _mainPage.AddToCart();
        _mainPage.ShoppingCart.Click();
        _mainPage.ButtonCheckout.Click();
        _mainPage.EnterInformation("Ivan", "Ivanov", "3431243");
        _mainPage.ButtonContinue.Click();
        _mainPage.ButtonFinish.Click();

        Assert.AreEqual(ResultMessages.ORDER_SUCCESS, _mainPage.CaptionFinishOrder.Text);
    }

    [Test]
    public void Logout()
    {
        Login();
        _mainPage.Logout();

        Assert.AreEqual(ResultMessages.LOGO_CAPTION, _loginPage.logo.Text);
    }

}
