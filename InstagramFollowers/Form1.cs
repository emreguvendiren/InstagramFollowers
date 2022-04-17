using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstagramFollowers
{
    public partial class Form1 : Form
    {
        IWebDriver driver;
        string username;
        List<string> Followers = new List<string>();
        List<string> Follows = new List<string>();
        List<string> notFollow = new List<string>();
        List<string> notFollowers = new List<string>();
        string deneme;

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
            listBox1.Visible = false;
            label3.Visible = false;
            listBox2.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            comboBox1.Visible = false;
            button1.Visible = false;
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
                Thread.Sleep(5000);
                IWebElement userNameInstagram = driver.FindElement(By.Name("username"));
                IWebElement passwordInstagram = driver.FindElement(By.Name("password"));
                IWebElement loginBtn = driver.FindElement(By.CssSelector(".sqdOP.L3NKy.y3zKF"));


                userNameInstagram.SendKeys(username);
                passwordInstagram.SendKeys(password);
                Thread.Sleep(2000);
                loginBtn.Click();
                Thread.Sleep(7000);
                driver.Navigate().GoToUrl("https://www.instagram.com/" + username);
                Thread.Sleep(2000);

                IWebElement follows = driver.FindElement(By.CssSelector("#react-root > section > main > div > header > section > ul > li:nth-child(2) > a"));
                follows.Click();
                Thread.Sleep(1000);
                string jsCommand = "" +
                "sayfa = document.querySelector('.isgrP');" +
                "sayfa.scrollTo(0,sayfa.scrollHeight);" +
                "var sayfaSonu = sayfa.scrollHeight;" +
                "return sayfaSonu;";

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                var sayfaSonu = Convert.ToInt32(js.ExecuteScript(jsCommand));

                while (true)
                {
                    var son = sayfaSonu;
                    Thread.Sleep(1100);
                    sayfaSonu = Convert.ToInt32(js.ExecuteScript(jsCommand));
                    if (son == sayfaSonu)
                        break;
                }

                IReadOnlyCollection<IWebElement> TakipEdilenler = driver.FindElements(By.CssSelector(".notranslate._0imsa"));
                foreach (IWebElement item in TakipEdilenler)
                {
                    Follows.Add(item.Text);
                }
                Thread.Sleep(500);

                IWebElement close = driver.FindElement(By.XPath("/html/body/div[6]/div/div/div/div[1]/div/div[3]/div/button"));
                close.Click();
                Thread.Sleep(500);
                //#react-root > section > main > div > header > section > ul > li:nth-child(3) > a
                IWebElement followers = driver.FindElement(By.CssSelector("#react-root > section > main > div > header > section > ul > li:nth-child(3) > a"));
                followers.Click();
                Thread.Sleep(1000);

                var sayfaSonu2 = Convert.ToInt32(js.ExecuteScript(jsCommand));

                while (true)
                {
                    var son = sayfaSonu2;
                    Thread.Sleep(1100);
                    sayfaSonu2 = Convert.ToInt32(js.ExecuteScript(jsCommand));
                    if (son == sayfaSonu2)
                        break;
                }

                IReadOnlyCollection<IWebElement> Takipciler = driver.FindElements(By.CssSelector(".notranslate._0imsa"));
                foreach (IWebElement item in Takipciler)
                {
                    Followers.Add(item.Text);
                }
                Thread.Sleep(500);

                foreach (var follow in Follows)
                {
                    int sayac = 0;
                    foreach (var followrs in Followers)
                    {
                        if (follow == followrs)
                        {
                            sayac = 1;
                        }
                    }
                    if (sayac == 0)
                    {
                        notFollow.Add(follow);
                    }
                }
                foreach (var followrs in Followers)
                {
                    int sayac = 0;
                    foreach (var follow in Follows)
                    {
                        if (followrs == follow)
                        {
                            sayac = 1;
                        }
                    }
                    if (sayac == 0)
                    {
                        notFollowers.Add(followrs);
                    }
                }

                listBox1.Visible = true;
                label3.Visible = true;
                listBox2.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                comboBox1.Visible = true;
                button1.Visible = true;
                int sayac2 = 1;
                foreach (var item in notFollow)
                {
                    listBox1.Items.Add(sayac2+"->"+item);
                    sayac2++;
                }
                int sayac3 = 1;
                foreach (var item in notFollowers)
                {
                    listBox2.Items.Add(sayac3 + "->" + item);
                    comboBox1.Items.Add(item);
                    sayac3++;
                }



            }
        }
        public static void scrollToBack() {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                driver.Navigate().GoToUrl("https://www.instagram.com/" + comboBox1.SelectedItem.ToString());
                Thread.Sleep(3000);
                IWebElement unfollow = driver.FindElement(By.XPath("/html/body/div[1]/section/main/div/header/section/div[1]/div[1]/div/div[2]/div/span/span[1]/button"));
                Thread.Sleep(500);
                unfollow.Click();
                Thread.Sleep(1000);
                IWebElement unfollowclick = driver.FindElement(By.XPath("/html/body/div[6]/div/div/div/div[3]/button[1]"));
                unfollowclick.Click();
                MessageBox.Show(comboBox1.SelectedItem.ToString()+" Hesabı başarıyla takipten çıkıldı!");
            }
            catch (Exception)
            {

                MessageBox.Show("Beklenmeyen bir hata olustu.");
            }
        }
    }
}
