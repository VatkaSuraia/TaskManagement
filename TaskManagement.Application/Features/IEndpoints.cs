namespace TaskManagement.Application;
using Microsoft.AspNetCore.Routing;


public interface IEndpoints
{
    public void MapEndpoint(IEndpointRouteBuilder app, CancellationToken cancellationToken);
}