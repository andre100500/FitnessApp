using SqlTest.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using WpfApp.MVVM.Models;
using WpfApp.Utils;

namespace WpfApp.MVVM.Converter
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ExercisProgress && value != null)
            {
                ExercisProgress status = (ExercisProgress)value;
                var color = new SolidColorBrush(Colors.White);

                switch (status)
                {
                    case ExercisProgress.Done:
                        color = new SolidColorBrush(Colors.Green);
                        break;

                    case ExercisProgress.UnDone:
                        color = new SolidColorBrush(Colors.Red);
                        break;

                    case ExercisProgress.InProcess:
                        color = new SolidColorBrush(Colors.Aqua);
                        break;
                    default:
                        color = new SolidColorBrush(Colors.White);
                        break;
                }
                return color;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
