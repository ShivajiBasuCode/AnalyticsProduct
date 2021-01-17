using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealityCS.SharedMethods.Extensions
{
    public static class RealitycsCommonExtensions
    {
        public static void TryParse(this string value,out string type)
        {
            try
            {
                if(string.IsNullOrEmpty(value))
                {
                    type = null;
                    return;
                }
                if(Boolean.TryParse(value,out Boolean bType))
                {
                    type = nameof(Boolean);
                    return;
                }
                if (Int64.TryParse(value, out Int64 iType))
                {
                    type = nameof(Int64);
                    return;
                }
                if (Decimal.TryParse(value, out Decimal deType))
                {
                    type = nameof(Decimal);
                    return;
                }
                if (DateTime.TryParse(value, out DateTime dType))
                {
                    type = nameof(DateTime);
                    return;
                }
                type = nameof(String);
                return;
            }
            catch(Exception ex)
            {
                type = nameof(String);
                return;
            }
        }
        public static void DataType(this List<string> value, out string type)
        {
            try
            {
                if (value.Count == 1)
                {
                    type = value.FirstOrDefault();
                    return;
                }
                if (value.Where(x => x == nameof(String)).ToList().Count > 0)
                {
                    type = nameof(String);
                    return;
                }
                if (value.Where(x => x == nameof(Decimal)).ToList().Count > 0
                    && value.Where(x => x == nameof(Boolean)).ToList().Count == 0
                    && value.Where(x => x == nameof(DateTime)).ToList().Count == 0)
                {
                    type = nameof(Decimal);
                    return;
                }
                type = null;
                return;

            }
            catch (Exception ex)
            {
                type = nameof(String);
                return;
            }
        }
    }
}
