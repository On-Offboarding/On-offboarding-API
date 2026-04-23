using CoreFlowAPI.Business.Interface;

namespace CoreFlowAPI.Business.Helpers
{
    public interface ISystemAccessHelper
    {
        Task<List<string>> GetSystemNamesByIdsAsync(IEnumerable<int> systemAccessIds);
    }

    public class SystemAccessHelper : ISystemAccessHelper
    {
        private readonly ISystemAccessService _systemAccessService;
        private Dictionary<int, string>? _systemAccessCache;

        public SystemAccessHelper(ISystemAccessService systemAccessService)
        {
            _systemAccessService = systemAccessService;
        }

        public async Task<List<string>> GetSystemNamesByIdsAsync(IEnumerable<int> systemAccessIds)
        {
            // Hämta alla system första gången (cache)
            if (_systemAccessCache == null)
            {
                var allSystems = await _systemAccessService.GetAllAsync();
                _systemAccessCache = allSystems.ToDictionary(s => s.Id, s => s.Name);
            }

            // Konvertera ID till namn
            return systemAccessIds
                .Where(id => _systemAccessCache.ContainsKey(id))
                .Select(id => _systemAccessCache[id])
                .ToList();
        }
    }
}