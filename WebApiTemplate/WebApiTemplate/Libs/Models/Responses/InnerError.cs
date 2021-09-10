using System.Collections.Generic;

namespace WebApiTemplate.Libs
{
    public class InnerError
    {
        public List<string> Messages { get; set; }

        public InnerError(List<string> messages)
        {
            Messages = messages;
        }
    }
}
