using System;
using Cairo;
using System.Collections.Generic;

namespace RevengeSolver
{

	public class ColorCube
	{

		private Color[] _colors = new Color[6 * 16];
		private Dictionary<Faces, Color> colorDictionary;
		int[,] edgeLocations = {{ 1, 66 }, { 65, 2 }, { 17, 4 }, { 7, 50 }, { 8, 18 }, { 49, 11 }, { 33, 13 }, { 14, 34 },
			{ 71, 20 }, { 55, 68 }, { 23, 36 }, { 39, 52 }, { 40, 27 }, { 56, 43 }, { 24, 75 }, { 72, 59 },
			{ 81, 45 }, { 46, 82 }, { 30, 84 }, { 87, 61 }, { 88, 29 }, { 62, 91 }, { 78, 93 }, { 94, 77 }
		};
		int[,] cornerLocations = { { 0, 16, 67 },  { 15, 48, 35 }, { 3,  64, 51 }, { 12,  32, 19 },
			{ 80, 31, 44 }, { 95, 63, 76 }, { 83, 47, 60 }, { 92, 79, 28 }
		};
		int[] centerLocations = {5,  6,  9,  10, 53, 54, 57, 58,
			37, 38, 41, 42, 85, 86, 89, 90,
			21, 22, 25, 26,	69, 70, 73, 74
		};

		public ColorCube ()
		{

			colorDictionary = new Dictionary<Faces, Color> ();
			colorDictionary.Add (Faces.U, CubeColor.White);
			colorDictionary.Add (Faces.L, CubeColor.Red);
			colorDictionary.Add (Faces.F, CubeColor.Blue);
			colorDictionary.Add (Faces.R, CubeColor.Orange);
			colorDictionary.Add (Faces.B, CubeColor.Green);
			colorDictionary.Add (Faces.D, CubeColor.Yellow);

		}

		public void setColors (int[] edges, int[] corners, int[] orientations)
		{
			
			for (int i = 0; i < _colors.Length; i++) {
				_colors [i] = CubeColor.order [i / 16];
			}
			for (int i = 0; i < 24; i++) {
				_colors [edgeLocations [i, 0]] = colorDictionary [Edge.order [edges [i]].face1];
				_colors [edgeLocations [i, 1]] = colorDictionary [Edge.order [edges [i]].face2];
			}
			for (int i = 0; i < 8; i++) {
				int offset = orientations [i];
				_colors [cornerLocations [i, mod (0 + offset, 3)]] = colorDictionary [Corner.order [corners [i]].face1];
				_colors [cornerLocations [i, mod (1 + offset, 3)]] = colorDictionary [Corner.order [corners [i]].face2];
				_colors [cornerLocations [i, mod (2 + offset, 3)]] = colorDictionary [Corner.order [corners [i]].face3];
			}
			for (int i = 0; i < 12; i++) {
				
			}

		}

		public Color[] colors {
			get { return _colors; }
		}

		private int mod (int k, int n)
		{
			return ((k %= n) < 0) ? k + n : k;
		}

	}
}

