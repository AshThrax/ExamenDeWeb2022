using Api.ApiModel;
using MediatR;
using Newtonsoft.Json;

namespace Api.Client
{
    public interface IActeurClient
    {
        Task<IEnumerable<ActeurDto>> GetallAsync();
        Task<ActeurDto> GetAsync(int id);
        Task<Unit> PostAsync(ActeurDto model);
        Task<Unit> PutAsync(ActeurDto model, int id);
        Task<Unit> DeleteAsync(int id);

    }
    public class ActeurClient : IActeurClient
    {
        private readonly HttpClient Client;

        public ActeurClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://localhost:7299/api/Acteur");
            Client = client;
        }

        public async Task<Unit> DeleteAsync(int id)
        {
            string url = "Film/delete" + id;
            await Client.DeleteAsync(url);
            return Unit.Value;
        }

        public async Task<IEnumerable<ActeurDto>> GetallAsync()
        {
            IEnumerable<ActeurDto> movies;

            string url = "Film/get";
            var Response = await Client.GetAsync(url);
            if (!Response.IsSuccessStatusCode)
            {
                throw new Exception("cannot retrieve Data");
            }
            var content = await Response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<IEnumerable<ActeurDto>>(content);

            return task;
        }
        public async Task<ActeurDto> GetAsync(int id)
        {
            string url = "Film/get";
            var Response = await Client.GetAsync(url);
            if (!Response.IsSuccessStatusCode)
            {
                throw new Exception("cannot retrieve Data");
            }
            var content = await Response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<ActeurDto>(content);
            return task;
        }

        public async Task<Unit> PostAsync(ActeurDto model)
        {
            string url = "Film/get";
            var json = JsonConvert.SerializeObject(model);
            var httpResponse = await Client.PostAsJsonAsync(url, json);
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("cannot add the data you asked");
            }
            var PostTask = JsonConvert.DeserializeObject<ActeurDto>(await httpResponse.Content.ReadAsStringAsync());
            return Unit.Value;
        }

        public async Task<Unit> PutAsync(ActeurDto model, int id)
        {
            string url = "Film/put" + id;
            var json = JsonConvert.SerializeObject(model);
            var httpResponse = await Client.PutAsJsonAsync(url, json);
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("cannot add the data you asked");
            }
            
            return Unit.Value;

        }
    }
}
