using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using CQRSStudy.Commands;

namespace CQRSStudy.Queries
{
    public class CalculateFactorialQuery : IRequest<int>
    {
        public int N;

        public CalculateFactorialQuery(int n)
        {
            N = n;
        }

        public class CalculateFactorialQueryHandler : IRequestHandler<CalculateFactorialQuery, int>
        {
            private IMediator _mediator;

            public CalculateFactorialQueryHandler(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task<int> Handle(CalculateFactorialQuery request, CancellationToken cancellationToken)
            {
                if (request.N < 0)
                    throw new Exception("Can't calculate factorial of number less than 0");

                var result = 1;

                for (int i = 1; i <= request.N; ++i)
                    result *= i;

                await _mediator.Publish(new SaveCalculationResultCommand("factorial", result));

                return result;
            }
        }
    }
}