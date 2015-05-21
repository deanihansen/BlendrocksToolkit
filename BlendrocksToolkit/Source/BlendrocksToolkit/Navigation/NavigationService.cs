using BlendrocksToolkit.Helpers;
using BlendrocksToolkit.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BlendrocksToolkit.Navigation
{
    public class NavigationService
    {
        private readonly App App;
        private readonly PageTransitionHelper pageTransitionHelper;
        private Frame newFrame;
        private Frame oldFrame;
        private IList<Tuple<Type, NavigationTransition, object>> BackStack;

        public NavigationService(App app)
        {
            this.App = app;
            pageTransitionHelper = new PageTransitionHelper();
            BackStack = new List<Tuple<Type, NavigationTransition, object>>();
        }

        public bool CanGoBack
        {
            get
            {
                if (BackStack.Count > 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void NavigateTo(Type type, NavigationTransition transition = NavigationTransition.None, object navigationParameter = null)
        {
            PerformNavigation(type, navigationParameter);
            BackStack.Add(new Tuple<Type, NavigationTransition, object>(type, transition, navigationParameter));
            pageTransitionHelper.BeginTransition(newFrame, oldFrame, transition);
        }
        public void NavigateTo<T>(NavigationTransition transition = NavigationTransition.None, object navigationParameter = null) where T : Page
        {
            NavigateTo(typeof(T), transition, navigationParameter);
        }

        private void PerformNavigation(Type type, object navigationParameter)
        {
            newFrame = App.RootFrame.ActiveFrame == App.RootFrame.NavigationFrameOne ? App.RootFrame.NavigationFrameTwo : App.RootFrame.NavigationFrameOne;
            oldFrame = App.RootFrame.ActiveFrame != App.RootFrame.NavigationFrameOne ? App.RootFrame.NavigationFrameTwo : App.RootFrame.NavigationFrameOne;

            App.RootFrame.InactiveFrame = oldFrame;
            App.RootFrame.ActiveFrame = newFrame;

            newFrame.Navigate(type, navigationParameter);
        }

        public void GoBack(bool reverseTransition = true)
        {
            if (CanGoBack)
            {
                var stackCount = BackStack.Count;
                var stackItem = BackStack[stackCount - 2];
                var lastInStack = BackStack.Last();

                PerformNavigation(stackItem.Item1, stackItem.Item3);
                pageTransitionHelper.BeginReverseTransition(newFrame, oldFrame, lastInStack.Item2);
                BackStack.RemoveAt(stackCount - 1);
            }
            else
            {
                Application.Current.Exit();
            }
        }
    }
}
