using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace QuanLyKhoApi.Helper
{
    public class BaseEnum
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum GIOITINH
        {
            [EnumMember(Value = "Nam")]
            Nam,
            [EnumMember(Value = "Nữ")]
            Nu
        }

    }
}
