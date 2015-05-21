using BlendrocksToolkit.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace BlendrocksToolkit.Helpers
{
    internal class PageTransitionHelper
    {
        private const int animationDurationInMiliseconds = 240;
        private const double scaleFactorForScaleAnimation = 2.2;
        private Duration defaultDuration = new Duration(TimeSpan.FromMilliseconds(animationDurationInMiliseconds));
        private readonly AnimationHelper animationHelper;
        private Frame newFrame;
        private Frame oldFrame;

        public PageTransitionHelper()
        { 
            animationHelper = new AnimationHelper();
        }

        internal void BeginTransition(Frame newFrame, Frame oldFrame, NavigationTransition transition)
        {
            this.newFrame = newFrame;
            this.oldFrame = oldFrame;
            ResetFramesBeforeAnimation(newFrame, oldFrame);
            
            var board = new Storyboard();
            board.Completed += Board_Completed;

            if (transition == NavigationTransition.SlideDown || transition == NavigationTransition.SlideUp || transition == NavigationTransition.SlideLeft || transition == NavigationTransition.SlideRight)
            {
                ApplySlideTransition(newFrame, oldFrame, transition, board);
            }
            else if (transition == NavigationTransition.Fade)
            {
                ApplyFadeTransition(newFrame, oldFrame, transition, board);
            }
            else if (transition == NavigationTransition.FadeOnTop)
            {
                ApplyFadeOnTopTransition(newFrame, oldFrame, transition, board);
            }

            board.Begin();
        }

        private void Board_Completed(object sender, object e)
        {
            var transformOutOfViewPort = new CompositeTransform();
            transformOutOfViewPort.TranslateX = 5000;
            oldFrame.RenderTransform = transformOutOfViewPort;
        }

        private  void ResetFramesBeforeAnimation(Frame newFrame, Frame oldFrame)
        {
            newFrame.RenderTransform = new CompositeTransform();
            oldFrame.RenderTransform = new CompositeTransform();
            newFrame.Opacity = 1;
            oldFrame.Opacity = 1;
        }

        internal void BeginReverseTransition(Frame newFrame, Frame oldFrame, NavigationTransition transition)
        {
            this.newFrame = newFrame;
            this.oldFrame = oldFrame;
            ResetFramesBeforeAnimation(newFrame, oldFrame);

            var board = new Storyboard();
            board.Completed += (se, ea) => { oldFrame.SetValue(Canvas.ZIndexProperty, -1); };

            if (transition == NavigationTransition.SlideDown || transition == NavigationTransition.SlideUp || transition == NavigationTransition.SlideLeft || transition == NavigationTransition.SlideRight)
            {
                var reversedTransition = FindReverseTransition(transition);
                ApplySlideTransition(newFrame, oldFrame, reversedTransition, board);
            }
            else if (transition == NavigationTransition.Fade)
            {
                ApplyFadeTransition(newFrame, oldFrame, transition, board);
            }
            else if (transition == NavigationTransition.FadeOnTop)
            {
                ApplyFadeTopOutTransition(newFrame, oldFrame, transition, board);
            }

            board.Begin();
        }

        private void ApplyFadeOnTopTransition(Frame newFrame, Frame oldFrame, NavigationTransition transition, Storyboard board)
        {
            newFrame.Opacity = 0;
            var resetPositionTransform = new CompositeTransform();
            newFrame.RenderTransformOrigin = new Point(0.5, 0.5);
            resetPositionTransform.ScaleX = scaleFactorForScaleAnimation;
            resetPositionTransform.ScaleY = scaleFactorForScaleAnimation;
            newFrame.RenderTransform = resetPositionTransform;
            newFrame.SetValue(Canvas.ZIndexProperty, 1);
            oldFrame.SetValue(Canvas.ZIndexProperty, 0);
            animationHelper.CreateDoubleAnimation(board, defaultDuration, TimeSpan.FromMilliseconds(0), scaleFactorForScaleAnimation, 1, "(UIElement.RenderTransform).(CompositeTransform.ScaleX)", newFrame, true, false);
            animationHelper.CreateDoubleAnimation(board, defaultDuration, TimeSpan.FromMilliseconds(0), scaleFactorForScaleAnimation, 1, "(UIElement.RenderTransform).(CompositeTransform.ScaleY)", newFrame, true, false);
            animationHelper.CreateDoubleAnimation(board, defaultDuration, TimeSpan.FromMilliseconds(0), 0, 1, "(UIElement.Opacity)", newFrame, true, false);
        }

        private void ApplyFadeTopOutTransition(Frame newFrame, Frame oldFrame, NavigationTransition transition, Storyboard board)
        {
            animationHelper.CreateDoubleAnimation(board, defaultDuration, TimeSpan.FromMilliseconds(0), 1, scaleFactorForScaleAnimation, "(UIElement.RenderTransform).(CompositeTransform.ScaleX)", oldFrame, true, false);
            animationHelper.CreateDoubleAnimation(board, defaultDuration, TimeSpan.FromMilliseconds(0), 1, scaleFactorForScaleAnimation, "(UIElement.RenderTransform).(CompositeTransform.ScaleY)", oldFrame, true, false);
            animationHelper.CreateDoubleAnimation(board, defaultDuration, TimeSpan.FromMilliseconds(0), 1, 0, "(UIElement.Opacity)", oldFrame, true, false);
        }

        private void ApplyFadeTransition(Frame newFrame, Frame oldFrame, NavigationTransition transition, Storyboard board)
        {
            newFrame.Opacity = 0;
            var resetPositionTransform = new CompositeTransform();
            newFrame.RenderTransform = resetPositionTransform;
            newFrame.SetValue(Canvas.ZIndexProperty, 1);
            oldFrame.SetValue(Canvas.ZIndexProperty, 0);

            animationHelper.CreateDoubleAnimation(board, defaultDuration, TimeSpan.FromMilliseconds(0), 0, 1, "(UIElement.Opacity)", newFrame, true, false);
        }

        internal void ApplySlideTransition(Frame newFrame, Frame oldFrame, NavigationTransition transition, Storyboard board)
        {
            string propertyToAnimate;
            double boundsSize;

            if (transition == NavigationTransition.SlideRight || transition == NavigationTransition.SlideLeft)
            {
                propertyToAnimate = "(UIElement.RenderTransform).(CompositeTransform.TranslateX)";
                boundsSize = Window.Current.Bounds.Width;

                if (transition == NavigationTransition.SlideRight)
                {
                    boundsSize = -boundsSize;
                }
            }
            else if (transition == NavigationTransition.SlideUp || transition == NavigationTransition.SlideDown)
            {
                propertyToAnimate = "(UIElement.RenderTransform).(CompositeTransform.TranslateY)";
                boundsSize = Window.Current.Bounds.Height;

                if (transition == NavigationTransition.SlideDown)
                {
                    boundsSize = -boundsSize;
                }
            }
            else
            {
                throw new ArgumentException(nameof(transition));
            }

            animationHelper.CreateDoubleAnimation(board, defaultDuration, TimeSpan.FromMilliseconds(0), boundsSize, 0, propertyToAnimate, newFrame, true, false);
            animationHelper.CreateDoubleAnimation(board, defaultDuration, TimeSpan.FromMilliseconds(0), 0, -boundsSize, propertyToAnimate, oldFrame, true, false);
        }

        private NavigationTransition FindReverseTransition(NavigationTransition transition)
        {
            NavigationTransition reversedTransition = NavigationTransition.None;
            if (transition == NavigationTransition.SlideDown) reversedTransition = NavigationTransition.SlideUp;
            else if (transition == NavigationTransition.SlideUp) reversedTransition = NavigationTransition.SlideDown;
            else if (transition == NavigationTransition.SlideLeft) reversedTransition = NavigationTransition.SlideRight;
            else if (transition == NavigationTransition.SlideRight) reversedTransition = NavigationTransition.SlideLeft;
            return reversedTransition;
        }
    }
}
