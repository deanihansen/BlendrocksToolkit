using BlendrocksToolkit.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using BlendrocksToolkit.Navigation;
using Windows.Phone.UI.Input;

namespace BlendrocksToolkit.UI.Xaml
{
    public class App : Windows.UI.Xaml.Application
    {
        protected Type StartupPage;

        public App()
        {
            Navigation = new NavigationService(this);

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            { 
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Navigation.GoBack();
        }

        public NavigationService Navigation { get; private set; }

        public ApplicationHostPage RootFrame { get; private set; }

        public CoreDispatcher Dispatcher { get; private set; }

        public static new App Current
        {
            get { return (App)Application.Current; }
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);
            if (StartupPage == null) throw new ArgumentNullException(nameof(StartupPage));

            Dispatcher = Window.Current.Dispatcher;
            CreateRootFrame();
            Navigation.NavigateTo(StartupPage);
            Window.Current.Activate();
        }

        private void CreateRootFrame()
        {
            var frame = Window.Current.Content as ApplicationHostPage;

            if (frame == null)
            {
                RootFrame = new ApplicationHostPage();
                Window.Current.Content = RootFrame;
            }
        }
    }
}
