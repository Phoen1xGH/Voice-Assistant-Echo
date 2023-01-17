using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace VAEcho
{
    /// <summary>
    /// Class with animation
    /// </summary>
    internal class AnimationClass
    {
        /// <summary>
        /// Animation of smooth window appearance
        /// </summary>
        /// <param name="win"></param>
        public static void OpacityAnimation(Window win)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.8)
            };
            win.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
        }
    }
}
