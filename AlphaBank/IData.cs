﻿using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace AlphaBank
{
    [ServiceContract]
    public interface IData
    {
        [OperationContract(Name = "TestConnection")]
        [WebGet(UriTemplate = "TestConnection", RequestFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        bool TestConnection();
    }
}