using HistoricalTrainApiModels.HistoricService.ServiceMetrics;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HistoricalTrainApiRepositories
{
    public interface IHistoricServiceRepository
    {
        Task<ServiceMetricsResponse> GetTrainTimes(
            DateTime startDate,
            DateTime endDate,
            string fromLocation,
            string toLocation,
            CancellationToken cancellationToken);
    }
}