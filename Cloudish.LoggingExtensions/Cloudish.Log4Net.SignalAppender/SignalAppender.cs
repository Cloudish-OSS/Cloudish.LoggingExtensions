using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

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
        /// Gets and sets the Cloudish API Key.
        /// </summary>
        public string APIKey { get; set; }
        #endregion

        #region AppenderSkeleton implementation.
        /// <see cref="AppenderSkeleton.Append(LoggingEvent)"/>
        protected override void Append(LoggingEvent loggingEvent)
        {
            // Render the logging event to a string.
            string logMessage = RenderLoggingEvent(loggingEvent);

            // Send the log message to the web service.
            try
            {
                Send(logMessage.ToString());
            }
            catch (Exception e)
            {
                ErrorHandler.Error("An error occurred while connecting to the logging service.", e);
            }

        }

        /// <see cref="AppenderSkeleton.Append(LoggingEvent[])"/>
        protected override void Append(LoggingEvent[] loggingEvents)
        {
            // Prepare a StringBuilder to write the messages to.
            StringBuilder logMessages = new StringBuilder();

            // Render each logging event to a string, separating events with a new line.
            foreach (LoggingEvent loggingEvent in loggingEvents)
                logMessages.Append(RenderLoggingEvent(loggingEvent)).AppendLine();

            // Send the log message to the web service.
            try
            {
                Send(logMessages.ToString());
            }
            catch (Exception e)
            {
                ErrorHandler.Error("An error occurred while connecting to the logging service.", e);
            }
        }
        #endregion

        #region Internal logging handler.
        /// <summary>
        /// Sends the log message to the Cloudish logging service.
        /// </summary>
        /// <param name="message">The message to log.</param>
        protected void Send(string message)
        {
            var url = new Uri(string.Concat(ServiceUrl, APIKey));

            var client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            client.UploadStringAsync(url, "POST", message);
        
        }
        #endregion
    }
}
