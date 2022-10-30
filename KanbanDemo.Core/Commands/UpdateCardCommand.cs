using System;

namespace KanbanDemo.Core.Commands
{
    public class UpdateCardCommand
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public string Lista { get; set; }
    }
}
