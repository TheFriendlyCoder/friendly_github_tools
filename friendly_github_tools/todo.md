* Add support for CI to build the package in 3 variations: windows, linux and mac
* Figure out how to pass release info to tool in a simple way for publishing files
* Make use of the tool itself to publish it's own release to Github
* Need to consider ways to minimize the number of REST API calls so as to not prematurely exhaust the rate limit (particularly with edit operations)