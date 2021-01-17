using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.RealitycsClient
{
    public class ClientDataImportDTO
    {
        public string dataSourceName { get; set; }
        public List<ImportCSVFileDTO> item { get; set; }
    }
    public class ImportCSVFileDTO
    {
        public string dataSource { get; set; }
        public string dataSourceType { get; set; }
        public IFormFile file { get; set; }
    }
}
