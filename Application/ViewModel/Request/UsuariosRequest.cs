using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModel.Request
{
    public class UsuariosResquest : IValidatableObject
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
           

            if (string.IsNullOrWhiteSpace(Nome))
            {
                yield return new ValidationResult(
                    "O Nome nao deve ser estar vazio .",
                    new[] { nameof(Nome) }
                );
            }
            if (string.IsNullOrWhiteSpace(Email)) {
                yield return new ValidationResult(
                       "O Email nao deve ser estar vazio .",
                       new[] { nameof(Email) }
                   );
            }
            if (string.IsNullOrWhiteSpace(Login))
            {
                yield return new ValidationResult(
                    "O Login nao deve ser estar vazio .",
                    new[] { nameof(Login) }
                );
            }
            if (string.IsNullOrWhiteSpace(Senha) || Senha.Length < 8)
            {
                yield return new ValidationResult(
                    "A senha nao deve ser estar vazio e deve ter no mínimo 8 caracteres.",
                    new[] { nameof(Senha) }
                );
            }
        }
    }
}
