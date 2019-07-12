using MediatR;
using System;

namespace ReembolsoComMediatr.Web.Domain.Despesas.Commands.Atualizar
{
    public class Request : Validatable, IRequest<Result>
    {
        public Guid SolicitacaoId { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }

        public override void Validate()
        {
            if (SolicitacaoId.Equals(Guid.Empty))
                AddNotification("SolicitacaoId", "Solicitação Inválida");

            if (Valor <= 0)
                AddNotification("Valor", "Valor inválido");

            if (string.IsNullOrEmpty(Descricao))
                AddNotification("Descricao", "Descrição é obrigatória");
        }
    }
}
