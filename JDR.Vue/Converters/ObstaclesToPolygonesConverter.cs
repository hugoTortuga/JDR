using JDR.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Shapes;

namespace JDR.Vue.Converters
{
    public class ObstaclesToPolygonesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var polygons = new List<Polygon>();
            if (value != null && value is Scene scene && scene.Obstacles != null)
            {
                foreach(var obstacle in scene.Obstacles)
                {
                    polygons.Add(new Polygon
                    {
                        Points = new System.Windows.Media.PointCollection(
                            obstacle.Lines.Select(l => new System.Windows.Point(l.Start.X, l.Start.Y))
                        )
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
