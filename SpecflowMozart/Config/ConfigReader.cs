using SpecflowMozart.Bases;
using SpecflowMozart.Helper;
using SpecflowMozart.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
            XPathItem useLocalSettings;
            XPathItem userName;
            XPathItem password;
            XPathItem customer;
            XPathItem reportPath;


            //string strFilename = Environment.CurrentDirectory.ToString() + @"\Config\GlobalConfig.xml";
            //Console.WriteLine(Environment.CurrentDirectory.ToString());

            string strFileName = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\Config\GlobalConfig.xml";
            FileStream stream = new FileStream(strFileName, FileMode.Open);

            XPathDocument document = new XPathDocument(stream);


            XPathNavigator navigator = document.CreateNavigator();
            //Get XML Details and pass it in XPathItem type variables
            browser = navigator.SelectSingleNode("SpecflowMozart/RunSettings/Browser");
            aut = navigator.SelectSingleNode("SpecflowMozart/RunSettings/AUT");
            buildname = navigator.SelectSingleNode("SpecflowMozart/RunSettings/BuildName");
            testtype = navigator.SelectSingleNode("SpecflowMozart/RunSettings/TestType");
            islog = navigator.SelectSingleNode("SpecflowMozart/RunSettings/IsLog");
            isreport = navigator.SelectSingleNode("SpecflowMozart/RunSettings/IsReport");
            logPath = navigator.SelectSingleNode("SpecflowMozart/RunSettings/LogPath");
            useLocalSettings = navigator.SelectSingleNode("SpecflowMozart/RunSettings/UseLocalSettings");

            userName = navigator.SelectSingleNode("SpecflowMozart/RunSettings/UserName");
            password = navigator.SelectSingleNode("SpecflowMozart/RunSettings/Password");
            customer = navigator.SelectSingleNode("SpecflowMozart/RunSettings/Customer");
            reportPath = navigator.SelectSingleNode("SpecflowMozart/RunSettings/ReportPath");
            //Set XML Details in the property to be used accross framework
            Settings.AUT = aut.Value.ToString();
            Settings.BuildName = buildname.Value.ToString();
            Settings.TestType = testtype.Value.ToString();
            Settings.IsLog = islog.Value.ToString();
            Settings.IsReporting = isreport.Value.ToString();
            Settings.LogPath = logPath.Value.ToString();
            Settings.BrowserType = (BrowserType)Enum.Parse(typeof(BrowserType), browser.Value.ToString());
            Settings.useLocalSettings = useLocalSettings.Value.ToString();
            Settings.customer = customer.Value.ToString();
            Settings.userName = userName.Value.ToString();
            Settings.password = password.Value.ToString();
            Settings.ReportPath = reportPath.Value.ToString();
            //LogHelpers.CreateLogFile();
            //string path = $"{Settings.ReportPath}\\Test_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            //Report.ReportInitialize(path);
            //Console.WriteLine(path);


        }

    }
}
