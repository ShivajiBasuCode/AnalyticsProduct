using CsvHelper;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using RealityCS.BusinessLogic.Customer;
using RealityCS.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static RealityCS.SharedMethods.RealitycsConstants;

namespace RealityCS.BusinessLogic.Import
{
    public class RealitycsImportManager : IRealitycsImportManagerConfiguration
    {
        public RealitycsImportManager()
        {

        }
        public async Task<bool> ImportDataFromCSV(IFormFile file,int ParentId, Guid operationId)
        {
            try
            {
                List<List<string>> values = new List<List<string>>();
                List<string> attributes = new List<string>();
                string value;
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Configuration.HasHeaderRecord = true;
                        bool IsReadHeader = false;
                        int headerCount = 0;
                        long rowNumber = 0;
                        while (await csv.ReadAsync())
                        {
                            if (!IsReadHeader)
                            {
                                for (int i = 0; csv.TryGetField<string>(i, out value); i++)
                                {
                                    attributes.Add(value);
                                }
                                headerCount = attributes.Count;
                                IsReadHeader = true;
                            }
                            else
                            {
                                rowNumber++;
                                List<string> row = new List<string>();
                                for (int i = 0; csv.TryGetField<string>(i, out value); i++)
                                {
                                    row.Add(value);
                                }
                                if (headerCount != row.Count)
                                {
                                    string message = $"Error in row number: {rowNumber} value count mismatch.";
                                    return false;
                                }
                                else
                                {
                                    values.Add(row);
                                }
                            }
                        }
                    }
                }
                var legalEntityService = RealitycsEngineContext.Current.Resolve<ILegalEntityService>();
                await legalEntityService.SaveImportDataFromCSV(attributes, values, CustomerData.DataSourceElementEntityName, ParentId,operationId);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
