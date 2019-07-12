using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ReembolsoComMediatr.Web.Domain.Despesas.Commands.Inserir
{
    public class Handler : IRequestHandler<Request, Result>
    {
        private readonly IMediator _mediator;
        private readonly Repository.ISolicitacaoWrite _solicitacaoWrite;
        private readonly Repository.IFuncionarioRead _funcionarioRead;

        public Handler(IMediator mediator,
                                Repository.ISolicitacaoWrite solicitacaoWrite,
                                Repository.IFuncionarioRead funcionario)
        {
            _mediator = mediator;
            _solicitacaoWrite = solicitacaoWrite;
            _funcionarioRead = funcionario;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var funcionario = await _funcionarioRead.ObterPorId(request.IdFuncionario, cancellationToken);
            var solicitacao = new Solicitacao(funcionario, request.Valor, request.Descricao);
            await _solicitacaoWrite.Inserir(solicitacao, cancellationToken);

            await _mediator.Publish(new Notification
            {
                NomeFuncionario = solicitacao.Funcionario.Nome,
                Valor = solicitacao.Valor,
                Descricao = solicitacao.Descricao
            }, cancellationToken);

            return Result.Ok;
        }
    }
}
