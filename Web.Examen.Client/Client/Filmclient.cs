
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Web.Examen.Client.Models;
using Web.Examen.Client.Models.Film;

namespace Web.Examen.Client.Client
{
    public interface IFilmClient
    {
        Task<IList<FilmViewModel>> GetallAsync();
        Task<FilmViewModel> GetAsync(int id);
        void PostAsync(CreateFilm model);
        void PutAsync( int id ,SaveFilm model);
        void DeleteAsync(int id);
        Task<PagedList<FilmViewModel>> GetAllPageAsync(GetQueryParameter query);
    }
    public class Filmclient : IFilmClient
    {
        private readonly HttpClient Client;
        private const string address = "https://localhost:7189/api/Film";
        public Filmclient(HttpClient client)
        {

            Client = client;
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.Timeout = TimeSpan.FromMinutes(2);
            Client.DefaultRequestHeaders.Accept.Add(
              new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            Client.DefaultRequestHeaders.Add("User-Agent", "C# App");

        }

        public async void DeleteAsync(int id)
        {
           
            await Client.DeleteAsync($"{address}/{id}");

        }

        public async Task<IList<FilmViewModel>> GetallAsync()
        {
            var Response = await Client.GetAsync(address);
            if (!Response.IsSuccessStatusCode)
            {
                throw new Exception("cannot retrieve Data");
            }
            var content = await Response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<IList<FilmViewModel>>(content);

            return task;
        }
        public async Task<FilmViewModel> GetAsync(int id)
        {
            
            var Response = await Client.GetAsync($"{address}/{id}");
            if (!Response.IsSuccessStatusCode)
            {
                throw new Exception("cannot retrieve Data");
            }
            var content = await Response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<FilmViewModel>(content);
            return task;
        }

        public async void PostAsync(CreateFilm model)
        {
           
            var json = JsonConvert.SerializeObject(model);
            var httpResponse = await Client.PostAsync(address,new StringContent(json,Encoding.Default,"application/json"));
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("cannot add the data you asked");
            }

        }

        public async void PutAsync(int id ,SaveFilm model)
        {
            
            var json = JsonConvert.SerializeObject(model);
            var httpResponse = await Client.PutAsync($"{address}/{id}", new StringContent(json, Encoding.Default, "application/json"));
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("cannot add the data you asked");
            }
            var PutTask = JsonConvert.DeserializeObject<FilmViewModel>(await httpResponse.Content.ReadAsStringAsync());
         

        }
        //paging get tstj'
        public async Task<PagedList<FilmViewModel>> GetAllPageAsync(GetQueryParameter query)
        {
            var Response = await Client.GetAsync($"{address}");
            
            if (!Response.IsSuccessStatusCode)
            {
                throw new Exception("cannot retrieve Data");
            }
            var content = await Response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<PagedList<FilmViewModel>>(content);
            return task;
        }
    }
}
