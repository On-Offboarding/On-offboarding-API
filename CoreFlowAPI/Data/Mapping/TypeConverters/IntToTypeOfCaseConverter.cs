using AutoMapper;
using CoreFlowSharedLibrary.Enums;

namespace CoreFlowAPI.Data.Mapping.TypeConverters
{
    public class IntToTypeOfCaseConverter : ITypeConverter<int, TypeOfCase>
    {
        public IntToTypeOfCaseConverter() { }
        
        public TypeOfCase Convert(int source, TypeOfCase destination, ResolutionContext context)
        {
            return Enum.IsDefined(typeof(TypeOfCase), source)
            ? (TypeOfCase)source
            : TypeOfCase.Unknown;
        }
    }
}
