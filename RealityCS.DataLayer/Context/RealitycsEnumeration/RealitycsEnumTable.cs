using RealityCS.DataLayer.Context.RealitycsClient;
using System;
using System.Collections.Generic;
using System.Text;
using RealityCS.SharedMethods.Extensions;
namespace RealityCS.DataLayer.Context.RealitycsEnumeration
{
    public class RealitycsEnumTable<TEnum>:RealitycsEnumerationBase where TEnum : Enum
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public RealitycsEnumTable()
        {

        }
        public RealitycsEnumTable(TEnum enumClass)
        {
            PK_Id = Convert.ToInt32(enumClass);
            Description = enumClass.GetDescription();
            Name = enumClass.ToString();
        }

        public static implicit operator RealitycsEnumTable<TEnum>(TEnum enumClass)=>new RealitycsEnumTable<TEnum>(enumClass);
    }
}
