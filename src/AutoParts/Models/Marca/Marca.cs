using AutoParts.Models.Produtos;
using System.ComponentModel.DataAnnotations;

namespace AutoParts.Models
{
    public class Marca
    {
        public int Id { get; private set; }

        [Required]
        [MaxLength(100)]
        public string Descricao { get; private set; } = string.Empty;

        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();

        // Necessário para o Entity Framework
        private Marca()
        {
        }

        // Construtor de domínio
        public Marca(string descricao)
        {
            AlterarDescricao(descricao);
        }

        public void AlterarDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("A descrição é obrigatória.");

            Descricao = descricao.Trim();
        }

    }
}
