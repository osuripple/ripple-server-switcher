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
            get => Cert.GetSerialNumberString();
        }
        public byte[] Certificate;
        public X509Certificate2 Cert
        {
            get
            {
                return X509Certificate2(Certificate);
            }
        }

        private X509Certificate2Collection FindRippleCertificates(X509Certificate2 store)
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

                try
                {
                    store.Add(Cert);
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
                {
                    try
                    {
                        store.Remove(c);
                    }
                    catch (CryptographicException)
                    {
                        string serialNumber;
                        try
                        {
                            serialNumber = c.GetSerialNumberString();
                        }
                        catch
                        {
                            serialNumber = "unknown";
                        }
                        throw new HumanReadableException("Cannot remove.", $"The certificate with serial number '{serialNumber}' was not uninstalled because the user denied its removal or the access was denied (non-locally installed cert).");
                    }
                }
            }
            finally
            {
                store.Close();
            }
        }
    }
}
