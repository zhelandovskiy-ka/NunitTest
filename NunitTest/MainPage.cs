using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest;

public class MainPage
{
    private readonly IWebDriver _driver;

    public IWebElement LogoCaption => _driver.FindElement(By.XPath("//div[@class='app_logo']"));
    public IWebElement ShoppingCart => _driver.FindElement(By.XPath("//span[@class='shopping_cart_badge']"));
    public IList<IWebElement> ButtonsAddCart => _driver.FindElements(By.CssSelector("button.btn.btn_primary.btn_small.btn_inventory"));
    public IWebElement ButtonCheckout => _driver.FindElement(By.XPath("//*[@id='checkout']"));
    public IWebElement FieldFN => _driver.FindElement(By.XPath("//*[@id='first-name']"));
    public IWebElement FieldLN => _driver.FindElement(By.XPath("//*[@id='last-name']"));
    public IWebElement FieldZip => _driver.FindElement(By.XPath("//*[@id='postal-code']"));
    public IWebElement ButtonContinue => _driver.FindElement(By.XPath("//*[@id='continue']"));
    public IWebElement ButtonFinish => _driver.FindElement(By.XPath("//*[@id='finish']"));
    public IWebElement CaptionFinishOrder => _driver.FindElement(By.XPath("//*[@id='checkout_complete_container']/h2"));
    public IWebElement ButtonMenu => _driver.FindElement(By.Id("react-burger-menu-btn"));
    public IWebElement LinkLogout => _driver.FindElement(By.Id("logout_sidebar_link"));

    public MainPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public bool CheckCaption(String text)
    {
        return LogoCaption.Text.Equals(text);
    }

    public IList<IWebElement> GetItemList()
    {
        IList<IWebElement> cartItem = _driver.FindElements(By.ClassName("cart_item"));

        IList<IWebElement> buttonRemoveFromCartList = new List<IWebElement>();

        for (int i = 0; i < cartItem.Count; i++)
        {
            buttonRemoveFromCartList.Add(_driver.FindElement(By.XPath("//html/body/div/div/div/div[2]/div/div[1]/div[" + (i + 3) + "]/div[2]/div[2]/button")));
        }

        return buttonRemoveFromCartList;
    }

    public void AddToCart()
    {
        for (int i = 0; i < 3; i++)
        {
            ButtonsAddCart[i].Click();
        }
    }

    public void RemoveFromCart()
    {
        foreach (IWebElement webElement in GetItemList())
        {
            webElement.Click();
        }
    }

    public int ShoppingCartCount()
    {
        try
        {
            return Int32.Parse(ShoppingCart.Text);
        }
        catch (NoSuchElementException e)
        {
            Console.WriteLine(e);
            return 0;
        }
    }

    public void EnterInformation(String firstName, String lastName, String zipCode)
    {
        FieldFN.SendKeys(firstName);
        FieldLN.SendKeys(lastName);
        FieldZip.SendKeys(zipCode);
    }

    public void Logout()
    {
        ButtonMenu.Click();
        //ожидание появления ссылки Logout в боковом меню
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));
        try
        {
            IWebElement element = wait.Until<IWebElement>(_driver =>
            {
                IWebElement temp = _driver.FindElement(By.Id("logout_sidebar_link"));
                return (temp.Displayed && temp.Enabled) ? temp : null;
            });
            element.Click();
        }
        catch (WebDriverTimeoutException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}