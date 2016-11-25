using System;
using Gtk;
using Cairo;
using RevengeSolver;
using System.Linq;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading;

namespace RevengeSolver
{
	public class MainClass
	{

		static void Main ()
		{
			Application.Init ();
			Gtk.Window w = new Gtk.Window ("Rubik's Revenge");

			ColorCube cube = new ColorCube ();
		
			int[] edges = Enumerable.Range (0, 24).ToArray ();
			int[] corners = Enumerable.Range (0, 8).ToArray ();
			int[] orientations = new int[8];
		
			CubeCanvas cubeCanvas = new CubeCanvas ();
			cubeCanvas.Colors = cube.colors; 

			Box box = new HBox (true, 0);
			box.Add ((DrawingArea)cubeCanvas);
			w.Add (box);
			w.Resize (40 * 4 * 4, 40 * 4 * 3);
			//w.DeleteEvent += close_window;
			w.ShowAll ();

			MainClass solver = new MainClass (cubeCanvas);

			Thread solverThread = new Thread (new ThreadStart (solver.solveCube));
			solverThread.Start ();

			//solver.compare (Twist.l);

			Application.Run ();
		}

		private JObject jObject = ReadJson ();

		static JObject ReadJson ()
		{
			JObject o1 = JObject.Parse (File.ReadAllText (@"/home/andre/WorkProjects/rubiksrevengesolver/Output.json"));
			return o1;
		}

		private ColorCube colorCube;
		private CubeCanvas cubeCanvas;

		public MainClass (CubeCanvas canvas)
		{
			colorCube = new ColorCube ();
			canvas.Colors = colorCube.colors;
			cubeCanvas = canvas;
		}

		private Boolean compare (Twist twist)
		{
			int[] edgeList = twist.apply (Enumerable.Range (0, 24).ToArray (), Type.Edges);
			int[] edgeReference = jObject [twist.Name] ["Edges"].Select (jv => (int)jv).ToArray ();
			Console.WriteLine (string.Join (",", edgeList));
			Console.WriteLine (string.Join (",", edgeReference));
			Console.WriteLine ("");
			int[] cornerList = twist.apply (Enumerable.Range (0, 8).ToArray (), Type.Corners);
			int[] cornerReference = jObject [twist.Name] ["Corners"].Select (jv => (int)jv).ToArray ();
			Console.WriteLine (string.Join (",", cornerList));
			Console.WriteLine (string.Join (",", cornerReference));
			Console.WriteLine ("");
			int[] centerList = twist.apply (Enumerable.Range (0, 24).ToArray (), Type.Centers);
			int[] centerReference = jObject [twist.Name] ["Centers"].Select (jv => (int)jv).ToArray ();
			Console.WriteLine (string.Join (",", centerList));
			Console.WriteLine (string.Join (",", centerReference));
			Console.WriteLine ("");
			return Enumerable.SequenceEqual (edgeList, edgeReference) &&
			Enumerable.SequenceEqual (cornerList, cornerReference) &&
			Enumerable.SequenceEqual (centerList, centerReference);
		}

		private void solveCube ()
		{

			LinkedList<IPhase> phases = new LinkedList<IPhase> ();
			//phases.AddLast (new Phase8 ());
			//phases.AddLast (new Phase7 ());
			//phases.AddLast (new Phase6 ());
			//phases.AddLast (new Phase5 ());
			phases.AddLast (new Phase4());

			Cube cube = new Cube ();

			foreach (IPhase phase in phases) {
				phase.scramble (cube, 10);
			}

			colorCube.setColors (cube);

			foreach (IPhase phase in phases.Reverse()) {
				LinkedList<Twist> twists = phase.search (cube);
				cube.twist (twists);
				colorCube.setColors (cube);
				cubeCanvas.QueueDraw ();
				Console.WriteLine ("Solved");
			}
		}
	}

}

