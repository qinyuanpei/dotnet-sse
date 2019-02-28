using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet_SSE.Models
{
    [Serializable]
    public class Message
    {
        public string Content { get; set; }
        public DateTime Time { get; set; }
    }
}
