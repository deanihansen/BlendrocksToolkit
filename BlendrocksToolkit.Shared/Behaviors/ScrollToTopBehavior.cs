using Microsoft.Xaml.Interactivity;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace BlendrocksToolkit.Shared.Behaviors
{
    public class ScrollToTopBehavior : DependencyObject, IBehavior
    {
        public static readonly DependencyProperty ScrollToTopButtonProperty = DependencyProperty.Register("ScrollToTopButton", typeof(Button), typeof(ScrollToTopBehavior), new PropertyMetadata(null));

        private DependencyObject _associatedObject;
        private ScrollViewer scrollviewer;
        private List<double> offsets;
        private int count;
        private bool isHidden;
        private bool buttonAdded;
        private Grid grid;
        private Grid container;

        public Button ScrollToTopButton
        {
            get { return (Button)GetValue(ScrollToTopButtonProperty); }
            set { SetValue(ScrollToTopButtonProperty, value); }
        }

        DependencyObject IBehavior.AssociatedObject
        {
            get
            {
                return _associatedObject;
            }
        }

        public void Attach(DependencyObject associatedObject)
        {
            offsets = new List<double>();
            isHidden = true;
            buttonAdded = false;

            if (!DesignMode.DesignModeEnabled)
            {
                _associatedObject = associatedObject;

                scrollviewer = _associatedObject as ScrollViewer;

                if (scrollviewer != null)
                {
                    scrollviewer.ViewChanging += Scrollviewer_ViewChanging;
                    scrollviewer.Loaded += Scrollviewer_Loaded;
                }

                if (ScrollToTopButton != null)
                {
                    ScrollToTopButton.Tapped += ScrollToTopButton_Tapped;
                    ScrollToTopButton.Name = "ScrollToTopButton";
                }
            }
        }

        public void Detach()
        {
            if (scrollviewer != null)
            {
                scrollviewer.ViewChanging -= Scrollviewer_ViewChanging;
                scrollviewer.Loaded -= Scrollviewer_Loaded;
            }

            if (ScrollToTopButton != null)
            {
                ScrollToTopButton.Tapped -= ScrollToTopButton_Tapped;
                ScrollToTopButton = null;
            }
        }

        private void Scrollviewer_Loaded(object sender, RoutedEventArgs e)
        {
            FindParts(scrollviewer);
        }

        private void FindParts(DependencyObject dp)
        {
            if (buttonAdded == false)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dp); i++)
                {
                    var control = VisualTreeHelper.GetChild(dp, i);

                    if (buttonAdded == false && control is Grid)
                    {
                        container = new Grid();
                        container.Children.Add(ScrollToTopButton);
                        container.Visibility = Visibility.Collapsed;

                        grid = control as Grid;
                        grid.Children.Add(container);
                        buttonAdded = true;
                        break;
                    }
                    else
                    {
                        FindParts(control);
                    }
                }
            }
            else
            {
                return;
            }
        }

        private void ScrollToTopButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            scrollviewer.ChangeView(null, 0, null);
            HideGoTopTopButton();
        }

        private void Scrollviewer_ViewChanging(object sender, ScrollViewerViewChangingEventArgs e)
        {
            var offset = scrollviewer.VerticalOffset;

            if (e.IsInertial)
            {
                Debug.WriteLine(offset);
                offsets.Add(offset);
                count = count + 1;

                DetermineVisualstateChange();
            }
        }

        private void DetermineVisualstateChange()
        {
            if (count > 4)
            {
                if (offsets[count - 1] > 250.0 && offsets[count - 2] < offsets[count - 3]
                    && offsets[count - 1] < offsets[count - 2])
                {
                    ShowGoToTopButton();
                }
                else if ((offsets[count - 1] > 250.0 && offsets[count - 3] < offsets[count - 2]
                    && offsets[count - 2] < offsets[count - 1]) || offsets[count - 1] < 250.0)
                {
                    HideGoTopTopButton();
                }
            }
        }

        private void HideGoTopTopButton()
        {
            if (!isHidden)
            {
                isHidden = true;

                var storyboard = new Storyboard();
                var animation = new FadeOutThemeAnimation();
                animation.SetValue(Storyboard.TargetNameProperty, "ScrollToTopButton");
                Storyboard.SetTarget(animation, ScrollToTopButton);
                storyboard.Children.Add(animation);
                storyboard.Completed += (e, a) => { container.Visibility = Visibility.Collapsed; };
                storyboard.Begin();
            }
        }

        private void ShowGoToTopButton()
        {
            if (isHidden)
            {
                container.Visibility = Visibility.Visible;
                isHidden = false;
                var storyboard = new Storyboard();
                var animation = new FadeInThemeAnimation();
                animation.SetValue(Storyboard.TargetNameProperty, "ScrollToTopButton");
                Storyboard.SetTarget(animation, ScrollToTopButton);
                storyboard.Children.Add(animation);
                storyboard.Begin();
            }
        }
    }
}
