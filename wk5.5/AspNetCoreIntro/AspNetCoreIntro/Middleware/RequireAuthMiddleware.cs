namespace AspNetCoreIntro.Middleware
{

    public class RequireAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public RequireAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Query["authenticated"] == "true")
            {
                // "this middleware is done, let the next one in the pipeline take over"
                await _next(context);
            }
            else
            {
                // if we don't invoke "next", this middleware is "short-circuiting" the pipeline -
                // it better finish setting up the response.

                context.Response.StatusCode = 401;
                context.Response.ContentType = "text/plain";
                //await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes("error, not authenticated"));
                await context.Response.WriteAsync("error, not authenticated");
                //context.Response.Body
            }
        }
    }
}
