using System.Text;
using Newtonsoft.Json;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Targets;
using System;
using System.Net;

namespace Cloudish.NLog.SignalTarget
{
    [Target("SignalTarget")]
    public sealed class SignalTarget : TargetWithLayout
    {
        #region Public instance properties.
        /// <summary>
        /// Gets and sets the URL of the service which the appender will connect to.
        /// </summary>
        [RequiredParameter]
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Gets and sets the Cloudish API Key.
        /// </summary>
        [RequiredParameter]
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets and sets the Cloudish signal type.
        /// </summary>
        [RequiredParameter]
        public string SignalType { get; set; }

        /// <summary>
        /// Gets and sets the tags to add to all signals.
        /// </summary>
        [RequiredParameter]
        public string Tags { get; set; }

        #endregion

        #region TargetWithLayout Write implementation.

        /// <see>
        ///     <cref>TargetWithLayout Write(AsyncLogEventInfo[])</cref>
        /// </see>
        protected override void Write(AsyncLogEventInfo[] logEvents) {
            var bulkSignals = new StringBuilder();

            foreach (var logEvent in logEvents) {
                var eventInfo = logEvent.LogEvent;
                var signal = new Signal() {
                    Logger = "NLog",
                    SequenceId = eventInfo.SequenceID,
                    TimeStamp = eventInfo.TimeStamp,
                    LogLevel = eventInfo.Level.Name,
                    LogLevelOrdinal = eventInfo.Level.Ordinal,
                    LoggerName = eventInfo.LoggerName,
                    Message = eventInfo.Message,
                    FormattedMessage = eventInfo.FormattedMessage,
                    StackTrace = eventInfo.HasStackTrace ? eventInfo.StackTrace.ToString() : null,
                    MethodName = eventInfo.HasStackTrace ? eventInfo.UserStackFrame.GetMethod().ToString() : null,
                    StackFrame =
                        eventInfo.HasStackTrace
                            ? eventInfo.StackTrace.GetFrame(eventInfo.UserStackFrameNumber).ToString()
                            : null,
                    Exception = eventInfo.Exception != null ? eventInfo.Exception.ToString() : null
                };

                bulkSignals.Append(string.Concat(JsonConvert.SerializeObject(signal), "\n"));
            }

            var url = new Uri(string.Format("{0}/{1}/{2}/{3}", ServiceUrl, ApiKey, SignalType, Tags));

            var client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            client.Headers.Add("cloudish-bulk", "true");
            client.UploadStringAsync(url, "POST", bulkSignals.ToString());
        }

        #endregion
    }
}
