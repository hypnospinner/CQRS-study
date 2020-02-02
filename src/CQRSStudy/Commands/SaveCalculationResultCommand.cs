using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using CQRSStudy.Persistence;

namespace CQRSStudy.Commands
{
    public class SaveCalculationResultCommand : INotification
    {
        public string CalculationResultType;
        public int CalculationResult;

        public SaveCalculationResultCommand(string calculationResultType, int calculationResult)
        {
            CalculationResultType = calculationResultType;
            CalculationResult = calculationResult;
        }

        public class SaveCalculationResultCommandHandler : INotificationHandler<SaveCalculationResultCommand>
        {
            private ApplicationDb _db;

            public SaveCalculationResultCommandHandler(ApplicationDb db)
            {
                _db = db;
            }

            public Task Handle(SaveCalculationResultCommand notification, CancellationToken cancellationToken)
            {
                _db.Results.Add(new CalculationResult
                {
                    CalculationResultType = notification.CalculationResultType,
                    Result = notification.CalculationResult,
                    At = DateTime.UtcNow
                });

                return Task.CompletedTask;
            }
        }
    }
}