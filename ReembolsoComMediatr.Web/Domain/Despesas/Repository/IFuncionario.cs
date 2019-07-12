using System.Threading;
using System.Threading.Tasks;

namespace ReembolsoComMediatr.Web.Domain.Despesas.Repository
{
    public interface IFuncionarioRead
    {
        Task<Funcionario> ObterPorId(string id, CancellationToken cancellationToken);
    }
}
