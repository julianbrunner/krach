﻿// Copyright © Julian Brunner 2010

// This file is part of Krach.
//
// Krach is free software: you can redistribute it and/or modify it under the
// terms of the GNU Lesser General Public License as published by the Free
// Software Foundation, either version 3 of the License, or (at your option) any
// later version.
//
// Krach is distributed in the hope that it will be useful, but WITHOUT ANY
// WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR
// A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with
// Krach. If not, see <http://www.gnu.org/licenses/>.

using Krach.Basics;

namespace Krach.Maps.Linear
{
	public class Volume2DoubleMap : MapVector2Double
	{
		readonly Volume2Double source;
		readonly Volume2Double destination;

		public Volume2Double Source { get { return source; } }
		public Volume2Double Destination { get { return destination; } }

		public Volume2DoubleMap(Volume2Double source, Volume2Double destination)
			: base(new MapDouble(source.RangeX, destination.RangeX), new MapDouble(source.RangeY, destination.RangeY))
		{
			this.source = source;
			this.destination = destination;
		}
	}
}