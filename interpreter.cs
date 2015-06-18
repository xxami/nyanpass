
using System;
using System.IO;

namespace NyanPass {

	class PluginInfo {

		public string name = "";
		public string author = "";
		public string description = "";
		public string version = "";
		public string url = "";

	}

	class Interpreter {

		public Interpreter(string input_file) {
			this.ParseFile(input_file);
		}

		/**
		 * recursively read in and do the initial parsing
		 * on the input file and it's imported files
		 */
		private void ParseFile(string input_file) {
			using (StreamReader sr = File.OpenText(input_file)) {

				string line = "";
				while ((line = sr.ReadLine()) != null) {

				}

			}

		}

	}
	
}
