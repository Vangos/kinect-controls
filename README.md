# Kinect Controls

This project contains some Kinect user controls you can integrate and use into your own applications. Oh, and it's amazingly easy!

## Kinect cursor control
Kinect Cursor control displays a hand icon that follows the movements of the user's hands. Here's how to use it:

### XAML
    <Controls:KinectCursor x:Name="cursor" Width="100" Height="100" />
  
### C#
    var activeHand = handRight.Position.Z <= handLeft.Position.Z ? handRight : handLeft;
    var position = _sensor.CoordinateMapper.MapSkeletonPointToColorPoint(
                                            activeHand.Position,
                                            ColorImageFormat.RgbResolution640x480Fps30);

    cursor.Flip(activeHand);                            
    cursor.Update(position);
  
That's it, folks! You now have a cursor control that follows the active hand of a user.
