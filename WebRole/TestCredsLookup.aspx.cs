using Data;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebRole
{
    public partial class TestCredsLookup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string filePath = GetLocalStoragePath("TmpLocalStorage");

            var accountName1 = ConfigurationManager.AppSettings.Get("AccountName1");
            var subscriptionID1 = ConfigurationManager.AppSettings.Get("SubID1"); 

            var cert = CertificateHelper.LoadCertificate(StoreName.My, StoreLocation.LocalMachine, ConfigurationManager.AppSettings.Get("CertMgmtFootprint"));

            var inputs = new MgmtStorageControllerInputs(subscriptionID1, accountName1, Convert.ToBase64String(cert.RawData), filePath);
            var controller = new MgmtStorageController(inputs);

            bool isSuccess = false;
            
            try
            {
                var test = controller.GetStorageAccountSecondaryKeyAsync().Result;
                isSuccess = true;
            }
            catch(Exception ex)
            {
                Exception innerEx = null;
                if (ex is AggregateException)
                    innerEx = ((AggregateException)ex).Flatten().InnerException;

                Response.Write(String.Format("Error {0} | ", innerEx.Message)); 
            }

            Response.Write(String.Format("Is Success:  {0}", isSuccess)); 
        }

        public string GetLocalStoragePath(string localResourceName)
        {
            if (RoleEnvironment.IsAvailable)
            {
                return RoleEnvironment.GetLocalResource(localResourceName).RootPath;
            }

            return ConfigurationManager.AppSettings[localResourceName];
        }
    }
}