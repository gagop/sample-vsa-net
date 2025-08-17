namespace VsaSample.Shared.Responses;

public class ApiErrorResponse
{
    public bool Success => false;
    public ApiError Error { get; set; } = new();
}