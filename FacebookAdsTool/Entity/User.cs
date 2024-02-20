
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FacebookAdsTool.Entity
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Menu
    {
        public int id { get; set; }
        public string title { get; set; }
        public string route { get; set; }
        public string icon { get; set; }
        public string iconSVG { get; set; }
        public string parent { get; set; }
    }

    public class UserLoginResponse
    {
        public string accessToken { get; set; }
        public List<Menu> menus { get; set; }
        public UserInfo userInfo { get; set; }
    }

    public class UserInfo
    {
        public string userName { get; set; }
        public string fullName { get; set; }
        public string picture { get; set; }
        public string roleName { get; set; }
        public bool isAdmin { get; set; }
    }

    public class WSChrome
    {
        public string Browser { get; set; }

        [JsonProperty("Protocol-Version")]
        public string ProtocolVersion { get; set; }

        [JsonProperty("User-Agent")]
        public string UserAgent { get; set; }

        [JsonProperty("V8-Version")]
        public string V8Version { get; set; }

        [JsonProperty("WebKit-Version")]
        public string WebKitVersion { get; set; }
        [JsonProperty("webSocketDebuggerUrl")]
        public string WebSocketDebuggerUrl { get; set; }
    }
}
