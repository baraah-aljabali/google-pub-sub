using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google.PubSub.Publisher
{
    /// <summary>
    /// Provides programmatic configuration for the CAP Google Cloud Platform Pub/Sub project.
    /// </summary>
    public class GooglePublisherOptions
    {
        /// <summary>
        /// The GCP <c>Project</c> ID.
        /// </summary>
        public string ProjectId { get; set; }

        public string TopiId { get; set; } = "my-topic";
    }
}
