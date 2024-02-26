using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NightRiderWPF.Resources
{
    public class Statics
    {
        // Main colors
        public static SolidColorBrush BackgroundMain => new SolidColorBrush(Colors.AliceBlue);
        public static SolidColorBrush BackgroundPage => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#99999999"));
        public static SolidColorBrush BackgroundItem => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EDE9E3"));
        public static SolidColorBrush PrimaryColor => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7081A3"));
        public static SolidColorBrush SecondaryColor => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3D527A"));
        public static SolidColorBrush AccentColor => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#20355C"));
        public static SolidColorBrush ButtonText => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CFD5E2"));
        public static SolidColorBrush StaticGrey => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#999999"));

        // Images
        public static BitmapImage LogoWide => new BitmapImage(new Uri("pack://application:,,,/NightRiderWPF;component/Resources/Images/NightRider-01.png"));
        public static BitmapImage Logo => new BitmapImage(new Uri("pack://application:,,,/NightRiderWPF;component/Resources/Images/NightRider-02.png"));
    }
}
// Checked by James Williams