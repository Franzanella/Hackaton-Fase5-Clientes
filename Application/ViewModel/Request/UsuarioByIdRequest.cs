using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModel.Request
{
    public class UsuarioByIdRequest
    {
        public int IdUsuario { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {


            if (IdUsuario == 0)
            {
                yield return new ValidationResult(
                    "O idUsuario nao deve ser estar vazio .",
                    new[] { nameof(IdUsuario) }
                );
            }
        }

    }
}
