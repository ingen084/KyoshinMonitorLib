using SkiaSharp;
using System;

namespace KyoshinMonitorLib.SkiaImages
{
	/// <summary>
	/// 色を震度に変換する
	/// </summary>
	public static class ColorConverter
	{
		/// <summary>
		/// スケールを震度に変換します。
		/// </summary>
		/// <param name="scale">変換前のスケール</param>
		/// <returns></returns>
		public static double ConvertToIntensityFromScale(double scale)
			=> scale * 10 - 3;

		/// <summary>
		/// スケールをPGA(最大加速度)に変換します。
		/// </summary>
		/// <param name="scale">変換前のスケール</param>
		/// <returns></returns>
		public static double ConvertToPgaFromScale(double scale)
			=> Math.Pow(10, 5 * scale - 2);

		/// <summary>
		/// スケールをPGV(最大速度)に変換します。
		/// </summary>
		/// <param name="scale">変換前のスケール</param>
		/// <returns></returns>
		public static double ConvertToPgvFromScale(double scale)
			=> Math.Pow(10, 5 * scale - 3);

		/// <summary>
		/// スケールをPGD(最大変位)に変換します。
		/// </summary>
		/// <param name="scale">変換前のスケール</param>
		/// <returns></returns>
		public static double ConvertToPgdFromScale(double scale)
			=> Math.Pow(10, 5 * scale - 4);

		/// <summary>
		/// 多項式補完を使用して色をスケールに変換します。
		/// from: https://qiita.com/NoneType1/items/a4d2cf932e20b56ca444
		/// </summary>
		/// <param name="color">変換元の色</param>
		/// <returns>変換後のスケール</returns>
		public static double ConvertToScaleAtPolynomialInterpolation(SKColor color)
			=> Math.Max(0, ConvertToScaleAtPolynomialInterpolationInternal(color));
		private static double ConvertToScaleAtPolynomialInterpolationInternal(SKColor color)
		{
			// Input : color in hsv space
			(var h, var s, var v) = GetHsv(color);
			h /= 360;

			// Check if the color belongs to the scale
			if (v <= 0.1 || s <= 0.75)
				return 0;

			if (h > 0.1476)
				return 280.31 * Math.Pow(h, 6) - 916.05 * Math.Pow(h, 5) + 1142.6 * Math.Pow(h, 4) - 709.95 * Math.Pow(h, 3) + 234.65 * Math.Pow(h, 2) - 40.27 * h + 3.2217;
			else if (h > 0.001)
				return 151.4 * Math.Pow(h, 4) - 49.32 * Math.Pow(h, 3) + 6.753 * Math.Pow(h, 2) - 2.481 * h + 0.9033;
			else
				return -0.005171 * Math.Pow(v, 2) - 0.3282 * v + 1.2236;
		}

		/// <summary>
		/// 指定した色をHSVで返す
		/// </summary>
		/// <param name="rgb">変換する色</param>
		/// <returns>変換した色</returns>
		private static (double h, double s, double v) GetHsv(SKColor rgb)
		{
			var max = Math.Max(rgb.Red, Math.Max(rgb.Green, rgb.Blue));
			var min = Math.Min(rgb.Red, Math.Min(rgb.Green, rgb.Blue));

			if (min == max)
				return (0, 0, max / 255d);
			double w = max - min;
			var h = 0d;
			if (rgb.Red == max)
				h = (rgb.Green - rgb.Blue) / w;
			if (rgb.Green == max)
				h = ((rgb.Blue - rgb.Red) / w) + 2;
			if (rgb.Blue == max)
				h = ((rgb.Red - rgb.Green) / w) + 4;
			if ((h *= 60) < 0)
				h += 360;
			return (h, (double)(max - min) / max, max / 255d);
		}
	}
}