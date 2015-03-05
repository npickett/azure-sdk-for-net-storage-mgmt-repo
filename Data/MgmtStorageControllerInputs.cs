using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class MgmtStorageControllerInputs
    {
        #region properties

        public string StorageAccountName { get; set; }
        public string SubscriptionID { get; set; }
        public string Base64EncodedCertificate { get; set; }
        public string TempFilePath { get; set; }

        #endregion

        #region constructor

        public MgmtStorageControllerInputs(string subscriptionID, string storageAccountName, string base64EncodedCertificate, string tempFilePath)
        {
            this.SubscriptionID = subscriptionID;
            this.StorageAccountName = storageAccountName;
            this.Base64EncodedCertificate = base64EncodedCertificate;
            this.TempFilePath = tempFilePath;
        }

        #endregion
    }
}
