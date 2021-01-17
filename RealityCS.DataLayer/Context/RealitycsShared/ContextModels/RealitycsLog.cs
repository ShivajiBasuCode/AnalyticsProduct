using RealityCS.DataLayer.Context.RealitycsClient;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsShared.ContextModels
{
    public class RealitycsLog:RealitycsSharedBase
    {
        public EnumerationLogLevelType LogLevel { get; set; }

        public string ShortMessage { get; set; }

        public string FullMessage { get; set; }

        public string IpAddress { get; set; }

        public int UserId { get; set; }

        public string EndPoint { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public string RequestParameters { get; set; }

    }
}
