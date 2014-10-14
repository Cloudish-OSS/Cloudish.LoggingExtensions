using System;
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
        public string StackTrace { get; set; }

        [JsonProperty(PropertyName = "methodName")]
        public string MethodName { get; set; }

        [JsonProperty(PropertyName = "stackFrame")]
        public string StackFrame { get; set; }

        [JsonProperty(PropertyName = "exception")]
        public string Exception { get; set; }

        [JsonProperty(PropertyName = "logger")]
        public string Logger { get; set; }
    }
}
