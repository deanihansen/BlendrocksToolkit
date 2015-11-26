using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BlendrocksToolkit.Win2D
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool imageLoaded;
        private ScaleEffect scaleEffect;
        private GaussianBlurEffect blurEffect;
        private PixelShaderEffect shader;
        private CanvasControl canvascontrol;

        public MainPage()
        {
            this.InitializeComponent();
            canvascontrol = new CanvasControl();
            canvascontrol.Draw += Canvascontrol_Draw;
            container.Children.Add(canvascontrol);
        }

        private void Canvascontrol_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
                CanvasCommandList cl = new CanvasCommandList(sender);
                using (CanvasDrawingSession clds = cl.CreateDrawingSession())
                {
                    clds.FillCircle(20f, 20f, 20f, Colors.Red);
                    clds.FillCircle(60, 60, 20f, Colors.Red);
                    clds.FillCircle(100, 100, 20f, Colors.Red);
                }

            GaussianBlurEffect blur = new GaussianBlurEffect();
            blur.Source = cl;
            blur.BlurAmount = 10f;

            var coloreffect = new ColorMatrixEffect();
            coloreffect.AlphaMode = CanvasAlphaMode.Straight;
            var matrix = new Matrix5x4()
            {
                M11 = ConvertToFloat(m11.Text),
                M12 = ConvertToFloat(m12.Text),
                M13 = ConvertToFloat(m13.Text),
                M14 = ConvertToFloat(m14.Text),
                M21 = ConvertToFloat(m21.Text),

                M22 = ConvertToFloat(m22.Text),
                M23 = ConvertToFloat(m23.Text),
                M24 = ConvertToFloat(m24.Text),
                M31 = ConvertToFloat(m31.Text),
                M32 = ConvertToFloat(m32.Text),

                M33 = ConvertToFloat(m33.Text),
                M34 = ConvertToFloat(m34.Text),
                M41 = ConvertToFloat(m41.Text),
                M42 = ConvertToFloat(m42.Text),
                M43 = ConvertToFloat(m43.Text),

                M44 = ConvertToFloat(m44.Text),
                M51 = ConvertToFloat(m51.Text),
                M52 = ConvertToFloat(m52.Text),
                M53 = ConvertToFloat(m53.Text),
                M54 = ConvertToFloat(m54.Text)
            };
            coloreffect.ColorMatrix = matrix;
            coloreffect.Source = blur;
            args.DrawingSession.DrawImage(coloreffect);
        }

        public float ConvertToFloat(string text)
        {
            try
            {
                return string.IsNullOrEmpty(text) ? 0.0f : float.Parse(text.Trim());
            }
            catch (Exception ex)
            {
                return 0f;
            }
        }

        private void m11_LostFocus(object sender, RoutedEventArgs e)
        {
            canvascontrol.Invalidate();
        }
    }
}
