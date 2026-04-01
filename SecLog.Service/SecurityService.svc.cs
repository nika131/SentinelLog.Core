using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SecLog.Contracts;
using SecLog.Models;
using SecLog.Data;
using System.Linq.Expressions;
using System.Net;
using System.ServiceModel.Web;
using System.Configuration;

namespace SecLog.Service 
{
    public class SecurityService : ISecurityService
    {
        public string PostLog(SecurityEvent newEvent)
        {
            var incomingRequest = WebOperationContext.Current.IncomingRequest;
            string clientKey = incomingRequest.Headers["X-API-KEY"];

            string ValidKey = ConfigurationManager.AppSettings["SentinelApiKey"];

            if (clientKey != ValidKey)
            {
                if (WebOperationContext.Current != null)
                {
                    WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Unauthorized;
                }
                return "Error: Unauthorized - Invalid API Key";
            }

            if (newEvent == null) return "Error: Request is empty";
            if (string.IsNullOrEmpty(newEvent.SourceSystem)) return "Error: Source System is requiered";
            if (string.IsNullOrEmpty(newEvent.IpAddress)) return "Error: IP adress is missing";

            try
            {
                using (var db = new SecLogContext())
                {
                    db.SecurityEvents.Add(newEvent);
                    db.SaveChanges();
                    return "Succes: Log event saved";
                }
            }
            catch (Exception ex)
            {
                return $"Database error: {ex.Message}";
            }
        }

        public List<SecurityEvent> GetLogs()
        {
            using (var db = new SecLogContext())
            {
                return db.SecurityEvents.OrderByDescending(x => x.Timestamp).ToList();
            }
        }
    }
}
