﻿using System;
using System.Collections.Generic;

namespace RevengeSolver
{
	public interface IPhase
	{

		LinkedList<Twist> search(Cube cube);

	}
}
