using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

using Web.Examen.Client.Models.Posts;

namespace Web.Examen.Client.Client
{
    public interface IPostClient
    {
        Task<IList<Post>> GetAllAsync();
        Task<Post> GetPost(int Id);
        void CreatePost(CreatePost post);
        void UpdatePost(int postid,UpdatePost model);
        void DeletePost(int id);
       
    }
    public class PostClient : IPostClient
    {
        private readonly HttpClient MyPostClient;
        private const string address = "https://localhost:7189/api/Post/";

        public PostClient(HttpClient client)
        {
            MyPostClient = client;
            MyPostClient.Timeout = TimeSpan.FromMinutes(5);
            MyPostClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            MyPostClient.DefaultRequestHeaders.Accept.Add(
              new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            MyPostClient.DefaultRequestHeaders
                .Add("User-Agent", "C# App");
        }

        public async void CreatePost(CreatePost post)
        {
           var content =JsonConvert.SerializeObject(post);
            var httpResponse = await MyPostClient.PostAsync(address, new StringContent(content,Encoding.Default,"application/json"));
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot add the Post ");
            }    
            
        }

        public async void DeletePost(int id)
        {
            var httpResponse= await MyPostClient.DeleteAsync($"{address}/{id}");
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot delete the Post");
            }
        }

        public async Task<IList<Post>> GetAllAsync()
        {

            var Response = await MyPostClient.GetAsync($"{address}");
            if (!Response.IsSuccessStatusCode)
            {
                throw new Exception("cannot retrieve the Post ");
            }
            var content = await Response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<IList<Post>>(content);

            return task;
        }

        public async Task<Post> GetPost(int Id)
        {
            var Response = await MyPostClient.GetAsync($"{address}/{Id}");
            if (!Response.IsSuccessStatusCode)
            {
                throw new Exception("cannot retrieve Post");
            }
            var content = await Response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<Post>(content);

            return task;
        }

        public async void UpdatePost(int postid ,UpdatePost post)
        {
            var content = JsonConvert.SerializeObject(post);
            var httpResponse = await MyPostClient.PutAsync($"{address}/{postid}", new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse != null)
            {
                throw new Exception("Cannot Update the Post ");
            }
        }
    }
}
