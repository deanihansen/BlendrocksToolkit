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

namespace BlendrocksToolkit.SampleApp
{
    public sealed partial class SecondPage : Page
    {
        public SecondPage()
        {
            this.InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Navigation.NavigateTo(typeof(MainPage), BlendrocksToolkit.Navigation.NavigationTransition.SlideRight);
        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.Navigation.NavigateTo(typeof(MainPage), BlendrocksToolkit.Navigation.NavigationTransition.SlideRight);
        }

        private void Button_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            App.Current.Navigation.GoBack();
        }
    }
}
