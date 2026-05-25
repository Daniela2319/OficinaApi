using OficinalAPI.Exceptions;
using OficinalAPI.Models;

namespace OficinalAPI.Validators
{
    /// <summary>
    /// Validador para os dados de orçamento
    /// Implementa regras de negócio conforme especificação
    /// </summary>
    public class OrcamentoValidator
    {
        /// <summary>
        /// Valida uma requisição de criação de orçamento
        /// </summary>
        /// <param name="request">Requisição com dados do orçamento</param>
        /// <exception cref="OrcamentoValidacaoException">Lança exceção se houver erros de validação</exception>
        public void Validar(CriarOrcamentoRequest request)
        {
            var erros = new List<string>();

            // Validar ClienteId
            if (request.ClienteId <= 0)
            {
                erros.Add("ClienteId é obrigatório e deve ser maior que zero.");
            }

            // Validar VeiculoId
            if (request.VeiculoId <= 0)
            {
                erros.Add("VeiculoId é obrigatório e deve ser maior que zero.");
            }

            // Validar se existe pelo menos 1 item
            if (request.Itens == null || request.Itens.Count == 0)
            {
                erros.Add("O orçamento deve conter pelo menos 1 item.");
            }
            else
            {
                // Validar cada item
                ValidarItens(request.Itens, erros);
            }

            // Se houver erros, lançar exceção
            if (erros.Count > 0)
            {
                throw new OrcamentoValidacaoException(
                    "Falha na validação dos dados do orçamento.",
                    erros
                );
            }
        }

        /// <summary>
        /// Valida os itens do orçamento
        /// </summary>
        private void ValidarItens(List<ItemOrcamentoDto> itens, List<string> erros)
        {
            for (int i = 0; i < itens.Count; i++)
            {
                var item = itens[i];

                // Validar descrição
                if (string.IsNullOrWhiteSpace(item.Descricao))
                {
                    erros.Add($"Item {i + 1}: Descrição é obrigatória.");
                }

                // Validar quantidade
                if (item.Quantidade <= 0)
                {
                    erros.Add($"Item {i + 1}: Quantidade deve ser maior que zero.");
                }

                // Validar valor unitário
                if (item.ValorUnitario <= 0)
                {
                    erros.Add($"Item {i + 1}: Valor unitário deve ser maior que zero.");
                }
            }
        }
    }
}
