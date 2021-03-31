using System.Runtime.Serialization;

namespace HistoricalTrainApi.Models.HistoricService.ServiceMetrics
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
