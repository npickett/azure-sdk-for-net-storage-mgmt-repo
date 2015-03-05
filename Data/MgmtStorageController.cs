using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Management.Storage;
using Microsoft.WindowsAzure.Management.Storage.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class MgmtStorageController : IDisposable
    {
        private StorageManagementClient mStorageManagementClient;
        private MgmtStorageControllerInputs mParameters;

        public MgmtStorageController(MgmtStorageControllerInputs parameters)
        {
            mParameters = parameters;
            var credential = CertificateHelper.GetCredentials(parameters.SubscriptionID, parameters.Base64EncodedCertificate, parameters.TempFilePath);
            mStorageManagementClient = CloudContext.Clients.CreateStorageManagementClient(credential);
        }

        public void Dispose()
        {
            if (mStorageManagementClient != null)
                mStorageManagementClient.Dispose();
        }

        private async Task<StorageAccountGetKeysResponse> GetStorageAccountKeysAsync()
        {
            try
            {
                return await mStorageManagementClient.StorageAccounts.GetKeysAsync(mParameters.StorageAccountName).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
                throw;
            }
        }

        public async Task<string> GetStorageAccountSecondaryKeyAsync()
        {
            var keys = await GetStorageAccountKeysAsync().ConfigureAwait(false);

            return keys.SecondaryKey;
        }

    }
}
