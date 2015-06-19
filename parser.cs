
using System;
using System.IO;
using System.Collections.Generic;

namespace NyanPass {

	/**
	 * required sourcepawn plugin info structure
	 */
	class PluginInfo {

		public string name = "";
		public string author = "";
		public string description = "";
		public string version = "";
		public string url = "";

	}

	/**
	 * parse nyanlang source to AST
	 */
	class Parser {

		/**
		 * parse a multi-line comment to provide support for
		 * defining plugin information in comments using the
		 * @property value syntax
		 */
		class MultilineComment {

			private string prop_key = "";
			private string prop_value = "";
			private char prev_c = '\0';

			private bool in_key = false;
			private bool in_value = false;

			/**
			 * whether the comment block has been closed
			 */
			public bool closed = true;

			/**
			 * whether a plugin info property can be read from a comment
			 */
			public bool has_property = false;

			public MultilineComment() {
				this.closed = false;
			}

			/**
			 * parse multiline comment char by char
			 */
			public void ParseChar(char c) {
				if (c == '@') {
					this.prop_key = "";
					this.prop_value = "";
					this.in_key = true;
				}
				else if (c == ' ') {
					if (this.in_key) {
						this.in_value = true;
					}
				}
				else if (c == '\n' || c == '\r') {
					if (this.in_value) {
						this.has_property = true;
					}
				}
				else if (prev_c == '*' && c == '/') {
					this.closed = true;
				}

				if (this.in_key)
					this.prop_key += c;
				else this.prop_value += c;

				this.prev_c = c;
			}

			/**
			 * return the current plugin info property
			 */
			public KeyValuePair<string, string> GetProperty() {
				return new KeyValuePair<string,
					string>(this.prop_key, this.prop_value);
			}
			
		}

		/**
		 * multiline comment state parser object
		 */
		private MultilineComment multi_comment_p = null;
		
		/**
		 * parsed plugin info data - error if not included
		 */
		private PluginInfo plugin_info = new PluginInfo();

		/**
		 * parse out the given file
		 */
		public Parser(string input_file) {
			this.ReadFile(input_file);
		}

		/**
		 * recursively read in and do the initial parsing
		 * on the input file and it's imported files
		 */
		private void ReadFile(string input_file) {
			using (StreamReader sr = File.OpenText(input_file)) {
				string line = "";
				while ((line = sr.ReadLine()) != null) {
					this.ParseLine(line);
				}
			}
		}

		/**
		 * parse the ordered loc from a call to ReadFile
		 */
		private void ParseLine(string line) {
			
			string buf = "";

			foreach (char c in line) {

				if (this.multi_comment_p != null &&
					!this.multi_comment_p.closed) {

					this.multi_comment_p.ParseChar(c);
					if (this.multi_comment_p.has_property) {
						KeyValuePair<string,
							string> kvp =
								this.multi_comment_p.GetProperty();

						Util.LogMessage(kvp.ToString());

						switch (kvp.Key) {

							case "name":
								this.plugin_info.name = kvp.Value;
								break;

							case "author":
								this.plugin_info.author = kvp.Value;
								break;

							case "description":
							case "desc":
								this.plugin_info.description = kvp.Value;
								break;

							case "version":
								this.plugin_info.version = kvp.Value;
								break;

							case "url":
								this.plugin_info.url = kvp.Value;
								break;

							default: break;
						}
					}

					continue;
				}

				/**
				 * parse unknown
				 */
				buf += c;
				if (buf == "/*") {
					this.multi_comment_p = new MultilineComment();
				}

			}

		}

	}

}
