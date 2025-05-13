using Newtonsoft.Json;

namespace FirebaseWrapper
{
    public class FirebaseAuthResponse
    {
        [JsonProperty("idToken")] public string IdToken { get; set; }
        [JsonProperty("localId")] public string LocalId { get; set; }
    }
}