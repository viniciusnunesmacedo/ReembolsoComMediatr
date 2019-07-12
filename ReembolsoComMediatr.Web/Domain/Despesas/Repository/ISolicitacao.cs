using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ReembolsoComMediatr.Web.Domain.Despesas.Repository
{
    public interface ISolicitacaoWrite
    {
        Task Inserir(Solicitacao solicitacao, CancellationToken cancellationToken);
        Task Atualizar(Guid id, decimal valor, string descricao, CancellationToken cancellationToken);
    }

    public interface ISolicitacaoRead
    {
        Task<IList<Solicitacao>> ListarPorFuncionarioId(string id, CancellationToken cancellationToken);
        Task<IList<Solicitacao>> ListarPorStatus(Status status, CancellationToken cancellationToken);
        Task<Solicitacao> ObterPorId(Guid id, CancellationToken cancellationToken);
    }
}
