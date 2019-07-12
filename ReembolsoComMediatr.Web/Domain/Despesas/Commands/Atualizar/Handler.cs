using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ReembolsoComMediatr.Web.Domain.Despesas.Commands.Atualizar
{
    public class Handler : IRequestHandler<Request, Result>
    {
        private readonly IMediator _mediator;
        private readonly Repository.ISolicitacaoWrite _solicitacaoWrite;
        private readonly Repository.ISolicitacaoRead _solicitacaoRead;
        private readonly Repository.IFuncionarioRead _funcionarioRead;
        public Handler(IMediator mediator,
                       Repository.ISolicitacaoWrite solicitacaoWrite,
                       Repository.ISolicitacaoRead solicitacaoRead,
                       Repository.IFuncionarioRead funcionario)
        {
            _mediator = mediator;
            _solicitacaoWrite = solicitacaoWrite;
            _solicitacaoRead = solicitacaoRead;
            _funcionarioRead = funcionario;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            Solicitacao solicitacao = await _solicitacaoRead.ObterPorId(request.SolicitacaoId, cancellationToken);
            if (solicitacao.Status == Status.EmAberto || solicitacao.Status == Status.Rejeitado)
            {
                await _solicitacaoWrite.Atualizar(request.SolicitacaoId, request.Valor, request.Descricao, cancellationToken);
                await _mediator.Publish(new Notification
                {
                    NomeFuncionario = solicitacao.Funcionario.Nome,
                    ValorAntigo = solicitacao.Valor,
                    Valor = request.Valor,
                    DescricaoAntiga = request.Descricao,
                    Descricao = solicitacao.Descricao
                }, cancellationToken);

                return Result.Ok;
            }
            else
            {
                var validation = "";
                if (solicitacao.Status == Status.EmProcessamento)
                    validation = "Despesa Em Processamento Não Pode Ser Atualizada";

                if (solicitacao.Status == Status.Pago)
                    validation = "Despesa já Paga Não Pode Ser Atualizada";

                var result = new Result();
                result.AddValidation(validation);
                return result;
            }
        }
    }
}
