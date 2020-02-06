using System;
using EventFlow;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DojoEventSourcing.Controllers
{
    [Route("[controller]")]
    public class CapacityController : Controller
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandBus _commandBus;

        public CapacityController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        [HttpGet("{date}")]
        public CapacityDto GetByDate(DateTime date)
        {
            // return (CapacityDto)core.handle(new GetCapacityByDate(LocalDate.parse(date)));
            return new CapacityDto();
        }

        [HttpGet("{start}/{end}")]
        public CapacityDto[] GetByDateRange(DateTime start, DateTime end)
        {
            // return (CapacityDto[])core.handle(new GetCapacityByDateRange(LocalDate.parse(start), LocalDate.parse(end)));
            return new CapacityDto[] { };
        }

        public class CapacityDto
        {
        }
    }
}