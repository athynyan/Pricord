using System.Net.Http.Json;
using Pricord.Contracts.BattleRecords;
using Pricord.Contracts.Common.Constants;

namespace Pricord.Web.Features.BattleRecords.Services;

public sealed class BattleRecordService : IBattleRecordService
{
    private readonly HttpClient _httpClient;

    public BattleRecordService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<BattleRecordResponse>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync(ApiEndpoints.BattleRecordEndpoint);

        return await response.Content.ReadFromJsonAsync<IEnumerable<BattleRecordResponse>>() 
            ?? Enumerable.Empty<BattleRecordResponse>();
    }

    public async Task<BattleRecordDetailsResponse?> GetBattleRecordDetailsAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"{ApiEndpoints.BattleRecordEndpoint}/{id}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<BattleRecordDetailsResponse>();
        }

        return null;
    }
}
