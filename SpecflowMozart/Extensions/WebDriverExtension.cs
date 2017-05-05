using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SpecflowMozart.Bases;
using SpecflowMozart.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpecflowMozart.Extensions
{
    public static class WebDriverExtensions
    {

        /// <summary>
        /// Wait for the page to load
        /// </summary>
        /// <param name="driver"></param>
        public static void WaitForPageLoaded(this IWebDriver driver)
        {
            driver.WaitForCondition(dri =>
            {
                string state = dri.ExecuteJs("return document.readyState").ToString();
                return state == "complete";
            }, 10);
        }

        public static void WaitForCondition<T>(this T obj, Func<T, bool> condition, int timeOut)
        {
            Func<T, bool> execute =
                (arg) =>
                {
                    try
                    {
                        return condition(arg);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                };

            var stopWatch = Stopwatch.StartNew();
            while (stopWatch.ElapsedMilliseconds < timeOut)
            {
                if (execute(obj))
                {
                    break;
                }
            }
        }


        public static object ExecuteJs(this IWebDriver driver, string script)
        {
            return ((IJavaScriptExecutor)DriverContext.Driver).ExecuteScript(script);
        }

        /// <summary>
        /// Uses javascript execute to click an element
        /// Useful when DOM thinks an element is invisible and it is not
        /// Also helpful when scrolling to an element in a list doesn't work
        /// </summary>
        /// <param name="element">the element to click</param>
        public static void ClickWithJS(IWebElement element)
        {
            IJavaScriptExecutor js = DriverContext.Driver as IJavaScriptExecutor;
            js.ExecuteScript("arguments[0].click();", element);
        }


        /// <summary>
        /// Waits for an element's .displayed attribute to be false.  Sometimes unreliable 
        /// </summary>
        /// <param name="id">HTML element id</param>
        public static void WaitForElementVisibleNoError(string id, int timeout = 5, int pollingMilliseconds = 100)
        {
            try
            {
                WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeout));
                //moved the to less time for the timing 
                Wait.PollingInterval = TimeSpan.FromMilliseconds(pollingMilliseconds);
                Wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(NoSuchFrameException));
                Wait.Until<bool>((d) =>
                {
                    try
                    {
                        return d.FindElement(By.Id(id)).Displayed;
                    }
                    catch (WebDriverException)
                    {
                        return false;
                    }
                });
            }
            catch { }
        }

        /// <summary>
        /// Waits for an element's .displayed attribute to be false.  Sometimes unreliable 
        /// </summary>
        /// <param name="id">HTML element id</param>
        public static void WaitForElementVisibleNoError(IWebElement element, int timeout = 5, int pollingMilliseconds = 100)
        {
            try
            {
                WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeout));
                //moved the to less time for the timing 
                Wait.PollingInterval = TimeSpan.FromMilliseconds(pollingMilliseconds);
                Wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(NoSuchFrameException));
                Wait.Until<bool>((d) =>
                {
                    try
                    {
                        return element.Displayed;
                    }
                    catch (WebDriverException)
                    {
                        return false;
                    }
                });
            }
            catch { }
        }

        /// <summary>
        /// Waits for an element's .displayed attribute to be false.  Sometimes unreliable 
        /// </summary>
        /// <param name="id">HTML element id</param>
        public static void WaitForElementVisibleNoError(By by, int timeout = 5, int pollingMilliseconds = 100)
        {
            try
            {
                WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeout));
                //moved the to less time for the timing 
                Wait.PollingInterval = TimeSpan.FromMilliseconds(pollingMilliseconds);
                Wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(NoSuchFrameException));
                Wait.Until<bool>((d) =>
                {
                    try
                    {
                        return d.FindElement(by).Displayed;
                    }
                    catch (WebDriverException)
                    {
                        return false;
                    }
                });
            }
            catch { }
        }


        /// <summary>
        /// Waits for an element's .displayed attribute to be false.  Sometimes unreliable 
        /// </summary>
        /// <param name="id">HTML element id</param>
        public static void WaitForElementInvisible(string id, int timeout = 120, int pollingMilliseconds = 5000, bool sleep = false)
        {
            if (sleep == true)
            {
                Thread.Sleep(1000);
            }
            WaitForElementVisibleNoError(id);
            WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeout));
            //moved the to less time for the timing 
            Wait.PollingInterval = TimeSpan.FromMilliseconds(pollingMilliseconds);
            Wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(NoSuchFrameException));
            Wait.Until<bool>((d) =>
            {
                try
                {
                    return !d.FindElement(By.Id(id)).Displayed;
                }
                catch (WebDriverException)
                {
                    return false;
                }
            });
        }

        public static void WaitForElementInvisible(By by, int timeout = 120, int pollingMilliseconds = 5000, bool sleep = false)
        {
            if (sleep == true)
            {
                Thread.Sleep(1000);
            }
            WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeout));
            //moved the to less time for the timing 
            Wait.PollingInterval = TimeSpan.FromMilliseconds(pollingMilliseconds);
            Wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(NoSuchFrameException));
            Wait.Until<bool>((d) =>
            {
                try
                {
                    return !d.FindElement(by).Displayed;
                }
                catch (WebDriverException)
                {
                    return false;
                }
            });
        }

        public static void WaitForElementInvisibleXPath(string path, int timeout = 60)
        {
            WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeout));
            wait.Until<bool>((d) =>
            {
                try
                {
                    IWebElement element = d.FindElement(By.XPath(path));
                    return !element.Displayed;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
            });
        }

        public static void WaitForElementInvisibleXPath(IWebElement element, int timeout = 60)
        {

            try
            {
                WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeout));
                wait.PollingInterval = TimeSpan.FromMilliseconds(200);
                wait.Until<bool>((d) =>
                {
                    try
                    {
                        return !element.Displayed;
                    }
                    catch (Exception)
                    {
                        return true;
                    }
                });

            }
            catch { }
            
        }

        public static void TryClickElementRepeatedlyIgnoringError(IWebElement element, int tries = 100)
        {
            for (int i = 0; i == tries; i++)
            {
                try
                {
                    element.Click();
                    //it worked stop trying
                    break;
                }
                catch (Exception)
                {

                }
            }
        }

        public static void TryClickElementHandleError(IWebElement element, int timeoutSeconds = 5, int pollingMilliSeconds = 300)
        {
            try
            {
                WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeoutSeconds));
                Wait.PollingInterval = TimeSpan.FromMilliseconds(pollingMilliSeconds);
                Wait.Until<bool>((d) =>
                {
                    try
                    {
                        element.Click();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });

            }
            catch (Exception e)
            {
                LogHelpers.Write($"Element not clicked Error = {e.ToString()}");
            }

        }

        /// <summary>
        /// Waits for an element's .displayed attribute to be false.  Sometimes unreliable
        /// </summary>
        /// <param name="className"> Class name from HTML</param>
        public static void WaitForElementInvisibleByClassName(string className, int timeoutInSeconds = 60)
        {
            Thread.Sleep(1000);
            WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until<bool>((d) =>
            {
                try
                {
                    IWebElement element = d.FindElement(By.ClassName(className));
                    return !element.Displayed;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
            });
        }

        public static void WaitForElementInvisible(this IWebDriver Driver, IWebElement element, int timeout = 10)
        {
            WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeout));
            wait.Until<bool>((d) =>
            {
                try
                {
                    return !element.Displayed;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
            });
        }

        /// <summary>
        /// Method is used to hold the execution till class for input element is changed.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="expectedClassName"></param>
        public static void WaitForElementClassChange(string id, string expectedClassName, int timeout = 10)
        {
            WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeout));
            wait.Until<bool>((d) =>
            {
                try
                {
                    IWebElement element = d.FindElement(By.Id(id));
                    if (element.GetAttribute("class") == expectedClassName)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
            });
        }

        /// <summary>
        /// Waits for an element's .displayed attribute to be true.  Sometimes unreliable
        /// </summary>
        /// <param name="id">HTML id of the element</param>
        /// <param name="timeout">seconds to wait before timeout</param>
        public static void WaitForElementVisible(string id, int timeout = 30, int pollingMilliSeconds = 200, bool logError = false)
        {
            try
            {
                WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeout));
                Wait.PollingInterval = TimeSpan.FromMilliseconds(pollingMilliSeconds);
                Wait.Until<bool>((d) =>
                {
                    try
                    {
                        return d.FindElement(By.Id(id)).Displayed;
                    }
                    catch (WebDriverException)
                    {
                        return false;
                    }
                });

            }
            catch (Exception e)
            {
                if (logError)
                    LogHelpers.Write($"WaitForElementVisible error = {e.Message} ");
            }
        }

        /// <summary>
        /// Waits for an element's .displayed attribute to be true.  Sometimes unreliable
        /// </summary>
        /// <param name="id">HTML id of the element</param>
        /// <param name="timeout">seconds to wait before timeout</param>
        public static void WaitForElementVisible(this IWebDriver Driver, By by, int timeout)
        {
            //DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            try
            {
                WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeout));
                Wait.PollingInterval = TimeSpan.FromMilliseconds(5000);
                Wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(NoSuchFrameException));
                Wait.Until<bool>((d) =>
                {
                    try
                    {
                        return d.FindElement(by).Displayed;
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });
            }
            catch (Exception)
            {
            }
            //finally { DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10)); }
        }

        /// <summary>
        /// Waits for an element's .displayed attribute to be true.  Sometimes unreliable

        public static bool WaitForElementVisible(IWebElement element, int timeout = 60)
        {
            bool isVisible = true;
            DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            try
            {
                WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeout));
                Wait.PollingInterval = TimeSpan.FromMilliseconds(5000);
                Wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(NoSuchFrameException));
                Wait.Until<bool>((d) =>
                {
                    try
                    {
                        isVisible = element.Displayed;
                        return element.Displayed;
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });
            }
            catch (Exception)
            {
                isVisible = false;
            }
            finally { DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10)); }
            return isVisible;
        }

        public static void WaitForElementVisibleQuick(this IWebDriver Driver, IWebElement element, int timeout = 60)
        {
            
            try
            {
                WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeout));
                Wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                Wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(NoSuchFrameException));
                Wait.Until<bool>((d) =>
                {
                    try
                    {
                        return element.Displayed;
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });
            }
            catch (Exception)
            {
            }
            
        }

        public static void WaitForElementInVisibleQuick(IWebElement element, int timeout = 60)
        {
            DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            try
            {
                WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeout));
                Wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                Wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(NoSuchFrameException));
                Wait.Until<bool>((d) =>
                {
                    try
                    {
                        return !element.Displayed;
                    }
                    catch (NoSuchElementException)
                    {
                        return true;
                    }
                });
            }
            catch (Exception)
            {
            }
            finally { DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10)); }
        }
        /// <summary>
        /// This method returns true or false if element is present. It doesnt wait for default timeout.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static bool IsElementVisibleQuick(IWebElement element, int timeout = 60)
        {
            bool present = true;
            DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            try
            {
                WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeout));
                Wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                Wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(NoSuchFrameException));
                Wait.Until<bool>((d) =>
                {
                    try
                    {
                        return element.Displayed;
                    }
                    catch (NoSuchElementException)
                    {
                        present = false;
                        return false;
                    }
                });
            }
            catch (Exception)
            {
            }
            finally { DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10)); }
            return present;
        }
        public static bool WaitForElementNotFound(string id, int timeoutSeconds)
        {

            DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            try
            {
                WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeoutSeconds));
                Wait.Until<bool>((d) =>
                {
                    if (d.FindElements(By.Id(id)).Count > 0) { return false; }
                    else { return true; }

                });
                //return that element not found
                return true;
            }
            catch (Exception)
            {
                //timed out so element is found 

                return false;
            }
            finally { DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10)); }
        }

        public static bool WaitForElementNotFound(By by, int timeoutSeconds)
        {
            DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            try
            {
                WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeoutSeconds));
                Wait.Until<bool>((d) =>
                {
                    if (d.FindElements(by).Count > 0) { return false; }
                    else { return true; }

                });
                //return that element not found
                return true;
            }
            catch (Exception)
            {
                //timed out so element is found 
                return false;
            }
            finally { DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10)); }
        }

        public static bool IsElementPresentDisplayed(By by)
        {
            DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            try
            {
                IWebElement ele = DriverContext.Driver.FindElement(by);

                if (ele.Displayed) { return true; }
                else { return false; }
            }
            catch (Exception)
            {
                //timed out so element is not found 
                return false;
            }
            finally { DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10)); }
        }

        public static bool ClickElementUntilNotFound(IWebElement element, int timeoutSeconds)
        {
            try
            {
                WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeoutSeconds));
                Wait.Until<bool>((d) =>
                {
                    element.Click();
                    if (element.Displayed) { return false; }
                    else { return true; }

                });
                //return that element not found
                return true;
            }
            catch (Exception)
            {
                //timed out so element is found 
                return false;
            }
        }

        /// <summary>
        /// Waits for an elements test to change form the passed in intial state.
        /// </summary>
        /// <param name="id">HTML id of the element</param>
        /// <param name="initialText">starting text of the element</param>
        public static void WaitForTextChanged(string id, string initialText)
        {
            WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(25));
            wait.Until<bool>((d) =>
            {
                try
                {
                    IWebElement element = d.FindElement(By.Id(id));
                    if (String.Compare(element.Text, "", true) != 0)
                    {
                        if (String.Compare(element.Text, initialText, true) != 0)
                        {
                            return true;
                        }
                        else { return false; }
                    }
                    else { return false; }
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
            });
        }

        /// <summary>
        /// Wait for a control to contain .text
        /// </summary>
        /// <param name="control">Control to see if text exists in</param>
        /// <param name="seconds">seconds to wait, 25 by default</param>
        public static void WaitForTextNotEmpty(IWebElement control, int seconds = 25)
        {
            WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(seconds));
            wait.Until<bool>((d) =>
            {
                try
                {
                    if (String.Compare(control.Text, "", true) != 0)
                    {
                        return true;
                    }
                    else { return false; }
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
            });
        }

        public static IWebElement GetVisibleElementFromLocator(By by)
        {
            var elements = DriverContext.Driver.FindElements(by);

            foreach (var element in elements)
            {
                if (element.Displayed == true)
                {
                    return element;
                }
            }
            return null;
        }

        /// <summary>
        /// Waits for an element to have no child image.  
        /// Good for use when spinners are employed
        /// </summary>
        /// <param name="id">HTML id of element</param>
        public static void WaitForElementHaveNoChildImage(string id)
        {
            WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(25));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until<bool>((d) =>
            {
                try
                {
                    IWebElement element = d.FindElement(By.Id(id));
                    element.FindElement(By.TagName("img"));
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
            });
        }

        public static void WaitForElementHaveNoChildImage(IWebElement element, int timeout = 60)
        {
            WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeout));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until<bool>((d) =>
            {
                try
                {
                    element.FindElement(By.TagName("img"));
                    return false;
                }
                catch (Exception)
                {
                    return true;
                }
            });
        }

        /// <summary>
        /// Waits for an image to be not found on the page using 
        /// the source path to identify the image
        /// </summary>
        /// <param name="source">source path</param>
        public static void WaitForImageBySourceInvisible(string source)
        {
            WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(25));
            wait.Until<bool>((d) =>
            {
                try
                {
                    IWebElement element = d.FindElement(By.XPath("//img[contains(@src,'" + source + "')]"));
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
            });
        }

        /// <summary>
        /// Waits for the inner text of an element to contain text
        /// </summary>
        /// <param name="id"> the HTML id of the element</param>
        public static void WaitForInnerText(string id)
        {
            WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(30));
            wait.Until<bool>((d) =>
            {
                try
                {
                    IWebElement element = d.FindElement(By.Id(id));
                    if (string.IsNullOrEmpty(element.Text))
                    {
                        return false;
                    }
                    else { return true; }

                }
                catch (NoSuchElementException)
                {
                    return true;
                }
            });
        }

        public static void WaitForInnerTextToNotEqual(string id, string text)
        {
            WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(30));
            wait.Until<bool>((d) =>
            {
                try
                {
                    IWebElement element = d.FindElement(By.Id(id));
                    if (string.IsNullOrEmpty(element.Text))
                    {
                        return false;
                    }
                    else
                    {
                        if (string.Compare(element.Text, text, true) != 0)
                        {
                            return true;
                        }
                        else { return false; }
                    }

                }
                catch (NoSuchElementException)
                {
                    return true;
                }
            });
        }

        public static void WaitForInnerTextToNotEqual(By by, string text)
        {
            WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(30));
            wait.Until<bool>((d) =>
            {
                try
                {
                    IWebElement element = d.FindElement(by);
                    if (string.IsNullOrEmpty(element.Text))
                    {
                        return false;
                    }
                    else
                    {
                        if (string.Compare(element.Text, text, true) != 0)
                        {
                            return true;
                        }
                        else { return false; }
                    }

                }
                catch (NoSuchElementException)
                {
                    return true;
                }
            });
        }

        public static void WaitForInnerTextToNotEqual(IWebElement element, string text, int timeoutSeconds, int pollingMilliseconds)
        {
            DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            try
            {
                WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeoutSeconds));
                Wait.PollingInterval = TimeSpan.FromMilliseconds(pollingMilliseconds);
                Wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(NoSuchFrameException));
                Wait.Until<bool>((d) =>
                {
                    try
                    {
                        if (string.Compare(element.Text, text, true) != 0)
                        {
                            return true;
                        }
                        else { return false; }
                    }
                    catch (WebDriverException)
                    {
                        return false;
                    }
                });

            }
            catch (Exception)
            {
            }
            finally { DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10)); }
        }

        public static bool WaitForInnerTextToEqual(IWebElement element, string text, int timeoutSeconds, int pollingMilliseconds)
        {
            bool status = true;
            try
            {
                WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeoutSeconds));
                Wait.PollingInterval = TimeSpan.FromMilliseconds(pollingMilliseconds);
                Wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(NoSuchFrameException));
                Wait.Until<bool>((d) =>
                {
                    try
                    {
                        if (string.Compare(element.Text, text, true) == 0)
                        {
                            return true;
                        }
                        else { return false; }
                    }
                    catch (WebDriverException)
                    {
                        return false;
                    }
                });

            }
            catch (Exception e)
            {
                LogHelpers.Write($"Inner text never set to {text} Prior to timeout of {timeoutSeconds} exception = { e}");
                status = false;
            }

            return status;
        }

        public static bool WaitForTextContains(By by, string text, int timeoutSeconds, int pollingMilliseconds)
        {
            bool status = true;
            try
            {
                WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeoutSeconds));
                Wait.PollingInterval = TimeSpan.FromMilliseconds(pollingMilliseconds);
                Wait.Until<bool>((d) =>
                {
                    try
                    {
                        string actualText = d.FindElement(by).Text;
                        //TestRunner.Reporter.Report("Inner text is  " + actualText,"", TestRun.Status.Done);
                        if (actualText.Trim().Contains(text.Trim()))
                        {
                            return true;
                        }
                        else { return false; }
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });

            }
            catch (Exception)
            {
               
                status = false;
            }

            return status;
        }


        /// <summary>
        /// Uses javascript execute to click an element
        /// Useful when DOM thinks an element is invisible and it is not
        /// Also helpful when scrolling to an element in a list doesn't work
        /// </summary>
        /// <param name="id">the HTML id of the element</param>
        public static void ClickWithJS(string id)
        {
            IJavaScriptExecutor js = DriverContext.Driver as IJavaScriptExecutor;
            js.ExecuteScript("javascript:document.getElementById('" + id + "').click();");
        }

        public static void DoubleClickWithMouseEvent(IWebElement element)
        {
            IJavaScriptExecutor js = DriverContext.Driver as IJavaScriptExecutor;
            js.ExecuteScript("arguments[0].dispatchEvent(new MouseEvent('dblclick', {view: window, bubbles:true, cancelable: true}))", element);
        }

        public static void ClickWithMouseEvent(IWebElement element)
        {
            IJavaScriptExecutor js = DriverContext.Driver as IJavaScriptExecutor;
            js.ExecuteScript("arguments[0].dispatchEvent(new MouseEvent('click', {view: window, bubbles:true, cancelable: true}))", element);
        }

        public static void ScrollIntoView(IWebElement element)
        {
            IJavaScriptExecutor js = DriverContext.Driver as IJavaScriptExecutor;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        /// <summary>
        /// Highlight an element for a time 
        /// Useful in debugging
        /// </summary>
        /// <param name="element">the eleement to highlight</param>
        /// <param name="time">time to highlight before returning to normal</param>
        public static void HighlightElement(IWebElement element, int highlightTimMilliseconds = 20000)
        {
            IJavaScriptExecutor js = DriverContext.Driver as IJavaScriptExecutor;
            String oldStyle = element.GetAttribute("style");
            String args = "arguments[0].setAttribute('style', arguments[1]);";
            js.ExecuteScript(args, element, "border: 4px solid yellow;display:block;");
            Thread.Sleep(highlightTimMilliseconds);
            js.ExecuteScript(args, element, oldStyle);

        }

        /// <summary>
        /// Checks if a string is null or empty
        /// </summary>
        /// <param name="toCheck">string to check</param>
        /// <returns></returns>
        public static bool CheckNull(string toCheck)
        {
            if (string.IsNullOrEmpty(toCheck)) { return true; }
            else { return false; }
        }

        public static void ClickHref(string url)
        {
            var links = DriverContext.Driver.FindElements(By.TagName("a"));
            foreach (var l in links)
            {
                string href = l.GetAttribute("href");
                string logoHref = url.Trim();
                if (String.Compare(href, logoHref, true) == 0)
                {
                    if (l.Displayed)
                    {
                        l.Click();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Clear and then fill a textbox
        /// Great to ensure textbox filled as desired
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        public static void ClearAndFillTextBox(IWebElement element, string text)
        {
            element.Clear();
            if (!CheckNull(text))
            {
                element.SendKeys(text.Trim());
            }
        }

        /// <summary>
        /// Selects an item in a dropdown. if you pass an empty text prameter it will select the passed default
        /// </summary>
        /// <param name="element">DD element</param>
        /// <param name="text">text to select or empty if you want th edefault</param>
        /// <param name="defaultSelection">the default to use</param>
        public static void SelectDropDownByTextWithEmptyDefault(SelectElement element, string text, string defaultSelection)
        {
            if (!CheckNull(text))
            {
                element.SelectByText(text.Trim());
            }
            else { element.SelectByText(defaultSelection); }
        }

        /// <summary>
        /// When interacting with more than one window, wait until the count of windows equals what you want
        /// </summary>
        /// <param name="numberOfWindows">number of windows</param>
        /// <param name="timeoutSeconds">seconds to wait before timeout</param>
        public static void WaitForNumberOfWindowsToEqual(int numberOfWindows, int timeoutSeconds)
        {
            WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.Until(drv => drv.WindowHandles.Count == numberOfWindows);
        }

        //Developer: Ranjit P
        /// <summary>
        /// Returns windows title text by given handle number.
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <returns></returns>
        public static string GetWindowTitleByWindowHandleIndex(int windowHandle = 0)
        {
            string winTitle = String.Empty;
            string currentWindowHandle = DriverContext.Driver.WindowHandles[0];
            if (DriverContext.Driver.WindowHandles.Count > 1)
            {
                DriverContext.Driver.SwitchTo().Window(DriverContext.Driver.WindowHandles[windowHandle]);
                winTitle = DriverContext.Driver.Title;
                // Reset back to calling window as active window.
                DriverContext.Driver.SwitchTo().Window(currentWindowHandle);
            }
            return winTitle;
        }

        //Developer: Rohit C
        /// <summary>
        /// Returns the inner HTML for input element.
        /// </summary>
        /// <param name="webElement"></param>
        /// <returns></returns>
        public static string GetInnerHtml(IWebElement webElement)
        {
            //In case of hidden element "Text" property do not return value even it is present. 
            //In such case this method is useful
            IJavaScriptExecutor js = DriverContext.Driver as IJavaScriptExecutor;
            var innerHTML = js.ExecuteScript("return arguments[0].innerHTML", webElement);
            return innerHTML.ToString();
        }

        //Developer: Ranjit P
        /// <summary>
        /// Return text from a given control from a page
        /// </summary>
        /// <param name="jscriptToRun"></param>
        /// <param name="currentWindowHandle"></param>
        /// <returns></returns>
        public static string RunJavaScript(string jscriptToRun, string currentWindowHandle)
        {
            IJavaScriptExecutor js = DriverContext.Driver as IJavaScriptExecutor;
            return js.ExecuteScript("return " + jscriptToRun, currentWindowHandle).ToString();
        }

        //Developer: Ranjit P
        /// <summary>
        /// Open new browser window and set window handle to last opened window.
        /// </summary>
        public static void OpenNewBrowserWindow()
        {
            IJavaScriptExecutor js = DriverContext.Driver as IJavaScriptExecutor;
            js.ExecuteScript("window.open()");

            //Switch window handles
            List<string> handles = DriverContext.Driver.WindowHandles.ToList<string>();
            DriverContext.Driver.SwitchTo().Window(handles.Last());
        }

        public static long PageLoadTime()
        {
            IJavaScriptExecutor js = DriverContext.Driver as IJavaScriptExecutor;
            var loadEventEnd = js.ExecuteScript("return window.performance.timing.loadEventEnd;");
            var navigationStart = js.ExecuteScript("return window.performance.timing.navigationStart;");
            var domInteractive = js.ExecuteScript("return window.performance.timing.domInteractive;");
            var perf = js.ExecuteScript("return window.performance.getEntries();");
            long toInteractive = Convert.ToInt64(domInteractive) - Convert.ToInt64(navigationStart);
            long complete = Convert.ToInt64(loadEventEnd) - Convert.ToInt64(navigationStart);
            return complete;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout">miliseconds</param>
        /// <returns>true if event not null, false on timeout</returns>
        public static bool WaitPageLoadEventEnd(int timeout)
        {
            IJavaScriptExecutor js = DriverContext.Driver as IJavaScriptExecutor;
            Boolean stillLoading = true;
            int i = 0;
            while (stillLoading)
            {
                var loadEventEnd = js.ExecuteScript("return window.performance.timing.loadEventEnd;");
                if (loadEventEnd != null)
                {
                    return true;
                }
                i += 500;
                if (i < timeout) { System.Threading.Thread.Sleep(500); }
                else
                {
                    return false;
                }

            }
            return false;


        }

        public static void WaitForPageReady(int timeout)
        {
            WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeout));
            wait.Until(driver1 => (bool)((IJavaScriptExecutor)DriverContext.Driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public static void WaitUntilDisplayed(IWebElement element, int timeout = 30000)
        {
            bool go = true;
            int elapsed = 0;
            while (go)
            {
                if (element.Displayed == true) { break; }
                if (elapsed >= timeout) { break; }
                Thread.Sleep(200);
                elapsed += 200;


            }
        }

        public static void WaitUntilEnabledAndDisplayed(IWebElement element, int timeOut = 30000)
        {
            bool go = true;
            int elapsed = 0;
            while (go)
            {
                if ((element.Enabled == true) && (element.Displayed == true)) { break; }
                if (elapsed >= timeOut) { break; }
                Thread.Sleep(200);
                elapsed += 200;
            }
        }

        public static IWebElement SelectItemFromDropDown(IWebElement list, string itemToBeSelected)
        {
            SelectElement selecteOption;
            selecteOption = new SelectElement(list);
            selecteOption.SelectByText(itemToBeSelected);
            return selecteOption.SelectedOption;
        }

        public static string SelectItemFromDropDown(IWebElement list, int index)
        {
            SelectElement selecteOption;
            selecteOption = new SelectElement(list);
            selecteOption.SelectByIndex(index);
            return selecteOption.SelectedOption.Text;
        }

        public static IWebElement GetSelectedItemFromDropDown(IWebElement list)
        {
            SelectElement selecteOption;
            selecteOption = new SelectElement(list);
            return selecteOption.SelectedOption;
        }

        public static void EnterTextInToInput(IWebElement inputControl, bool clearExistingText, string inputText)
        {
            if (clearExistingText)
            {
                inputControl.Clear();
            }
            inputControl.SendKeys(inputText);
        }

        public static bool IsElementPresent(string id)
        {
            if (DriverContext.Driver.FindElements(By.Id(id)).Count > 0) { return true; }
            else { return false; }
        }

        public static bool IsElementPresentAndVisible(By by)
        {
            DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            try
            {
                if (DriverContext.Driver.FindElements(by).Count > 0) { if (DriverContext.Driver.FindElement(by).Displayed) { return true; } else { return false; } }
            }
            catch { }
            finally { DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10)); }
            return false;
        }

        public static bool VerifyElementPresentNotPresent(By by, string messagePart, bool positiveCheck = true)
        {
            if (positiveCheck)
            {
                if (IsElementPresentAndVisible(by))
                {
                    
                    return true;
                }
                else
                {
                    
                    return false;
                }
            }
            else
            {
                if (IsElementPresentAndVisible(by))
                {
                   
                    return false;
                }
                else
                {
                    
                    return true;
                }
            }
        }

        public static string GetStringAfterThisChar(string sourceData, string whereToStart)
        {
            int l = sourceData.IndexOf(whereToStart);
            if (l > 0)
            {
                return sourceData.Substring(l + 1, (sourceData.Length - l - 1));
            }
            return "";

        }

        /// <summary>
        /// Compare 2 Lists in order
        /// Will fail is count is different or text at each index is different.
        /// </summary>
        /// <param name="expected">Expected list</param>
        /// <param name="actual">Actual List</param>
        /// <returns>True if lists are the same</returns>
        public static bool ListSame(List<string> expected, List<string> actual)
        {
            List<string> results = actual.Except(expected).ToList();
            if (results.Count == 0) { return true; }
            else { return false; }
        }

        public static bool ListSame(List<int> expected, List<int> actual)
        {
            List<int> results = actual.Except(expected).ToList();
            if (results.Count == 0) { return true; }
            else { return false; }
        }

        public static void ScrollDownUntilVisible(int pixels, IWebElement element)
        {
            IJavaScriptExecutor js = DriverContext.Driver as IJavaScriptExecutor;

            for (int i = 0; i < 5; i++)
            {
                if (element.Enabled != true)
                {
                    js.ExecuteScript("scroll(0," + pixels + ");");
                }
                else { break; }
            }

        }

        //Developer: Ranjit P
        /// <summary>
        /// Wait for any active jQuery to complete.
        /// </summary>
        /// <param name="timeout"></param>
        public static void WaitForAjax(this IWebDriver Driver, int timeout = 10)
        {
            WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeout));
            wait.Until(driver1 => (bool)((IJavaScriptExecutor)DriverContext.Driver).ExecuteScript("return jQuery.active == 0"));
        }

        //public static IWebElement LookFast(By by, int timeoutSeconds = 5, int pollingIntervalMilliseconds = 200, bool reportError = true)
        //{
        //    DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
        //    IWebElement element = null;
        //    try
        //    {
        //        WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeoutSeconds));
        //        Wait.PollingInterval = TimeSpan.FromMilliseconds(pollingIntervalMilliseconds);
        //        Wait.Until<bool>((d) =>
        //        {

        //            try
        //            {
        //                element = d.FindElement(by);
        //                if (element.Displayed && element.Enabled)
        //                    return true;
        //                else return false;
        //            }
        //            catch (Exception)
        //            {
        //                return false; // wait.until keeps going
        //            }

        //        });
        //    }
        //    catch (Exception e)
        //    {
        //        if (reportError)
        //        {
        //            //this means the wait threw an uncaught exception and is done trying
        //            TestRunner.Reporter.Report("find element fast", " By statement = " + by.ToString() + " the error = " + e.ToString(), TestRun.Status.Done);
        //        }
        //    }
        //    finally
        //    {
        //        DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        //    }
        //    return element;
        //}

        public static IWebElement LookFastWithScope(By scopeElementBy, By targetElementBy, int timeoutSeconds = 5, int pollingIntervalMilliseconds = 200, bool reportError = true)
        {
            DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            IWebElement element = null;
            try
            {
                WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeoutSeconds));
                Wait.PollingInterval = TimeSpan.FromMilliseconds(pollingIntervalMilliseconds);
                Wait.Until<bool>((d) =>
                {

                    try
                    {
                        IWebElement scopeElement = d.FindElement(scopeElementBy);
                        element = scopeElement.FindElement(targetElementBy);
                        if (element.Displayed && element.Enabled)
                            return true;
                        else return false;
                    }
                    catch (Exception)
                    {
                        return false; // wait.until keeps going
                    }

                });
            }
            catch (Exception e)
            {
                if (reportError)
                {
                    //this means the wait threw an uncaught exception and is done trying
                    
                }
            }
            finally
            {
                DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            }
            return element;
        }

        public static IWebElement LookFastWithScope(IWebElement scopeElement, By targetElementBy, int timeoutSeconds = 5, int pollingIntervalMilliseconds = 200, bool reportError = true)
        {
            // in this method we are only using the timeout function as the d is never used 
            DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            IWebElement element = null;
            try
            {
                WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeoutSeconds));
                Wait.PollingInterval = TimeSpan.FromMilliseconds(pollingIntervalMilliseconds);
                Wait.Until<bool>((d) =>
                {

                    try
                    {
                        element = scopeElement.FindElement(targetElementBy);
                        if (element.Displayed && element.Enabled)
                            return true;
                        else return false;
                    }
                    catch (Exception)
                    {
                        return false; // wait.until keeps going
                    }

                });
            }
            catch (Exception e)
            {
                if (reportError)
                {
                    //this means the wait threw an uncaught exception and is done trying
                    
                }
            }
            finally
            {
                DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            }
            return element;
        }

        //public static IReadOnlyCollection<IWebElement> LookFastElementCollection(By by, int timeoutSeconds = 5, int pollingIntervalMilliseconds = 200, bool reportError = true)
        //{
            
        //    IReadOnlyCollection<IWebElement> elements = null;
        //    try
        //    {
        //        WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeoutSeconds));
        //        Wait.PollingInterval = TimeSpan.FromMilliseconds(pollingIntervalMilliseconds);
        //        Wait.Until<bool>((d) =>
        //        {

        //            try
        //            {
        //                elements = d.FindElements(by);
        //                return true;
        //            }
        //            catch (Exception)
        //            {
        //                return false; // wait.until keeps going
        //            }

        //        });
        //    }
        //    catch (Exception e)
        //    {
        //        if (reportError)
        //        {
        //            //this means the wait threw an uncaught exception and is done trying
        //            LogHelpers.Write($"find element fast By statement =  {by.ToString()} the error = {e.ToString()}");
        //        }
        //    }
        //    finally
        //    {
        //        DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        //    }
        //    return elements;
        //}

        public static IReadOnlyCollection<IWebElement> LookFastElementCollectionWithScope(By scopeBy, By targetBy, int timeoutSeconds = 5, int pollingIntervalMilliseconds = 200, bool reportError = true)
        {
            DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            IReadOnlyCollection<IWebElement> elements = null;
            try
            {
                WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeoutSeconds));
                Wait.PollingInterval = TimeSpan.FromMilliseconds(pollingIntervalMilliseconds);
                Wait.Until<bool>((d) =>
                {

                    try
                    {
                        elements = d.FindElement(scopeBy).FindElements(targetBy);
                        return true;
                    }
                    catch (Exception)
                    {
                        return false; // wait.until keeps going
                    }

                });
            }
            catch (Exception)
            {
                if (reportError)
                {
                    //this means the wait threw an uncaught exception and is done trying
                }
            }
            finally
            {
                DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            }
            return elements;
        }

        //public static IReadOnlyCollection<IWebElement> LookFastElementCollectionWithScope(IWebElement element, By targetBy, int timeoutSeconds = 5, int pollingIntervalMilliseconds = 200, bool reportError = true)
        //{
        //    DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeoutSeconds));
        //    IReadOnlyCollection<IWebElement> elements = null;
        //    try
        //    {
        //        WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeoutSeconds));
        //        Wait.PollingInterval = TimeSpan.FromMilliseconds(pollingIntervalMilliseconds);
        //        Wait.Until<bool>((d) =>
        //        {

        //            try
        //            {
        //                elements = element.FindElements(targetBy);
        //                return true;
        //            }
        //            catch (Exception)
        //            {
        //                return false; // wait.until keeps going
        //            }

        //        });
        //    }
        //    catch (Exception e)
        //    {
        //        if (reportError)
        //        {
        //            //this means the wait threw an uncaught exception and is done trying
        //            TestRunner.Reporter.Report("find element fast", " By statement = " + targetBy.ToString() + " the error = " + e.ToString(), TestRun.Status.Done);
        //        }
        //    }
        //    finally
        //    {
        //        DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        //    }
        //    return elements;
        //}

        //public static void WaitTemplate(IWebElement element, By by = null, int timeoutSeconds = 20, int pollingIntervalMilliseconds = 1000, bool reportError = true)
        //{
        //    if (by == null)
        //    {
        //        by = By.Id("garbage");
        //    }

        //    int i = 0;

        //    try
        //    {
        //        //sets the implicit wait to zero for the element parameter
        //        DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
        //        WebDriverWait Wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeoutSeconds));
        //        Wait.PollingInterval = TimeSpan.FromMilliseconds(pollingIntervalMilliseconds);
        //        //Using the below statement you can have the wait itself ignore one or many thrown exception types
        //        //Doing this would keep the wait until running even if those exceptions are  
        //        //Wait.IgnoreExceptionTypes(typeof(NoSuchFrameException),typeof(NoSuchElementException),
        //        //    typeof(NoSuchWindowException),typeof(StaleElementReferenceException));

        //        // d below is the webdriver we have created to use the polling mechanism and time out
        //        // we may use d if we need to using the By in the parameters.  Remember d's implicit wait will be 0
        //        Wait.Until<bool>((d) =>
        //        {
        //            i++;
        //            System.Diagnostics.Debug.WriteLine("Starting the Logic");
        //            System.Diagnostics.Debug.WriteLine("i =  " + i + " time is " + DateTime.Now.ToString("h:mm:ss.fff tt"));
        //            try
        //            {
        //                if (i > 10)
        //                {
        //                    //cause an exception by navigating away from google but still looking for Google element
        //                    DriverContext.Driver.Navigate().GoToUrl("yahoo.com");
        //                    System.Diagnostics.Debug.WriteLine("Now on yahoo find starts at " + DateTime.Now.ToString("h:mm:ss.fff tt"));
        //                    element.GetAttribute("value");
        //                    return true; //would kill the wait.until - this is what you would return when your condition is met - we never get here becaue previous line throws error
        //                }
        //                else
        //                {
        //                    if (i <= 10)
        //                    {
        //                        System.Diagnostics.Debug.WriteLine("element value " + element.GetAttribute("value") + " time is " + DateTime.Now.ToString("h:mm:ss.fff tt"));
        //                        // Throw exception with d by looking for garbage
        //                        System.Diagnostics.Debug.WriteLine("try to use d.FindElemnt " + d.FindElement(by).Text + " time is " + DateTime.Now.ToString("h:mm:ss.fff tt"));
        //                    }
        //                    return false; // wait.until keeps going
        //                }
        //            }
        //            catch (NoSuchElementException e)
        //            {
        //                System.Diagnostics.Debug.WriteLine("When NSE exception caught " + DateTime.Now.ToString("h:mm:ss.fff tt"));
        //                System.Diagnostics.Debug.WriteLine(e);
        //                return false; // wait.until keeps going
        //            }
        //            catch (StaleElementReferenceException e)
        //            {
        //                System.Diagnostics.Debug.WriteLine("When SERE exception caught " + DateTime.Now.ToString("h:mm:ss.fff tt"));
        //                System.Diagnostics.Debug.WriteLine(e);
        //                return false; // wait.until keeps going
        //            }
        //        });
        //    }
        //    catch (Exception e)
        //    {
        //        //this means the wait threw an exception and is done trying
        //        System.Diagnostics.Debug.WriteLine("Wait Until Exception at " + DateTime.Now.ToString("h:mm:ss.fff tt"));
        //        System.Diagnostics.Debug.WriteLine(e);
        //    }
        //    finally
        //    {
        //        //Must reset implicit wait for the DriverContext.Driver to or it will remain 0
        //        DriverContext.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        //        System.Diagnostics.Debug.WriteLine("In finally: WebDriver reset to Default implicit wait default = " + 10);
        //    }

        //}

       

        //public static DataSet DownloadExcelFile(string sheetName)
        //{
        //    DataSet excelDataTable = null;
        //    try
        //    {
        //        DownLoadAndReadFile downLoadExcel = new DownLoadAndReadFile();
        //        var directory = new DirectoryInfo(ConfigurationManager.AppSettings["fileDownloadPath"]);
        //        string[] files = directory.GetFiles()
        //                     .OrderByDescending(f => f.CreationTime).Select(a => a.FullName).ToArray();
        //        excelDataTable = downLoadExcel.GetExcelData(files[0], sheetName);
                
        //    }
        //    catch (Exception e)
        //    {
        //        LogHelpers.Write("Error while downloading an excel file");                
        //    }
        //    return excelDataTable;
        //}
    }
}
