using System;
using Application.Interfaces;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;

namespace Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly MailgunSettings _emailOptions;
        public EmailSender(IOptions<MailgunSettings> emailOptions)
        {
            this._emailOptions = emailOptions.Value;
        }

        public string SendEmail(string email, string subject, string htmlMessage)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri(_emailOptions.ApiBaseUri);
            client.Authenticator = new HttpBasicAuthenticator("api", _emailOptions.ApiKey);

            RestRequest request = new RestRequest();
            request.AddParameter("domain", _emailOptions.RequestUri, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", _emailOptions.From);
            //request.AddParameter("to", "sandstream107@gmail.com");
            //request.AddParameter("to", "jom@gapuz.me");
            request.AddParameter("to", "i.ekzeed.av@gmail.com");
            request.AddParameter("subject", subject);
            request.AddParameter("html", htmlMessage);
            request.Method = Method.POST;
            var response = client.Execute(request);

            return response.StatusCode.ToString();
            // throw new System.NotImplementedException();
        }
    }
}