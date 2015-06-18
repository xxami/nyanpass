
using System;
using System.IO;

namespace NyanPass {

	class Program {

		/**
		 * command line flag configuration
		 */
		public static bool use_transitional_syntax = false;
		public static bool quiet_mode_enabled = false;

		/**
		 * parse flags and input and start compilation if needed
		 */
		public static void Main(string[] args) {

			int len = args.Length;

			if (len < 2) {
				Program.DisplaySyntax();
				return;
			}

			/**
			 * process flags
			 */
			else if (len > 2) {

				for (int i = 2; i < len; i++) {
					
					switch (args[i]) {

						/**
						 * syntax of sourcepawn code to generate
						 */
						case "--syntax":
							
							if (i+1 >= len) {
								break;
							}

							switch (args[i+1]) {

								/**
								 * legacy syntax (prior to sourcepawn v1.7)
								 */
								case "legacy":
									Program.use_transitional_syntax = false;
									i++;
									break;

								/**
								 * transitional syntax (v1.7+)
								 */
								case "transitional":
								case "trans":
								case "v1.7":
									Program.use_transitional_syntax = true;
									i++;
									break;

								default: break;

							}

							break;

						/**
						 * do not print to stdout when quiet flag is enabled
						 */
						case "--quiet":
							Program.quiet_mode_enabled = true;
							break;

						default: break;

					}
				}
			}

			string input_file;

			/**
			 * process commands
			 */
			switch (args[0]) {

				/**
				 * compile to source, plus process with the
				 * sourcepawn compiler, and trace any errors back
				 */
				case "compile":
					Util.LogMessage("compile");
					input_file = args[1];
					if (File.Exists(input_file)) 
						Program.Compile(input_file);
					else Util.LogError("error: input file not found");
					break;

				/**
				 * compile to source
				 */
				case "source":
					input_file = args[1];
					if (File.Exists(input_file))
						Program.Compile(input_file);
					else Util.LogError("error: input file not found");
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

			Util.LogMessage("finished... meow :)");

		}

		/**
		 * actually display help, version, syntax, etc.
		 */
		public static void DisplaySyntax() {
			Util.LogMessage("syntax: nyanpass compile | source \"file.nyan\"" + 
				" --flag value\n\nflags:\n");
			Util.LogMessage("    --syntax\n        legacy\t\t\t\t" +
				"generate sourcepawn legacy syntax\n        transitional, " +
				"trans, v1.7\tgenerate sourcepawn transitional syntax\n");
			Util.LogMessage("    --quiet\t\t\t\tdisable stdout messages/logging\n");
			Util.LogMessage("find out more: https://github.com/xxami/nyanpass/");
		}

		/**
		 * initialize compiler
		 */
		public static void Compile(string input_file) {
			Util.LogMessage("using " + ((Program.use_transitional_syntax) ?
				"transitional syntax..." : "legacy syntax..."));
			Util.LogMessage("compiling " + input_file + "...");

			new Interpreter(input_file);
		}

	}

}
