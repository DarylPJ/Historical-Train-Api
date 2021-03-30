using System.Runtime.Serialization;

namespace HistoricalTrainApiModels.HistoricService.ServiceMetrics
{
    public enum Days
    {
        [EnumMember(Value = "WEEKDAY")]
        Weekday,

        [EnumMember(Value = "SATURDAY")]
        Saturday,

        [EnumMember(Value = "SUNDAY")]
        Sunday
    }
}
