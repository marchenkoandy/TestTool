using System;
using System.Configuration;

namespace ToolsQA.Utilities
{
    public static class TestConfiguration
    {
        public static T Get<T>(string name)
        {
            var value = ConfigurationManager.AppSettings[name];            
            if (typeof(T).IsEnum)
                return (T)Enum.Parse(typeof(T), value);
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
