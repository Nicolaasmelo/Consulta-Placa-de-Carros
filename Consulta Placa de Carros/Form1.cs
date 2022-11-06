using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Consulta_Placa_de_Carros
{
    public partial class Form1 : Form
    {
        public static IWebDriver driver;
        public static WebDriverWait wait;
        public static string url = "https://www.consultarplaca.com.br/";
        public static string pageUrl = "https://www.consultarplaca.com.br/";

        

        public Form1()
        {
            InitializeComponent();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string placa = txtPlaca.Text;
            //driver = webdriver.Chrome()
            //driver = new ChromeDriver(OptionsChrome());
            ChromeDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl(url);

            driver.FindElement(By.Id("query_car_plate")).SendKeys(placa);

            driver.FindElement(By.Id("query_email")).SendKeys(email);

            Thread.Sleep(2000);

            driver.FindElement(By.Id("btn_consult_car_plate")).Click();

           // var modelo = driver.FindElement(By.CssSelector("#table table-striped > tbody > th > td")).GetCssValue;
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {

        }
    }
}