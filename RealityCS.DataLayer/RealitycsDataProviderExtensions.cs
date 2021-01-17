using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace RealityCS.DataLayer
{
    public static class RealitycsDataProviderExtensions
    {
        #region Utilities

        /// <summary>
        /// Get DB parameter
        /// </summary>
        /// <param name="dataProvider">Data provider</param>
        /// <param name="dbType">Data type</param>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterValue">Parameter value</param>
        /// <returns>Parameter</returns>
        private static DbParameter GetParameter(this IRealitycsDataProvider dataProvider, DbType dbType, string parameterName, object parameterValue)
        {
            var parameter = dataProvider.GetParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            parameter.DbType = dbType;

            return parameter;
        }

        /// <summary>
        /// Get output DB parameter
        /// </summary>
        /// <param name="dataProvider">Data provider</param>
        /// <param name="dbType">Data type</param>
        /// <param name="parameterName">Parameter name</param>
        /// <returns>Parameter</returns>
        private static DbParameter GetOutputParameter(this IRealitycsDataProvider dataProvider, DbType dbType, string parameterName)
        {
            var parameter = dataProvider.GetParameter();
            parameter.ParameterName = parameterName;
            parameter.DbType = dbType;
            parameter.Direction = ParameterDirection.Output;

            return parameter;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get string parameter
        /// </summary>
        /// <param name="dataProvider">Data provider</param>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterValue">Parameter value</param>
        /// <returns>Parameter</returns>
        public static DbParameter GetStringParameter(this IRealitycsDataProvider dataProvider, string parameterName, string parameterValue)
        {
            return dataProvider.GetParameter(DbType.String, parameterName, (object)parameterValue ?? DBNull.Value);
        }

        /// <summary>
        /// Get output string parameter
        /// </summary>
        /// <param name="dataProvider">Data provider</param>
        /// <param name="parameterName">Parameter name</param>
        /// <returns>Parameter</returns>
        public static DbParameter GetOutputStringParameter(this IRealitycsDataProvider dataProvider, string parameterName)
        {
            return dataProvider.GetOutputParameter(DbType.String, parameterName);
        }

        /// <summary>
        /// Get int parameter
        /// </summary>
        /// <param name="dataProvider">Data provider</param>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterValue">Parameter value</param>
        /// <returns>Parameter</returns>
        public static DbParameter GetInt32Parameter(this IRealitycsDataProvider dataProvider, string parameterName, int? parameterValue)
        {
            return dataProvider.GetParameter(DbType.Int32, parameterName, parameterValue.HasValue ? (object)parameterValue.Value : DBNull.Value);
        }

        /// <summary>
        /// Get output int32 parameter
        /// </summary>
        /// <param name="dataProvider">Data provider</param>
        /// <param name="parameterName">Parameter name</param>
        /// <returns>Parameter</returns>
        public static DbParameter GetOutputInt32Parameter(this IRealitycsDataProvider dataProvider, string parameterName)
        {
            return dataProvider.GetOutputParameter(DbType.Int32, parameterName);
        }

        /// <summary>
        /// Get boolean parameter
        /// </summary>
        /// <param name="dataProvider">Data provider</param>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterValue">Parameter value</param>
        /// <returns>Parameter</returns>
        public static DbParameter GetBooleanParameter(this IRealitycsDataProvider dataProvider, string parameterName, bool? parameterValue)
        {
            return dataProvider.GetParameter(DbType.Boolean, parameterName, parameterValue.HasValue ? (object)parameterValue.Value : DBNull.Value);
        }

        /// <summary>
        /// Get decimal parameter
        /// </summary>
        /// <param name="dataProvider">Data provider</param>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterValue">Parameter value</param>
        /// <returns>Parameter</returns>
        public static DbParameter GetDecimalParameter(this IRealitycsDataProvider dataProvider, string parameterName, decimal? parameterValue)
        {
            return dataProvider.GetParameter(DbType.Decimal, parameterName, parameterValue.HasValue ? (object)parameterValue.Value : DBNull.Value);
        }

        /// <summary>
        /// Get datetime parameter
        /// </summary>
        /// <param name="dataProvider">Data provider</param>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterValue">Parameter value</param>
        /// <returns>Parameter</returns>
        public static DbParameter GetDateTimeParameter(this IRealitycsDataProvider dataProvider, string parameterName, DateTime? parameterValue)
        {
            return dataProvider.GetParameter(DbType.DateTime, parameterName, parameterValue.HasValue ? (object)parameterValue.Value : DBNull.Value);
        }

        #endregion
    }
}
