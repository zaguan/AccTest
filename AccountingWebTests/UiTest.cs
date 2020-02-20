using System.Web.UI.HtmlControls;
using Atata;
using NUnit.Framework;

namespace AccountingWebTests
{
    using _ = BudgetPage;

    [Url("Index")] // Relative URL of the page.
    public class BudgetPage : Page<_>
    {
        [FindById("YearMonth")] public TextInput<_> YearMonth { get; private set; }

        [FindById("Amount")] public TextInput<_> Amount { get; private set; }

        [FindById("Create")] public Button<_> Create { get; private set; }
    }

    [TestFixture]
    public class UiTest
    {
        //if you cannot run web test, please check if you use the Chrome version 80 browser.
        //if your version is less than 80, you may need to downgrade nuget package "Selenium.WebDriver.ChromeDriver" version to the appropriate version

        [SetUp]
        public void SetUp()
        {
            // Find information about AtataContext set-up on https://atata.io/getting-started/#set-up
            AtataContext.Configure()
                .UseChrome()
                //    WithArguments("start-maximized").
                .UseBaseUrl("https://localhost:44314/Budget/")
                .UseCulture("en-us").UseNUnitTestName()
                .AddNUnitTestContextLogging().LogNUnitError()
                .UseAssertionExceptionType<NUnit.Framework.AssertionException>()
                .UseNUnitAggregateAssertionStrategy().Build();
        }

        [TearDown]
        public void TearDown()
        {
            AtataContext.Current?.CleanUp();
        }

        [Test]
        public void create_budget()
        {
            // Go.ToUrl("https://dotblogs.com.tw/hatelove/1");
            Go.To<BudgetPage>().YearMonth.Set("202001").Amount.Set("1000000").Create.Click();
        }
    }
}
