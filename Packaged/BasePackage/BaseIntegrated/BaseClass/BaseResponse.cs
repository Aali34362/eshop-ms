namespace BaseIntegrated.BaseClass;

public abstract class BaseResponse
{
    public Guid Id { get; set; }
    public string Lst_Crtd_User { get; set; } = string.Empty;
    public DateTime Lst_Crtd_Date { get; set; }
    public short Act_Ind { get; set; }
}
