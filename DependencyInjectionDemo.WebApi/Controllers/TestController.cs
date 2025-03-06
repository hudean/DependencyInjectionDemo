using DependencyInjectionDemo.WebApi.IocTest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionDemo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(IScopedService scopedService,
        ITransientService transientService,
        ISingletonService singletonService,
        IAttributeInjectService attributeInjectService) : ControllerBase
    {
        private readonly IScopedService _scopedService = scopedService;
        private readonly ITransientService _transientService = transientService;
        private readonly ISingletonService _singletonService = singletonService;
        private readonly IAttributeInjectService _attributeInjectService = attributeInjectService;

        [HttpPost()]
        public IActionResult Test()
        {
            var id1 = _scopedService.GetGuid();
            var id2 = _transientService.GetGuid();
            var id3 = _singletonService.GetGuid();
            var id4 = _attributeInjectService.GetGuid();
            var obj = new
            {
                id1,
                id2,
                id3,
                id4
            };
            return Ok(obj);
        }
    }
}