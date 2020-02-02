using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using CQRSStudy.Commands;

namespace CQRSStudy.Queries
{
    public class CalculateFibonacciQuery : IRequest<int>
    {
        public int N;

        public CalculateFibonacciQuery(int n)
        {
            N = n;
        }

        public class CalculateFibonacciQueryHandler : IRequestHandler<CalculateFibonacciQuery, int>
        {
            private IMediator _mediator;

            public CalculateFibonacciQueryHandler(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task<int> Handle(CalculateFibonacciQuery request, CancellationToken cancellationToken)
            {
                if (request.N <= 0)
                    throw new Exception("Can't calculate Fibonacci for number less than 0");

                var fibonacciSequence = new List<int>();
                fibonacciSequence.Add(0);
                if (request.N > 1)
                    fibonacciSequence.Add(1);

                for (int i = 2; i < request.N; ++i)
                    fibonacciSequence.Add(fibonacciSequence[i - 1] + fibonacciSequence[i - 2]);

                await _mediator.Publish(new SaveCalculationResultCommand("fibonacci", fibonacciSequence[request.N - 1]));

                return fibonacciSequence[request.N - 1];
            }
        }
    }
}