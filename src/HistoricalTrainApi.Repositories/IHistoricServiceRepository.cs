using HistoricalTrainApi.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HistoricalTrainApi.Repositories
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