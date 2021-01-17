using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.SharedMethods
{
    public class RealitycsException: Exception
    {
        public RealitycsException()
        {

        }
        public RealitycsException(string message):base(message)
        {

        }
        public RealitycsException(string messageFormat,params object[] args): base(string.Format(messageFormat,args))
        {

        }
        public RealitycsException(string message,Exception innerException):base(message,innerException)
        {

        }
    }
}
