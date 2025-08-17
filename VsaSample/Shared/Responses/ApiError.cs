namespace VsaSample.Shared.Responses;

public class ApiError
{
    public string Code { get; set; } = "ERROR";
    public string Message { get; set; } = "An error occurred.";
    public List<string>? Details { get; set; }
}