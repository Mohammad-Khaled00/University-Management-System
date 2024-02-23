using System;
using System.Collections.Generic;

namespace UniversityManagement.Core.Models;

public partial class RequestLog
{
    public int Id { get; set; }

    public string? LogMessage { get; set; }
}
