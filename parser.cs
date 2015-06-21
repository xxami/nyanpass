
using System;
using System.IO;
using System.Text;

/**
 * disable "possible mistaken empty statement"
 */
#pragma warning disable 642

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

		public interface IParseable {

			/**
			 * accept Parser.ParseLine input, figure out in
			 * the shortest time whether it's needed to parse further
			 * return true to indicate that other parsers need not be called
			 */
			bool ShouldConsume(string line, int i, char c, int len);
		}

		/**
		 * parse a multi-line comment to provide support for
		 * defining plugin information in comments using the
		 * @property value syntax
		 */
		public class MultilineCommentParser : IParseable {

			private StringBuilder prop_key;
			private StringBuilder prop_value;

			/**
			 * reference given in constructor to Parser.plugin_info
			 */
			private PluginInfo plugin_info;

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

			public MultilineCommentParser(PluginInfo plugin_info) {
				this.plugin_info = plugin_info;
				this.prop_key = new StringBuilder();
				this.prop_value = new StringBuilder();
			}

			/**
			 * parser for multi-line comments
			 * return true when in a multiline comments
			 */
			public bool ShouldConsume(string line, int i,
				char c, int len) {
				if (this.closed) {
					if (this.prev_c == '/' && c == '*') {
						this.closed = false;
					}
					else {
						this.prev_c = c;
						return false;
					}
				}
				if (c == '@') {
					this.prop_key.Length = 0;
					this.prop_value.Length = 0;
					this.in_key = true;
				}
				else if (c == ' ') {
					if (this.in_key) {
						this.in_key = false;
						this.in_value = true;
					}
				}
				else if (this.prev_c == '*' && c == '/') {
					this.closed = true;
				}
				else if (i == len) {
					this.prev_c = '\0';
					if (this.in_value) {
						this.in_value = false;
						this.in_key = false;
						this.prop_value.Append(c);
						this.SetProperties();
					}
				}

				if (this.in_key)
					this.prop_key.Append(c);
				else if (this.in_value)
					this.prop_value.Append(c);

				this.prev_c = c;
				if (this.closed)
					return false;
				return true;
			}

			/**
			 * set plugin info properties from @props comments
			 */
			private void SetProperties() {
				switch (this.prop_key.ToString()) {

					case "name":
						this.plugin_info.name = 
							this.prop_value.ToString();
						break;

					case "author":
						this.plugin_info.author =
							this.prop_value.ToString();
						break;

					case "description":
					case "desc":
						this.plugin_info.description =
							this.prop_value.ToString();
						break;

					case "version":
						this.plugin_info.version =
							this.prop_value.ToString();
						break;

					case "url":
						this.plugin_info.version =
							this.prop_value.ToString();
						break;

					default: break;
				}

			}

		}

		/**
		 * multiline comment state parser object
		 */
		private MultilineCommentParser comments_parser;
		
		/**
		 * parsed plugin info data - error if not included
		 */
		private PluginInfo plugin_info;

		/**
		 * parse out the given file
		 */
		public Parser(string input_file) {
			this.comments_parser = new MultilineCommentParser(this.plugin_info);
			this.plugin_info = new PluginInfo();
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

			int len = line.Length;
			for (int i = 0; i < len; i++) {

				/**
				 * handled by the multi-line comment parser?
				 */
				if (this.comments_parser.ShouldConsume(line, i, line[i], len - 1));
			}

		}

	}

}
