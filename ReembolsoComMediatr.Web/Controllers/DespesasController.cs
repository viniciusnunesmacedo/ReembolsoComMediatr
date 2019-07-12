using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReembolsoComMediatr.Web.Domain.Despesas;
using ReembolsoComMediatr.Web.Domain.Despesas.Repository;

namespace ReembolsoComMediatr.Web.Controllers
{
    [Authorize]
    public class DespesasController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ISolicitacaoRead _repositorio;

        public DespesasController(IMediator mediator, ISolicitacaoRead repositorio)
        {
            _mediator = mediator;
            _repositorio = repositorio;
        }

        public async Task<IActionResult> Index()
        {
            System.Collections.Generic.IList<Domain.Despesas.Solicitacao> solicitacoes =
                await _repositorio.ListarPorFuncionarioId(User.Identity.Name, CancellationToken.None);

            return View(solicitacoes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Domain.Despesas.Commands.Inserir.Request request)
        {
            Domain.Result result = await _mediator.Send(request, CancellationToken.None);
            return ValidationHandler(request, result, "Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            Solicitacao solicitacao = await _repositorio.ObterPorId(id, CancellationToken.None);
            Domain.Despesas.Commands.Atualizar.Request request = new Domain.Despesas.Commands.Atualizar.Request
            {
                SolicitacaoId = id,
                Descricao = solicitacao.Descricao,
                Valor = solicitacao.Valor
            };
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ReembolsoComMediatr.Web.Domain.Despesas.Commands.Atualizar.Request request)
        {
            var result = await _mediator.Send(request, CancellationToken.None);
            return ValidationHandler(request, result, "Index");
        }

        private IActionResult ValidationHandler<TCommand>(TCommand command, Domain.Result result, string redirectAction)
        {
            if (!result.HasValidation)
            {
                return RedirectToAction(redirectAction);
            }

            foreach (string validation in result.Validations)
                ModelState.AddModelError("", validation);

            return View(command);
        }
    }
}