using System.Drawing;

namespace JDR.Model {
	public class Obstacle {

        public List<Point> Points { get; set; }

        public Obstacle()
        {
			Points = new List<Point>();
        }
    }

}