﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace G17_PaulaNacho_DSI
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Combate : Page
    {
        DispatcherTimer cardTimer;
        public Combate()
        {
            this.InitializeComponent();
        }

        //void cardTimerIni()
        //{
        //    cardTimer = new DispatcherTimer();
        //    cardTimer.Interval = new TimeSpan(100000);
        //    cardTimer.Tick += cardTimer_Tick;
        //    cardTimer.Start();

        //}
        //void cardTimer_Tick(object sender, object e)
        //{

        //}
        private void Go_Settings(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Ajustes));
        }

        private void MagoImage_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            playerEstado.Visibility = Visibility.Visible;
        }
        private void BatImage_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            batEstado.Visibility = Visibility.Visible;
        }
        private void MagoImage_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            playerEstado.Visibility = Visibility.Collapsed;
        }
        private void BatImage_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            batEstado.Visibility = Visibility.Collapsed;
        }

        private void ContentControl_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            ContentControl O = sender as ContentControl;
            args.Data.SetText(O.Name);
            args.Data.RequestedOperation = DataPackageOperation.Copy;
        }
        private void MiZona_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private async void MiZonaBat_Drop(object sender, DragEventArgs e)
        {
            //Encuentra la carta 
            var Oname = await e.DataView.GetTextAsync();
            ContentControl O = FindName(Oname.ToString()) as ContentControl;
           
            if (MisCartas.Children.Count >= 1) 
            {
                MisCartas.Children.Remove(O); //quita la carta del stackpanel
            }
            
            //Coloca la carta en la posicion adecuada
            MiZonaBat.Children.Add(O);
            Point pos = e.GetPosition(MiZonaBat);
            O.CanDrag = false;
            O.SetValue(Grid.ColumnProperty, pos.X+100);
            O.SetValue(Grid.RowProperty, pos.Y+20);

            //Cambiasu source para que parezca que brilla
            Image img= this.FindName(Oname + "Img") as Image;
            Uri imageUri = new Uri("ms-appx:///Assets/Combate/cartaBrillo.png");
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            img.Source =imageBitmap;

            //Baja 3 puntos al escudo
            int currentShield = int.Parse(batShield.Text);

            if (currentShield - 3 >= 0)
            {
                batShield.Text = (currentShield - 4).ToString();
            }
            else //Si no quedan puntos al escudo se los baja a la vida
            {
                int currentLife = int.Parse(batLife.Text);
                if (currentLife - 3 >= 0)
                {
                    batLife.Text = (currentLife - 4).ToString();
                }

            }

        }
        private async void MiZonaOgro_Drop(object sender, DragEventArgs e)
        {
            var Oname = await e.DataView.GetTextAsync();
            ContentControl O = FindName(Oname.ToString()) as ContentControl;

            if (MisCartas.Children.Count >= 1) 
            {
                MisCartas.Children.Remove(O);
            }
            
            MiZonaOgro.Children.Add(O);
            Point pos = e.GetPosition(MiZonaOgro);
            O.CanDrag = false;
            O.SetValue(Canvas.LeftProperty, pos.X-40);
            O.SetValue(Canvas.TopProperty, pos.Y-100);

            Image img = this.FindName(Oname + "Img") as Image;
            Uri imageUri = new Uri("ms-appx:///Assets/Combate/cartaBrillo.png");
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            img.Source = imageBitmap;

            int currentShield = int.Parse(ogroShield.Text);

            if (currentShield - 3 >= 0)
            {
                ogroShield.Text = (currentShield-4).ToString();
            }
            else //Si no quedan puntos al escudo se los baja a la vida
            {
                int currentLife = int.Parse(ogroLife.Text);
                if (currentLife - 3 >= 0)
                {
                    ogroLife.Text = (currentLife - 4).ToString();
                }

            }
        }
    }
}
