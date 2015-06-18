using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NPushOver.Converters;
using NPushOver.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPushOver.ResponseObjects
{
    public class ListMessagesResponse : PushoverResponse
    {
        [JsonProperty("messages")]
        public StoredMessage[] Messages { get; set; }

        //TODO: This user "conflicts" with the base PushoverResponse object BUG? Report?
        [JsonProperty("user")]
        public ListMessagesUser User { get; set; }
    }
}
