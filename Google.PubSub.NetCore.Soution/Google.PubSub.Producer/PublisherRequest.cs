using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google.PubSub.Publisher
{
    public class PublisherRequest
    {
        public IEnumerable<string> messageTexts { get; set; }
    }
}
