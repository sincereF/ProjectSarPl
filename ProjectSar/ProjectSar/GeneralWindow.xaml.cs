using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using System.Windows.Shapes;
using System.Windows.Threading;
using BarcodeLib;
using System.Drawing.Imaging;

namespace ProjectSar
{
    /// <summary>
    /// Логика взаимодействия для GeneralWindow.xaml
    /// </summary>




    public partial class GeneralWindow : Window
    {
        public class usertwo
        {
            public string flightnumtwo { get; set; }
            public string flighttwo { get; set; }
            public string timedeptwo { get; set; }
            public string timearrtwo { get; set; }
            public string trainnumtwo { get; set; }

        }

        void LoadUsersTwo()
        {

            DataTable dt_ticketstwo = Select("SELECT * FROM [dbo].[Table_2]"); // данные из БД

            for (int i = 0; i < dt_ticketstwo.Rows.Count; i++) // перебираем данные

            {

                usertwo dataUserTwo = new usertwo() // создаём экземпляр класса

                {

                    flightnumtwo = dt_ticketstwo.Rows[i][0].ToString(), 

                    flighttwo = dt_ticketstwo.Rows[i][1].ToString(), 

                    timedeptwo = dt_ticketstwo.Rows[i][2].ToString(), 

                    timearrtwo = dt_ticketstwo.Rows[i][3].ToString(),

                    trainnumtwo = dt_ticketstwo.Rows[i][4].ToString() 

                };

                generalListViewTwo.Items.Add(dataUserTwo); // выводим строку в список

            }

        }

        public class userthree
        {
            public string FIOthree { get; set; }
            public string flightnumthree { get; set; }
            public string passportthree { get; set; }
            public string agethree { get; set; }
            public string sexthree { get; set; }

        }

        void LoadUsersThree()
        {

            DataTable dt_ticketsthree = Select("SELECT * FROM [dbo].[Table_3]"); // данные из БД

            for (int i = 0; i < dt_ticketsthree.Rows.Count; i++) // перебираем данные

            {

                userthree dataUserThree = new userthree() // создаём экземпляр класса

                {

                    FIOthree = dt_ticketsthree.Rows[i][0].ToString(), 

                    flightnumthree = dt_ticketsthree.Rows[i][1].ToString(), 

                    passportthree = dt_ticketsthree.Rows[i][2].ToString(), 

                    agethree = dt_ticketsthree.Rows[i][3].ToString(), 

                    sexthree = dt_ticketsthree.Rows[i][4].ToString() 

                };

                generalListViewThree.Items.Add(dataUserThree); // выводим строку в список

            }

        }

        DispatcherTimer timer = new DispatcherTimer();

        int interv = 0;
        int minute = 10;

        public GeneralWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }


        void timer_Tick(object sender, EventArgs e)
        {
            labelTime.Content = "До конца сеанса осталось " + minute + ":" + interv + " минут";
            if (interv == 0 && minute > 0)
            {
                if (minute == 5)
                {
                    MessageBox.Show("Внимание: до окончания сеанса остается 5 минут");
                }
                minute--;
                interv = 59;
            }
            else
            {
                if (interv == 0 && minute == 0)
                {
                    MainWindow mwin = new MainWindow();
                    mwin.Show();
                    mwin.intrerv = 60;
                    mwin.timer.Start();
                    

                    this.Close();
                    timer.Stop();
                }
                else
                {
                    interv--;
                }

            }
        }


        public class user
        {
            public string ticketnum { get; set; }
            public string flightnum { get; set; }
            public string flight { get; set; }
            public string timedep { get; set; }
            public string timearr { get; set; }
            public string trainnum { get; set; }
            public string vannum { get; set; }
            public string van { get; set; }
            public string passenger { get; set; }
            public string passengerinfo { get; set; }
            public string cost { get; set; }


        }





        void LoadUsers()
        {

            DataTable dt_tickets = Select("SELECT * FROM [dbo].[Table_1]"); // данные из БД

            for (int i = 0; i < dt_tickets.Rows.Count; i++) // перебираем данные

            {

                user dataUser = new user() // создаём экземпляр класса

                {

                    ticketnum = dt_tickets.Rows[i][0].ToString(), 

                    flightnum = dt_tickets.Rows[i][1].ToString(), 

                    flight = dt_tickets.Rows[i][2].ToString(), 

                    timedep = dt_tickets.Rows[i][3].ToString(), 

                    timearr = dt_tickets.Rows[i][4].ToString(), 

                    trainnum = dt_tickets.Rows[i][5].ToString(), 

                    vannum = dt_tickets.Rows[i][6].ToString(), 

                    van = dt_tickets.Rows[i][7].ToString(), 

                    passenger = dt_tickets.Rows[i][8].ToString(), 

                    passengerinfo = dt_tickets.Rows[i][9].ToString(), 

                    cost = dt_tickets.Rows[i][10].ToString() 
                };

                generalListView.Items.Add(dataUser); // выводим строку в список

            }

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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            base.OnClosed(e);
            App.Current.Shutdown();
        }

        private void generalBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mwin = new MainWindow();
            mwin.Show();
            timer.Stop();
            this.Close();
        }

        private void ticketsButton_Click(object sender, RoutedEventArgs e)
        {
            generalListViewTwo.Visibility = Visibility.Hidden;
            generalListViewThree.Visibility = Visibility.Hidden;
            generalListView.Visibility = Visibility.Visible;
            generalListView.Items.Clear();
            LoadUsers();
        }

        private void generalListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FlightsButton_Click(object sender, RoutedEventArgs e)
        {
            generalListView.Visibility = Visibility.Hidden;
            generalListViewThree.Visibility = Visibility.Hidden;
            generalListViewTwo.Visibility = Visibility.Visible;
            generalListViewTwo.Items.Clear();
            LoadUsersTwo();
        }

        private void PassengersButton_Click(object sender, RoutedEventArgs e)
        {
            generalListView.Visibility = Visibility.Hidden;
            generalListViewTwo.Visibility = Visibility.Hidden;
            generalListViewThree.Visibility = Visibility.Visible;
            generalListViewThree.Items.Clear();
            LoadUsersThree();
        }
        
        DateTime date = DateTime.Now;

        private void buttonCode_Click(object sender, RoutedEventArgs e)
        {
            string gener = "51" + date.Day + date.Month + date.Year + "123";
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            System.Drawing.Image img = b.Encode(BarcodeLib.TYPE.UPCA, gener, System.Drawing.Color.Black, System.Drawing.Color.White, 290, 120);

            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Bmp);//save image in memory

            byte[] buffer = ms.GetBuffer();
            MemoryStream bufferPasser = new MemoryStream(buffer);
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = bufferPasser;
            bitmap.EndInit();
            imageCode.Source = bitmap;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            imageCode.Source = null;
        }
    }
}
