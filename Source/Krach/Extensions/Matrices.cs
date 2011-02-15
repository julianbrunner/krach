// Copyright © Julian Brunner 2010

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
using System.Collections.Generic;
using System.Linq;
using Krach.Basics;

namespace Krach.Extensions
{
	public static class Matrices
	{
		public static Matrix ValuesToMatrix(IEnumerable<double> values)
		{
			double[] items = values.ToArray();

			Matrix matrix = new Matrix(items.Length, 1);
			for (int index = 0; index < items.Length; index++) matrix[index, 0] = items[index];

			return matrix;
		}
		public static IEnumerable<double> MatrixToValues(Matrix matrix)
		{
			if (matrix.Columns != 1) throw new ArgumentException("Cannot convert non-vector to values.");

			double[] items = new double[matrix.Rows];
			for (int index = 0; index < items.Length; index++) items[index] = matrix[index, 0];

			return items;
		}
		public static MatrixComplex ValuesToMatrix(IEnumerable<Complex> values)
		{
			Complex[] items = values.ToArray();

			MatrixComplex matrix = new MatrixComplex(items.Length, 1);
			for (int index = 0; index < items.Length; index++) matrix[index, 0] = items[index];

			return matrix;
		}
		public static IEnumerable<Complex> MatrixToValues(MatrixComplex matrix)
		{
			if (matrix.Columns != 1) throw new ArgumentException("Cannot convert non-vector to values.");

			Complex[] items = new Complex[matrix.Rows];
			for (int index = 0; index < items.Length; index++) items[index] = matrix[index, 0];

			return items;
		}
	}
}