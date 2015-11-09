using BlendrocksToolkit.Navigation;
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

namespace BlendrocksToolkit.UAPSample
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Tapped_4(object sender, TappedRoutedEventArgs e)
        {
            App.Current.Navigation.NavigateTo<SecondPage>(NavigationTransition.FadeOnTop, DateTime.Now.ToString());
        }

        private void Button_Tapped_3(object sender, TappedRoutedEventArgs e)
        {

            App.Current.Navigation.NavigateTo<SecondPage>(NavigationTransition.Fade, DateTime.Now.ToString());
        }

        private void Button_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            App.Current.Navigation.NavigateTo<SecondPage>(NavigationTransition.SlideDown, DateTime.Now.ToString());
        }

        private void button_Copy_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.Navigation.NavigateTo<SecondPage>(NavigationTransition.SlideRight, DateTime.Now.ToString());

        }

        private void button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.Navigation.NavigateTo<SecondPage>(NavigationTransition.SlideUp, DateTime.Now.ToString());
        }

        private void Button_Tapped_2(object sender, TappedRoutedEventArgs e)
        {

            App.Current.Navigation.NavigateTo<SecondPage>(NavigationTransition.SlideLeft, DateTime.Now.ToString());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

            App.Current.Navigation.GoBack();
        }
    }
}
