using System;
using System.Text.Json.Serialization;

namespace TaskTrackerCLI
{
    public class TaskItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = "todo";

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Description} - {Status} (Created: {CreatedAt})";
        }
    }
}
