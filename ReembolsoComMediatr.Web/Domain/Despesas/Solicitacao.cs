using System;

namespace ReembolsoComMediatr.Web.Domain.Despesas
{
    public class Solicitacao
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime DataHora { get; private set; } = DateTime.Now;
        public Funcionario Funcionario { get; private set; }
        public decimal Valor { get; private set; }
        public string Descricao { get; private set; }
        public Status Status { get; private set; }

        public Solicitacao() { }

        public Solicitacao(Funcionario funcionario, decimal valor, string descricao)
        {
            this.Funcionario = funcionario;
            this.Valor = valor;
            this.Descricao = descricao;
            this.Status = Status.EmAberto;
        }
    }
}
