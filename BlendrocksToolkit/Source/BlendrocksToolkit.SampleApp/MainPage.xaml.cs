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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BlendrocksToolkit.SampleApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private void button_Copy_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.Navigation.NavigateTo(typeof(SecondPage), BlendrocksToolkit.Navigation.NavigationTransition.SlideRight, DateTime.Now.ToString());
        }

        private void button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.Navigation.NavigateTo(typeof(SecondPage), BlendrocksToolkit.Navigation.NavigationTransition.SlideUp, DateTime.Now.ToString());
        }

        private void Button_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            App.Current.Navigation.NavigateTo(typeof(SecondPage), BlendrocksToolkit.Navigation.NavigationTransition.SlideDown, DateTime.Now.ToString());
        }

        private void Button_Tapped_2(object sender, TappedRoutedEventArgs e)
        {
            App.Current.Navigation.NavigateTo(typeof(SecondPage), BlendrocksToolkit.Navigation.NavigationTransition.SlideLeft, DateTime.Now.ToString());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Navigation.GoBack();
        }

        private void Button_Tapped_3(object sender, TappedRoutedEventArgs e)
        {
            App.Current.Navigation.NavigateTo(typeof(SecondPage), BlendrocksToolkit.Navigation.NavigationTransition.Fade, DateTime.Now.ToString());
        }

        private void Button_Tapped_4(object sender, TappedRoutedEventArgs e)
        {
            App.Current.Navigation.NavigateTo<SecondPage>(BlendrocksToolkit.Navigation.NavigationTransition.FadeOnTop, DateTime.Now.ToString());
        }
    }
}
