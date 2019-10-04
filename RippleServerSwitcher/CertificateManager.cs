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
            get => Certificate.GetSerialNumberString();
        }
        public X509Certificate2 Certificate;
        private Byte[] Bytes;

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

                try
                {
                    store.Add(Certificate);
                }
                catch (CryptographicException e)
                {

                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";

                    // Create the Cert file from built-in ressource
                    System.IO.File.WriteAllBytes("Certificate.cert", Bytes);

                    startInfo.Arguments = "/C certmgr /add Certificate.cert /s Root";
                    process.StartInfo = startInfo;
                    process.Start();

                    if(!IsCertificateInstalled())
                    {
                        Program.RavenClient.Capture(new SharpRaven.Data.SentryEvent(e));

                        System.IO.File.Delete("Certificate.cert");

                        throw new HumanReadableException(
                        "You must install the certificate manually.",
                        "The certificate is needed to connect to Ripple through HTTPs. Without it, you won't be able to connect. " +
                        "To install the certificate manually, visit https://ripple.moe/doc/install_certificate_manually."
                        );
                    }
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
