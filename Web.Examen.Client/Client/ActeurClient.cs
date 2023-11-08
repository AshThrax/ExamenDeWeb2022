
using Auth0.ManagementApi.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Web.Examen.Client.Models;
using Web.Examen.Client.Models.Acteur;

namespace Web.Examen.Client.Client
{
    public interface IActeurClient
    {
        Task<IEnumerable<ActeurViewModel>> GetallAsync(int filmid);
        Task<ActeurViewModel> GetAsync(int filmid,int acteurid);
        void PostAsync(CreateActeur model);
        void PutAsync(int id,SaveActeur model);
        void DeleteAsync(int filmid, int acteurid);

    }
    public class ActeurClient : IActeurClient
    {
        private readonly HttpClient ActClient;
        private const string adress = "https://localhost:7189/api/Acteur";

        public ActeurClient(HttpClient client)
        {
            ActClient = client;
            ActClient.Timeout = TimeSpan.FromMinutes(2);
            ActClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ActClient.BaseAddress = new Uri("https://localhost:7189/api/Acteur");
            ActClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            ActClient.DefaultRequestHeaders.Add("User-Agent", "C# App");
        }

        public async void DeleteAsync(int Filmid,int acteurid)
        {
           
            await ActClient.DeleteAsync($"{adress}/{Filmid}/{acteurid}");
          
        }

        public async Task<IEnumerable<ActeurViewModel>> GetallAsync(int filmID)
        {
            IEnumerable<ActeurViewModel> movies;

           
            var Response = await ActClient.GetAsync($"{adress}/{filmID}");
            if (!Response.IsSuccessStatusCode)
            {
                throw new Exception("cannot retrieve Data");
            }
            var content = await Response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<IEnumerable<ActeurViewModel>>(content);

            return task;
        }
        public async Task<ActeurViewModel> GetAsync(int filmid,int acteurID )
        {
            
            var Response = await ActClient.GetAsync($"{adress}/{filmid}/{acteurID}");
            if (!Response.IsSuccessStatusCode)
            {
                throw new Exception("cannot retrieve Data");
            }
            var content = await Response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<ActeurViewModel>(content);
            return task;
        }

        public async void PostAsync(CreateActeur model)
        {
           
            var json = JsonConvert.SerializeObject(model);
            var httpResponse = await ActClient.PostAsync(adress, new StringContent(json, Encoding.Default, "application/json"));
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("cannot add the data you asked");
            }
                      
        }

        public async void PutAsync(int id,SaveActeur model)
        {
         
            var json = JsonConvert.SerializeObject(model);
            var httpResponse = await ActClient.PutAsync($"{adress}/{id}", new StringContent(json, Encoding.Default, "application/json"));
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("cannot add the data you asked");
            }
          

           
        }
        
        
        
    }
}
