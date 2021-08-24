using System.Linq;

namespace MonitoringStations.Common.ExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static string RemoveChars(this string str, char[] prm)
        {
            return str.Where(it => prm.All(x => x != it)).Aggregate(string.Empty, (current, it) => current + it);
        }
    }
}
