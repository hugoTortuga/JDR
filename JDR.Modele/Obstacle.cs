using System.Drawing;

namespace JDR.Model {
	public class Obstacle {

        public List<Line> Lines { get; set; }

        public Obstacle()
        {
            Lines = new List<Line>();
        }
    }

    public class Line {

        public Point Start { get; set; }
        public Point End { get; set; }

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

    }
}