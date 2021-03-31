using System.Collections.Generic;

namespace HistoricalTrainApi.Repositories
{
    public interface IStationCodeRepository
    {
        IDictionary<string, string> GetStationCodes();
    }
}