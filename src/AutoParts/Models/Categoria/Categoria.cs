using AutoParts.Models.Produtos;
using System.ComponentModel.DataAnnotations;

namespace AutoParts.Models
{
    public class Categoria
    {
        public int Id { get; private set; }

        [Required]
        [MaxLength(255)]
        public string Descricao { get; private set; } = string.Empty;
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();

        // Necessário para o Entity Framework
        private Categoria()
        {
        }

        // Construtor de domínio
        public Categoria(string descricao)
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
