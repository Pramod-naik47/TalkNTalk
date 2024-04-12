namespace UserAuthentication.Models;

public class ErrorModel
{
    public int Status { get; set; }
    public string? Message { get; set; }
    public string? StackTrace { get; set; }
}
