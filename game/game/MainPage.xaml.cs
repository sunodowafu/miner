using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace game
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int step;
        int count;
        int size;
        Color color;
        Color colorBorder;
        Color colorText;
        Matrix map;
        Button[,] Qu;


        public MainPage()
        {
            this.InitializeComponent();
            step = 0;
            color = Color.FromArgb(255, 255, 255, 255);
            colorBorder = Color.FromArgb(255, 122, 122, 122);
            colorText = Color.FromArgb(255, 0, 0, 0);
            Message();
        }



        public async void Message()
        {
            string title = "Выберите размер поля:";
            ContentDialog message = new ContentDialog();
            {
                message.Title = title;
                message.PrimaryButtonText = "10*10";
                message.SecondaryButtonText = "15*15";
            }
            ContentDialogResult result = await message.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                count = 10;
                size = 10;
                Qu = new Button[10, 10];
                Qu = FieldBuilder();
            }
            else
            {
                count = 15;
                size = 15;
                Qu = new Button[15, 15];
                Qu = FieldBuilder();
            }
        }


        //create field
        private Button[,] FieldBuilder()
        {            
            Count.Text = count.ToString();
            Button[,] butQu = new Button[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    this.CreateQ(i, j, 50, 50);
                }
            return butQu;
        }

        //create the cells
        private void CreateQ(int i, int j, double width, double height)
        {
            Qu[i, j] = new Button();
            Qu[i, j].Width = width;
            Qu[i, j].Height = height;
            Sq.Children.Add(Qu[i, j]);
            Qu[i, j].Margin = new Thickness(width * j, height * i, 0, 0);
            Qu[i, j].RightTapped += new RightTappedEventHandler(Button_RightClick);
            Qu[i, j].Click += new RoutedEventHandler(Button_Click);
            Qu[i, j].Background = new SolidColorBrush(color);
            Qu[i, j].BorderBrush = new SolidColorBrush(colorBorder);
            Qu[i, j].Foreground = new SolidColorBrush(colorText);
            Qu[i, j].Tag = i.ToString() + " " + j.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string[] tagBut = new string[2];
            tagBut = btn.Tag.ToString().Split(' ');
            int i = Convert.ToInt32(tagBut[0]);
            int j = Convert.ToInt32(tagBut[1]);
            if (step == 0)
            {
                map = new Matrix(i, j, size, count);
                OpenQ(i, j);
            }
            else
            {
                if (map.sqList.Count() == size * size - count) MessageUs("Вы выиграли.");
                else
                {
                    if (map.Ex(i, j) == 1) MessageUs("Вы проиграли.");
                    else OpenQ(i, j);
                }
            }
        }

        //open the cells
        private void OpenQ(int i, int j)
        {
            step++;
            map.Square(i, j);
            foreach (int[] a in map.sqList)
            {
                Qu[a[0], a[1]].Content = map.mines[a[0], a[1]].ToString();
            }
        }

        //puts the flags
        private void Button_RightClick(object sender, RightTappedRoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string[] tagBut = new string[2];
            tagBut = btn.Tag.ToString().Split(' ');
            if (!(btn.Content=="1" && btn.Content=="2" && btn.Content == "3" && btn.Content == "4" && btn.Content == "5" && btn.Content == "6" && btn.Content == "7" && btn.Content == "8" && btn.Content == "0"))
            {
                btn.Content = "!!!";
            }
            count--;
            Count.Text = count.ToString();
        }

        //message for user
        private async void MessageUs(string mes)
        {
            ContentDialog messageUs = new ContentDialog();
            {
                messageUs.Title = mes;
                messageUs.PrimaryButtonText = "Начать новую игру.";
            }
            ContentDialogResult result = await messageUs.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                Frame.Navigate(typeof(game.MainPage));
            }
        }


    }
}
