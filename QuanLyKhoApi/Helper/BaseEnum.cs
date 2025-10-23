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

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum TRANGTHAI
        {
            [EnumMember(Value = "Chờ xử lý")]
            Pending = 1,
            [EnumMember(Value = "Hoàn thành")]
            Done = 2,
            [EnumMember(Value = "Hủy")]
            Cancel = 3
        }

        public enum PHIEU
        {
            Nhap,
            Xuat
        }
    }
}
