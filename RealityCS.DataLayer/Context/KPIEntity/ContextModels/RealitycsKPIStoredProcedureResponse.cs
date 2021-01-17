using RealityCS.DataLayer.Context.GraphicalEntity.ContextModels;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace RealityCS.DataLayer.Context.KPIEntity.ContextModels
{
    public class RealitycsKPIStoredProcedureResponse : DynamicObject
    {
        public Dictionary<string, KeyValuePair<Type, object>> Fields { get; }

        public RealitycsKPIStoredProcedureResponse()
        {
            this.Fields = new Dictionary<string, KeyValuePair<Type, object>>();
        }


        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (this.Fields.ContainsKey(binder.Name))
            {
                var type = this.Fields[binder.Name].Key;
                if (value.GetType() == type)
                {
                    this.Fields[binder.Name] = new KeyValuePair<Type, object>(type, value);
                    return true;
                }
                else
                {
                    throw new Exception("value " + value + " is not of " + type + " type");
                }

            } 
            else
            {
                this.Fields[binder.Name] = new KeyValuePair<Type, object>(value.GetType(), value);
                return true;
            }
            return false;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = this.Fields[binder.Name].Value;
            return true;
        }
        public bool SetMember(string name, Object value)
        {
            if (this.Fields.ContainsKey(name))
            {
                var type = this.Fields[name].Key;
                if (value.GetType() == type)
                {
                    this.Fields[name] = new KeyValuePair<Type, object>(type, value);
                    return true;
                }
                else
                {
                    throw new Exception("value " + value + " is not of " + type + " type");
                }

            }
            else
            {
                this.Fields[name] = new KeyValuePair<Type, object>(value.GetType(), value);
                return true;
            }
            return false;
        }

        public object GetMember(string name)
        {
            return this.Fields[name].Value;
        }
    }
}
