namespace keepr.Models
{
  public class Keep
  {
    public int id { get; set; }
    public string creatorId { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string img { get; set; }
    public int views { get; set; }
    public int shares { get; set; }
    public int keeps { get; set; }
    public Account Creator { get; set; }
  }
}