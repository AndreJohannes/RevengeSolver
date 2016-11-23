using System;
using System.Collections.Generic;

namespace RevengeSolver {


	public class Phase8: IPhase {

		private class Node : BFSearch<int, Twists>.INode {

			private readonly int[] _cornerPosition;
			private readonly int[] _pairPosition;

			public Node(int[] cornerPosition, int[] pairPosition){
				_cornerPosition = cornerPosition;
				_pairPosition = pairPosition;
			}

			public int getID () {
				return 1;
			}

			public LinkedList<Twists> getMoves () {
				return null;
			}

			public BFSearch<int, Twists>.INode copyAndMove (Twists move) {
				return null;
			}

		}

		private readonly BFSearch<String, Twist> bfSearch;

		public Phase8 () {

			Twist[] generators = new Twist[] {
				Twist.U2, Twist.D2, Twist.R2, Twist.L2, Twist.F2, Twist.B2,
			};

			new BFSearch<String, Twist> (generators);
		}

		public LinkedList<Twist> search(Cube cube){
						
		} 
	}
}

