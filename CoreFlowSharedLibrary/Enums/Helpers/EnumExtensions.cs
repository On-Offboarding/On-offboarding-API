using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CoreFlowSharedLibrary.Enums.Helpers
{
    public static class EnumExtensions
    {
        public static string GetDescription(this AuditAction value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttribute<DisplayAttribute>();

            return attr?.GetDescription() ?? value.ToString();
        }
        public static string GetDisplayName(this AuditAction value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttribute<DisplayAttribute>();

            return attr?.GetName() ?? value.ToString();
        }
    }
}
