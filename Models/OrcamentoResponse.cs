namespace OficinalAPI.Models
{
    /// <summary>
    /// DTO para retornar o orçamento criado
    /// </summary>
    public class OrcamentoResponse
    {
        /// <summary>
        /// ID único do orçamento
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID do cliente
        /// </summary>
        public int ClienteId { get; set; }

        /// <summary>
        /// ID do veículo
        /// </summary>
        public int VeiculoId { get; set; }

        /// <summary>
        /// Itens do orçamento
        /// </summary>
        public List<ItemOrcamentoDto> Itens { get; set; } = new();

        /// <summary>
        /// Valor total do orçamento
        /// </summary>
        public decimal ValorTotal { get; set; }

        /// <summary>
        /// Data de criação do orçamento
        /// </summary>
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        /// <summary>
        /// Status do orçamento
        /// </summary>
        public string Status { get; set; } = "Aberto";
    }
}
