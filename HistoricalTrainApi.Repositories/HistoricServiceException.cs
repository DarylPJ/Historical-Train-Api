using System;

namespace HistoricalTrainApi.Repositories
{
    public class HistoricServiceException: Exception
    {
        public HistoricServiceException() : base()
        { }

        public HistoricServiceException(string message) : base(message)
        { }

        public HistoricServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
