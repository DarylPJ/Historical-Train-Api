using HistoricalTrainApi.Models;
using HistoricalTrainApi.Models.HistoricService.ServiceDetails;
using HistoricalTrainApi.Models.HistoricService.ServiceMetrics;
using System.Collections.Generic;

namespace HistoricalTrainApi.IntegrationTests
{
    public static class HistoricalServiceMockData
    {
        public static ServiceMetricsResponse GetServiceMetricsResponse() =>
            new ServiceMetricsResponse
            {
                Header = new Header
                {
                    FromLocation = "CFL",
                    ToLocation = "LDS"
                },
                Services =
                {
                    new Service
                    {
                        ServiceAttributesMetrics = new ServiceAttributesMetrics
                        {
                            OriginLocation = "SKI",
                            DestinationLocation = "LDS",
                            TimetabledDeparture = "0719",
                            TimetabledArrival = "0744",
                            TrainOperator = "NT",
                            MatchedServices = 1,
                            Rids =
                            {
                                "202103257842095"
                            }
                        },
                        Metrics =
                        {
                            new Metric
                            {
                                ToleranceValue = 0,
                                NumberOutsideTolerance = 0,
                                NumberInsideTolerance = 1,
                                PercentageInsideTolerance = 100,
                                GlobalTolerance = true
                            }
                        }
                    }
                }
            };

        public static ServiceDetailsResponse GetServiceDetailsResponse() =>
            new ServiceDetailsResponse
            {
                ServiceAttributesDetails = new ServiceAttributesDetails
                {
                    DateOfService = "2021-03-25",
                    TrainOperator = "NT",
                    Rid = "202103257842095",
                    Locations =
                    {
                        new LocationDetail
                        {
                            Location = "SKI",
                            TimetabledDeparture = "0701",
                            TimetabledArrival = "",
                            ActualDeparture = "0701",
                            ActualArrival = "",
                            DelayReason = ""
                        },
                        new LocationDetail
                        {
                            Location = "CEY",
                            TimetabledDeparture = "0706",
                            TimetabledArrival = "0706",
                            ActualDeparture = "0705",
                            ActualArrival = "0704",
                            DelayReason = ""
                        },
                        new LocationDetail
                        {
                            Location = "SON",
                            TimetabledDeparture = "0711",
                            TimetabledArrival = "0711",
                            ActualDeparture = "0710",
                            ActualArrival = "0709",
                            DelayReason = ""
                        },
                        new LocationDetail
                        {
                            Location = "KEI",
                            TimetabledDeparture = "0715",
                            TimetabledArrival = "0715",
                            ActualDeparture = "0715",
                            ActualArrival = "0714",
                            DelayReason = ""
                        },
                        new LocationDetail
                        {
                            Location = "CFL",
                            TimetabledDeparture = "0719",
                            TimetabledArrival = "0719",
                            ActualDeparture = "0718",
                            ActualArrival = "0717",
                            DelayReason = ""
                        },
                        new LocationDetail
                        {
                            Location = "BIY",
                            TimetabledDeparture = "0722",
                            TimetabledArrival = "0721",
                            ActualDeparture = "0721",
                            ActualArrival = "0720",
                            DelayReason = ""
                        },
                        new LocationDetail
                        {
                            Location = "SAE",
                            TimetabledDeparture = "0726",
                            TimetabledArrival = "0726",
                            ActualDeparture = "0725",
                            ActualArrival = "0725",
                            DelayReason = ""
                        },
                        new LocationDetail
                        {
                            Location = "SHY",
                            TimetabledDeparture = "0729",
                            TimetabledArrival = "0728",
                            ActualDeparture = "0729",
                            ActualArrival = "0728",
                            DelayReason = ""
                        },
                        new LocationDetail
                        {
                            Location = "APY",
                            TimetabledDeparture = "0734",
                            TimetabledArrival = "0733",
                            ActualDeparture = "0733",
                            ActualArrival = "0733",
                            DelayReason = ""
                        },
                        new LocationDetail
                        {
                            Location = "LDS",
                            TimetabledDeparture = "",
                            TimetabledArrival = "0744",
                            ActualDeparture = "",
                            ActualArrival = "0743",
                            DelayReason = ""
                        }
                    }
                }
            };

        public static IList<HistoricalRecord> GetExpectedApiResponse() => new List<HistoricalRecord>
        {
            new HistoricalRecord
            {
                OriginLocation = "SKI",
                DestinationLocation = "LDS",
                LocationData =
                {
                    ["CFL"] = new HistoricalData
                    {
                        TimetabledDeparture = "07:19",
                        TimetabledArrival = "07:19",
                        ActualDeparture = "07:18",
                        ActualArrival = "07:17",
                        DelayReason = ""
                    },
                    ["LDS"] = new HistoricalData
                    {
                        TimetabledDeparture = "",
                        TimetabledArrival = "07:44",
                        ActualDeparture = "",
                        ActualArrival = "07:43",
                        DelayReason = ""
                    }
                }
            }
        };
    }
}