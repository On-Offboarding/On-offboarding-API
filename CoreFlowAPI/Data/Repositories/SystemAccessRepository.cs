using AutoMapper;
using CoreFlowAPI.Data.Interface;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Models;
using Dapper;

namespace CoreFlowAPI.Data.Repositories
{
    public class SystemAccessRepository : ISystemAccessRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public SystemAccessRepository(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SystemAccess>> GetAllAsync()
        {
            using var connection = _dbContext.CreateConnection();
            var list = await connection.QueryAsync<SystemAccess>(
                "select Id, Name from dbo.SystemAccesses");
            return _mapper.Map<IEnumerable<SystemAccess>>(list);
        }

        public async Task<IEnumerable<ProfileSystemAccessDTO>> GetAllProfilesAsync()
        {
            var sql = @"SELECT
                p.Id,
                p.Name,
                sa.Id,
                sa.Name
            FROM SystemAccessProfile p
            LEFT JOIN ProfileSystemAccess psa ON psa.ProfileId = p.Id
            LEFT JOIN SystemAccesses sa ON sa.Id = psa.SystemAccessId";

            var profileDictionary = new Dictionary<int, ProfileSystemAccessDTO>();
            using var connection = _dbContext.CreateConnection();
            await connection.QueryAsync<SystemAccessProfile, SystemAccess, ProfileSystemAccessDTO>(
               sql, (profile, systemAccess) =>
               {
                   if (!profileDictionary.TryGetValue(profile.Id, out var currentProfile))
                   {
                       currentProfile = _mapper.Map<ProfileSystemAccessDTO>(profile);

                       currentProfile.SystemAccesses = new List<SystemAccessDTO>();
                       profileDictionary.Add(profile.Id, currentProfile);

                   }

                   if (systemAccess != null && systemAccess.Id != 0)
                   {
                       currentProfile.SystemAccesses.Add(_mapper.Map<SystemAccessDTO>(systemAccess));
                   }

                   return currentProfile;
               },
                splitOn: "Id"
                    );
            return profileDictionary.Values.ToList();
        }
    }
}
