using System;
using System.Collections.Generic;

namespace WebApiSQLite;

public class Metter
{
    public long Id { get; set; }

    public string? Address { get; set; }

    public long? Status { get; set; }

    public long? Usages { get; set; }
}
