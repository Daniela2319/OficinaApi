using Microsoft.AspNetCore.Mvc;
using OficinalAPI.Models;
using OficinalAPI.Services;

namespace OficinalAPI.Controllers
{
    /// <summary>
    /// Controller para gerenciar orçamentos
    /// Padrão: REST API com Controller Pattern
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrcamentosController : ControllerBase
    {
        private readonly IOrcamentoService _orcamentoService;
        private readonly ILogger<OrcamentosController> _logger;

        public OrcamentosController(IOrcamentoService orcamentoService, ILogger<OrcamentosController> logger)
        {
            _orcamentoService = orcamentoService;
            _logger = logger;
        }

        /// <summary>
        /// Cria um novo orçamento para um cliente e veículo
        /// </summary>
        /// <param name="request">Dados do orçamento</param>
        /// <returns>Orçamento criado com ID e valor total calculado</returns>
        /// <response code="201">Orçamento criado com sucesso</response>
        /// <response code="400">Dados inválidos ou incompletos</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPost("criar")]
        [ProducesResponseType(typeof(OrcamentoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErroResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarOrcamento([FromBody] CriarOrcamentoRequest request)
        {
            _logger.LogInformation(
                "Criando novo orçamento para Cliente {ClienteId} e Veículo {VeiculoId}",
                request.ClienteId,
                request.VeiculoId
            );

            var resultado = await _orcamentoService.CriarOrcamentoAsync(request);

            _logger.LogInformation(
                "Orçamento {OrcamentoId} criado com sucesso. Valor total: {ValorTotal}",
                resultado.Id,
                resultado.ValorTotal
            );

            return CreatedAtAction(nameof(CriarOrcamento), new { id = resultado.Id }, resultado);
        }
    }
}
