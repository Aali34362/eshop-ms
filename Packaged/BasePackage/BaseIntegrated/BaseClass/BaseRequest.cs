using System.ComponentModel;

namespace BaseIntegrated.BaseClass;

public abstract class BaseRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Crtd_User { get; set; } = string.Empty;
    public string Lst_Crtd_User { get; set; } = string.Empty;
    public DateTime Crtd_Date { get; set; } = DateTime.Now;
    public DateTime Lst_Crtd_Date { get; set; }
    [DefaultValue(1)]
    public short Act_Ind { get; set; } = 1;
    [DefaultValue(0)]
    public short Del_Ind { get; set; } = 0;
    //For Sql Server Only
    ////[Timestamp]
    ////public byte[]? Version { get; set; }

    //[ConcurrencyCheck]
    //public Guid Version { get; set; }
}
