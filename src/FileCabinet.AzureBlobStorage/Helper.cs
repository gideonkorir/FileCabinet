using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace FileCabinet.AzureBlobStorage
{
    internal static class Helper
    {
        const string dateFormat = "dd/MMM/yyyy HH:mm:ss.f";
        public static async Task<FileMetaData> GetMetaDataAsync(this CloudBlockBlob blob)
        {
            await blob.FetchAttributesAsync().ConfigureAwait(false);

            var utcString = blob.Metadata.GetOrDefault(Metadata.DateCreatedUtc);
            DateTime date;
            if (!DateTime.TryParseExact(utcString, dateFormat, null, DateTimeStyles.None, out date))
                date = blob.Properties.LastModified?.UtcDateTime ?? DateTime.MinValue;

            return new FileMetaData(
                blob.Properties.ContentType,
                blob.Metadata.GetOrDefault(Metadata.OriginalName),
                blob.Metadata.GetOrDefault(Metadata.Description),
                blob.Metadata.GetOrDefault(Metadata.UploaderId),
                date
                );            
        }

        public static void SetMetaData(this CloudBlockBlob blob, string id, IFileUpload upload, Func<DateTime> utcClock)
        {
            blob.Metadata[Metadata.FileId] = id.ToString();
            blob.Properties.ContentType = upload.ContentType;
            blob.Metadata[Metadata.OriginalName] = upload.FileName;
            blob.Metadata[Metadata.Description] = upload.Description;
            blob.Metadata[Metadata.UploaderId] = upload.UploaderId;
            blob.Metadata[Metadata.DateCreatedUtc] = utcClock().ToString("");
        }

        public static string GetOrDefault(this IDictionary<string, string> data, string key)
        {
            string value;
            if (data.TryGetValue(key, out value))
                return value;
            return default(string);
        }

        static class Metadata
        {
            public const string
            FileId = "filecabinet_fileId",
            OriginalName = "filecabinet_originalname",
            DateCreatedUtc = "filecabinet_datecreatedutc",
            UploaderId = "filecabinet_uploaderid",
            Description = "filecabinet_description";
        }

    }
}
