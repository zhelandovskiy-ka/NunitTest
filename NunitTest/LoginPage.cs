using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest;

public class LoginPage
{
    private readonly IWebDriver _driver;

    public IWebElement loginField => _driver.FindElement(By.Id("user-name"));
    public IWebElement passwordField => _driver.FindElement(By.Id("password"));
    public IWebElement loginButton => _driver.FindElement(By.Id("login-button"));
    public IWebElement logo => _driver.FindElement(By.ClassName("login_logo"));

    public IWebElement errorMessage =>
        _driver.FindElement(By.XPath("//*[@id=\"login_button_container\"]/div/form/div[3]/h3"));

    public LoginPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void Login(LoginData login, LoginData pass)
    {
        loginField.SendKeys(login.ToString());
        passwordField.SendKeys(pass.ToString());
        loginButton.Click();
    }

}