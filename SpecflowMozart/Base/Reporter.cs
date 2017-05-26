using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;

namespace SpecflowMozart.Bases
{
    public class Report
    {
        public static ExtentReports extent;
        public static ExtentHtmlReporter reporter;
        public static ExtentTest feature;
        public static MediaEntityModelProvider builder;
        public static ExtentTest scenario;


        public static void ReportInitialize(string reportFilePath)
        {

            reporter = new ExtentHtmlReporter(reportFilePath);
            extent = new ExtentReports();

            extent.AttachReporter(reporter);
            

            //test = extent.CreateTest(testName);

            //builder = MediaEntityBuilder.CreateScreenCaptureFromPath("D:\\DataLoad POC\\TestReports", "Error.png").Build();

        }

        public static void AddFeature(string featureName)
        {
            feature = extent.CreateTest<Feature>(featureName);
        }

        public static void TestReportInitialize(string TestName)
        {
            scenario = feature.CreateNode<Scenario>(TestName);
        }

        public static void GenerateReport()
        {
            //reporter.Flush();
            extent.Flush();
        }

    }
}
