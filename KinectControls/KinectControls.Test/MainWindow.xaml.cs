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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KinectControls.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KinectSensor _sensor;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _sensor = KinectSensor.KinectSensors.Where(s => s.Status == KinectStatus.Connected).FirstOrDefault();

            _sensor.ColorStream.Enable();
            _sensor.DepthStream.Enable();
            _sensor.SkeletonStream.Enable();

            _sensor.SkeletonFrameReady += Sensor_SkeletonFrameReady;

            _sensor.Start();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_sensor != null)
            {
                _sensor.Stop();
            }
        }

        void Sensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (var frame = e.OpenSkeletonFrame())
            {
                if (frame != null)
                {
                    Skeleton[] bodies = new Skeleton[frame.SkeletonArrayLength];

                    frame.CopySkeletonDataTo(bodies);

                    var body = bodies.Where(b => b.TrackingState == SkeletonTrackingState.Tracked).FirstOrDefault();

                    if (body != null)
                    {
                        Joint handLeft = body.Joints[JointType.HandLeft];
                        Joint handRight = body.Joints[JointType.HandRight];

                        if (handLeft.TrackingState == JointTrackingState.NotTracked && handRight.TrackingState == JointTrackingState.NotTracked)
                        {
                            // If no hand is tracked, do nothing.
                        }
                        else
                        {
                            // Select the hand that is closer to the sensor.
                            var activeHand = handRight.Position.Z <= handLeft.Position.Z ? handRight : handLeft;
                            var position = _sensor.CoordinateMapper.MapSkeletonPointToColorPoint(activeHand.Position, ColorImageFormat.RgbResolution640x480Fps30);

                            cursor.Flip(activeHand);                            
                            cursor.Update(position);
                        }
                    }
                }
            }
        }
    }
}
