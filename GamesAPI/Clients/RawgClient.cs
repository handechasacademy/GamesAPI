namespace GamesAPI.Clients
{
    public class RawgClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public RawgClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["Rawg:ApiKey"]!;
            _httpClient.BaseAddress = new Uri("https://api.rawg.io/api/");
        }

        public async Task<string> GetGameDetailsAsync(string gameName)
        {
            var response = await _httpClient.GetAsync($"games?key={_apiKey}&search={gameName}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}