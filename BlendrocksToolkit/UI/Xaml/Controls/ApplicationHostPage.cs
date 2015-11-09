using BlendrocksToolkit.Helpers;
using BlendrocksToolkit.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.Phone.UI.Input;

namespace BlendrocksToolkit.UI.Xaml.Controls
{
    public class ApplicationHostPage : Page
    {
        private readonly AnimationHelper animationHelper;
        private Grid frameGrid;

        public ApplicationHostPage()
        {
            animationHelper = new AnimationHelper();
            RootGrid = new Grid();
            frameGrid = new Grid();

            NavigationFrameOne = new Frame();
            NavigationFrameTwo = new Frame();
            frameGrid.Children.Add(NavigationFrameOne);
            frameGrid.Children.Add(NavigationFrameTwo);

            RootGrid.Children.Add(frameGrid);

            MoveFrameOutOfViewPort(NavigationFrameTwo);
            ActiveFrame = NavigationFrameOne;
            InactiveFrame = NavigationFrameTwo;

            this.Content = RootGrid;

            Window.Current.SizeChanged += Current_SizeChanged;

        }


        public Frame ActiveFrame { get; set; }

        public Frame InactiveFrame { get; set; }

        public Frame NavigationFrameOne { get; private set; }

        public Frame NavigationFrameTwo { get; private set; }

        public Grid RootGrid { get; private set; }

        private void MoveFrameOutOfViewPort(Frame frame)
        {
            var transform = new CompositeTransform();
            transform.TranslateX = 5000;
            frame.RenderTransform = transform;
        }

        private void ResetViewPort()
        {
            var transform = new CompositeTransform();
            ActiveFrame.SetValue(Canvas.ZIndexProperty, 1);
            InactiveFrame.SetValue(Canvas.ZIndexProperty, 0);
            ActiveFrame.RenderTransform = transform;
            InactiveFrame.RenderTransform = transform;
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            var size = e.Size;
            ActiveFrame.Width = size.Width;
            ActiveFrame.Height = size.Height;
            InactiveFrame.Width = size.Width;
            InactiveFrame.Height = size.Height;
        }
    }
}
