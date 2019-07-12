using MediatR;
using System;

namespace ReembolsoComMediatr.Web.Domain.Despesas.Commands.Atualizar
{
    public class Notification : INotification
    {
        public string NomeFuncionario { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorAntigo { get; set; }
        public string Descricao { get; set; }
        public string DescricaoAntiga { get; set; }
        public DateTime DataHora { get; set; } = DateTime.Now;

        public override string ToString()
         => $"Nova despesa atualizada por {NomeFuncionario} no dia {DataHora} no valor de R${Valor} com a descrição \"{Descricao}\"";
    }
}
