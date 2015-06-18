using Newtonsoft.Json;

namespace NPushOver.ResponseObjects
{
    public class ListMessagesUser
    {
        [JsonProperty("quiet_hours")]
        public bool QuietHours { get; set; }

        [JsonProperty("is_android_licensed")]
        public bool IsAndroidLicensed { get; set; }

        [JsonProperty("is_ios_licensed")]
        public bool IsIosLicensed { get; set; }

        [JsonProperty("is_desktop_licensed")]
        public bool IsDesktopLicensed { get; set; }
    }
}
