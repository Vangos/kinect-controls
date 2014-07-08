using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KinectControls.v2
{
    /// <summary>
    /// Interaction logic for KinectCursor.xaml
    /// </summary>
    public partial class KinectCursor : UserControl
    {
        #region Dependency properties

        /// <summary>
        /// The cursor color (defaults to white).
        /// </summary>
        public Brush Fill
        {
            get { return (Brush)this.GetValue(FillProperty); }
            set { this.SetValue(FillProperty, value); }
        }
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush), typeof(KinectCursor), new PropertyMetadata(Brushes.White));

        /// <summary>
        /// The cursor effect (defaults to a drop-shadow effect).
        /// </summary>
        public Effect Effect
        {
            get { return (Effect)this.GetValue(EffectProperty); }
            set { this.SetValue(EffectProperty, value); }
        }
        public static readonly DependencyProperty EffectProperty =
            DependencyProperty.Register("Effect", typeof(Effect), typeof(KinectCursor), new PropertyMetadata(new DropShadowEffect
            {
                Color = Colors.Black,
                Direction = -90.0,
                BlurRadius = 40,
                Opacity = 0.4
            }));

        #endregion

        #region Constructor

        public KinectCursor()
        {
            InitializeComponent();
            DataContext = this;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the position of the cursor.
        /// </summary>
        /// <param name="x">The position in the X-axis.</param>
        /// <param name="y">The position in the Y-axis.</param>
        public void Update(double x, double y)
        {
            Canvas.SetLeft(this, x - Width / 2);
            Canvas.SetTop(this, y - Height / 2);
        }

        /// <summary>
        /// Updates the position of the cursor.
        /// </summary>
        /// <param name="point">The specified point in the color space.</param>
        /// <param name="ratioX">The specified horizontal scale scale ratio.</param>
        /// <param name="ratioY">The specified vertical scale ratio.</param>
        public void Update(ColorSpacePoint point, double ratioX = 1.0, double ratioY = 1.0)
        {
            Update(point.X * ratioX, point.Y * ratioY);
        }

        /// <summary>
        /// Updates the position of the cursor.
        /// </summary>
        /// <param name="point">The specified point in the depth space.</param>
        /// <param name="ratioX">The specified horizontal scale scale ratio.</param>
        /// <param name="ratioY">The specified vertical scale ratio.</param>
        public void Update(DepthSpacePoint point, double ratioX = 1.0, double ratioY = 1.0)
        {
            Update(point.X * ratioX, point.Y * ratioY);
        }

        /// <summary>
        /// Flips the cursor to represent a different active hand.
        /// </summary>
        public void Flip()
        {
            ScaleTransform transform = root.LayoutTransform as ScaleTransform;

            transform.ScaleX = transform.ScaleX == 1.0 ? -1.0 : 1.0;
        }

        /// <summary>
        /// Flips the cursor according to the specified active hand.
        /// </summary>
        /// <param name="activeHand">The left or right hand the cursor is supposed to represent.</param>
        public void Flip(Joint activeHand)
        {
            double scaleX = activeHand.JointType == JointType.HandRight ? 1.0 : -1.0;

            ScaleTransform transform = root.RenderTransform as ScaleTransform;

            if (transform.ScaleX != scaleX)
            {
                transform.ScaleX = scaleX;
            }
        }

        #endregion
    }
}
