using Auth0.ManagementApi.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Web.Examen.Client.Models.RePosts;

namespace Web.Examen.Client.Client
{
    public interface IRepostClient
    {
        Task<List<RePost>> GetAll();
        Task<RePost> GetById(int id);
        void Create(CreateRepost post);
        void Update(int repostid,UpdateRepost post);
        void Delete(int id);
    }
    public class RePostClient : IRepostClient
    {
        private readonly HttpClient _myRepostClient;
        private const string address = "https://localhost:7189/api/RePost/";
        public RePostClient(HttpClient client)
        {
            _myRepostClient = client;
            _myRepostClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _myRepostClient.Timeout = TimeSpan.FromMinutes(2);
            _myRepostClient.DefaultRequestHeaders.Accept.Add(
              new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            _myRepostClient.DefaultRequestHeaders.Add("User-Agent", "C# App");
        }

        public async void Create(CreateRepost post)
        {
           var Content =JsonConvert.SerializeObject(post);
            var httpResponse = await _myRepostClient.PostAsync($"{address}", new StringContent(Content,Encoding.Default ,"application/json"));
            if (!httpResponse.IsSuccessStatusCode)
            { 
                throw new Exception("Cannot Add The repost");
            }
            
        }

        public async void Delete(int id)
        {
           
            var httpResponse = await _myRepostClient.DeleteAsync($"{address}{id}");
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot Add The repost");
            }
        }

        public async Task<List<RePost>> GetAll()
        {
          var httpResponse = await _myRepostClient.GetAsync(address);
            if (httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve All the ");
            }
            var content = await httpResponse.Content.ReadAsStringAsync();
            var GetTask = JsonConvert.DeserializeObject<List<RePost>>(content);
            return GetTask;
        }

        public async Task<RePost> GetById(int id)
        {
            var httpResponse = await _myRepostClient.GetAsync($"{address}/{id}");
            if (httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve All the ");
            }
            var content = await httpResponse.Content.ReadAsStringAsync();
            var GetTask = JsonConvert.DeserializeObject<RePost>(content);
            return GetTask;
        }

        public async void Update(int repostid,UpdateRepost post)
        {
            var Content = JsonConvert.SerializeObject(post);
            var httpResponse = await _myRepostClient.PostAsync($"{address}{repostid}", new StringContent(Content, Encoding.Default, "application/json"));
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot Add The repost");
            }
        }
    }
}
