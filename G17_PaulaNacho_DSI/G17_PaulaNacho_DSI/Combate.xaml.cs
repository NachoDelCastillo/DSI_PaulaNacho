using System;
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
        //Timer para hacer desaparecer las cartas tras un segundo
        DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        //Variabe para guardar la carta que se acaba de poner en un enemigo
        Image cartaActualBat = null;
        Image cartaActualOgro = null;

    public Combate()
        {
            this.InitializeComponent();
        }

        //Navegacion ajustes
        private void Go_Settings(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Ajustes));
        }

        //Mostrar texto sobre estado del jugador
        private void MagoImage_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            playerEstado.Visibility = Visibility.Visible;
        }

        //Mostrar texto sobre estado del enemigo murcielago
        private void BatImage_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            batEstado.Visibility = Visibility.Visible;
        }

        //Quitar texto sobre estado del jugador
        private void MagoImage_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            playerEstado.Visibility = Visibility.Collapsed;
        }
        //Quitar texto sobre estado del enemigo murcielago

        private void BatImage_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            batEstado.Visibility = Visibility.Collapsed;
        }

        //Drag starting para las cartas del jugador
        private void ContentControl_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            ContentControl O = sender as ContentControl;
            args.Data.SetText(O.Name);
            args.Data.RequestedOperation = DataPackageOperation.Copy;
        }

        //Copia las cartas en la zona del enemigo correspondiente
        private void MiZona_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        //Metodo del timer para que desaparezcan las cartas que tienen los enemigosencima tras un segundo 
        void Timer_Tick(object sender, object e)
        {
            timer.Stop();
            if (cartaActualBat != null) //Si existe una carta sobre el murcielago, quita su visibilidad
            {
                cartaActualBat.Visibility = Visibility.Collapsed;
            }
            if (cartaActualOgro != null) //Análogo para el ogro
            {
                cartaActualOgro.Visibility = Visibility.Collapsed;
            }
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

            //Cambia su source para que parezca que brilla
            Image img= this.FindName(Oname + "Img") as Image;
            Uri imageUri = new Uri("ms-appx:///Assets/Combate/cartaBrillo.png");
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            img.Source =imageBitmap;

            //Asigna la imagen a la variable de carta del murcielago (para poder hacerla desaprecer despues)

            cartaActualBat = img;
            //Baja 3 puntos al escudo
            int currentShield = int.Parse(batShield.Text);

            if (currentShield - 3 > 0)
            {
                batShield.Text = (currentShield - 3).ToString();
            }
            else if (currentShield == 1) 
            {
                batShield.Text = (currentShield-1).ToString();
                int currentLife = int.Parse(batLife.Text);
                if (currentLife - 2 >= 0)
                {
                    batLife.Text = (currentLife - 2).ToString();
                    batLifeBar.Value = currentLife;
                }
            }
            else //Si no quedan puntos al escudo se los baja a la vida
            {
                int currentLife = int.Parse(batLife.Text);
                if (currentLife - 3 >= 0)
                {
                    batLife.Text = (currentLife - 3).ToString();
                    batLifeBar.Value = currentLife;
                }

            }
            //Activa el timer
            timer.Start();
            timer.Tick += Timer_Tick;


        }
        private async void MiZonaOgro_Drop(object sender, DragEventArgs e)
        {
            //Localiza la carta
            var Oname = await e.DataView.GetTextAsync();
            ContentControl O = FindName(Oname.ToString()) as ContentControl;

            if (MisCartas.Children.Count >= 1) 
            {
                MisCartas.Children.Remove(O); //quita la carta del stackpanel
            }
            
            //Coloca la carta en la posicion adecuada
            MiZonaOgro.Children.Add(O);
            Point pos = e.GetPosition(MiZonaOgro);
            O.CanDrag = false;
            O.SetValue(Canvas.LeftProperty, pos.X-40);
            O.SetValue(Canvas.TopProperty, pos.Y-100);

            //Cambia su source para que parezca que brilla

            Image img = this.FindName(Oname + "Img") as Image;
            Uri imageUri = new Uri("ms-appx:///Assets/Combate/cartaBrillo.png");
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            img.Source = imageBitmap;

            //Asigna la imagen a la variable de carta del ogro (para poder hacerla desaprecer despues)
            cartaActualOgro = img;
            int currentShield = int.Parse(ogroShield.Text);

            //Baja la vida del escudo
           
            if (currentShield - 3 > 0)
            {
                ogroShield.Text = (currentShield - 3).ToString();
            }
            else if (currentShield == 1)
            {
                ogroShield.Text = (currentShield - 1).ToString();
                int currentLife = int.Parse(ogroLife.Text);
                if (currentLife - 2 >= 0)
                {
                    ogroLife.Text = (currentLife - 2).ToString();
                    ogroLifeBar.Value = currentLife;
                }
            }
            else //Si no quedan puntos al escudo se los baja a la vida
            {
                int currentLife = int.Parse(ogroLife.Text);
                if (currentLife - 3 >= 0)
                {
                    ogroLife.Text = (currentLife - 3).ToString();
                    batLifeBar.Value = currentLife;
                }

            }

            //Activa el timer
            timer.Start();
            timer.Tick += Timer_Tick;
        }
    }
}
