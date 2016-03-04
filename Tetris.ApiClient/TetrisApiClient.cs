namespace Tetris.ApiClient
{
    using System.Net.Http;

    using Newtonsoft.Json;

    public class TetrisApiClient
    {
        private readonly HttpClient httpClient = new HttpClient();

        public TetrisApiClient()
        {
            this.httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json'");
        }

        public bool PostStats(object a)
        {
            var b = this.httpClient.PostAsync("", new StringContent(JsonConvert.SerializeObject(a))).Result;
            return b.IsSuccessStatusCode;
        }
    }
}
