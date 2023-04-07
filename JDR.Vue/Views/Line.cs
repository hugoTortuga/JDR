using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace JDR.Vue.Views {

	public class Line {
		public Point Start { get; set; }
		public Point End { get; set; }

		public Line(Point start, Point end) {
			Start = start;
			End = end;
		}

		public IList<Point> Intersects(Geometry geometry) {
			var intersectionPoints = new List<Point>();
			foreach (var line in GetLinesFromGeometry(geometry)) {
				var point = TryGetIntersectionWith(line);
				if (point != null) intersectionPoints.Add(point.Value);
			}
			return intersectionPoints;
		}

		private List<Line> GetLinesFromGeometry(Geometry geometry) {
			switch (geometry) {
				case RectangleGeometry rectangleGeometry:
					return GetLinesFromRectangle(rectangleGeometry);
				case EllipseGeometry ellipseGeometry:
					//IterateEllipsePoints(ellipseGeometry);
					break;
				case LineGeometry lineGeometry:
					//IterateLinePoints(lineGeometry);
					break;
				case PathGeometry pathGeometry:
					return GetLinesFromPathGeometry(pathGeometry);
			}
			return new List<Line>();
		}

		private List<Line> GetLinesFromPathGeometry(PathGeometry pathGeometry) {
			var lines = new List<Line>();
			foreach (PathFigure figure in pathGeometry.Figures) {
				Console.WriteLine("PathFigure:");

				Point startPoint = figure.StartPoint;
				Console.WriteLine($"  Start point: ({startPoint.X}, {startPoint.Y})");

				foreach (PathSegment segment in figure.Segments) {
					Console.WriteLine($" PathSegment type: {segment.GetType().Name}");
					if (segment is PolyLineSegment polyLineSegment) {
						Point? previousPoint = null;
						foreach (Point point in polyLineSegment.Points) {
							if (previousPoint != null)
								lines.Add(new Line(previousPoint.Value, point));
							previousPoint = point;
						}
						lines.Add(new Line(polyLineSegment.Points[0], polyLineSegment.Points[polyLineSegment.Points.Count - 1]));
					}
					else if (segment is LineSegment lineSegment) {
						Point point = lineSegment.Point;
						Console.WriteLine($"    Point: ({point.X}, {point.Y})");
					}
					else if (segment is ArcSegment arcSegment) {
						Point point = arcSegment.Point;
						Console.WriteLine($"    Point: ({point.X}, {point.Y})");
						Console.WriteLine($"    Size: ({arcSegment.Size.Width}, {arcSegment.Size.Height})");
						Console.WriteLine($"    Rotation Angle: {arcSegment.RotationAngle}");
						Console.WriteLine($"    IsLargeArc: {arcSegment.IsLargeArc}");
						Console.WriteLine($"    SweepDirection: {arcSegment.SweepDirection}");
					}
					else if (segment is BezierSegment bezierSegment) {
						Point point1 = bezierSegment.Point1;
						Point point2 = bezierSegment.Point2;
						Point point3 = bezierSegment.Point3;

						Console.WriteLine($"    Point1: ({point1.X}, {point1.Y})");
						Console.WriteLine($"    Point2: ({point2.X}, {point2.Y})");
						Console.WriteLine($"    Point3: ({point3.X}, {point3.Y})");
					}
					else if (segment is QuadraticBezierSegment quadraticBezierSegment) {
						Point point1 = quadraticBezierSegment.Point1;
						Point point2 = quadraticBezierSegment.Point2;

						Console.WriteLine($"    Point1: ({point1.X}, {point1.Y})");
						Console.WriteLine($"    Point2: ({point2.X}, {point2.Y})");
					}
					else {
						Console.WriteLine("    Unsupported PathSegment type.");
					}
				}
			}

			return lines;
		}

		private List<Line> GetLinesFromRectangle(RectangleGeometry rectangleGeometry) {

			var rectangle = rectangleGeometry.Rect;
			var lines = new List<Line>() {
				new Line(rectangle.TopLeft, rectangle.TopRight),
				new Line(rectangle.TopRight, rectangle.BottomRight),
				new Line(rectangle.BottomRight, rectangle.BottomLeft),
				new Line(rectangle.BottomLeft, rectangle.TopLeft)
			};
			return lines;
		}


		public Point? TryGetIntersectionWith(Line other) {
			var intersection = new Point();

			double a1 = End.Y - Start.Y;
			double b1 = Start.X - End.X;
			double c1 = a1 * Start.X + b1 * Start.Y;

			double a2 = other.End.Y - other.Start.Y;
			double b2 = other.Start.X - other.End.X;
			double c2 = a2 * other.Start.X + b2 * other.Start.Y;

			double delta = a1 * b2 - a2 * b1;
			if (delta == 0) {
				return null;
			}

			intersection.X = Math.Round((b2 * c1 - b1 * c2) / delta, 5, MidpointRounding.ToEven);
			intersection.Y = Math.Round((a1 * c2 - a2 * c1) / delta, 5, MidpointRounding.ToEven);

			if (IsInsideLineBounds(intersection) && other.IsInsideLineBounds(intersection)) {
				return intersection;
			}

			return null;
		}

		private bool IsInsideLineBounds(Point point) {
			double minX = Math.Round(Math.Min(Start.X, End.X), 5, MidpointRounding.ToEven);
			double maxX = Math.Round(Math.Max(Start.X, End.X), 5, MidpointRounding.ToEven);
			double minY = Math.Round(Math.Min(Start.Y, End.Y), 5, MidpointRounding.ToEven);
			double maxY = Math.Round(Math.Max(Start.Y, End.Y), 5, MidpointRounding.ToEven);

			return minX <= point.X && point.X <= maxX && minY <= point.Y && point.Y <= maxY;
		}
	}
}
