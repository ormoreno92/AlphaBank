﻿using Logic;
using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AlphaBank
{
    public class RebAuthManager : ServiceAuthorizationManager
    {
        private readonly AuthSessions _auth = new AuthSessions();

        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            try
            {
                var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
                var timestamp = WebOperationContext.Current.IncomingRequest.Headers["timestamp"];
                var publicKey = WebOperationContext.Current.IncomingRequest.Headers["publicKey"];
                var hash = WebOperationContext.Current.IncomingRequest.Headers["hash"];
                var adminKey = WebOperationContext.Current.IncomingRequest.Headers["adminKey"];
                if ((authHeader != null) && (authHeader != string.Empty))
                {
                    var svcCredentials = Encoding.ASCII
                        .GetString(Convert.FromBase64String(authHeader.Substring(6)))
                        .Split(':');
                    return _auth.AuthorizeAccess(svcCredentials, timestamp, publicKey, hash, adminKey);
                }
            }
            catch (Exception Ex)
            {
            }
            WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"AlphaBankAuthService\"");
            throw new WebFaultException(HttpStatusCode.Unauthorized);
        }
    }
}