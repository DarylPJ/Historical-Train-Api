using HistoricalTrainApiModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HistoricalTrainApiRepositories
{
    public interface IHistoricServiceRepository
    {
        Task<IList<HistoricalRecord>> GetTrainTimes(
            DateTime startDate,
            DateTime endDate,
            string startLocation,
            string endLocation,
            CancellationToken cancellationToken);
    }
}