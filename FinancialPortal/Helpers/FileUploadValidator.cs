using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace FinancialPortal.Helpers
{
    public class FileUploadValidator
    {
        public static bool IsWebFriendlyFile(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return false;
            }
            if (file.ContentLength > 2 * 1024 * 1024 || file.ContentLength < 2048)
            {
                return false;
            }
            try
            {
                //Look at the extension of the incoming file and compare it to a list of acceptable extensions
                var fileExtension = Path.GetExtension(file.FileName);
                var allowableExtensions = WebConfigurationManager.AppSettings["AllowableExtensions"].Split(',');
                return allowableExtensions.Contains(fileExtension);
            }
            catch
            {
                return false;
            }
        }
    }
}