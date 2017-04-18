using SpecflowMozart.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.XPath;

namespace SpecflowMozart.Config
{
    public class ConfigReader : BasePage
    {


        public static void SetFrameworkSettings()
        {

            XPathItem aut;
            XPathItem testtype;
            XPathItem islog;
            XPathItem isreport;
            XPathItem buildname;
            XPathItem logPath;
            XPathItem browser;



            //string strFilename = Environment.CurrentDirectory.ToString() + @"\Config\GlobalConfig.xml";
            //Console.WriteLine(Environment.CurrentDirectory.ToString());

            string strFileName = @"E:\Project\SpecflowFramework\SpecflowFramework\Config\GlobalConfig.xml";
            FileStream stream = new FileStream(strFileName, FileMode.Open);

            XPathDocument document = new XPathDocument(stream);


            XPathNavigator navigator = document.CreateNavigator();
            //Get XML Details and pass it in XPathItem type variables
            browser = navigator.SelectSingleNode("SpecflowFramework/RunSettings/Browser");
            aut = navigator.SelectSingleNode("SpecflowFramework/RunSettings/AUT");
            buildname = navigator.SelectSingleNode("SpecflowFramework/RunSettings/BuildName");
            testtype = navigator.SelectSingleNode("SpecflowFramework/RunSettings/TestType");
            islog = navigator.SelectSingleNode("SpecflowFramework/RunSettings/IsLog");
            isreport = navigator.SelectSingleNode("SpecflowFramework/RunSettings/IsReport");
            logPath = navigator.SelectSingleNode("SpecflowFramework/RunSettings/LogPath");

            //Set XML Details in the property to be used accross framework
            Settings.AUT = aut.Value.ToString();
            Settings.BuildName = buildname.Value.ToString();
            Settings.TestType = testtype.Value.ToString();
            Settings.IsLog = islog.Value.ToString();
            Settings.IsReporting = isreport.Value.ToString();
            Settings.LogPath = logPath.Value.ToString();
            Settings.BrowserType = (BrowserType)Enum.Parse(typeof(BrowserType), browser.Value.ToString());
        }

    }
}
