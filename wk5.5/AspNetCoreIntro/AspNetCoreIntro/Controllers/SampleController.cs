using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIntro.Controllers
{
    // asp.net is using reflection to discover this class automatically
    // based on its name ("_____Controller")

    public class SampleController
    {
        // a controller's job is to handle a subset of requests to the server
        // based on url path and the http method

        // each category of request with the same things to do
        // will be one method in this class.

        // static so it remembers between requests
        // ...because a new controller instance is created for every request
        private static readonly List<int> s_samples = new() { 12 };

        // [Route] works for all http methods(verbs)
        // [HttpGet], [HttpPost], etc - to also limit it to a specific method.

        // the job of an action method is to handle one set of requests
        // and return some "result" which asp.net will turn into the response
        [HttpGet("/samples")]
        [HttpGet("/sample")] // c# supports multiple attributes (same or different type)
        public ContentResult GetSamples()
        {
            // asp.net provides a bunch of data types under the IActionResult interface
            // and/or the ActionResult abstract base class.
            // the job of an action result is to deserialize itself into an http response.
            // a basic one we can start with is ContentResult.
            // good for when you have something to put in the response body.
            // (otherwise... maybe StatusCodeResult)

            string json = JsonSerializer.Serialize(s_samples);

            var result = new ContentResult()
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = json
            };

            return result;
        }

        // "model binding" runs before the action method:
        //   - looks at the action method parameters, tries to fill them in
        //     by deserializing parts of the request, based on the parameter name and type.
        //   - [From___] attributes can influence/tell model binding what to do too
        //   - if model binding doesn't find anything in the request... it'll be left at default (not exception)
        //       so it's important to validate those parameters. (user input)
        [HttpPost("/samples")]
        public ContentResult AddSample([FromBody] int sample)
        {
            // this is bad... List is NOT threadsafe. (use ConcurrentList iirc)
            s_samples.Add(sample);
            string json = JsonSerializer.Serialize(sample);

            // good practice with POST, return a representation of the created resource in the body.
            return new ContentResult()
            {
                StatusCode = StatusCodes.Status201Created,
                ContentType = "application/json",
                Content = json
            };
        }
    }
}
