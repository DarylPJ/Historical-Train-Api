using HistoricalTrainApiModels.HistoricService.ServiceDetails;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HistoricalTrainApiRepositories
{
    public interface IHistoricServiceRepository
    {
        Task<IList<LocationDetail>> GetTrainTimes(
            DateTime startDate,
            DateTime endDate,
            string fromLocation,
            string toLocation,
            CancellationToken cancellationToken);
    }
}