using Api.ApiModel;
using Newtonsoft.Json;

namespace UI.Client
{
    public interface IFilmClient
    {
        Task<FilmVm> GetallAsync();
        Task<FilmDto> GetAsync(int id);
        Task<FilmDto> PostAsync (FilmDto model);
        Task<FilmDto> PutAsync (FilmDto model, int id);
        void DeleteAsync (int id);

    }
    public class Filmclient :  IFilmClient
    {
        private readonly HttpClient Client;
        private const string address = "https://localhost:7299/api/Film/";
        public Filmclient(HttpClient client)
        {
           
            Client = client;
        }

        public async void DeleteAsync(int id)
        {
            var url = id;
            await Client.DeleteAsync($"{id}");
           
        }

        public async Task<FilmVm> GetallAsync()
        {
            

            string url = "get";
           var Response = await Client.GetAsync(address);
            if (!Response.IsSuccessStatusCode)
            {
                throw new Exception("cannot retrieve Data");
            }
            var content =await Response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<FilmVm>(content);

            return task;
        }
        public async Task<FilmDto> GetAsync(int id)
        {
            string url = "Film/get";
            var Response = await Client.GetAsync($"{address}{id}");
            if (!Response.IsSuccessStatusCode)
            {
                throw new Exception("cannot retrieve Data");
            }
            var content = await Response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<FilmDto>(content);
            return task;
        }

        public async Task<FilmDto> PostAsync(FilmDto model)
        {
                string url = "Film/get";
                var json = JsonConvert.SerializeObject(model);
                var httpResponse =await Client.PostAsJsonAsync(address, json);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("cannot add the data you asked");
                }
                var PostTask = JsonConvert.DeserializeObject<FilmDto>(await httpResponse.Content.ReadAsStringAsync());
                return PostTask;
        }

        public async Task<FilmDto>  PutAsync(FilmDto model, int id)
        {
                string url = "Film/put"+id;
                var json =JsonConvert.SerializeObject(model);
                var httpResponse =await Client.PutAsJsonAsync($"{address}{id}", json);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("cannot add the data you asked");
                }
                var PutTask =JsonConvert.DeserializeObject<FilmDto>(await httpResponse.Content.ReadAsStringAsync());
                return PutTask;

        }
    }
}
