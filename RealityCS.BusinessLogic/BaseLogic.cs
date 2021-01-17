using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealityCS.BusinessLogic
{
    public abstract class BaseLogic
    {
        /// <summary>
        /// Returns cange set of a table row when update an entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public string GetChangeLog<TEntity>(DbContext dbContext) where TEntity : class
        {

            var change = dbContext.ChangeTracker.Entries<TEntity>().First();

            var changedlist = change.Properties.Where(x => !x.IsTemporary && x.IsModified && !Convert.ToString(x.OriginalValue)
                            .Equals(Convert.ToString(x.CurrentValue)))
                            .Select(x => new { Field = x.Metadata.Name, x.OriginalValue, x.CurrentValue })
                            .ToList();

            var changejson = JsonConvert.SerializeObject(changedlist).ToString();



            return changejson;
        }
    }

    public static class LinqExtention
    {
        //https://stackoverflow.com/a/5644623/4336330
        public static IEnumerable<TEntity> DynamicFieldQuery<TEntity>(this IEnumerable<TEntity> source, string columnName, object propertyValue)
        {
            
            if (!string.IsNullOrEmpty(columnName) && source.First().GetType().GetProperty(columnName) != null)            {

                var propertyinfo = source.First().GetType().GetProperty(columnName);
                var type = propertyinfo.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    type = type.GetGenericArguments()[0];
                }

                IEnumerable<TEntity> result = null;

                switch (type.Name)
                {
                    case nameof(String):
                        result = source.Where(m => { return Convert.ToString(m.GetType().GetProperty(columnName).GetValue(m, null)).Contains(Convert.ToString(propertyValue)); });
                        break;
                    case nameof(Int32):
                        result = source.Where(m => { return Convert.ToInt32(m.GetType().GetProperty(columnName).GetValue(m, null)) == Convert.ToInt32(propertyValue); });
                        break;
                    case nameof(DateTime):
                        result = source.Where(m =>
                        {
                            return Convert.ToDateTime(m.GetType().GetProperty(columnName).GetValue(m, null)) >= Convert.ToDateTime(propertyValue)
                                && Convert.ToDateTime(m.GetType().GetProperty(columnName).GetValue(m, null)) <= Convert.ToDateTime(propertyValue);
                        });
                        break;
                }
                return result;
            }
            return source;

            
        }
    }
}
