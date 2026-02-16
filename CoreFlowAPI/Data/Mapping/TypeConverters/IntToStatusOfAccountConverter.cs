using AutoMapper;
using CoreFlowSharedLibrary.Enums;

namespace CoreFlowAPI.Data.Mapping.TypeConverters
{
    public class IntToStatusOfAccountConverter : ITypeConverter<int, StatusOfAccount>
    {
        public StatusOfAccount Convert(int source, StatusOfAccount destination, ResolutionContext context)
        {
            return Enum.IsDefined(typeof(StatusOfAccount), source)
                ? (StatusOfAccount)source
                : StatusOfAccount.Unknown;
        }
    }
}
