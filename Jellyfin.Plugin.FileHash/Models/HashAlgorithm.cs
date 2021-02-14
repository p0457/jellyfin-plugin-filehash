using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using MediaBrowser.Model.Tasks;
using MediaBrowser.Controller.Entities;
using System.IO;
using MediaBrowser.Controller.Library;
using MediaBrowser.Model.Entities;
using Microsoft.Extensions.Logging;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Entities.TV;
using Jellyfin.Data.Enums;
using MediaBrowser.Model.Querying;
using System.Diagnostics;
using Jellyfin.Plugin.FileHash.Providers.ExternalId;
using System.Security.Cryptography;
using MediaBrowser.Model.Plugins;
using Jellyfin.Plugin.FileHash.Configuration;

namespace Jellyfin.Plugin.FileHash
{
    public enum HashAlgorithmEnums
    {
        SHA1,
        SHA256,
        SHA384,
        SHA512,
        MD5
    }

    public static class HashAlgorithmsDictionary
    {
        public static List<HashAlgorithm> GetConfiguredAlgorithms() 
        {
            PluginConfiguration config = Plugin.Instance.PluginConfiguration;
            List<HashAlgorithm> algs = new List<HashAlgorithm>();
            if (config.MD5Enabled) algs.Add(new MD5HashAlgorithm(config.MD5BufferSize));
            if (config.SHA1Enabled) algs.Add(new SHA1HashAlgorithm(config.SHA1BufferSize));
            if (config.SHA256Enabled) algs.Add(new SHA256HashAlgorithm(config.SHA256BufferSize));
            if (config.SHA384Enabled) algs.Add(new SHA384HashAlgorithm(config.SHA384BufferSize));
            if (config.SHA512Enabled) algs.Add(new SHA512HashAlgorithm(config.SHA512BufferSize));
            return algs;
        }
    }

    public abstract class HashAlgorithm
    {
        public HashAlgorithmEnums Enum;
        public string ExternalId;
        public int BufferSize;
        public abstract string Hash(BufferedStream stream);
    }

    public class SHA1HashAlgorithm : HashAlgorithm
    {
        public SHA1HashAlgorithm(int bufferSize = 1200000) 
        {
            Enum = HashAlgorithmEnums.SHA1;
            ExternalId = FileHashSHA1ExternalIdStrings.Key;
            BufferSize = bufferSize;
        }
        public override string Hash(BufferedStream stream) 
        {
            byte[] checksum = null;
            System.Security.Cryptography.SHA1 sha = System.Security.Cryptography.SHA1.Create();
            checksum = sha.ComputeHash(stream);
            string hashResult = BitConverter.ToString(checksum).Replace("-", String.Empty);
            return hashResult;
        }
    }

    public class SHA256HashAlgorithm : HashAlgorithm
    {
        public SHA256HashAlgorithm(int bufferSize = 1200000) 
        {
            Enum = HashAlgorithmEnums.SHA256;
            ExternalId = FileHashSHA256ExternalIdStrings.Key;
            BufferSize = bufferSize;
        }
        public override string Hash(BufferedStream stream) 
        {
            byte[] checksum = null;
            SHA256Managed sha = new SHA256Managed();
            checksum = sha.ComputeHash(stream);
            string hashResult = BitConverter.ToString(checksum).Replace("-", String.Empty);
            return hashResult;
        }
    }

    public class SHA384HashAlgorithm : HashAlgorithm
    {
        public SHA384HashAlgorithm(int bufferSize = 1200000) 
        {
            Enum = HashAlgorithmEnums.SHA384;
            ExternalId = FileHashSHA384ExternalIdStrings.Key;
            BufferSize = bufferSize;
        }
        public override string Hash(BufferedStream stream) 
        {
            byte[] checksum = null;
            SHA384 sha = SHA384.Create();
            checksum = sha.ComputeHash(stream);
            string hashResult = BitConverter.ToString(checksum).Replace("-", String.Empty);
            return hashResult;
        }
    }

    public class SHA512HashAlgorithm : HashAlgorithm
    {
        public SHA512HashAlgorithm(int bufferSize = 1200000) 
        {
            Enum = HashAlgorithmEnums.SHA512;
            ExternalId = FileHashSHA512ExternalIdStrings.Key;
            BufferSize = bufferSize;
        }
        public override string Hash(BufferedStream stream) 
        {
            byte[] checksum = null;
            SHA512 sha = SHA512.Create();
            checksum = sha.ComputeHash(stream);
            string hashResult = BitConverter.ToString(checksum).Replace("-", String.Empty);
            return hashResult;
        }
    }

    public class MD5HashAlgorithm : HashAlgorithm
    {
        public MD5HashAlgorithm(int bufferSize = 1200000) 
        {
            Enum = HashAlgorithmEnums.MD5;
            ExternalId = FileHashMD5ExternalIdStrings.Key;
            BufferSize = bufferSize;
        }
        public override string Hash(BufferedStream stream) 
        {
            byte[] checksum = null;
            MD5 md5 = MD5.Create();
            checksum = md5.ComputeHash(stream);
            string hashResult = BitConverter.ToString(checksum).Replace("-", String.Empty);
            return hashResult;
        }
    }
}
