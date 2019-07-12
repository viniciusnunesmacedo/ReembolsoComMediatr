using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ReembolsoComMediatr.Web.Domain.Despesas;
using ReembolsoComMediatr.Web.Domain.Despesas.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReembolsoComMediatr.Web.Repository.Despesas
{
    public class SolicitacaoRepository : RepositoryBase<Solicitacao>, ISolicitacaoWrite, ISolicitacaoRead
    {
        public SolicitacaoRepository(IConfiguration configuration)
            : base(configuration) { }

        public async Task Atualizar(Guid id, decimal valor, string descricao, CancellationToken cancellationToken)
        {
            var update = Builders<Solicitacao>
                                .Update
                                .Set(s => s.Valor, valor)
                                .Set(s => s.Descricao, descricao);

            await Collection.UpdateOneAsync(o => o.Id == id, update, cancellationToken: cancellationToken);
        }

        public async Task Inserir(Solicitacao solicitacao, CancellationToken cancellationToken)
            => await Collection.InsertOneAsync(solicitacao, cancellationToken: cancellationToken);

        public async Task<IList<Solicitacao>> ListarPorFuncionarioId(string id, CancellationToken cancellationToken)
        {
            var filter = Builders<Solicitacao>.Filter.Eq(s => s.Funcionario.Id, id);
            var despesas = await Collection.Find(filter).ToListAsync(cancellationToken);
            return despesas;
        }

        public async Task<IList<Solicitacao>> ListarPorStatus(Status status, CancellationToken cancellationToken)
        {
            var filter = Builders<Solicitacao>.Filter.Eq(s => s.Status, status);
            var despesas = await Collection.Find(filter).ToListAsync(cancellationToken);
            return despesas;
        }

        public async Task<Solicitacao> ObterPorId(Guid id, CancellationToken cancellationToken)
        {
            var filter = Builders<Solicitacao>.Filter.Eq(s => s.Id, id);
            var results = await Collection.Find(filter).ToListAsync(cancellationToken);
            return results.FirstOrDefault();
        }
    }
}
