using Google.Api;
using Google.Cloud.Logging.Type;
using Google.Cloud.Logging.V2;

namespace GoogleLogs4DotNet
{
    public class GoogleLogger
    {
        private readonly string _projectId;
        private readonly string _logId;
        private readonly LoggingServiceV2Client _loggingClient;

        public GoogleLogger(string ProjectId, string logId)
        {
            _projectId = ProjectId ?? "";
            _logId = logId ?? "";
            _loggingClient = LoggingServiceV2Client.Create();
        }

        public void LoggedToGoogleLog(LogSeverity logSeverity, string textPayload)
        {
            var logName = new LogName(_projectId, _logId);
            var resource = new MonitoredResource { Type = "global" };
            LogEntry logEntry = new LogEntry
            {
                InsertId = Guid.NewGuid().ToString(),
                LogName = logName.ToString(),
                Resource = resource,
                Severity = logSeverity,
                TextPayload = textPayload,
                Labels = { { "development", "dotnetcorewebapi" }, { "empEmail", "vbarnwal@abc.com" } }

            };
            _loggingClient.WriteLogEntries(logName, resource, null, new[] { logEntry });
        }
    }
}
