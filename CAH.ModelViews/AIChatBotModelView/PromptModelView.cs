using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAH.ModelViews.AIChatBotModelView
{
    public class PromptModelView
    {
        public string Prompt { get; set; }
        public List<ChatMessage>? ChatHistory { get; set; }
    }
}
