using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class UtilityPL
{
    public int OpCode { get; set; }
    public string exceptionMessage { get; set; }
    public bool isException { get; set; }
    public DataTable dt { get; set; }
}