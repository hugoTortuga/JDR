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
					//IteratePathGeometryPoints(pathGeometry);
					break;
			}
			return null;

			//PathGeometry pathGeometry = PathGeometry.CreateFromGeometry(geometry);

			//foreach (PathFigure figure in pathGeometry.Figures) {
			//	Point startPoint = figure.StartPoint;

			//	foreach (PathSegment segment in figure.Segments) {
			//		if (segment is LineSegment lineSegment) {
			//			Point endPoint = lineSegment.Point;
			//			lines.Add(new Line(startPoint, endPoint));
			//			startPoint = endPoint;
			//		}
			//		else if (segment is PolyLineSegment polyLineSegment) {
			//			foreach (Point point in polyLineSegment.Points) {
			//				lines.Add(new Line(startPoint, point));
			//				startPoint = point;
			//			}
			//		}
			//	}
				
			//}
			//return lines;
		}

		private List<Line> GetLinesFromRectangle(RectangleGeometry rectangleGeometry) {
			var lines = new List<Line>();
			var rect = rectangleGeometry.Rect;

			lines.Add(new Line(rect.TopLeft, rect.TopRight));
			lines.Add(new Line(rect.TopRight, rect.BottomRight));
			lines.Add(new Line(rect.BottomRight, rect.BottomLeft));
			lines.Add(new Line(rect.BottomLeft, rect.TopLeft));
			
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
