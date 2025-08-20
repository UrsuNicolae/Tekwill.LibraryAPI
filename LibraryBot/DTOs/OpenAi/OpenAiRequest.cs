namespace LibraryBot.DTOs.OpenAi
{
    public class OpenAiRequest
    {
        public string Model { get; set; }

        public double Temperature { get; set; }

        public OpenAiMessage[] Messages { get; set; }

        //public int MaxTokens { get; set; }
    }

    public class OpenAiMessage
    {
        public string Role { get; set; }

        public string Content { get; set; }
    }


}
