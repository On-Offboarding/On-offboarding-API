using AutoMapper;
using CoreFlowSharedLibrary.Enums;

namespace CoreFlowAPI.Data.Mapping.TypeConverters
{
    public class IntToStatusOfCaseConverter : ITypeConverter<int, StatusOfCase>
    {
        public StatusOfCase Convert(int source, StatusOfCase destination, ResolutionContext context)
        {
            return Enum.IsDefined(typeof(StatusOfCase), source)
            ? (StatusOfCase)source
            : StatusOfCase.None;
        }
    }
}
