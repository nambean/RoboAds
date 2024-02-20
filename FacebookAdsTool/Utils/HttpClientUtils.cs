using FacebookAdsTool.Entity;
using Newtonsoft.Json;
using RestSharp;

namespace FacebookAdsTool.Utils
{
    class HttpClientUtils
    {
        private RestClientOptions options = new RestClientOptions("https://ads0806.com/api") { MaxTimeout = -1 };
        //private RestClientOptions options = new RestClientOptions("http://localhost:3000/api") { MaxTimeout = -1 };
        private static RestClient _client;

        private static HttpClientUtils _instance;
        public static HttpClientUtils Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HttpClientUtils();
                return _instance;
            }
        }

        public HttpClientUtils()
        {
            if (_client == null)
                _client = new RestClient(options);
        }

        public RestResponse Login(string userName, string password)
        {
            var userLogin = new UserLogin() { userName = userName, password = password };
            var body = JsonConvert.SerializeObject(userLogin);

            var request = new RestRequest("/user/login", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddStringBody(body, DataFormat.Json);

            return _client.Execute(request);
        }

        public RestResponse PostCookieStatus(string url, int idCookie, string license)
        {
            var accessToken = RegistryUtil.GetConfigValueFromRegistry(RegistryType.AccessToken);
            var cookieUsing = new CookieUsing() { id = idCookie, licenceKey = license };
            var body = JsonConvert.SerializeObject(cookieUsing);

            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", string.Format("Bearer {0}", accessToken));
            request.AddStringBody(body, DataFormat.Json);


            return _client.Execute(request);
        }

        public RestResponse GetCurrentUser()
        {
            var accessToken = RegistryUtil.GetConfigValueFromRegistry(RegistryType.AccessToken);
            var request = new RestRequest("/user/getCurrent", Method.Get);
            request.Method = Method.Get;
            request.AddHeader("Authorization", string.Format("Bearer {0}", accessToken));

            return _client.Execute(request);
        }

        public RestResponse GetWSFromChrome()
        {
            var options = new RestClientOptions("http://localhost:9222") { MaxTimeout = -1 };
            var client = new RestClient(options);
            var request = new RestRequest("/json/version", Method.Get);

            return client.Execute(request);
        }

        public RestResponse PostProcessor(string url, int _currentPage, int pageSize)
        {
            var accessToken = RegistryUtil.GetConfigValueFromRegistry(RegistryType.AccessToken);
            var request = new RestRequest(url, Method.Get);
            request.AddQueryParameter("page", _currentPage);
            request.AddQueryParameter("limit", pageSize);
            request.AddHeader("Authorization", string.Format("Bearer {0}", accessToken));
            request.RequestFormat = DataFormat.Json;


            return _client.Execute(request);
        }

        public RestResponse Save2Fa(string url, TwoFa twoFa)
        {
            var accessToken = RegistryUtil.GetConfigValueFromRegistry(RegistryType.AccessToken);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", string.Format("Bearer {0}", accessToken));
            request.AddStringBody(JsonConvert.SerializeObject(twoFa), DataFormat.Json);

            return _client.Execute(request);
        }


        public RestResponse StopRunningCookie(int idCookie)
        {
            var cookieUsing = new CookieUsing() { id = idCookie };

            var accessToken = RegistryUtil.GetConfigValueFromRegistry(RegistryType.AccessToken);
            var request = new RestRequest("/cookie/stopRunningCookie", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", string.Format("Bearer {0}", accessToken));
            request.AddStringBody(JsonConvert.SerializeObject(cookieUsing), DataFormat.Json);

            return _client.Execute(request);
        }

        public class UserLogin
        {
            public string userName { get; set; }
            public string password { get; set; }
        }
    }
}
