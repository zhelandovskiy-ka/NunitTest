using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest;

[TestFixture]
public class LoginPageTest
{
    private IWebDriver _driver;
    private LoginPage _loginPage;
    private MainPage _mainPage;

    [SetUp]
    public void SetUp()
    {
        _driver = new ChromeDriver();
        _driver.Manage().Window.Maximize();
        _driver.Navigate().GoToUrl("https://www.saucedemo.com/");

        _loginPage = new LoginPage(_driver);
        _mainPage = new MainPage(_driver);
    }

    [TearDown]
    public void Quit()
    {
        _driver.Quit();
    }

    [Test]
    public void LoginOk()
    {
        _loginPage.Login(LoginData.standard_user, LoginData.secret_sauce);
        Assert.AreEqual(true, _mainPage.CheckCaption(ResultMessages.LOGO_CAPTION));
    }

    [Test]
    public void ErrorUsernameRequired()
    {
        _loginPage.loginButton.Click();
        Assert.AreEqual(ResultMessages.ERROR_UESERNAME_REQ, _loginPage.errorMessage.Text);
    }

    [Test]
    public void ErrorUsernamePassword()
    {
        _loginPage.Login(LoginData.standard_user, LoginData.secret_sauce_wrong);
        Assert.AreEqual(ResultMessages.ERROR_UESERNAME_PASSWORD, _loginPage.errorMessage.Text);
    }

    [Test]
    public void ErrorLockedUser()
    {
        _loginPage.Login(LoginData.locked_out_user, LoginData.secret_sauce);
        Assert.AreEqual(ResultMessages.ERROR_LOCKED_USERS, _loginPage.errorMessage.Text);
    }
}