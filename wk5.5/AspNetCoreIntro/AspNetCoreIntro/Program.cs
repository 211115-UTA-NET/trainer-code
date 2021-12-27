using System.Text;
using Microsoft.AspNetCore.Server.Kestrel.Core;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
WebApplication app = builder.Build();

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
app.Use((context, next) =>
{
    if (context.Request.Query["authenticated"] == "true")
    {
        // "this middleware is done, let the next one in the pipeline take over"
        next(context);
    }
    else
    {
        // if we don't invoke "next", this middleware is "short-circuiting" the pipeline -
        // it better finish setting up the response.

        context.Response.StatusCode = 401;
        context.Response.ContentType = "text/plain";
        context.Response.Body.Write(Encoding.UTF8.GetBytes("error, not authenticated"));
        //context.Response.Body
    }

    return Task.CompletedTask;
});

// broken?
app.Map("/map1", context =>
{
    context.Response.Body.Write(Encoding.UTF8.GetBytes("Hello from map1"));
    return Task.CompletedTask;
});

app.Map("/map2", context =>
{
    context.Response.Body.Write(Encoding.UTF8.GetBytes("Hello from map2"));
    return Task.CompletedTask;
});

// "Run" middleware is at the end of the pipeline (no "next")
app.Run(context =>
{
    // the HttpContext parameter (context) gives access to all the request data
    // and lets you modify all the response data.

    string path = context.Request.Path;
    string dataValue = context.Request.Query["data"];

    context.Response.StatusCode = 200;
    context.Response.ContentType = "text/plain";
    // serializing the string as bytes using UTF8 encoding, and writing it to the
    // HTTP response directly
    context.Response.Body.Write(Encoding.UTF8.GetBytes($"path was: {path}, data was {dataValue}"));

    // need this line so it compiles
    // (really this delegate should be async but we haven't done that yet)
    return Task.CompletedTask;
});


// this call runs the app
app.Run();
