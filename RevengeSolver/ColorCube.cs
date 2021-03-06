﻿using System;
using Cairo;
using System.Collections.Generic;

namespace RevengeSolver
{

	public class ColorCube
	{

		/*
 *                        -------------------
 *                       |  0 |  1 |  2 |  3 |
 *                       ---------------------
 *                       |  4 |  5 |  6 |  7 |
 *                       ---------------------
 *                       |  8 |  9 | 10 | 11 |
 *                       ---------------------
 *                       | 12 | 13 | 14 | 15 |
 *                        -------------------
 *  -------------------   -------------------   -------------------   -------------------
 * | 16 | 17 | 18 | 19 | | 32 | 33 | 34 | 35 | | 48 | 49 | 50 | 51 | | 64 | 65 | 66 | 67 |
 * --------------------- --------------------- --------------------- ---------------------
 * | 20 | 21 | 22 | 23 | | 36 | 37 | 38 | 39 | | 52 | 53 | 54 | 55 | | 68 | 69 | 70 | 71 |
 * --------------------- --------------------- --------------------- ---------------------
 * | 24 | 25 | 26 | 27 | | 40 | 41 | 42 | 43 | | 56 | 57 | 58 | 59 | | 72 | 73 | 74 | 75 |
 * --------------------- --------------------- --------------------- ---------------------
 * | 28 | 29 | 30 | 31 | | 44 | 45 | 46 | 47 | | 60 | 61 | 62 | 63 | | 76 | 77 | 78 | 79 |
 *  -------------------   -------------------   -------------------   -------------------
 *                        -------------------
 *                       | 80 | 81 | 82 | 83 |
 *                       ---------------------
 *                       | 84 | 85 | 86 | 87 |
 *                       ---------------------
 *                       | 88 | 89 | 90 | 91 |
 *                       ---------------------
 *                       | 92 | 93 | 94 | 95 |
 *                        -------------------
 */

		private Color[] _colors = new Color[6 * 16];
		private Dictionary<Faces, Color> colorDictionary;
		private static int[,] EDGE_LOCATIONS = {{ 1, 66 }, { 65, 2 }, { 17, 4 }, { 7, 50 }, { 8, 18 }, { 49, 11 }, { 33, 13 }, { 14, 34 },
			{ 71, 20 }, { 55, 68 }, { 23, 36 }, { 39, 52 }, { 40, 27 }, { 56, 43 }, { 24, 75 }, { 72, 59 },
			{ 81, 45 }, { 46, 82 }, { 30, 84 }, { 87, 61 }, { 88, 29 }, { 62, 91 }, { 78, 93 }, { 94, 77 }
		};
		private static int[,] CORNER_LOCATIONS = { { 0, 16, 67 },  { 15, 48, 35 }, { 3,  64, 51 }, { 12,  32, 19 },
			{ 80, 31, 44 }, { 95, 63, 76 }, { 83, 47, 60 }, { 92, 79, 28 }
		};
		private static int[] CENTER_LOCATIONS = { 53, 54, 57, 58,
			21, 22, 25, 26,
			5,  6,  9,  10,
			85, 86, 89, 90,
			37, 38, 41, 42,
			69, 70, 73, 74
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

		public void setColors(Cube cube){
			setColors (cube.EdgePosition, cube.CornerPosition, cube.CornerOrientation, cube.CenterPosition);
		}

		public void setColors (int[] edges, int[] corners, int[] orientations, int[] centers)
		{
			
			for (int i = 0; i < _colors.Length; i++) {
				_colors [i] = CubeColor.order [i / 16];
			}
			for (int i = 0; i < 24; i++) {
				_colors [EDGE_LOCATIONS [i, 0]] = colorDictionary [Edge.order [edges [i]].face1];
				_colors [EDGE_LOCATIONS [i, 1]] = colorDictionary [Edge.order [edges [i]].face2];
			}
			for (int i = 0; i < 8; i++) {
				int offset = orientations [i];
				_colors [CORNER_LOCATIONS [i, mod (0 + offset, 3)]] = colorDictionary [Corner.order [corners [i]].face1];
				_colors [CORNER_LOCATIONS [i, mod (1 + offset, 3)]] = colorDictionary [Corner.order [corners [i]].face2];
				_colors [CORNER_LOCATIONS [i, mod (2 + offset, 3)]] = colorDictionary [Corner.order [corners [i]].face3];
			}
			for (int i = 0; i < 24; i++) {
				_colors[CENTER_LOCATIONS[i]] = colorDictionary[(Faces)centers[i]];
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

