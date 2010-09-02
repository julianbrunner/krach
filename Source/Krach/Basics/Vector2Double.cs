// Copyright � Julian Brunner 2010

// This file is part of Krach.
//
// Krach is free software: you can redistribute it and/or modify it under the
// terms of the GNU Lesser General Public License as published by the Free
// Software Foundation, either version 3 of the License, or (at your option) any
// later version.
//
// Krach is distributed in the hope that it will be useful, but WITHOUT ANY
// WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR
// A PARTICULAR PURPOSE. See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with
// Krach. If not, see <http://www.gnu.org/licenses/>.

using System;
using Krach.Extensions;

namespace Krach.Basics
{
	public struct Vector2Double : IEquatable<Vector2Double>
	{
		readonly double x;
		readonly double y;

		public static Vector2Double Origin { get { return new Vector2Double(0, 0); } }
		public static Vector2Double UnitX { get { return new Vector2Double(1, 0); } }
		public static Vector2Double UnitY { get { return new Vector2Double(0, 1); } }
		public static Vector2Double UnitXY { get { return new Vector2Double(1, 1); } }

		public double X { get { return x; } }
		public double Y { get { return y; } }
		public double Length { get { return LengthSquared.SquareRoot(); } }
		public double LengthSquared { get { return x.Square() + y.Square(); } }

		public Vector2Double(double x, double y)
		{
			this.x = x;
			this.y = y;
		}

		public override bool Equals(object obj)
		{
			return obj is Vector2Double && this == (Vector2Double)obj;
		}
		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode();
		}
		public override string ToString()
		{
			return "(" + x + "," + y + ")";
		}
		public bool Equals(Vector2Double other)
		{
			return this == other;
		}

		public static bool operator ==(Vector2Double vector1, Vector2Double vector2)
		{
			return vector1.x == vector2.x && vector1.y == vector2.y;
		}
		public static bool operator !=(Vector2Double vector1, Vector2Double vector2)
		{
			return vector1.x != vector2.x || vector1.y != vector2.y;
		}

		public static Vector2Double operator +(Vector2Double vector1, Vector2Double vector2)
		{
			return new Vector2Double(vector1.x + vector2.x, vector1.y + vector2.y);
		}
		public static Vector2Double operator -(Vector2Double vector1, Vector2Double vector2)
		{
			return new Vector2Double(vector1.x - vector2.x, vector1.y - vector2.y);
		}
		public static Vector2Double operator *(Vector2Double vector, double factor)
		{
			return new Vector2Double(vector.x * factor, vector.y * factor);
		}
		public static Vector2Double operator *(double factor, Vector2Double vector)
		{
			return new Vector2Double(factor * vector.x, factor * vector.y);
		}

		public static Vector2Double InterpolateLinear(Vector2Double vector1, Vector2Double vector2, double fraction)
		{
			return new Vector2Double(Scalars.InterpolateLinear(vector1.x, vector2.x, fraction), Scalars.InterpolateLinear(vector1.y, vector2.y, fraction));
		}
	}
}
