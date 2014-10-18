using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Cloudish.NLog.SignalTarget
{
    public class Signal
    {
        [JsonProperty(PropertyName = "sequenceId")]
        public int SequenceId { get; set; }

        [JsonProperty(PropertyName = "@timestamp")]
        public DateTime TimeStamp { get; set; }

        [JsonProperty(PropertyName = "logLevel")]
        public string LogLevel { get; set; }

        [JsonProperty(PropertyName = "logLevelOrdinal")]
        public int LogLevelOrdinal { get; set; }

        [JsonProperty(PropertyName = "loggerName")]
        public string LoggerName { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "formattedMessage")]
        public string FormattedMessage { get; set; }

        [JsonProperty(PropertyName = "stackTrace")]
        public StackTrace StackTrace { get; set; }

        [JsonProperty(PropertyName = "exception")]
        public Exception Exception { get; set; }

        [JsonProperty(PropertyName = "logger")]
        public string Logger { get; set; }

        [JsonProperty(PropertyName = "rawMessage")]
        public string RawMessage { get; set; }
    }
}
