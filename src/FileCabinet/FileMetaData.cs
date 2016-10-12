using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileCabinet
{
    public class FileMetaData
    {
        public string ContentType { get; }
        public string OriginalName { get; }
        public string Description { get; }
        public string UploaderId { get; }
        public DateTime DateCreatedUtc { get; }

        public FileMetaData(string contentType, string originalName,
            string description, string uploaderId, DateTime dateCreatedUtc)
        {
            ContentType = contentType;
            OriginalName = originalName;
            Description = description;
            UploaderId = uploaderId;
            DateCreatedUtc = dateCreatedUtc;
        }
    }
}
