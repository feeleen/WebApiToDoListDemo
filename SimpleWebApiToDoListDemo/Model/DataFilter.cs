using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SimpleWebApiToDoListDemo.Model
{
    public class DataFilter
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [EnumDataType(typeof(ItemState))]
        public ItemState State { get; set; } = ItemState.All;

        public DateTime DateCreatedFrom { get; set; } = DateTime.MinValue;
        public DateTime DateCreatedTo { get; set; } = DateTime.MaxValue;
    }

    public enum ItemState
    {
        [EnumMember(Value = nameof(All))]
        All,
        [EnumMember(Value = nameof(Completed))]
        Completed,
        [EnumMember(Value = nameof(NotCompleted))]
        NotCompleted
    }
}
