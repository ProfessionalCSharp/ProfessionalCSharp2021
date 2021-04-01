namespace ChatClient.Services
{
    public class UrlService
    {
        private const string BaseUri = "http://localhost:3509";
        public string ChatAddress => $"{BaseUri}/chat";
        public string GroupAddress => $"{BaseUri}/groupchat";
    }
}
