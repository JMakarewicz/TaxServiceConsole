using Newtonsoft.Json;

namespace Common
{
    //Lightweight wrapper for the Newtonsoft lightweight wrapper.  Done this way for reusability by other
    //components as needed.  Otherwise would need to be placed in the client (hard to justify) or with each
    //implementation (code duplication).
    public static class JsonDeserializer
    {
        public static T Deserialize<T>(string response)
        {
            T result = JsonConvert.DeserializeObject<T>(response);
            return result;
        }
    }
}
