using App.Domain.Models;
using System;

namespace App.Application.ViewModels.Response
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public int DuracaoToken { get; set; }
        public DateTime DataHoraExpiracao { get; set; }

    }
}
