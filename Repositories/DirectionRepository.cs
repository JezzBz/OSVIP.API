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
      /// <summary>
      /// Выборка всех направлений из базы данных
      /// </summary>
      /// <returns></returns>
        public  IEnumerable<Direction> Get()
        {
            IEnumerable<Direction> directions = Context.Directions.Select(x=>x);
            return directions;
        }
        

    }
}

