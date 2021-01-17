using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealityCS.ViewComponents
{
    public class CssLinkGenerator : ViewComponent
    {
        private readonly string wwwroot = "/wwwroot/";
        private readonly IWebHostEnvironment webHostEnvironment;
        private string baseFolder = string.Empty;
        private static List<object> filespath;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        public CssLinkGenerator(IWebHostEnvironment webHostEnvironment)
        {
            filespath = new List<object>(); 
            baseFolder = webHostEnvironment.ContentRootPath;
            this.webHostEnvironment = webHostEnvironment;
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RootFolderName"></param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(string RootFolderName)

        {
            string path = Path.Combine(wwwroot, RootFolderName);
            ProcessDirectory(this.baseFolder + path);

            return View(filespath);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetDirectory"></param>
        private void ProcessDirectory(string targetDirectory)
        {

            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {

                string filepath = string.Empty;
                filepath = Path.GetRelativePath(this.baseFolder + this.wwwroot, fileName);
                filepath = filepath.Replace(Path.DirectorySeparatorChar, '/');
                filepath = string.Concat("~/", filepath);


                filespath.Add(filepath);
            }
            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                ProcessDirectory(subdirectory);
            }
        }

    }
}
