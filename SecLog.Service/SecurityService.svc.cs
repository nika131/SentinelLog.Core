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

namespace SecLog.Service 
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SecurityService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SecurityService.svc or SecurityService.svc.cs at the Solution Explorer and start debugging.
    public class SecurityService : ISecurityService
    {
        public string PostLog(SecurityEvent newEvent)
        {
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
