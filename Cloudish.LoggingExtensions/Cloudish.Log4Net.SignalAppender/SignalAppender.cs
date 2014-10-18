using log4net.Appender;
using log4net.Core;
using System;
using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace Cloudish.Log4Net.SignalAppender
{
    public class SignalAppender: AppenderSkeleton
    {
        #region Public instance properties.
        /// <summary>
        /// Gets and sets the URL of the service which the appender will connect to.
        /// </summary>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Gets and sets the Cloudish Api Key.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets and sets the required signal type.
        /// </summary>
        public string SignalType { get; set; }

        /// <summary>
        /// Gets and sets the tags to append to the signal.
        /// </summary>
        public string Tags { get; set; }

        #endregion

        #region AppenderSkeleton implementation.
        /// <see cref="AppenderSkeleton.Append(LoggingEvent)"/>
        protected override void Append(LoggingEvent loggingEvent)
        {
            // Render the logging event to a string.
            var rawMessage = RenderLoggingEvent(loggingEvent);
            var signal = new Signal()
            {
                Logger = "log4net",
                AppDomain = loggingEvent.Domain,
                Identity = loggingEvent.Identity,
                TimeStamp = loggingEvent.TimeStamp,
                LogLevel = loggingEvent.Level.DisplayName,
                LoggerName = loggingEvent.LoggerName,
                Message = loggingEvent.RenderedMessage,
                Exception = loggingEvent.ExceptionObject,
                RawMessage = rawMessage
            };

            // Send the log message to the web service.
            try
            {
                Send(JsonConvert.SerializeObject(signal), false);
            }
            catch (Exception e)
            {
                ErrorHandler.Error("An error occurred while connecting to the Cloudish logging service.", e);
            }

        }

        /// <see cref="AppenderSkeleton.Append(LoggingEvent[])"/>
        protected override void Append(LoggingEvent[] loggingEvents)
        {
            // Prepare a StringBuilder to write the messages to.
            var logMessages = new StringBuilder();

            //TODO: In the future this will call bulk send
            // Render each logging event to a string, separating events with a new line.
            foreach (var logEvent in loggingEvents)
            {
                var rawMessage = RenderLoggingEvent(logEvent);
                var signal = new Signal()
                {
                    Logger = "log4net",
                    AppDomain = logEvent.Domain,
                    Identity = logEvent.Identity,
                    TimeStamp = logEvent.TimeStamp,
                    LogLevel = logEvent.Level.DisplayName,
                    LoggerName = logEvent.LoggerName,
                    Message = logEvent.RenderedMessage,
                    Exception = logEvent.ExceptionObject,
                    RawMessage = rawMessage
                };

                logMessages.Append(string.Concat(JsonConvert.SerializeObject(signal), "\n"));
            }

            // Send the log message to the web service.
            try
            {
                Send(logMessages.ToString(), true);
            }
            catch (Exception e)
            {
                ErrorHandler.Error("An error occurred while connecting to the Cloudish logging service while trying to bulk log.", e);
            }
        }
        #endregion

        #region Internal logging handler.

        /// <summary>
        /// Sends the log message to the Cloudish logging service.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="isBulk">Indicates that the message is bulk and if true, adds the "cloudish-bulk" header</param>
        protected void Send(string message, bool isBulk)
        {
            var url = new Uri(string.Format("{0}/{1}/{2}/{3}", ServiceUrl, ApiKey, SignalType, Tags));

            var client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            if (isBulk) {
                client.Headers.Add("cloudish-bulk", "true");
            }

            client.UploadStringAsync(url, "POST", message);
        }

        #endregion
    }
}
