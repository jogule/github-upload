using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.ApplicationInsights;

namespace source.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private TelemetryClient aiClient;

        public IndexModel(ILogger<IndexModel> logger, TelemetryClient aiClient)
        {
            _logger = logger;
            this.aiClient = aiClient;
        }

        public void OnGet()
        {
            // Track an event
            this.aiClient.TrackEvent("CommentSubmitted");

            // Track an event with properties
            this.aiClient.TrackEvent("VideoUploaded", new Dictionary<string, string> { { "Category", "Sports" }, { "Format", "mp4" } });

            this.aiClient.GetMetric("SimultaneousPlays").TrackValue(new Random().Next(10));

            Metric userResponse = this.aiClient.GetMetric("UserResponses", "Kind");

            userResponse.TrackValue(24, "Likes");
            userResponse.TrackValue(5, "Loves");
        }
    }
}
