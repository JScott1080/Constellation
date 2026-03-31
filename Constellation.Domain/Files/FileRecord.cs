using System;
using System.Collections.Generic;
using System.Text;

namespace Constellation.Domain.Files;

public class FileRecord
{
    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public string FileName { get; private set; } = default!;
    public string ContentType { get; private set; } = default!;
    public long Size { get; private set; }
    public string StoragePath { get; private set; } = default!;
    public Guid UploadedBy { get; private set; }
    public DateTime UploadedAt { get; private set; }

    private FileRecord() { }

    public FileRecord(Guid companyId, string fileName, string contentType, long size, string storagePath, Guid uploadedBy)
    {
        Id = Guid.NewGuid();
        CompanyId = companyId;
        FileName = fileName;
        ContentType = contentType;
        Size = size;
        StoragePath = storagePath;
        UploadedBy = uploadedBy;
        UploadedAt = DateTime.UtcNow;
    }
}
