using OficinalAPI.Models;
using OficinalAPI.Validators;

namespace OficinalAPI.Services
{
    /// <summary>
    /// Serviço que implementa a lógica de negócio para orçamentos
    /// Padrão: Service Pattern (Camada de Negócio)
    /// </summary>
    public interface IOrcamentoService
    {
        /// <summary>
        /// Cria um novo orçamento
        /// </summary>
        Task<OrcamentoResponse> CriarOrcamentoAsync(CriarOrcamentoRequest request);
    }

    public class OrcamentoService : IOrcamentoService
    {
        private readonly OrcamentoValidator _validator;
        private static int _proximoId = 1; // Simulando banco de dados em memória

        public OrcamentoService()
        {
            _validator = new OrcamentoValidator();
        }

        /// <summary>
        /// Cria um novo orçamento validando os dados e calculando o total
        /// </summary>
        public async Task<OrcamentoResponse> CriarOrcamentoAsync(CriarOrcamentoRequest request)
        {
            // Validar dados de entrada
            _validator.Validar(request);

            // Simular processamento assíncrono
            await Task.Delay(100);

            // Calcular valor total
            decimal valorTotal = request.Itens.Sum(item => item.CalcularValorTotal());

            // Criar resposta
            var resposta = new OrcamentoResponse
            {
                Id = _proximoId++,
                ClienteId = request.ClienteId,
                VeiculoId = request.VeiculoId,
                Itens = request.Itens,
                ValorTotal = valorTotal,
                DataCriacao = DateTime.Now,
                Status = "Aberto"
            };

            return resposta;
        }
    }
}
