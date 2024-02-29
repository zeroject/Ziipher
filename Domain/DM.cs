namespace Domain;

public class DM
{
    public int DmId { get; set; }
    public int SenderID { get; set; }
    public int ReceiverID { get; set; }
    public string Message { get; set; }
    public DateTime DateStamp { get; set; }
}
