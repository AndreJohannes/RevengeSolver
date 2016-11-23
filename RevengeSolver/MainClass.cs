using System;
using Gtk;
using Cairo;
using RevengeSolver;
using System.Linq;
using System.IO;
using Newtonsoft.Json.Linq;

namespace RevengeSolver
{
	public class MainClass
	{

		static void Main ()
		{
			Application.Init ();
			Gtk.Window w = new Gtk.Window ("Rubik's Revenge");

			ColorCube cube = new ColorCube ();
		
			int[] edges = Enumerable.Range(0, 24).ToArray();
			int[] corners = Enumerable.Range (0, 8).ToArray ();
			int[] orientations = new int[8];
			cube.setColors (Twist.R_.apply(edges, Type.Edges), 
				Twist.R_.apply(corners, Type.Corners),
				Twist.R_.apply(orientations, Type.Corners, orientation:true));
		
			CubeCanvas a = new CubeCanvas ();
			a.Colors = cube.colors; 

			Box box = new HBox (true, 0);
			box.Add ((DrawingArea)a);
			w.Add (box);
			w.Resize (40 * 4 * 4, 40 * 4 * 3);
			//w.DeleteEvent += close_window;
			w.ShowAll ();
			Console.WriteLine (String.Format("Is equal: {0}" ,new MainClass ().compare (Twist.L)));
			Application.Run ();
		}

		private JObject jObject = ReadJson();

		static JObject ReadJson(){

			JObject o1 = JObject.Parse(File.ReadAllText(@"/home/andre/WorkProjects/rubiksrevengesolver/Output.json"));
			return o1;


		}

		private Boolean compare(Twist twist){

			int[] edgeList = twist.apply (Enumerable.Range (0, 24).ToArray (), Type.Edges);
			int[] edgeReference = jObject[twist.Name]["Edges"].Select(jv => (int)jv).ToArray();
			Console.WriteLine(string.Join(",", edgeList));
			Console.WriteLine(string.Join(",", edgeReference));
			Console.WriteLine ("");
			int[] cornerList = twist.apply (Enumerable.Range (0, 8).ToArray (), Type.Corners);
			int[] cornerReference = jObject[twist.Name]["Corners"].Select(jv => (int)jv).ToArray();
			Console.WriteLine(string.Join(",", cornerList));
			Console.WriteLine(string.Join(",", cornerReference));
			Console.WriteLine ("");
			int[] centerList = twist.apply (Enumerable.Range (0, 24).ToArray (), Type.Centers);
			int[] centerReference = jObject[twist.Name]["Centers"].Select(jv => (int)jv).ToArray();
			Console.WriteLine(string.Join(",", centerList));
			Console.WriteLine(string.Join(",", centerReference));
			Console.WriteLine ("");
			return Enumerable.SequenceEqual(edgeList, edgeReference) && 
				Enumerable.SequenceEqual(cornerList, cornerReference) &&
				Enumerable.SequenceEqual(centerList, centerReference);
		}


	}



}

