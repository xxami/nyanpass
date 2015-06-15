
using System;
using System.IO;

namespace NyanPass {

	class Program {

		public static void Main(string[] args) {

			int len = args.Length;
			
			if (len < 2) {
				Console.WriteLine(@"syntax: nyanpass compile | source ""file.nyan""");
				return;
			}

			string input_file;

			switch (args[0]) {

				/**
				 * compile to source, plus process with the
				 * sourcepawn compiler, and trace any errors back
				 */
				case "compile":
					Console.WriteLine("compile");
					input_file = args[1];
					if (File.Exists(input_file)) 
						Program.Compile(input_file);
					else Console.WriteLine(@"error: input file not found");
					break;

				/**
				 * compile to source
				 */
				case "source":
					input_file = args[1];
					if (File.Exists(input_file))
						Program.Compile(input_file);
					else Console.WriteLine(@"error: input file not found");
					break;
				default:
					Console.WriteLine(@"syntax: nyanpass compile | source ""file.nyan""");
					return;
			}

			Console.WriteLine("finished - meow :)");

		}

		public static void Compile(string input_file) {
			var interp = new Interpreter(input_file);
		}

	}

}
