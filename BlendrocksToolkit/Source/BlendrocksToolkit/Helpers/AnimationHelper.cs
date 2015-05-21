using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using System.Threading.Tasks;

namespace BlendrocksToolkit.Helpers
{
    public class AnimationHelper
    {
        private CircleEase circleEase;

        private void CreateDoubleAnimationUsingKeyframes(UIElement backImage, Storyboard storyBoard, string translateProperty, int from, int to, int beginMiliseconds, int endMiliseconds)
        {
            var doubleAnimation = new DoubleAnimationUsingKeyFrames();
            doubleAnimation.SetValue(Storyboard.TargetPropertyProperty, translateProperty);

            EasingDoubleKeyFrame frame = new EasingDoubleKeyFrame();
            frame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(beginMiliseconds));
            frame.Value = from;
            doubleAnimation.KeyFrames.Add(frame);

            frame = new EasingDoubleKeyFrame();
            frame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(endMiliseconds));
            frame.Value = to;
            doubleAnimation.KeyFrames.Add(frame);

            Storyboard.SetTarget(doubleAnimation, backImage);
            storyBoard.Children.Add(doubleAnimation);
        }

        public void CreateDoubleAnimation(Storyboard storyBoard, Duration duration, TimeSpan beginTime, double? from, double to, string propertyToAnimate, FrameworkElement controlElement, bool beginStoryboard = true, bool applyStandardEasing = true)
        {
            storyBoard.Stop();
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.SetValue(Storyboard.TargetPropertyProperty, propertyToAnimate);
            if (applyStandardEasing)
            {
                ApplyStandardEase(doubleAnimation);
            }

            if (from.HasValue)
            {
                doubleAnimation.From = from;
            }

            doubleAnimation.To = to;
            doubleAnimation.BeginTime = beginTime;
            Storyboard.SetTarget(doubleAnimation, controlElement);
            doubleAnimation.Duration = duration;
            storyBoard.Children.Add(doubleAnimation);

            if (beginStoryboard)
            {
                storyBoard.Begin();
            }
        }

        private void ApplyCirleEaseIn(DoubleAnimation animation)
        {
            circleEase.EasingMode = EasingMode.EaseIn;
            animation.EasingFunction = circleEase;
        }

        private void ApplyStandardEase(DoubleAnimation animation, double exponent = 6)
        {
            ExponentialEase exponentialEase = new ExponentialEase();
            exponentialEase.EasingMode = EasingMode.EaseIn;
            exponentialEase.Exponent = exponent;
            animation.EasingFunction = exponentialEase;
        }

        private void ApplyFrameEasing(EasingDoubleKeyFrame frame)
        {
            circleEase = new CircleEase();
            circleEase.EasingMode = EasingMode.EaseIn;
            frame.EasingFunction = circleEase;
        }
    }
}
