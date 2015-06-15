
/**
 * @name hello world
 * @author ami
 * @desc hello world test
 * @version 0.1
 * @url http://lilah.cat/
 */

// we can define plugin info like this too
def name 'hello world';
def author 'ami';
def description 'test: print hello world';
def version '0.1';
def url 'http://lilah.cat/';

// defines are not variables
def debug 0;

import sourcemod as sm;
map sourcemod.OnPluginStart plugin.start;

# reasonable comment tokens

class plugin {

	static void start() {

		ifdef (debug) {
			sm.PrintToServer('Hello, debug world!');
		}
		else {
			sm.PrintToServer('Hello, World');
		}

	}

};
