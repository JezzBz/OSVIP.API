using System;
using Osvip.Api.Data;
using Osvip.Api.Models;

namespace Osvip.Api.Repositories
{
    public class DirectionRepository
    {
        private readonly ApplicationContext Context;
        public DirectionRepository(ApplicationContext context)
        {
            Context = context;
        }
        public async Task<bool> AddAsync(Direction direction)
        {
            try
            {
                await Context.Directions.AddAsync(direction);
                await Context.SaveChangesAsync();
                return true;
            }
            catch 
            {
                return false;
            }
            
        }
        public  IEnumerable<Direction> Get()
        {
            IEnumerable<Direction> directions = Context.Directions.Select(x=>x);
            return directions;
        }
        

    }
}

