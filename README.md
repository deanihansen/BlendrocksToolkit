# BlendrocksToolkit
Windows UWP toolkit that which enabled insanely cool page transitions and easier navigation and app handling.
In order to use the kit your app needs to derive from a app class in the toolkit.

Code sample 
<blend:App
    x:Class="BlendrocksToolkitSampleApp.App"
    xmlns:blend="using:BlendrocksToolkit.UI.Xaml"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:BlendrocksToolkitSampleApp"
    RequestedTheme="Light">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary
                    Source="ms-appx:///SampleStyles/Styles/CustomStyles.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</blend:App>

and app.xaml cs:
 sealed partial class App : BlendrocksToolkit.UI.Xaml
 
 To start on any page, you have to set the StartupPage in the ctor like this:
   public App()
        {
            TelemetryClient = new Microsoft.ApplicationInsights.TelemetryClient();
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            StartupPage = typeof(MainPage);
        }
