using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceAPI.Models
{
    public class ContaBancaria
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Este campo é obrigatório")]
        [MinLength(5, ErrorMessage ="O nome de usuário deve conter entre 5 e 20 caracteres.")]
        [MaxLength(20, ErrorMessage = "O nome de usuário deve conter entre 5 e 20 caracteres.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(5, ErrorMessage = "A senha deve conter entre 5 e 20 caracteres.")]
        [MaxLength(20, ErrorMessage = "A senha deve conter entre 5 e 20 caracteres.")]
        public string Password {  get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int BancoId { get; set; }

        [ForeignKey("BancoId")]
        public Banco Banco { get; set; }
    }
}
