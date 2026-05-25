namespace OficinalAPI.Models
{
    /// <summary>
    /// DTO para representar um item do orçamento
    /// </summary>
    public class ItemOrcamentoDto
    {
        /// <summary>
        /// Descrição do serviço ou produto
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Quantidade de itens/horas
        /// </summary>
        public decimal Quantidade { get; set; }

        /// <summary>
        /// Valor unitário do item
        /// </summary>
        public decimal ValorUnitario { get; set; }

        /// <summary>
        /// Calcula o valor total do item (Quantidade * ValorUnitario)
        /// </summary>
        public decimal CalcularValorTotal()
        {
            return Quantidade * ValorUnitario;
        }
    }
}
