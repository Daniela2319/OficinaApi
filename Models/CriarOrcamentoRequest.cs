namespace OficinalAPI.Models
{
    /// <summary>
    /// DTO para receber requisição de criação de orçamento
    /// </summary>
    public class CriarOrcamentoRequest
    {
        /// <summary>
        /// ID do cliente (obrigatório)
        /// </summary>
        public int ClienteId { get; set; }

        /// <summary>
        /// ID do veículo (obrigatório)
        /// </summary>
        public int VeiculoId { get; set; }

        /// <summary>
        /// Lista de itens do orçamento (mínimo 1)
        /// </summary>
        public List<ItemOrcamentoDto> Itens { get; set; } = new();
    }
}
