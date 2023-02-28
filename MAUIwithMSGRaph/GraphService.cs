using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MAUIwithMSGRaph
{
    internal class GraphService
    {
        private readonly string[] _scopes = new[] { "User.Read" };
        private const string ClientId = "786c9d7b-d4f1-408a-bb5d-6f86454adbe1";
        private const string TenantId = "b35bcac0-6e4b-4e0c-af1b-e0dcf6470832";
        private GraphServiceClient _client;

        public GraphService()
        {
            Initialize();
        }

        private void Initialize()
        {
            // assume Windows for this sample
            if (OperatingSystem.IsWindows())
            {
                var options = new InteractiveBrowserCredentialOptions
                {
                    ClientId = ClientId,
                    TenantId = TenantId,
                    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
                    RedirectUri = new Uri("https://login.microsoftonline.com/common/oauth2/nativeclient"),
                };

                InteractiveBrowserCredential interactiveCredential = new(options);
                _client = new GraphServiceClient(interactiveCredential, _scopes);
            }
            else
            {
                // TODO: Add iOS/Android support
            }


        }

        public async Task<User> GetMyDetailsAsync()
        {
            try
            {
                return await _client.Me.GetAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading user details: {ex}");
                return null;
            }
        }
    }
}
