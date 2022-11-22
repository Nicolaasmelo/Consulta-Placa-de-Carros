using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Data.SqlClient;

namespace Consulta_Placa_de_Carros
{
    public partial class Form1 : Form
    {
        public static IWebDriver driver;
        public static WebDriverWait wait;
        public static string url = "https://www.consultarplaca.com.br/";
        public static string pageUrl = "https://www.consultarplaca.com.br/";
        string ConnectionString = "Data Source=DESKTOP-PII2JGD\\MSSQLSERVER01;Initial Catalog=master;Integrated Security=True";



        public Form1()
        {
            InitializeComponent();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            
            try
            {
                string email = txtEmail.Text;
                string placa = txtPlaca.Text;
                //driver = webdriver.Chrome()
                //driver = new ChromeDriver(OptionsChrome());
                ChromeDriver driver = new ChromeDriver();

                driver.Navigate().GoToUrl(url);

                lstbox.Items.Add("Iniciando a Captura de informações");

                driver.FindElement(By.Id("query_car_plate")).SendKeys(placa);

                driver.FindElement(By.Id("query_email")).SendKeys(email);

                Thread.Sleep(2000);

                driver.FindElement(By.Id("btn_consult_car_plate")).Click();

                Thread.Sleep(5000);

                string capturaInformacao = driver.FindElement(By.Id("modal-consult-result")).Text.ToString();
              

                var modelo = capturaInformacao.Substring(capturaInformacao.IndexOf("Marca/Modelo") + 13, capturaInformacao.IndexOf("Cor")).Split("\r\n")[0];

                var cor = capturaInformacao.Substring(capturaInformacao.IndexOf("Cor") + 3, capturaInformacao.IndexOf("Ano")).Split("\r\n")[0];

                var ano = capturaInformacao.Substring(capturaInformacao.IndexOf("Ano Fabricação") + 16).Split("\r\n")[0];

                var chassi = capturaInformacao.Substring(capturaInformacao.IndexOf("Chassi") + 6);

                lstbox.Items.Add("informações Informações capturadas com sucesso");

                driver.Close();

                Inserir(email, placa, modelo, ano, cor, chassi);

                lstbox.Items.Add("informações salvas no bando de dados com  sucesso");

            }
            catch(Exception ex) 
            {
                lstbox.Items.Add("Erro ao captuar as informações");
            }
        }

        public void Inserir( string Email, string Placa, string Modelo, string Ano, string Cor, string Chassi)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand("Insert into ConsultaPlaca (Email,Placa,Modelo,Ano,Cor,Chassi) values ('"+ Email+"','"+ Placa +"','"+ Modelo + "','"+ Ano + "','"+ Cor + "','"+ Chassi +"')", conn);

                    conn.Open();
                    command.ExecuteNonQuery();


                    conn.Close();

                }
                catch(Exception ex)
                {
                    conn.Close();
                    throw new Exception("Falha na conexão com banco de dados " + ex.Message);
                }
            }
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            try
            {
                //List<LucrosDTO> listar = distribuicaoLucros.Listar();
                //distribuicaoLucros.GerarRelatorio(listar);
                //List<LucrosDTO> relatorio = distribuicaoLucros.Listar();
                //dtgGrid2.DataSource = relatorio;
            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}