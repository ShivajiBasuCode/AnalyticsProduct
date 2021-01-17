using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RealityCS.SharedMethods
{
    public class FileHelper
    {
        private  readonly string baseFolder = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public FileHelper(IWebHostEnvironment hostingEnvironment)
        {
            baseFolder = hostingEnvironment.ContentRootPath;
        }
        /// <summary>
        /// check Folder Exists or not
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool checkFolderExists(string path)
        {
            
            return Directory.Exists( path);

        }
        /// <summary>
        /// check File Exists or not
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool checkFileExists(string path)
        {
            string tmp = path;

            return File.Exists( tmp);

        }
        /// <summary>
        /// check Directory Empty or not
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsDirectoryEmpty(string path)
        {

            if (checkFolderExists( path))
            {
                DirectoryInfo dirinfo = new DirectoryInfo( path);
                long siz = dirinfo.GetFiles().Sum(file => file.Length);
                return Convert.ToBoolean(siz);
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// File count
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Int32 countFile(string path)
        {
            if (checkFolderExists( path))
            {
                int fileCount = Directory.GetFiles( path).Length;
                return fileCount;
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// create a Directory
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool createDirectory(string path)
        {
            bool b = false;

            try
            {
                if (!checkFolderExists(path))
                {
                    Directory.CreateDirectory(path);
                    b = true;
                }
            }
            catch (Exception ex)
            {
                b = false;
            }
            return b;

        }
        /// <summary>
        /// delete File
        /// </summary>
        /// <param name="path"></param>
        public void deleteFile(string path)
        {
            

            string tmp = path;
            if (File.Exists(tmp))
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                File.Delete(tmp);
               
            }

        }
        /// <summary>
        /// deleteFolder
        /// </summary>
        /// <param name="path"></param>
        public void deleteFolder(string path)
        {
            string tmp = path;
            if (checkFolderExists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                di.Delete();
            }


        }
        /// <summary>
        /// deleteFolder
        /// </summary>
        /// <param name="path"></param>
        /// <param name="includesubfolders"></param>
        public void deleteFolder(string path, bool includesubfolders)
        {
            string tmp = path;
            if (checkFolderExists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                di.Delete(includesubfolders);
            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string SystemGeneratedFileName()
        {

            return Guid.NewGuid().ToString().ToUpper();
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AreaName"></param>
        /// <returns></returns>
        public string GetAreaLayout(string AreaName)
        {

            if (string.IsNullOrEmpty(AreaName) == false)
            {
                string path = string.Concat(this.baseFolder + $"/Areas/{AreaName}/Views/Shared/_Layout.cshtml");
                if (checkFileExists(path))
                {
                    string arelayoutfilepath= Path.GetRelativePath(this.baseFolder, path).Replace(Path.DirectorySeparatorChar, '/');
                    arelayoutfilepath = string.Concat("/", arelayoutfilepath);
                    return arelayoutfilepath;
                }
            }
            return string.Empty;

        }
    }
}
