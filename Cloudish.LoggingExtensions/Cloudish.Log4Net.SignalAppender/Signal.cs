using System;
using Newtonsoft.Json;

namespace Cloudish.Log4Net.SignalAppender
{
    public class Signal
    {
        [JsonProperty(PropertyName = "@timestamp")]
        public DateTime TimeStamp { get; set; }

        [JsonProperty(PropertyName = "logLevel")]
        public string LogLevel { get; set; }

        [JsonProperty(PropertyName = "loggerName")]
        public string LoggerName { get; set; }

        [JsonProperty(PropertyName = "appDomain")]
        public string AppDomain { get; set; }

        [JsonProperty(PropertyName = "identity")]
        public string Identity { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "exception")]
        public Exception Exception { get; set; }

        [JsonProperty(PropertyName = "logger")]
        public string Logger { get; set; }

        [JsonProperty(PropertyName = "rawMessage")]
        public string RawMessage { get; set; }
    }
}
