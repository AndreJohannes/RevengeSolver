using System;
using System.Collections.Generic;
using System.Linq;

namespace RevengeSolver
{
	public class Cube
	{

		private int[] _cornerPosition;
		private int[] _edgePosition;
		private int[] _centerPosition;
		private int[] _cornerOrientation;

		private LinkedList<Twist> twists;

		public Cube ()
		{
			twists = new LinkedList<Twist> ();
			_cornerPosition = Enumerable.Range (0, 8).ToArray ();
			_cornerOrientation = new int[8];
			_edgePosition = Enumerable.Range (0, 24).ToArray ();
			_centerPosition = Enumerable.Range (0, 24).ToArray ();
		}

		public int[] CornerPosition {
			get{ return _cornerPosition; }
		}

		public int[] CornerOrientation {
			get{ return _cornerOrientation; }
		}

		public int[] CenterPosition {
			get{ return _centerPosition; }
		}

		public int[] EdgePosition {
			get{ return _edgePosition; }
		}

		public int[] PairPosition {
			get { 
				int[] retArray = new int[_edgePosition.Length / 2];
				int[] pairs = Edge.getPairs ();
				for (int i = 0; i < _edgePosition.Length; i++) {
					retArray [pairs [i]] = pairs [_edgePosition [i]];
				}
				return retArray;
			}
		}

		public LinkedList<Twist> Twists {
			get{ return twists; }
		}

		public void twist(LinkedList<Twist> twists){
			foreach (Twist twist in twists) {
				this.twist (twist);
			}
		}

		public void twist (Twist twist)
		{
			_cornerPosition = twist.apply (_cornerPosition, Type.Corners);
			_cornerOrientation = twist.apply (_cornerOrientation, Type.Corners, orientation: true);
			_edgePosition = twist.apply (_edgePosition, Type.Edges);
			_centerPosition = twist.apply (_centerPosition, Type.Centers);
			twists.AddLast (twist);
		}

	}
}

