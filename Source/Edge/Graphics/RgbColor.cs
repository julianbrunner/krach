﻿using System;
using Utility.Extensions;

namespace Edge.Graphics
{
	public struct RgbColor
	{
		readonly double red;
		readonly double green;
		readonly double blue;

		public double Red { get { return red; } }
		public double Green { get { return green; } }
		public double Blue { get { return blue; } }

		public RgbColor(double red, double green, double blue)
		{
			if (red < 0 || red > 1) throw new ArgumentOutOfRangeException("red");
			if (green < 0 || green > 1) throw new ArgumentOutOfRangeException("green");
			if (blue < 0 || blue > 1) throw new ArgumentOutOfRangeException("blue");

			this.red = red;
			this.green = green;
			this.blue = blue;
		}

		public static RgbColor FromHsv(HsvColor color)
		{
			int hueIndex = (int)color.Hue;
			double hueFraction = color.Hue - hueIndex;

			double bottom = color.Value * (1 - color.Saturation);
			double top = color.Value;
			double rising = color.Value * (1 - (1 - hueFraction) * color.Saturation);
			double falling = color.Value * (1 - hueFraction * color.Saturation);

			switch (hueIndex)
			{
				case 0: return new RgbColor(top, rising, bottom);
				case 1: return new RgbColor(falling, top, bottom);
				case 2: return new RgbColor(bottom, top, rising);
				case 3: return new RgbColor(bottom, falling, top);
				case 4: return new RgbColor(rising, bottom, top);
				case 5: return new RgbColor(top, bottom, falling);
				default: throw new InvalidOperationException();
			}
		}
		public static double Distance(RgbColor color1, RgbColor color2)
		{
			double red = color1.red - color2.red;
			double green = color1.green - color2.green;
			double blue = color1.blue - color2.blue;

			return Math.Sqrt(red.Square() + green.Square() + blue.Square());
		}
	}
}