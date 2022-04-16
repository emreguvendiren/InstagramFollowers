using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstagramFollowers
{
    public partial class Form1 : Form
    {
        IWebDriver driver;
        string username;
        List<String> Followers = new List<String>();
        List<String> Follows = new List<String>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            //driver = new ChromeDriver();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            username = txtUserName.Text;
            string password = txtPassword.Text;
            if (username == null || username == "" || password == null || password == "")
            {
                MessageBox.Show("Gecersiz kullanici adi veya sifre!");
            }
            else
            {
                driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://www.instagram.com");
                IWebElement userNameInstagram = driver.FindElement(By.Name("username"));
                IWebElement passwordInstagram = driver.FindElement(By.Name("password"));

                userNameInstagram.SendKeys(username);
                passwordInstagram.SendKeys(password);


            }
        }
    }
}
