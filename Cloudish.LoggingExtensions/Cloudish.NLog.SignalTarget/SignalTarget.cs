using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

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
        public string APIKey { get; set; }
        #endregion

        #region TargetWithLayout Write implementation.
        /// <see cref="TargetWithLayout Write(LogEventInfo)"/>
        protected override void Write(LogEventInfo logEvent)
        {
            // Render the logging event to a string.
            string logMessage = this.Layout.Render(logEvent); 

            // Send the log message to the web service.
            Send(logMessage.ToString());

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
