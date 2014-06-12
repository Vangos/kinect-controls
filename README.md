# Kinect Controls
This project contains some Kinect user controls you can integrate and use into your own applications. Oh, and it's amazingly easy!

Like this project? [Buy me a beer](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=N5ELYBTYB3AYE)!

## Kinect cursor control
![Image](https://raw.githubusercontent.com/Vangos/kinect-controls/master/Images/kinect-cursor.png?raw=true)

Kinect Cursor control displays a hand icon that follows the movements of the user's hands. It is a vector shape (no bitmap images used), so you can scale, resize, or change its colors within your code.

### Using the code

* Build the project
* Add a reference to the KinectControls library
* Import the user controls into your XAML code using the following line of code

    xmlns:Controls="clr-namespace:KinectControls;assembly=KinectControls"
* You can now use the controls as follows:

#### XAML
    <Controls:KinectCursor x:Name="cursor" Width="100" Height="100" />
    
Need a different color? Here you are:

    <Controls:KinectCursor x:Name="cursor" Width="100" Height="100" Fill="Blue" />
  
#### C-Sharp
    // Select the hand closer to the sensor.
    var activeHand = handRight.Position.Z <= handLeft.Position.Z ? handRight : handLeft;
    
    // Get the hand's position relatively to the color image.
    var position = _sensor.CoordinateMapper.MapSkeletonPointToColorPoint(
                                            activeHand.Position,
                                            ColorImageFormat.RgbResolution640x480Fps30);

    // Flip the cursor to match the active hand and update its position.
    cursor.Flip(activeHand);                            
    cursor.Update(position);
  
That's it, folks! You now have a cursor control that follows the active hand of a user.

## Credits
* Developed by [Vangos Pterneas](http://pterneas.com) for [LightBuzz](http://lightbuzz.com)

## License
You are free to use these libraries in personal and commercial projects by clearly attributing their original author. Licensed under [MIT License](https://github.com/Vangos/kinect-controls/blob/master/LICENSE).

Like this project? [Buy me a beer](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=N5ELYBTYB3AYE)!
