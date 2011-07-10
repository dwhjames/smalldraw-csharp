using System;
using System.Drawing;

namespace SmallDraw.Util
{
    /// <summary>
    /// A collection of static helper methods that provide geometric functions
    /// </summary>
    public static class Geometry
    {
        /// <summary>
        /// Interpolate linearly between u and v with s as the increment
        /// </summary>
        /// <param name="u">the starting value of the interpolant</param>
        /// <param name="v">the ending value of the interpolant</param>
        /// <param name="s">the increment to be calculated along the interpolant,
        /// must be between 0 and 1.0f for linear interpolation</param>
        /// <returns>the interpolated value, which equals
        /// (u + (int)(0.5f + s * (float)(v - u)))</returns>
        public static int Prorata(int u, int v, float s)
        {
            return u + (int)(0.5f + s * (float)(v - u));
        }

        /// <summary>
        /// Get the midpoint of the line (start, end)
        /// </summary>
        /// <param name="start">the start of the line</param>
        /// <param name="end">the end of the line</param>
        /// <returns>the midpoint of the line (start, end)</returns>
        public static Point GetLineMidPoint(Point start, Point end)
        {
            return new Point(Math.Min(start.X, end.X) + Math.Abs(end.X - start.X) / 2,
                             Math.Min(start.Y, end.Y) + Math.Abs(end.Y - start.Y) / 2);
        }

        /// <summary>
        /// Test to see if the point center touches the line (start, end) with an error radius of radius.
        /// Essentially we're testing to see if a circle centered at center with the given radius touches
        /// the line
        /// </summary>
        /// <param name="start">the start of the line</param>
        /// <param name="end">the end of the line</param>
        /// <param name="center">the center of the circle</param>
        /// <param name="radius">the radius of the circle</param>
        /// <returns>true, if the circle touces the line</returns>
        public static bool LinePointIntersect(Point start, Point end, Point center, int radius)
        {
            long x0 = center.X, y0 = center.Y, x1 = start.X, y1 = start.Y, x2 = end.X, y2 = end.Y;
            long p = Dot(x1 - x2, y1 - y2, x0 - x1, y0 - y1), q = Dot(x2 - x1, y2 - y1, x0 - x2, y0 - y2);

            if (p > 0) // start is closest point to center
                return Square(radius) > Pythagoras(x1 - x0, y1 - y0);
            else if (q > 0) // end is closest point to center
                return Square(radius) > Pythagoras(x2 - x0, y2 - y0);
            else // closest point to center lines on line between start and end
                return Square(radius) * Pythagoras(x1 - x2, y1 - y2) > Square(Cross(x1 - x0, y1 - y0, x2 - x1, y2 - y1));
                
        }

        public static bool LinePointIntersect(Point start, Point end, Point center)
        {
            return LinePointIntersect(start, end, center, 3);
        }

        /// <summary>
        /// Square the given value
        /// </summary>
        /// <param name="x">the value to square</param>
        /// <returns>the squared value, (x*x)</returns>
        public static long Square(long x)
        {
            return x * x;
        }

        /// <summary>
        /// The square of the length of the vector (x,y)
        /// </summary>
        /// <param name="x">the x coordinate</param>
        /// <param name="y">the y coordinate</param>
        /// <returns>the squared length of (x,y)</returns>
        public static long Pythagoras(long x, long y)
        {
            return Square(x) + Square(y); // or dot(x,y,x,y)
        }

        /// <summary>
        /// The dot product of two vectors (x,y) and (z,w)
        /// </summary>
        /// <param name="x">x coordinate of point 1</param>
        /// <param name="y">y coordinate of point 1</param>
        /// <param name="z">z coordinate of point 2</param>
        /// <param name="w">w coordinate of point 2</param>
        /// <returns>the dot product of (x,y) and (z,w)</returns>
        public static long Dot(long x, long y, long z, long w)
        {
            return x * z + y * w;
        }

        /// <summary>
        /// The (scalar) cross product of two vectors (x,y) and (z,w)
        /// </summary>
        /// <param name="x">x coordinate of point 1</param>
        /// <param name="y">y coordinate of point 1</param>
        /// <param name="z">z coordinate of point 2</param>
        /// <param name="w">w coordinate of point 2</param>
        /// <returns>the cross product of (x,y) and (z,w)</returns>
        public static long Cross(long x, long y, long z, long w)
        {
            return x * w - z * y;
        }

        /// <summary>
        /// Create the minimal rectangle that encloses two points
        /// </summary>
        /// <param name="a">the first point</param>
        /// <param name="b">the second point</param>
        /// <returns></returns>
        public static Rectangle RectangleFromPoints(Point a, Point b)
        {
            return new Rectangle(Math.Min(a.X, b.X),
                                 Math.Min(a.Y, b.Y),
                                 Math.Abs(a.X - b.X),
                                 Math.Abs(a.Y - b.Y));
        }
    }
}
