using AutoMapper;
using CoreFlowSharedLibrary.Enums;

namespace CoreFlowAPI.Data.Mapping.TypeConverters
{
    public class StringToTitleOfEmployeeConverter : ITypeConverter<string, TitleOfEmployee>
    {
        public TitleOfEmployee Convert(string source, TitleOfEmployee destination, ResolutionContext context)
        {
            return Enum.IsDefined(typeof(TitleOfEmployee), source)
            ? Enum.Parse<TitleOfEmployee>(source)
            : TitleOfEmployee.Unknown;
        }
    }
}
