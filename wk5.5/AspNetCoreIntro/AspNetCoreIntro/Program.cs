WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

//app.MapGet("/", () => "Hello World!");

// after you have a WebApplication, you need to construct its
// request-processing pipeline using components called "middlewares".

// this call adds a middleware to the pipeline.
// each middleware runs in sequence.
app.Run(context =>
{
    // the HttpContext parameter (context) gives access to all the request data
    // and lets you modify all the response data.
    context.Response.StatusCode = 204;

    // need this line so it compiles
    // (really this delegate should be async but we haven't done that yet)
    return Task.CompletedTask;
});


// this call runs the app
app.Run();
