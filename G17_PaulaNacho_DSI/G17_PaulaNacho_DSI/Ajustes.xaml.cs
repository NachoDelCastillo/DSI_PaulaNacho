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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace G17_PaulaNacho_DSI
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Ajustes : Page
    {
        public Ajustes()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            UpdateLanguageTexts();

            // Cambiar el valor de la barra de volumen
            MusicSlider.Value = App.volumenMusica * 10;

            // Si has accedido a los ajustes desde el menu, eliminar el boton de ir al menu de los ajustes
            if (e.Parameter != "combate")
                MiStackpanel.Children.Remove(MenuPrincipal_Button);
        }

        private void goToMenu(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void goBackButtonClick(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void ChangeLaguage(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource == SpanishButton)
                App.idioma = 0;
            else
                App.idioma = 1;

            UpdateLanguageTexts();
        }

        void UpdateLanguageTexts()
        {
            if (App.idioma == 0)
            {
                Ajustes_Texto.Text = "AJUSTES";
                Idioma_Texto.Text = "Idioma";
                Volver_Texto.Text = "Volver";
                MenuPrincipal_Texto.Text = "Menu Principal";
                Salir_Texto.Text = "Salir Del Juego";
            }
            else
            {
                Ajustes_Texto.Text = "SETTINGS";
                Idioma_Texto.Text = "Language";
                Volver_Texto.Text = "Go Back";
                MenuPrincipal_Texto.Text = "Main Menu";
                Salir_Texto.Text = "Exit Game";
            }
        }

        private void VolumeChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            App.volumenMusica = e.NewValue/10;
            MiMusica.Volume = App.volumenMusica;
        }
    }
}
