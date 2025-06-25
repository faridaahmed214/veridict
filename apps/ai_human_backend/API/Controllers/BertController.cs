using DomanLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BertController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpointUrl;

        public BertController(IHttpClientFactory httpClientFactory, IOptions<ModelSettings> modelSettings)
        {
            _httpClient = httpClientFactory.CreateClient();
            _endpointUrl = modelSettings.Value.EndpointUrl;
        }

        [HttpPost("predict")]
        public async Task<IActionResult> Predict([FromBody] InputText input)
        {
            var response = await _httpClient.PostAsJsonAsync(_endpointUrl, new { text = input.Text });

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, "Error calling model API");

            var result = await response.Content.ReadFromJsonAsync<PredictionResponse>();
            return Ok(result);
        }
    }
}