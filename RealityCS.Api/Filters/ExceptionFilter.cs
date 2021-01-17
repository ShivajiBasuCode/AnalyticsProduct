using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RealityCS.Api.Filters
{
    //https://www.tutorialsteacher.com/csharp/csharp-exception
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            var exType = ex.GetType();

            switch (exType.Name)
            {
                case nameof(ArgumentException):
                    break;
                case nameof(ArgumentNullException):
                    break;
                case nameof(ArgumentOutOfRangeException):
                    break;
                case nameof(AmbiguousMatchException):
                    break;
                case nameof(DbUpdateException):
                    break;
                case nameof(DbUpdateConcurrencyException):
                    break;
                case nameof(DivideByZeroException):
                    break;
                case nameof(FileNotFoundException):
                    break;
                case nameof(FormatException):
                    break;
                case nameof(IndexOutOfRangeException):
                    break;
                case nameof(InvalidOperationException):
                    break;
                case nameof(KeyNotFoundException):
                    break;
                case nameof(NotSupportedException):
                    break;
                case nameof(NullReferenceException):
                    break;
                case nameof(OverflowException):
                    break;
                case nameof(OutOfMemoryException):
                    break;
                case nameof(StackOverflowException):
                    break;
                case nameof(SqlException):
                    break;
                case nameof(TimeoutException):
                    break;
                case nameof(InvalidCastException):
                    break;
            }



            base.OnException(context);
        }
    }
}
