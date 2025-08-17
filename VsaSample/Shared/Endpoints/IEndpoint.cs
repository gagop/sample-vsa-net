namespace VsaSample.Shared.Endpoints;

public interface IEndpoint
{
    void RegisterEndpoint(IEndpointRouteBuilder app);
}