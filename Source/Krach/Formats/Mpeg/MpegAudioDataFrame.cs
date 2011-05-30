﻿// Copyright © Julian Brunner 2010 - 2011

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

using System.Collections.Generic;
using System.IO;

namespace Krach.Formats.Mpeg
{
	public class MpegAudioDataFrame : MpegAudioFrame
	{
		readonly byte[] sideInformation;
		readonly byte[] data;

		public IEnumerable<byte> SideInformation { get { return sideInformation; } }
		public IEnumerable<byte> Data { get { return data; } }

		public MpegAudioDataFrame(BinaryReader reader)
			: base(reader)
		{
			this.sideInformation = reader.ReadBytes(SideInformationLength);
			this.data = reader.ReadBytes(DataLength);
		}

		public override void Write(BinaryWriter writer)
		{
			base.Write(writer);

			writer.Write(sideInformation);
			writer.Write(data);
		}
	}
}