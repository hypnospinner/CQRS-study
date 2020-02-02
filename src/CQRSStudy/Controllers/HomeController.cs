using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CQRSStudy.Queries;

namespace CQRSStudy.Controllers
{
    [ApiController]
    public class HomeController : Controller
    {
        private IMediator _mediator;

        public HomeController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        [Route("api/factorial/{n}")]
        public async Task<IActionResult> Factorial(int n)
        {
            var query = new CalculateFactorialQuery(n);

            try
            {
                var result = await _mediator.Send(query);
                return Json(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("api/fibonacci/{n}")]
        public async Task<IActionResult> Fibonacci(int n)
        {
            var query = new CalculateFibonacciQuery(n);

            try
            {
                var result = await _mediator.Send(query);
                return Json(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("api/results")]
        public async Task<IActionResult> LatestResults()
        {
            var query = new GetLatestCalculationsQuery();

            try
            {
                var result = await _mediator.Send(query);
                return Json(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}