using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class CertificateHelper
    {
        public static SubscriptionCloudCredentials GetCredentials(string subscriptionID, string base64EncodedCert, string filePath)
        {
            var file = Path.Combine(filePath, "Cert-" + Guid.NewGuid());

            try
            {
                File.WriteAllBytes(file, Convert.FromBase64String(base64EncodedCert));
                return new CertificateCloudCredentials(subscriptionID, new X509Certificate2(file));
            }
            finally
            {
                File.Delete(file);
            }

        }

        public static X509Certificate2 LoadCertificate(StoreName storeName, StoreLocation storeLocation, string thumbprint)
        {
            // The following code gets the cert from the keystore
            X509Store store = new X509Store(storeName, storeLocation);
            store.Open(OpenFlags.ReadOnly);

            X509Certificate2Collection certCollection = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);

            X509Certificate2Enumerator enumerator = certCollection.GetEnumerator();

            X509Certificate2 cert = null;

            while (enumerator.MoveNext())
            {
                cert = enumerator.Current;
            }

            return cert;
        }
    }
}
