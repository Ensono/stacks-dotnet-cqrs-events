using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Queries;
using xxAMIDOxx.xxSTACKSxx.Application.Integration;
using xxAMIDOxx.xxSTACKSxx.CQRS.Queries.GetMenuById;

namespace xxAMIDOxx.xxSTACKSxx.Application.QueryHandlers;

public class GetMenuByIdQueryHandler : IQueryHandler<GetMenuById, Menu>
{
    private readonly IMenuRepository repository;

    public GetMenuByIdQueryHandler(IMenuRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Menu> ExecuteAsync(GetMenuById criteria)
    {
        var menu = await repository.GetByIdAsync(criteria.Id);

        if (menu == null)
            return null;

        //You might prefer using AutoMapper in here
        var result = Menu.FromDomain(menu);

        return result;
    }
}
