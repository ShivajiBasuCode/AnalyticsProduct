using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels
{
    public enum EnumerationBasicUserGroup
    {
        [Description("System Admin User(s)")]
        SystemAdminUsers = 1,

        [Description("Admin User(s)")]
        AdminUsers = 2,
    }

    public class EnumerationEnumerationBasicUserGroupTable : RealitycsEnumTable<EnumerationBasicUserGroup>
    {
        public EnumerationEnumerationBasicUserGroupTable(EnumerationBasicUserGroup enumClass) : base(enumClass)
        {

        }
        public EnumerationEnumerationBasicUserGroupTable() : base()
        {

        }
    }
}
