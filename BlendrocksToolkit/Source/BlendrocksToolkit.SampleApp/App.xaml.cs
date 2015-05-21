using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
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
    sealed partial class App : BlendrocksToolkit.UI.Xaml.App
    {
        public static Microsoft.ApplicationInsights.TelemetryClient TelemetryClient;

        public App()
        {
            TelemetryClient = new Microsoft.ApplicationInsights.TelemetryClient();
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            StartupPage = typeof(MainPage);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}
