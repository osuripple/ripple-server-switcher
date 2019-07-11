using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace RippleServerSwitcher
{
    class CertificateManager
    {
        public string AuthoritySerialNumber
        {
            get => Collection[0].GetSerialNumberString();
        }
        public byte[] Certificate;
        public X509Certificate2Collection Collection
        {
            get
            {
                X509Certificate2Collection collection = new X509Certificate2Collection();
                collection.Import(Certificate);
                return collection;
            }
        }

        private X509Certificate2Collection FindRippleCertificates(X509Store store)
        {
            // Too bad I can't subclass X509Store...
            return store.Certificates.Find(X509FindType.FindBySerialNumber, AuthoritySerialNumber, true);
        }

        public bool IsCertificateInstalled()
        {
            X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadWrite);
                return FindRippleCertificates(store).Count > 0;
            }
            finally
            {
                store.Close();
            }
        }

        public void InstallCertificate()
        {
            X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadWrite);
                if (FindRippleCertificates(store).Count > 0)
                    return;

                foreach (X509Certificate2 cert in Collection)
                    try
                    {
                        store.Add(cert);
                    }
                    catch (CryptographicException)
                    {
                        throw new HumanReadableException(
                            "You must install the certificate.",
                            "The certificate is needed to connect to Ripple through HTTPs. Without it, you won't be able to connect. " +
                            "Please answer 'Yes' to the dialog window asking you to install the certificate in order to switch to Ripple."
                        );
                    }
            }
            finally
            {
                store.Close();
            }
        }

        public void RemoveCertificates()
        {
            X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadWrite);
                foreach (X509Certificate2 c in FindRippleCertificates(store))
                    store.Remove(c);
            }
            catch (CryptographicException)
            {
                throw new HumanReadableException("Removal halted by user.", "The certificate was not uninstalled because the user denied its removal.");
            }
            finally
            {
                store.Close();
            }
        }
    }
}
