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
    }
}
