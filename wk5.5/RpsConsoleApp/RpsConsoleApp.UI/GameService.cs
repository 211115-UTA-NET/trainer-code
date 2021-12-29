using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using RpsConsoleApp.UI.Dtos;
using RpsConsoleApp.UI.Exceptions;

namespace RpsConsoleApp.UI
{
    /// <summary>
    /// Encapsulate access to the game server, getting round history, submitting new moves, etc
    /// </summary>
    public class GameService : IGameService
    {
        private readonly HttpClient _httpClient = new();

        // e.g. "https://localhost:7175"
        public GameService(Uri serverUri)
        {
            // add some default headers for any HttpRequestMessage going through this HttpClient
            //s_httpClient.DefaultRequestHeaders.Accept.Add(new(MediaTypeNames.Application.Json));
            // you can give the httpclient a default base URL.
            // then the URL you give it for each individual request can be "relative URLs"
            _httpClient.BaseAddress = serverUri;
        }

        public async Task<List<Round>> GetRoundsOfPlayerAsync(string name)
        {
            // if you want to add any headers to the request, then can't use the HttpClient.GetAsync, PostAsync, PutAsync, etc.
            //    much less these GetFromJsonAsync extension methods.
            // instead, you need to construct a HttpRequestMessage, then call HttpClient.SendAsync.

            // even more restful would be to build this URL using some other response's hypermedia values
            // e.g.: starting point would be GET/POST a player, that response would have
            //    hypermedia links to... get all the rounds of that player, add a new round for that player
            // (HATEOAS) - effort into HATEOAS is not really required for P1 but it would be cool

            // better, more secure way to insert a string into a url's query string
            // with appropriate URL-encoding
            Dictionary<string, string> query = new() { ["player"] = name };
            string requestUri = QueryHelpers.AddQueryString("/api/rounds", query);

            HttpRequestMessage request = new(HttpMethod.Get, requestUri);
            // telling the server we expect application/json reply. ("content negotiation" in http/rest)
            request.Headers.Accept.Add(new(MediaTypeNames.Application.Json));

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                throw new UnexpectedServerBehaviorException("network error", ex);
            }

            response.EnsureSuccessStatusCode(); // throw if the status code is not 2xx
            //if (response.StatusCode == 401)
            //{
            //    throw new UnauthorizedException();
            //}

            if (response.Content.Headers.ContentType?.MediaType != MediaTypeNames.Application.Json)
            {
                throw new UnexpectedServerBehaviorException();
            }

            var rounds = await response.Content.ReadFromJsonAsync<List<Round>>();
            if (rounds == null)
            {
                throw new UnexpectedServerBehaviorException();
            }

            return rounds;
        }

        // WIP
        public async Task<string> SubmitMoveAsync(string playerName, string move)
        {
            SubmittedMove theMove = new()
            {
                PlayerName = playerName,
                Move = move
            };

            HttpRequestMessage request = new(HttpMethod.Post, "/api/rounds");
            request.Content = new StringContent(JsonSerializer.Serialize(theMove),
                Encoding.UTF8, MediaTypeNames.Application.Json);

            //could use _httpClient.PostAsync instead, would be a little more succinct

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                throw new UnexpectedServerBehaviorException("network error", ex);
            }

            var result = await response.Content.ReadFromJsonAsync<string>();
            return result!;
        }
    }
}
