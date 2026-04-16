using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Files;

public class FileRecord : BaseEntity
{
    public string FileName { get; private set; } = default!;
    public string ContentType { get; private set; } = default!;
    public long Size { get; private set; }
    public string StorageKey { get; private set; } = default!; 
    public string? BucketName { get; private set; }
    public Guid UploadedBy { get; private set; }

    private FileRecord() { }

    public FileRecord(Guid tenantId, string fileName, string contentType, long size, string storageKey, Guid uploadedBy, string? bucketName = null)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        FileName = fileName;
        ContentType = contentType;
        Size = size;
        StorageKey = storageKey;
        UploadedBy = uploadedBy;
        BucketName = bucketName;
        CreatedAtUtc = DateTime.UtcNow;
    }
}

