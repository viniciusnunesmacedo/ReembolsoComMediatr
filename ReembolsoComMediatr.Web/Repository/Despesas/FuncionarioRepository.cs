using ReembolsoComMediatr.Web.Domain.Despesas;
using ReembolsoComMediatr.Web.Domain.Despesas.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReembolsoComMediatr.Web.Repository.Despesas
{
    public class FuncionarioRepository : IFuncionarioRead
    {
        private static List<Funcionario> FUNCIONARIOS = new List<Funcionario>
        {
            new Funcionario
            {
                Nome = "João da Silva",
                Id = "DSfdlgjdfGHrthn"
            },

            new Funcionario
            {
                Nome = "Maria da Silva",
                Id = ""
            }
        };

        public async Task<Funcionario> ObterPorId(string id, CancellationToken cancellation)
            => await Task.Run(() => FUNCIONARIOS.FirstOrDefault(f => f.Id == id));
    }
}
