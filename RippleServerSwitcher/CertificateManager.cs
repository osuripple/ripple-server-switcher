using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace RippleServerSwitcher
{
    class CertificateInstallationFailedException : Exception { }

    class CertificateManager
    {
        public string AuthoritySerialNumber
        {
            get => Certificate.GetSerialNumberString();
        }
        public X509Certificate2 Certificate;
        private byte[] CertificateBytes
        {
            get => Certificate.RawData;
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

        private void InstallCertificateStore()
        {
            X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadWrite);
                if (FindRippleCertificates(store).Count > 0)
                    return;
                store.Add(Certificate);
            }
            finally
            {
                store.Close();
            }
        }

        private void InstallCertificateCertutil(bool user = true)
        {
            // Create the Cert file from built-in resource
            var tempCertPath = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".cert";
            try
            {
                System.IO.File.WriteAllBytes(tempCertPath, CertificateBytes);
            }
            catch (System.IO.IOException)
            {
                throw new HumanReadableException(
                    "Cannot create temp cert",
                    "There was an error while creating the temporary certificate file. Make sure you have the required privileges. You can also try turning off any antivirus software you may have installed."
                );
            }

            try
            {
                // Install through certutil
                var output = "";
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                    FileName = "certutil",
                    Arguments = $"{(user ? "-user " : " ")}-f -AddStore \"Root\" {tempCertPath}"
                };
                process.OutputDataReceived += (sender, args) => output += args.Data;
                // startInfo.Arguments = "-enterprise -f -v -AddStore \"Root\" Certificate.cert";
                process.Start();
                process.WaitForExit();
                if (process.ExitCode != 0 || !IsCertificateInstalled())
                {
                    // throw new HumanReadableException("certutil error", $"Certificate install error.\nExited with code {process.ExitCode}, output:\n\n{output}");
                    throw new CertificateInstallationFailedException();
                }
            }
            finally
            {
                System.IO.File.Delete(tempCertPath);
            }

            /*if (!IsCertificateInstalled())
            {
                var info = "Certificate installation failed." +
                    "The certificate is needed to connect to Ripple through HTTPs. " +
                    "Without it, you won't be able to connect. " +
                    "Try installing the certificate manually. " +
                    "Instructions for that are available on https://ripple.moe/doc/install_certificate_manually";
                throw new HumanReadableException("Certificate installation failed", info);
            }*/
        }

        public void InstallCertificate()
        {
            Action[] functions = {
                () => InstallCertificateStore(),
                () => InstallCertificateCertutil(true),
                () => InstallCertificateCertutil(false)
            };
            var success = false;
            foreach (var f in functions)
            {
                try
                {
                    f();
                    success = true;
                    break;
                }
                catch
                { }
            }
            if (!success)
            {
                throw new HumanReadableException(
                    "Certificate installation failed",
                    "Certificate installation failed." +
                    "The certificate is needed to connect to Ripple through HTTPs. " +
                    "Without it, you won't be able to connect. " +
                    "Try installing the certificate manually. " +
                    "Instructions for that are available on https://ripple.moe/doc/install_certificate_manually"
                );
            }
        }

        private void RemoveCertificateStore()
        {
            X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadWrite);
                foreach (X509Certificate2 c in FindRippleCertificates(store))
                {
                    /*try
                    {*/
                        store.Remove(c);
                    //}
                    /*catch (CryptographicException)
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
                    }*/
                }
            }
            finally
            {
                store.Close();
            }
        }

        private void RemoveCertificateCertutil(bool user = false)
        {
            var output = "";
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo = new System.Diagnostics.ProcessStartInfo
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = "certutil",
                Arguments = $"{(user ? "-user " : " ")}-delstore \"Root\" {Certificate.GetSerialNumberString()}"
            };
            process.OutputDataReceived += (sender, args) => output += args.Data;
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0 || IsCertificateInstalled())
            {
                throw new HumanReadableException("certutil error", $"Certificate uninstall error.\nExited with code {process.ExitCode}, output:\n\n{output}");
            }
        }

        public void RemoveCertificates()
        {
            Action[] functions = {
                () => RemoveCertificateStore(),
                () => RemoveCertificateCertutil(true),
                () => RemoveCertificateCertutil(false)
            };
            var success = false;
            foreach (var f in functions)
            {
                try
                {
                    f();
                    success = true;
                    break;
                }
                catch
                { }
            }
            if (!success)
            {
                throw new HumanReadableException("Certificate removal failed", "Could not remove the certificate.");
            }
        }
    }
}
