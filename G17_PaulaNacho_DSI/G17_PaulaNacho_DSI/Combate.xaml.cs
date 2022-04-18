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
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace G17_PaulaNacho_DSI
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Combate : Page
    {
        public Combate()
        {
            this.InitializeComponent();
        }
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

        private async void MiZona_Drop(object sender, DragEventArgs e)
        {
            var Oname = await e.DataView.GetTextAsync();
            ContentControl O = FindName(Oname.ToString()) as ContentControl;

            if (MisCartas.Children.Count >= 1) 
            {
                MisCartas.Children.Remove(O);
            }
            
            MiZona.Children.Add(O);
            Point pos = e.GetPosition(MiZona);
            //O.CanDrag = false;
            O.SetValue(Grid.ColumnProperty, pos.X-50);
            O.SetValue(Grid.RowProperty, pos.Y-50);

        }

    }
}
