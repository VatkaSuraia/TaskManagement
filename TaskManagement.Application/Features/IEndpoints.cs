namespace TaskManagement.Application;
using Microsoft.AspNetCore.Routing;


public interface IEndpoints
{
    public void Map(IEndpointRouteBuilder app, CancellationToken cancellationToken);
}