using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using SecLog.Models;

namespace SecLog.Contracts
{
    [ServiceContract]
    public interface ISecurityService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "LogEvent")]
        string PostLog(SecurityEvent newEvent);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetLogs")]
        List<SecurityEvent> GetLogs();
    }
}
