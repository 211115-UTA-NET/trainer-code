using System.Net.Mime;
using System.Xml.Serialization;

namespace AspNetCoreIntro.Middleware
{
    // in asp.net, quite a few things are conventions
    //  (there's an interface-based way to define middleware but more commonly it's done this way)
    // asp.net does a bunch of reflection to do things like "search this class for a
    // constructor that takes a RequestDelegate and use that"

    public class DataSerializeMiddleware
    {
        private static readonly XmlSerializer s_serializer = new(typeof(List<Data>));

        private readonly RequestDelegate _next;

        private readonly List<Data> _theData = new()
        {
            new Data { Number = 3, More = new() },
            new Data { Number = 2, More = new() },
            new Data { Number = 5 }
        };

        public DataSerializeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/data"))
            {
                context.Response.StatusCode = StatusCodes.Status200OK; // same as = 200, more verbose
                context.Response.ContentType = MediaTypeNames.Text.Xml; // same as = "text/xml", stops you from a typo

                // xmlserializer is not great and does not support async directly...
                // if it was important, i would need to refactor this code
                s_serializer.Serialize(stream: context.Response.Body, o: _theData);
            }
            else
            {
                // not my job, next middleware handle this one
                await _next(context);
            }
        }
    }
}
