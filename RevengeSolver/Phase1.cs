using System;
using System.Collections.Generic;

namespace RevengeSolver {


	public class Phase1 {

		private class Node : BFSearch<int, Twists>.INode {

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

		public Phase1 () {



		}
	}
}

