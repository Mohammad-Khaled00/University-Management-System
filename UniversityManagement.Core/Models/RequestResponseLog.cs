using System;
using System.Collections.Generic;

namespace UniversityManagement.Core.Models;

public partial class RequestResponseLog
{
    public int Id { get; set; }

    public string? RequestUrl { get; set; }

    public string? RequestMethod { get; set; }

    public string? RequestBody { get; set; }

    public int? ResponseStatusCode { get; set; }

    public string? ResponseBody { get; set; }

    public DateTime? LogDateTime { get; set; }
}
