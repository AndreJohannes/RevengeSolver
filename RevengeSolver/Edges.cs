using System;
using System.Collections.Generic;

namespace RevengeSolver {
	
	public class Edge {

		public static Edge UB = new Edge (Faces.U, Faces.B);
		public static Edge BU = new Edge (Faces.B, Faces.U);
		public static Edge LU = new Edge (Faces.L, Faces.U);
		public static Edge UR = new Edge (Faces.U, Faces.R);
		public static Edge UL = new Edge (Faces.U, Faces.L);
		public static Edge RU = new Edge (Faces.R, Faces.U);
		public static Edge FU = new Edge (Faces.F, Faces.U);
		public static Edge UF = new Edge (Faces.U, Faces.F);

		public static Edge BL = new Edge (Faces.B, Faces.L);
		public static Edge RB = new Edge (Faces.R, Faces.B);
		public static Edge LF = new Edge (Faces.L, Faces.F);
		public static Edge FR = new Edge (Faces.F, Faces.R);
		public static Edge FL = new Edge (Faces.F, Faces.L);
		public static Edge RF = new Edge (Faces.R, Faces.F);
		public static Edge LB = new Edge (Faces.L, Faces.B);
		public static Edge BR = new Edge (Faces.B, Faces.R);

		public static Edge DF = new Edge (Faces.D, Faces.F);
		public static Edge FD = new Edge (Faces.F, Faces.D);
		public static Edge LD = new Edge (Faces.L, Faces.D);
		public static Edge DR = new Edge (Faces.D, Faces.R);
		public static Edge DL = new Edge (Faces.D, Faces.L);
		public static Edge RD = new Edge (Faces.R, Faces.D);
		public static Edge BD = new Edge (Faces.B, Faces.D);
		public static Edge DB = new Edge (Faces.D, Faces.B);


		int[,] s = {{ 1, 66 }, { 65, 2 }, { 17, 4 }, { 7, 50 },
			{ 8, 18 },
			{ 11, 49 },
			{ 13, 33 },
			{ 14, 34 },
			{ 20, 71 },
			{ 55, 68 },
			{ 23, 36 },
			{ 52, 39 },
			{ 27, 40 },
			{ 56, 43 },
			{ 24, 75 },
			{ 59, 72 },
			{ 81, 45 },
			{ 82, 46 },
			{ 84, 30 },
			{ 87, 61 },
			{ 88, 29 },
			{ 91, 62 },
			{ 93, 78 },
			{ 94, 77 }
		};


		public static Edge[] order = new Edge[] {
			UB, BU, LU, UR, UL, RU, FU, UF,
			BL, RB, LF, FR, FL, RF, LB, BR,
			DF, FD, LD, DR, DL, RD, BD, DB
		};

		private Faces _face1;
		private Faces _face2;

		private Edge (Faces face1, Faces face2) {
			this._face1 = face1;
			this._face2 = face2;

		}

		public static int[] twist (Twist twist, int[] positions) {
			return null;
		}

		public Faces face1 {
			get { return _face1; }
		}

		public Faces face2 {
			get { return _face2; }
		}

		public int index {
			get {
				int idx = 0;
				foreach (Edge edge in order) {
					if (this.Equals (edge))
						return idx;
					idx++;
				}
				return -1;
			}
		}

		public override bool Equals (object obj) {
			var item = obj as Edge;

			if (item == null) {
				return false;
			}

			return this._face1.Equals (item._face1) && this._face2.Equals (item._face2);
		}

		public override int GetHashCode () {
			return this._face1.GetHashCode () + 31 * this._face2.GetHashCode ();
		}
	}
		
}

