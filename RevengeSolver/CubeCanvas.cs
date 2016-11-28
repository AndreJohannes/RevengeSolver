using System;
using Gtk;
using Cairo;

namespace RevengeSolver
{

	public class CubeColor
	{

		public static Color White = new Color (1d, 1d, 1d, 1d);
		public static Color Red = new Color (1d, 0, 0, 1d);
		public static Color Blue = new Color (0, 0, 1d, 1d);

		public static Color Orange = new Color (1d, 165d / 255d, 0, 1d);
		public static Color Green = new Color (0, 1d, 0);
		public static Color Yellow = new Color (1d, 1d, 0);

		public static Color[] order = {White, Red, Blue, Orange, Green, Yellow};

	}


	public class CubeCanvas : DrawingArea
	{

		private Rectangle[] rectangles;
		private Color[] _colors;

		public CubeCanvas (){
			rectangles = makeRectangles ();
		}

		public Color[] Colors {
			set {_colors = value;}
		}

		private Rectangle[] makeRectangles ()
		{
			int dx = 40;
			int dy = 40;
			int idx = 0;
			Rectangle[] retArray = new Rectangle[6 * 16];
			int[,] offsets = { { 4 * dx, 0 }, { 0, 4 * dy }, { 4 * dx, 4 * dy },
				{ 8 * dx, 4 * dy }, { 12 * dx, 4 * dy }, { 4 * dx, 8 * dy }
			};
			for (int o = 0; o < offsets.GetLength (0); o++) {
				for (int i = 0; i < 4; i++) {
					for (int j = 0; j < 4; j++) {
						retArray [idx++] = new Rectangle (offsets [o, 0] + j * dx, 
							offsets [o, 1] + i * dy, dx, dy); 
					}
				}
			}
			return retArray;
		}

		protected override bool OnExposeEvent (Gdk.EventExpose args)
		{
			using (Context g = Gdk.CairoHelper.Create (args.Window)) {
				int idx = 0;
				foreach (Rectangle rectangle in rectangles) {
					g.Rectangle (rectangle);
					g.Color = _colors[idx++];
					g.Fill ();
					g.Rectangle (rectangle);
					g.Color = new Color(0,0,0,1);
					g.Stroke ();
				}

			}
			return true;
		}
	}
}

