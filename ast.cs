
using System;
using System.IO;

namespace NyanPass {

	interface ISyntaxNodeVisitor {
	}

	interface ISyntaxNode {
		void accept(ISyntaxNodeVisitor visitor);
	}

}