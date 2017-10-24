using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
	class Program
	{
		static void Main(string[] args)
		{
			var solver = new ImageDiameterSolver(args[0]);
			var diameter = solver.GetDiameter();
			Console.WriteLine("Diameter of image is: {0}", diameter);
		}
	}
}
