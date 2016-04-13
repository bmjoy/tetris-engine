namespace Tetris.ApiClient
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using Tetris.ApiClient.Entities;
    using Tetris.ApiClient.Interfaces;

    public class TetrisApiClient
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly JsonSerializerSettings settings = new JsonSerializerSettings
        {
            DateParseHandling = DateParseHandling.DateTimeOffset,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore
        };

        public TetrisApiClient(Uri baseUri)
        {
            this.httpClient.BaseAddress = baseUri;
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public bool PostStats<T>(ITetrisGameResult<T> gameResult)
        {
            var result = this.httpClient.PostAsync($"/evolutions/{gameResult.AlgorithmId}/result", this.ConverToContent(gameResult)).Result;
            return result.IsSuccessStatusCode;
        }

        public TetrisAlgorithmSetting<TWeights> GetAlgorithmSettings<TAlgorithm, TWeights>(ITetrisAlgorithmT<TWeights> algorithmT) where TWeights : class, new()
        {
            var tetrisAlgorithm = this.GetAlgorithm<TAlgorithm>();
            if (tetrisAlgorithm == null)
            {
                return this.CreateAlgorithm(new TetrisAlgorithmT<TWeights>
                    {
                        Name = typeof(TAlgorithm).Name,
                        Weights = this.GetInstance<TWeights>()
                    });
            }

            var result = this.httpClient.GetStringAsync($"/evolutions/{tetrisAlgorithm.AlgorithmId}/settings").Result;
            return JsonConvert.DeserializeObject<TetrisAlgorithmSetting<TWeights>>(result);
        }

        protected TetrisAlgorithmSetting<TWeights> CreateAlgorithm<TWeights>(ITetrisAlgorithmT<TWeights> algorithmT)
        {
            var result = this.httpClient.PostAsync("/evolutions", this.ConverToContent(algorithmT)).Result;
            return JsonConvert.DeserializeObject<TetrisAlgorithmSetting<TWeights>>(result.Content.ReadAsStringAsync().Result);
        }

        protected TetrisAlgorithm[] GetAlgorithms()
        {
            var result = this.httpClient.GetStringAsync("/evolutions").Result;
            return JsonConvert.DeserializeObject<TetrisAlgorithm[]>(result);
        }

        private ITetrisAlgorithm GetAlgorithm<TAlgorithm>()
        {
            var name = typeof(TAlgorithm).Name;
            return this.GetAlgorithms().FirstOrDefault(x => name == x.Name);
        }

        private StringContent ConverToContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj, this.settings), Encoding.UTF8, "application/json");
        }

        private T GetInstance<T>() where T : class, new()
        {
            var t = typeof(T);

            return (T)Activator.CreateInstance(t);
        }
    }
}
