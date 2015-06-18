
using System;
using System.IO;

namespace NyanPass {

	/**
	 * genetic static util methods
	 */
	class Util {

		/**
		 * wrap Console.WriteLine for conditional printing on --quiet
		 */
		public static void LogMessage(string message) {
			if (!Program.quiet_mode_enabled) Console.WriteLine(message);
		}

		/**
		 * always write errors regardless
		 */
		public static void LogError(string message) {
			Console.Error.WriteLine(message);
		}

	}

}
