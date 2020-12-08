namespace Solution
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Hash evaluator.
    /// </summary>
    public static class Hasher
    {
        private static MD5 encriptor = MD5.Create();

        /// <summary>
        /// Evaluate directory hach function.
        /// </summary>
        /// <param name="directoryName">Directory name.</param>
        public static (bool, IEnumerable<byte>) EvaluateHash(string directoryName)
        {
            if (!Directory.Exists(directoryName))
            {
                return (false, default(byte[]));
            }

            var info = new DirectoryInfo(directoryName);

            return (true, DirectoryHash(info));
        }

        /// <summary>
        /// Evaluate file hash.
        /// </summary>
        /// <param name="info">File information.</param>
        /// <returns>Evaluated hash.</returns>
        private static IEnumerable<byte> FileHash(FileInfo info)
        {
            using (var fileStream = File.OpenRead(info.FullName))
            {
                var fileHash = encriptor.ComputeHash(fileStream);
                var nameHash = encriptor.ComputeHash(Encoding.Unicode.GetBytes(info.Name));

                return fileHash.Concat(nameHash);
            }
        }

        /// <summary>
        /// Evaluate directory hash.
        /// </summary>
        /// <param name="info">Directory information.</param>
        /// <returns>Evaluated hash and success flag.</returns>
        private static IEnumerable<byte> DirectoryHash(DirectoryInfo info)
        {
            var files = info.GetFiles();

            Array.Sort(files);

            IEnumerable<byte> hashes = new List<byte>();

            foreach (var fileInfo in files)
            {
                hashes = hashes.Concat(FileHash(fileInfo));
            }

            var directories = info.GetDirectories();

            Array.Sort(directories);

            foreach (var directoryInfo in directories)
            {
                hashes = hashes.Concat(DirectoryHash(directoryInfo));
            }

            return encriptor.ComputeHash(hashes.ToArray());
        }
    }
}
