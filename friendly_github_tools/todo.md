* finish setting up single file application:
https://docs.microsoft.com/en-us/dotnet/core/deploying/single-file
https://www.hanselman.com/blog/making-a-tiny-net-core-30-entirely-selfcontained-single-executable
https://docs.microsoft.com/en-us/dotnet/core/deploying/ready-to-run
* Figure out how to pass release info to tool in a simple way for publishing files
* Make use of the tool itself to publish it's own release to Github
* Need to consider ways to minimize the number of REST API calls so as to not prematurely exhaust the rate limit (particularly with edit operations)