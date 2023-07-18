using System.Text.Json;
using System.Text.Json.Serialization;

namespace RMPortal.WebServer.Helpers
{
    /// <summary>
    /// 日期格式化
    /// </summary>
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if(DateTime.TryParse(reader.GetString(),out DateTime value))
                    return value;
            }
            return reader.GetDateTime();
        }
        
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            //日期格式化，解决前端传入后端时，相差8小时的问题
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
