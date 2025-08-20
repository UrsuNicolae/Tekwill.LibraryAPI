using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace LibraryBot.DTOs.OpenAi
{
    public class OpenAiResponse
    {
        public string Id { get; set; }

        public List<OpenAiChoices> Choices { get; set; }
    }

    public class OpenAiChoices
    {
        public OpenAiChoicesMessage Message { get; set; }
    }

    public class OpenAiChoicesMessage
    {
        public string Content { get; set; }
    }
}
