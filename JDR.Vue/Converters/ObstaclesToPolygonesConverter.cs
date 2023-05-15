using JDR.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace JDR.Vue.Converters
{
    public class ObstaclesToPolygonesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var polygons = new List<Polygon>();
            if (value != null && value is IList<Obstacle> obstacles)
            {
                foreach(var obstacle in obstacles)
                {
                    polygons.Add(new Polygon
                    {
                        Points = new PointCollection(
                            obstacle.Points.Select(p => new System.Windows.Point(p.X, p.Y))
                        ),
                        Fill = new SolidColorBrush(Colors.Black),
                    });
                }
            }
            return polygons;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
