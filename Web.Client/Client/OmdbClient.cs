using Api.ApiModel;
using Newtonsoft.Json.Linq;

namespace Client.Omdb_Client
{

    public interface IOmdbClient
    { 
        Task<FilmOMdb> GetAsync(string request);
    }
    public class OmdbClient:IOmdbClient
    {
        private readonly  HttpClient Client ;
        public OmdbClient(HttpClient client)
        {
            client.BaseAddress = new Uri("http://www.omdbapi.com/");
            Client = client;
        }   

        public async Task<FilmOMdb> GetAsync(string request)
        {   
            string url = $"?t={request}&apikey=4762bfe9";   
            var result = await Client.GetStringAsync(url);

            JObject json = JObject.Parse(result);

            if (json.SelectToken("Response").Value<string>() == "True")
            {

                var movieDetails = new FilmOMdb();

                movieDetails.Title = json.SelectToken("Title").Value<string>();
                movieDetails.Year = json.SelectToken("Year").Value<string>();
                movieDetails.Director = json.SelectToken("Director").Value<string>();
                movieDetails.Country = json.SelectToken("Country").Value<string>();
                movieDetails.Actors = json.SelectToken("Actors").Value<string>();
                movieDetails.ImdbRating = json.SelectToken("imdbRating").Value<string>();
                movieDetails.PosterImage = json.SelectToken("Poster").Value<string>();
                movieDetails.Plot = json.SelectToken("Plot").Value<string>();
                movieDetails.Metascore = json.SelectToken("Metascore").Value<string>();
                movieDetails.Rated = json.SelectToken("Rated").Value<string>();
                movieDetails.Genre = json.SelectToken("Genre").Value<string>();
                movieDetails.Released = json.SelectToken("Released").Value<string>();
                movieDetails.Writer = json.SelectToken("Writer").Value<string>();
              

                return movieDetails;
            }

            return new FilmOMdb()
            {
                Title = request
            };
        }
       
    }
  


}
