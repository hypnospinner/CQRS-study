using System.Collections.Generic;
using System.Threading;
using MediatR;
using CQRSStudy.Persistence;
using System.Threading.Tasks;
using System.Linq;

namespace CQRSStudy.Queries
{
    public class GetLatestCalculationsQuery : IRequest<List<string>>
    {
        public class GetLatestCalculationsQueryHandler : IRequestHandler<GetLatestCalculationsQuery, List<string>>
        {
            private ApplicationDb _db;

            public GetLatestCalculationsQueryHandler(ApplicationDb db)
            {
                _db = db;
            }

            public Task<List<string>> Handle(GetLatestCalculationsQuery request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_db.Results
                    .OrderByDescending(r => r.At.Year)
                    .ThenByDescending(r => r.At.Month)
                    .ThenByDescending(r => r.At.Day)
                    .Select(r => r.CalculationResultType + " " + r.Result.ToString())
                    .ToList());
            }
        }
    }
}