using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VolumeWeb.Models;

namespace VolumeWeb.Data
{
    public class CalculationService
    {
        private readonly HttpClient _httpClient;
        
        public CalculationService(HttpClient client)
        {
            _httpClient = client;
        }
        
        // GET METHODS
        public async Task<IList<VolumeResult>> GetAllCalculationsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/calculate");
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            
            string responseContent = await response.Content.ReadAsStringAsync();
            
            List<VolumeResult> allVolumes = JsonSerializer.Deserialize<List<VolumeResult>>(responseContent, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            return allVolumes;
        }
        
        
        
        // POST METHODS
        public async Task RequestNewCalculationAsync(VolumeResult newVolume, string selectedType)
        {
            var jsonVolume = new StringContent(
                JsonSerializer.Serialize(newVolume, typeof(VolumeResult), new JsonSerializerOptions(JsonSerializerDefaults.Web)), Encoding.UTF8, "application/json");

            using var httpResponse = await _httpClient.PostAsync($"/calculate/{selectedType}?height={newVolume.Height}&radius={newVolume.Radius}", jsonVolume);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
            }
        }
        
    }
}