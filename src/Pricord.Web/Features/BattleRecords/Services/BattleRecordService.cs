using Pricord.Application.Common.Constants;
using Pricord.Web.Features.BattleRecords.Contracts;

namespace Pricord.Web.Features.BattleRecords.Services;

public sealed class BattleRecordService : IBattleRecordService
{
    private readonly HttpClient _httpClient;

    public BattleRecordService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Pricord.Api");
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
