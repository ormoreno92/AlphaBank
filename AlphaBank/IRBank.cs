using System.ServiceModel;
using System.ServiceModel.Web;

namespace AlphaBank
{
    [ServiceContract]
    public interface IRBank
    {
        [OperationContract(Name = "ReleaseData")]
        [WebGet(UriTemplate = "ReleaseData", RequestFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        bool ReleaseData();
    }
}
