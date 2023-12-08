using System.Reflection;
using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class AuctionUpdatedConsumer: IConsumer<AuctionUpdated>
{
    private readonly IMapper _mapper;

    public AuctionUpdatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<AuctionUpdated> context)
    {
        var item = _mapper.Map<Item>(context.Message);
        
        var result = await DB.Update<Item>()
            .MatchID(item.ID)
            .ModifyOnly(b => new { b.Make, b.Model, b.Year, b.Color, b.Mileage }, item)
            .ExecuteAsync();

        if (result.MatchedCount == 0)
        {
            throw new Exception("Item for update not found");
        }
    }
}