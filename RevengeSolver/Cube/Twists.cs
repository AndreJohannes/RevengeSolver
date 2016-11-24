using System;
using System.Linq;

namespace RevengeSolver
{

	public enum Type
	{
		Corners,
		Edges,
		EdgePairs,
		Centers
	}

	public class Twist
	{
		public static Twist U = new Twist ("U", new Edge[,] { { Edge.UF, Edge.UR, Edge.UB, Edge.UL },
			{ Edge.FU, Edge.RU, Edge.BU, Edge.LU }
		},
			                        new Corner[]{ Corner.ULF, Corner.URF, Corner.URB, Corner.ULB },
			                        new Center[]{ Center.U3, Center.U4, Center.U2, Center.U1 }
		                        );
		public static Twist D = new Twist ("D", new Edge[,] { { Edge.DL, Edge.DB, Edge.DR, Edge.DF },
			{ Edge.LD, Edge.BD, Edge.RD, Edge.FD }
		},
			                        new Corner[]{ Corner.DRF, Corner.DLF, Corner.DLB, Corner.DRB },
			                        new Center[]{ Center.D3, Center.D4, Center.D2, Center.D1 }
		                        );
		public static Twist F = new Twist ("F", new Edge[,] {{ Edge.FL, Edge.FD, Edge.FR, Edge.FU },
			{ Edge.LF, Edge.DF, Edge.RF, Edge.UF }
		},
			                        new Corner[]{ Corner.DLF, Corner.DRF, Corner.URF, Corner.ULF },
			                        new Center[]{ Center.F3, Center.F4, Center.F2, Center.F1 }
		                        );
		public static Twist B = new Twist ("B", new Edge[,] {{ Edge.BU, Edge.BR, Edge.BD, Edge.BL },
			{ Edge.UB, Edge.RB, Edge.DB, Edge.LB }
		},
			                        new Corner[]{ Corner.ULB, Corner.URB, Corner.DRB, Corner.DLB },
			                        new Center[]{ Center.B3, Center.B4, Center.B2, Center.B1 }
		                        );
		public static Twist R = new Twist ("R", new Edge[,] {{ Edge.RF, Edge.RD, Edge.RB, Edge.RU },
			{ Edge.FR, Edge.DR, Edge.BR, Edge.UR }
		},
			                        new Corner[]{ Corner.DRF, Corner.DRB, Corner.URB, Corner.URF },
			                        new Center[]{ Center.R3, Center.R4, Center.R2, Center.R1 }
		                        );
		public static Twist L = new Twist ("L", new Edge[,] {{ Edge.LU, Edge.LB, Edge.LD, Edge.LF, },
			{ Edge.UL, Edge.BL, Edge.DL, Edge.FL }
		},
			                        new Corner[]{ Corner.ULF, Corner.ULB, Corner.DLB, Corner.DLF },
			                        new Center[]{ Center.L3, Center.L4, Center.L2, Center.L1 }
		                        );

		public static Twist U2 = new Twist ("U2", U, 2);
		public static Twist U_ = new Twist ("U'", U, 3);
		public static Twist D2 = new Twist ("D2", D, 2);
		public static Twist D_ = new Twist ("D'", D, 3);
		public static Twist F2 = new Twist ("F2", F, 2);
		public static Twist F_ = new Twist ("F'", F, 3);
		public static Twist B2 = new Twist ("B2", B, 2);
		public static Twist B_ = new Twist ("B'", B, 3);
		public static Twist R2 = new Twist ("R2", R, 2);
		public static Twist R_ = new Twist ("R'", R, 3);
		public static Twist L2 = new Twist ("L2", L, 2);
		public static Twist L_ = new Twist ("L'", L, 3);

		private readonly string _name;
		private readonly int[] edgePermutation;
		private readonly int[] pairPermutation;
		private readonly int[] cornerPermutation;
		private readonly int[] cornerOrientation;
		private readonly int[] centerPermutation;

		private Twist (string name, Edge[,] edgeCycles, Corner[] cornerCycle, Center[] centerCycle)
		{

			this._name = name;
			edgePermutation = Enumerable.Range (0, 24).ToArray ();
			int length = edgeCycles.GetLength (1);
			for (int i = 0; i < edgeCycles.GetLength (0); i++) {
				for (int j = 0; j < length; j++) {
					int index1 = edgeCycles [i, j].index;
					int index2 = edgeCycles [i, (j + 1) % length].index;
					edgePermutation [index1] = index2;
				}
			}
			pairPermutation = Enumerable.Range (0, 12).ToArray ();
			length = edgePermutation.Length;
			int[] pairs = Edge.getPairs (); 
			for (int j = 0; j < length; j++) {
				pairPermutation [pairs [j]] = pairs [edgePermutation [j]];
			}
			cornerPermutation = Enumerable.Range (0, 8).ToArray ();
			cornerOrientation = new int[8];
			length = cornerCycle.Length;
			Faces baseFace = getBaseFace (cornerCycle);
			for (int j = 0; j < length; j++) {
				int index1 = cornerCycle [j].index;
				int index2 = cornerCycle [(j + 1) % length].index;
				cornerPermutation [index1] = index2;
				int faceIdx1 = getCornerFaceIndex (baseFace, cornerCycle [j]);
				int faceIdx2 = getCornerFaceIndex (baseFace, cornerCycle [(j + 1) % length]);
				cornerOrientation [index1] = (faceIdx1 - faceIdx2) % 3;
			}
			centerPermutation = Enumerable.Range (0, 24).ToArray ();
			length = centerCycle.Length;
			for (int j = 0; j < length; j++) {
				int index1 = centerCycle [j].index;
				int index2 = centerCycle [(j + 1) % length].index;
				centerPermutation [index1] = index2;
			}
		}

		private Twist (string name, Twist twist, int num)
		{
			_name = name;
			cornerPermutation = Enumerable.Range (0, 8).ToArray ();
			centerPermutation = Enumerable.Range (0, 24).ToArray ();
			edgePermutation = Enumerable.Range (0, 24).ToArray ();
			pairPermutation = Enumerable.Range (0, 12).ToArray ();
			cornerOrientation = new int[8];
			for (int i = 0; i < num; i++) {
				cornerPermutation = twist.apply (cornerPermutation, Type.Corners);
				edgePermutation = twist.apply (edgePermutation, Type.Edges);
				centerPermutation = twist.apply (centerPermutation, Type.Edges);
				cornerOrientation = twist.apply (cornerOrientation, Type.Corners, orientation: true);
				pairPermutation = twist.apply (pairPermutation, Type.EdgePairs);
			}		
		}

		public string Name {
			get{ return _name; }
		}

		/// <summary>
		/// Apply the specified configuration, type and orientation.
		/// </summary>
		/// <param name="configuration">Configuration.</param>
		/// <param name="type">Type.</param>
		/// <param name="orientation">If set to <c>true</c> orientation.</param>
		public int[] apply (int[] configuration, Type type, Boolean orientation = false)
		{
			int[] retArray = new int[configuration.Length];
			int[] permutations = type == Type.Edges ? edgePermutation : 
				type == Type.Corners ? cornerPermutation : 
				type == Type.EdgePairs ? pairPermutation : 
				centerPermutation; 
			for (int i = 0; i < retArray.Length; i++) {
				retArray [i] = configuration [permutations [i]]; 
			}
			if (orientation) {
				for (int i = 0; i < retArray.Length; i++) {
					retArray [i] = mod(retArray [i] + cornerOrientation [i],3);
				}
			}
			return retArray;
		}


		private Faces getBaseFace (Corner[] cornerCycle)
		{
			int[] faceArray = new int[6];
			foreach (Corner corner in cornerCycle) {
				faceArray [(int)(corner.face1)]++;
				faceArray [(int)(corner.face2)]++;
				faceArray [(int)(corner.face3)]++;
			}
			Faces retValue = Faces.B;
			int max = -1;
			foreach (Faces face in Enum.GetValues(typeof(Faces))) {
				int index = (int)face;
				if (faceArray [index] > max) {
					max = faceArray [index];
					retValue = face;
				}
			}
			return retValue;
		}

		private int getCornerFaceIndex (Faces face, Corner corner)
		{
			if (corner.face1 == face)
				return 0;
			if (corner.face2 == face)
				return 1;
			if (corner.face3 == face)
				return 2;
			throw new System.ArgumentException ("The face passed is not valid");
		}

		private int mod (int k, int n)
		{
			return ((k %= n) < 0) ? k + n : k;
		}

	}

}

