using RealityCS.DataLayer.Context.KPIEntity.ContextModels;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// KPI formulas Realytics users will be able to apply on KPI data source elements 
/// </summary>
namespace RealityCS.BusinessLogic.KPIEntity
{
    static class RealyticsKPIFormula
    {
        /// <summary>
        /// Formula to do a count operation on a data source element(field)
        /// </summary>
        /// <param name="FormulaToBeApplied">
        /// <param name="attributeName">
        /// formula to be applied on a attribute
        /// </param>
        /// <returns>
        /// the equivalent query of the operation
        /// </returns>
        public static string Count(EnumerationKPIFormulas FormulaToBeApplied, string aliasName, string attributeName )
        {
            if (FormulaToBeApplied != EnumerationKPIFormulas.Count)
                throw new ArgumentException("Expected Count Operation on DataElement!");

            if (attributeName.Length == 0)
                throw new ArgumentException("Data Source Element Can Not Be Empty!");

            string functionQuery = "";
            functionQuery = string.Format("COUNT(CAST({0}.{1} as DECIMAL(10,2)))", aliasName, attributeName);
            return functionQuery;
        }
        /// <summary>
        /// Formula to do a average operation on a data source element(field)
        /// </summary>
        /// <param name="FormulaToBeApplied">
        /// <param name="attributeName">
        /// formula to be applied on a attribute
        /// </param>
        /// <returns>
        /// the equivalent query of the operation
        /// </returns>
        public static string Average(EnumerationKPIFormulas FormulaToBeApplied, string aliasName, string attributeName )
        {
            if (FormulaToBeApplied != EnumerationKPIFormulas.Average)
                throw new ArgumentException("Expected Average Operation on DataElement!");

            if (attributeName.Length == 0)
                throw new ArgumentException("Data Source Element Can Not Be Empty!");


            string functionQuery = "";
            functionQuery = string.Format("AVG(CAST({0}.{1} as DECIMAL(10,2)))", aliasName, attributeName);
            return functionQuery;
        }
        /// <summary>
        /// Formula to do a percentage operation on a data source element(field)
        /// </summary>
        /// <param name="dataElement">
        /// data element on which the operation will be handled
        /// <param name="FormulaToBeApplied">
        /// <param name="attributeName">
        /// formula to be applied on a attribute
        /// </param>
        /// <returns>
        /// the equivalent query of the operation
        /// </returns>
        public static string Percentage(EnumerationKPIFormulas FormulaToBeApplied, string aliasName, string attributeName )
        {
            if (FormulaToBeApplied != EnumerationKPIFormulas.Percentage)
                throw new ArgumentException("Expected Percentage Operation on DataElement!");

            if (attributeName.Length == 0)
                throw new ArgumentException("Data Source Element Can Not Be Empty!");

            string functionQuery = "";
            functionQuery = string.Format("(SUM(CAST({0}.{1} as DECIMAL(10,2)))*100)/1000 as {2}Pecentage", aliasName, attributeName, attributeName );
            return functionQuery;
        }
        /// <summary>
        /// Formula to do a cumulative operation on a data source element(field)
        /// </summary>
        /// <param name="FormulaToBeApplied">
        /// <param name="attributeName">
        /// formula to be applied on a attribute
        /// </param>
        /// <returns>
        /// the equivalent query of the operation
        /// </returns>
        public static string Cumulative(EnumerationKPIFormulas FormulaToBeApplied, string aliasName, string attributeName )
        {
            if (FormulaToBeApplied != EnumerationKPIFormulas.Cumulative)
                throw new ArgumentException("Expected Cumulative Operation on DataElement!");

            if (attributeName.Length == 0)
                throw new ArgumentException("Data Source Element Can Not Be Empty!");

            string functionQuery = "";
            functionQuery = string.Format("SUM(CAST({0}.{1} as DECIMAL(10,2)))", aliasName, attributeName);
            return functionQuery;
        }

        /// <summary>
        /// Formula to do a summation operation on a data source element(field)
        /// </summary>
        /// <param name="FormulaToBeApplied">
        /// <param name="attributeNameOne">
        /// <param name="attributeNameTwo">
        /// Formula to be applied on two attributes
        /// </param>
        /// <returns>
        /// the equivalent query of the operation
        /// </returns>
        public static string Summation(EnumerationKPIFormulas FormulaToBeApplied, string aliasNameOne, string attributeNameOne, string aliasNameTwo, string attributeNameTwo )
        {
            if (FormulaToBeApplied != EnumerationKPIFormulas.Summation)
                throw new ArgumentException("Expected Summation Operation on DataElement!");

            if (attributeNameOne.Length == 0)
                throw new ArgumentException("Data Source Element One Can Not Be Empty!");

            if (attributeNameTwo.Length == 0)
                throw new ArgumentException("Data Source Element Two Can Not Be Empty!");

            string functionQuery = "";
            functionQuery = string.Format("CAST({0}.{1} as DECIMAL(10,2)) + CAST({2}.{3} as DECIMAL(10,2)) As {4}_{5}_Total", aliasNameOne, attributeNameOne, aliasNameTwo, attributeNameTwo, attributeNameOne, attributeNameTwo);
            return functionQuery;
        }

        /// <summary>
        /// Formula to do a differenciation operation on a data source element(field)
        /// </summary>
        /// <param name="FormulaToBeApplied">
        /// <param name="attributeNameOne">
        /// <param name="attributeNameTwo">
        /// Formula to be applied on two attributes
        /// </param>
        /// <returns>
        /// the equivalent query of the operation
        /// </returns>
        public static string Differentiation(EnumerationKPIFormulas FormulaToBeApplied, string aliasNameOne, string attributeNameOne, string aliasNameTwo, string attributeNameTwo)
        {
            if (FormulaToBeApplied != EnumerationKPIFormulas.Differentiation)
                throw new ArgumentException("Expected Differentiation Operation on DataElement!");

            if (attributeNameOne.Length == 0)
                throw new ArgumentException("Data Source Element One Can Not Be Empty!");

            if (attributeNameTwo.Length == 0)
                throw new ArgumentException("Data Source Element Two Can Not Be Empty!");

            string functionQuery = "";
            functionQuery = string.Format("(CAST({0}.{1} as DECIMAL(10,2)) - CAST({2}.{3} as DECIMAL(10,2))) As {4}_{5}_Difference", aliasNameOne, attributeNameOne, aliasNameTwo, attributeNameTwo, attributeNameOne, attributeNameTwo);
            return functionQuery;
        }
        /// <summary>
        /// Formula to do a Multiplication operation on a data source element(field)
        /// </summary>
        /// <param name="FormulaToBeApplied">
        /// <param name="attributeNameOne">
        /// <param name="attributeNameTwo">
        /// Formula to be applied on two attributes
        /// </param>
        /// <returns>
        /// the equivalent query of the operation
        /// </returns>
        public static string Multiplication(EnumerationKPIFormulas FormulaToBeApplied, string aliasNameOne, string attributeNameOne, string aliasNameTwo, string attributeNameTwo)
        {
            if (FormulaToBeApplied != EnumerationKPIFormulas.Multiplication)
                throw new ArgumentException("Expected Multiplication Operation on DataElement!");

            if (attributeNameOne.Length == 0)
                throw new ArgumentException("Data Source Element One Can Not Be Empty!");

            if (attributeNameTwo.Length == 0)
                throw new ArgumentException("Data Source Element Two Can Not Be Empty!");

            string functionQuery = "";
            functionQuery = string.Format("(CAST({0}.{1} as DECIMAL(10,2)) * CAST({2}.{3} as DECIMAL(10,2))) As {4}_{5}_Multiply", aliasNameOne, attributeNameOne, aliasNameTwo, attributeNameTwo, attributeNameOne, attributeNameTwo);
            return functionQuery;
        }
        /// <summary>
        /// Formula to do a Division operation on a data source element(field)
        /// </summary>
        /// <param name="FormulaToBeApplied">
        /// <param name="attributeNameOne">
        /// <param name="attributeNameTwo">
        /// Formula to be applied on two attributes
        /// </param>
        /// <returns>
        /// the equivalent query of the operation
        /// </returns>
        public static string Division(EnumerationKPIFormulas FormulaToBeApplied, string aliasNameOne, string attributeNameOne, string aliasNameTwo, string attributeNameTwo)
        {
            if (FormulaToBeApplied != EnumerationKPIFormulas.Division)
                throw new ArgumentException("Expected Division Operation on DataElement!");

            if (attributeNameOne.Length == 0)
                throw new ArgumentException("Data Source Element One Can Not Be Empty!");

            if (attributeNameTwo.Length == 0)
                throw new ArgumentException("Data Source Element Two Can Not Be Empty!");

            string functionQuery = "";
            functionQuery = string.Format("(CAST({0}.{1} as DECIMAL(10,2)) / CAST({2}.{3} as DECIMAL(10,2))) As {4}_{5}_Divide", aliasNameOne, attributeNameOne, aliasNameTwo, attributeNameTwo, attributeNameOne, attributeNameTwo);
            return functionQuery;
        }

    }
}
