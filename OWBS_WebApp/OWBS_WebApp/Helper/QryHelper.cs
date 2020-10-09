using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public sealed class QryHelper
{
    public static void Setting(string StationId, DateTime ABgnDate, DateTime AEndDate, int APercentage)
    {
        HttpContext.Current.Session["StationID"]  = StationId;
        HttpContext.Current.Session["BgnDate"]    = ABgnDate;
        HttpContext.Current.Session["EndDate"]    = AEndDate;
        HttpContext.Current.Session["Percentage"] = APercentage;        
    }
}