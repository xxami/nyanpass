
using System;
using System.IO;

namespace NyanPass {

	class Program {

		public static bool use_transitional_syntax = false;

		public static void Main(string[] args) {

			int len = args.Length;

			if (len < 2) {
				Program.DisplaySyntax();
				return;
			}

			/**
			 * params include program switches
			 */
			else if (len > 3) {

				for (int i = 2; (i + 1) < len; i += 2) {

					switch (args[i]) {

						/**
						 * syntax of sourcepawn code to generate
						 */
						case "--syntax":
							
							switch (args[i+1]) {

								/**
								 * legacy syntax (prior to sourcepawn v1.7)
								 */
								case "legacy":
									Program.use_transitional_syntax = false;
									break;

								/**
								 * transitional syntax (v1.7+)
								 */
								case "transitional":
								case "trans":
								case "v1.7":
									Program.use_transitional_syntax = true;
									break;

								default: break;

							}
							
							break;

						default: break;

					}
				}
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

				/**
				 * display help, version, syntax, etc.
				 */
				case "help":
				case "-h":
				case "--h":
				case "--help":
				case "/h":
				case "/help":
				case "version":
				case "-v":
				case "--version":
				case "/v":
				case "/version":
				default:
					Program.DisplaySyntax();
					return;
			}

			Console.WriteLine("finished... meow :)");

		}

		/**
		 * actually display help, version, syntax, etc.
		 */
		public static void DisplaySyntax() {
			Console.WriteLine("syntax: nyanpass compile | source \"file.nyan\"" + 
				" --flag value\n\nflags:\n    --syntax\n        legacy\t\t\t\t" +
				"generate sourcepawn legacy syntax\n        transitional, " +
				"trans, v1.7\tgenerate sourcepawn transitional syntax\n\n" +
				"find out more: https://github.com/xxami/nyanpass/");
		}

		/**
		 * initialize compiler
		 */
		public static void Compile(string input_file) {
			Console.WriteLine("using " + ((Program.use_transitional_syntax) ?
				"transitional syntax..." : "legacy syntax..."));
			Console.WriteLine("compiling " + input_file + "...");
			var interp = new Interpreter(input_file);
		}

	}

}
