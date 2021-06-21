using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Data;
using System.Data.SqlClient;

namespace ProjectSar
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer timer = new DispatcherTimer();

        public int intrerv = 10;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            authorizationLabel.Visibility = Visibility.Hidden;
            authorizationBorder.Visibility = Visibility.Hidden;
            blockLabel.Visibility = Visibility.Visible;
            loginTextBox.Visibility = Visibility.Hidden;
            passwordBox.Visibility = Visibility.Hidden;
            showPassword.Visibility = Visibility.Hidden;
            passwordTextBox.Visibility = Visibility.Hidden;
            captchaGenTextBox.Visibility = Visibility.Hidden;
            captchaTextBox.Visibility = Visibility.Hidden;
            refreshButton.Visibility = Visibility.Hidden;
            loginButton.Visibility = Visibility.Hidden;
            loginLabel.Visibility = Visibility.Hidden;
            passwordLabel.Visibility = Visibility.Hidden;
            captchaLabel.Visibility = Visibility.Hidden;
            imageOne.Visibility = Visibility.Hidden;

            intrerv--;
            if (intrerv == 0)
            {
                blockLabel.Visibility = Visibility.Hidden;
                authorizationBorder.Visibility = Visibility.Visible;
                authorizationLabel.Visibility = Visibility.Visible;
                loginTextBox.Visibility = Visibility.Visible;
                passwordBox.Visibility = Visibility.Visible;
                showPassword.Visibility = Visibility.Visible;
                captchaGenTextBox.Visibility = Visibility.Visible;
                captchaTextBox.Visibility = Visibility.Visible;
                refreshButton.Visibility = Visibility.Visible;
                loginButton.Visibility = Visibility.Visible;
                loginLabel.Visibility = Visibility.Visible;
                passwordLabel.Visibility = Visibility.Visible;
                captchaLabel.Visibility = Visibility.Visible;
                imageOne.Visibility = Visibility.Visible;

                CaptchaGenerator();
                timer.Stop();
                intrerv = 10;
            }

            blockLabel.Content = "Блокировка авторизации " + intrerv.ToString() + " секунд";
        }

        void CaptchaGenerator()
        {
            captchaGenTextBox.Visibility = Visibility.Visible;
            captchaTextBox.Visibility = Visibility.Visible;
            refreshButton.Visibility = Visibility.Visible;
            captchaLabel.Visibility = Visibility.Visible;

            char[] chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789".ToCharArray();
            string randomString = "";
            Random ran = new Random();
            for (int i = 0; i < 5; i++)
            {
                randomString += chars[ran.Next(0, chars.Length)];
            }
            captchaGenTextBox.Text = randomString;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            base.OnClosed(e);
            App.Current.Shutdown();
        }

        private void showPassword_Click(object sender, RoutedEventArgs e)
        {
            if (showPassword.IsChecked == true)
            {
                passwordTextBox.Text = passwordBox.Password;
                passwordTextBox.Visibility = Visibility.Visible;
                passwordBox.Visibility = Visibility.Hidden;
            }
            else
            {
                passwordBox.Password = passwordTextBox.Text;
                passwordTextBox.Visibility = Visibility.Hidden;
                passwordBox.Visibility = Visibility.Visible;
            }
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            String allowchar = "";
            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            char[] a = { ',' };
            String[] ar = allowchar.Split(a);
            String pwd = "";
            string temp = "";

            Random r = new Random();
            for (int i = 0; i < 4; i++)
            {
                temp = ar[(r.Next(0, ar.Length))];
                pwd += temp;
            }

            if (captchaGenTextBox.Text != "")
            {
                captchaGenTextBox.Text = null;
            }

            captchaGenTextBox.Text = pwd;
        }

        private void mainBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public DataTable Select(string selectSQL) // функция подключения к базе данных и обработка запросов

        {

            DataTable dataTable = new DataTable("dataBase"); // создаём таблицу в приложении
            SqlConnection sqlConnection = new SqlConnection("server=DESKTOP-7NIK29D\\SQLEXPRESS;Trusted_Connection=Yes;DataBase=Train;");
            sqlConnection.Open(); // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand(); // создаём команду
            sqlCommand.CommandText = selectSQL;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable); // возращаем таблицу с результатом
            return dataTable;

        }

        string log;
        int index;

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (loginTextBox.Text.Length > 0)
            {
                if (passwordBox.Password.Length > 0)
                {
                    if (captchaTextBox.Text == captchaGenTextBox.Text || captchaGenTextBox.Text.Length == 0) 
                    {
                        DataTable dt_Train = Select("SELECT * FROM [dbo].[Table_users] WHERE [Логин] = '" + loginTextBox.Text + "' AND [Пароль] = '" + passwordBox.Password + "'");
                        if (dt_Train.Rows.Count > 0)
                        {
                            dt_Train = Select("SELECT * FROM [dbo].[Table_users]"); // данные из БД
                            for (int i = 0; i < dt_Train.Rows.Count; i++) // перебираем данные
                            {
                                log = dt_Train.Rows[i][0].ToString();
                                if (log == loginTextBox.Text.ToString())
                                {
                                    index = i;
                                }

                            }
                            GeneralWindow gwin = new GeneralWindow();
                            gwin.Owner = this;
                            gwin.labelWorkOne.Content = dt_Train.Rows[index][2].ToString();
                            gwin.labelWorkTwo.Content = dt_Train.Rows[index][3].ToString();
                            gwin.labelWorkThree.Content = dt_Train.Rows[index][4].ToString();
                            gwin.labelPost.Content = dt_Train.Rows[index][6].ToString();
                            gwin.imageWork.Source = new BitmapImage(new Uri(dt_Train.Rows[index][5].ToString()));
                            gwin.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Данные неверные");
                            if (Convert.ToString(captchaGenTextBox.Text) != "")
                            {
                                CaptchaChecker();
                            }
                            else
                            {
                                CaptchaGenerator();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Каптча неверная");
                        if (Convert.ToString(captchaGenTextBox.Text) != "")
                        {
                            CaptchaChecker();
                        }
                        else
                        {
                            CaptchaGenerator();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Логин неверный");
                if (Convert.ToString(captchaGenTextBox.Text) != "")
                {
                    CaptchaChecker();
                }
                else
                {
                    CaptchaGenerator();
                }
                
            }
        }

        void CaptchaChecker()
        {
            if (Convert.ToString(captchaGenTextBox.Text) != captchaTextBox.Text)
            {
                authorizationLabel.Visibility = Visibility.Hidden;
                authorizationBorder.Visibility = Visibility.Hidden;
                blockLabel.Visibility = Visibility.Visible;
                loginTextBox.Visibility = Visibility.Hidden;
                passwordBox.Visibility = Visibility.Hidden;
                showPassword.Visibility = Visibility.Hidden;
                passwordTextBox.Visibility = Visibility.Hidden;
                captchaGenTextBox.Visibility = Visibility.Hidden;
                captchaTextBox.Visibility = Visibility.Hidden;
                refreshButton.Visibility = Visibility.Hidden;
                loginButton.Visibility = Visibility.Hidden;
                loginLabel.Visibility = Visibility.Hidden;
                passwordLabel.Visibility = Visibility.Hidden;
                captchaLabel.Visibility = Visibility.Hidden;
                imageOne.Visibility = Visibility.Hidden;

                timer.Start();
                captchaTextBox.Text = null;
            }
        }
    }
}
