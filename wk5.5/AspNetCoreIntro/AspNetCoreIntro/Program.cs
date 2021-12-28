using AspNetCoreIntro.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// before we "build" the WebApplication,
// we "set up" things that the middleware/etc will need when they run
builder.Services.AddControllers();

WebApplication app = builder.Build();

// by default (customizable), for security reasons, this builtin middleware
// only serves files inside a folder named wwwroot inside the project
app.UseStaticFiles();

// exercise option 1:
//    make an asp.net core app (start with the empty template) that
//      keeps track of some structured data in custom classes and uses serialization
//      to respond with the data. for example: static list of rock-paper-scissors roundresults,
//    and use XmlSerializer to convert it to XML in the response body.
// exercise option 2:
//   make an asp.net core app (start with the empty template) that
//     treat the "path" as a relative path on the server's file system.
//     the response should include the contents of that file.
//     example: https://localhost:7206/Program.cs
//     choose a good content type based on the file extension.
//     return 404 if no such file.
// 

//app.MapGet("/", () => "Hello World!");

// after you have a WebApplication, you need to construct its
// request-processing pipeline using components called "middlewares".

// this call adds a middleware to the pipeline.
// each middleware runs in sequence.
// "Use" middleware could be anywhere in the pipeline

// asp.net code should be async so that the app can handle multiple requests at a time more efficiently
//app.Use((context, next) =>
//{
//    // synchronously wait; simulate some long-running operation
//    // while not being async
//    Task.Delay(3000).Wait();
//    next(context).Wait();

//    return Task.CompletedTask;
//});

//app.UseMiddleware<RequireAuthMiddleware>();
// replaced by ^
//app.Use(async (context, next) =>
//{
//    if (context.Request.Query["authenticated"] == "true")
//    {
//        // "this middleware is done, let the next one in the pipeline take over"
//        await next(context);
//    }
//    else
//    {
//        // if we don't invoke "next", this middleware is "short-circuiting" the pipeline -
//        // it better finish setting up the response.

//        context.Response.StatusCode = 401;
//        context.Response.ContentType = "text/plain";
//        //await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes("error, not authenticated"));
//        await context.Response.WriteAsync("error, not authenticated");
//        //context.Response.Body
//    }
//});

// accessing shared data from multiple threads (runs of the request-processing pipeline)
// has a big concern called CONCURRENCY, some classes are not designed to handle it.
//   check the documentation for classes if they are "threadsafe" to see if it's ok or not
// if it's not, try something else, or use locks (something C# can do)
app.UseMiddleware<DataSerializeMiddleware>();
// replaced by ^
//app.Use(async (context, next) =>
//{
//    if (context.Request.Path.StartsWithSegments("/data"))
//    {
//        context.Response.StatusCode = StatusCodes.Status200OK; // same as = 200, more verbose
//        context.Response.ContentType = MediaTypeNames.Text.Xml; // same as = "text/xml", stops you from a typo

//        // xmlserializer is not great and does not support async directly...
//        // if it was important, i would need to refactor this code
//        serializer.Serialize(stream: context.Response.Body, o: theData);
//    }
//    else
//    {
//        // not my job, next middleware handle this one
//        await next(context);
//    }
//});


// these middlewares together activate a BUNCH of nice builtin behavior
// in concert with things called Controllers
//   which you will make (at least from a REST point of view) to handle specific resources
// nice things like: automatic serialization to the response (using "ActionResults")
//                   automatic deserialization from the request (model binding)
//                   convenient pattern-based routing based on the url path
//                   dependency injection (to be seen later)
app.UseRouting();
app.UseEndpoints(routeBuilder =>
{
    routeBuilder.MapControllers();
});

// broken?
app.Map("/map1", async context =>
{
    //await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes("Hello from map1"));
    await context.Response.WriteAsync("Hello from map1");
});

app.Map("/map2", async context =>
{
    await context.Response.WriteAsync("Hello from map2");
});

// "Run" middleware is at the end of the pipeline (no "next")
app.Run(async context =>
{
    // the HttpContext parameter (context) gives access to all the request data
    // and lets you modify all the response data.

    string path = context.Request.Path;
    string dataValue = context.Request.Query["data"];

    context.Response.StatusCode = 200;
    context.Response.ContentType = "text/plain";
    // serializing the string as bytes using UTF8 encoding, and writing it to the
    // HTTP response directly
    await context.Response.WriteAsync($"path was: {path}, data was {dataValue}");

    // need this line so it compiles
    // (really this delegate should've been async but we hadn't done that yet) (it's now async)
    //return Task.CompletedTask;
});


// this call runs the app
await app.RunAsync();
