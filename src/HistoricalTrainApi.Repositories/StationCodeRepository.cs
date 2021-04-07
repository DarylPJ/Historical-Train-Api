using LumenWorks.Framework.IO.Csv;
using System.Collections.Generic;
using System.IO;

namespace HistoricalTrainApi.Repositories
{
    public class StationCodeRepository : IStationCodeRepository
    {
        private readonly IDictionary<string, string> stationCodes = new Dictionary<string, string>();

        public StationCodeRepository()
        {
            using var csv = new CsvReader(new StreamReader("StationCodes.csv"), true);
            
            int fieldCount = csv.FieldCount;

            while (csv.ReadNextRecord())
            {
                for (int i = 0; i < fieldCount; i+=2)
                {
                    var name = csv[i];
                    var code = csv[i + 1];

                    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(code))
                    {
                        continue;
                    }

                    stationCodes[csv[i]] = csv[i + 1];
                }
            }
        }

        public StationCodeRepository(IDictionary<string, string> stationCodes)
        {
            this.stationCodes = stationCodes;
        }

        public IDictionary<string, string> GetStationCodes() => stationCodes;
    }
}
