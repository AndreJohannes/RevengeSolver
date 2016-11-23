using System;
using System.Collections.Generic;

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
			_cornerPosition= new int[8];
			_cornerOrientation = new int[8];
			_edgePosition = new int[24];
			_centerPosition = new int[24];
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

		public void twist (Twist twist)
		{
			_cornerPosition = twist.apply (_cornerPosition, Type.Corners);
			_cornerOrientation = twist.apply (_cornerOrientation, Type.Corners, orientation: true);
			_edgePosition = twist.apply (_edgePosition, Type.Edges);
			_centerPosition = twist.apply (_cornerOrientation, Type.Centers);
			twists.AddLast (twist);
		}

	}
}

