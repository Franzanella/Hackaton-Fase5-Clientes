using System;
using App.Domain.Models;
using Xunit;

namespace App.Domain.Tests
{
    public class ClienteBDTests
    {
        [Fact]
        public void ValidateEntity_ShouldThrowException_WhenCpfIsEmpty()
        {
            // Arrange
            var cliente = new ClienteBD
            {
                Cpf = string.Empty
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => cliente.ValidateEntity());
            Assert.Equal("O cpf não pode estar vazio!", exception.Message);
        }

        [Fact]
        public void ValidateEntity_ShouldNotThrowException_WhenCpfIsNotEmpty()
        {
            // Arrange
            var cliente = new ClienteBD
            {
                Cpf = "12345678901"
            };

            // Act & Assert
            var exception = Record.Exception(() => cliente.ValidateEntity());
            Assert.Null(exception);
        }

        [Fact]
        public void DefaultDataCadastro_ShouldBeCurrentDateTime()
        {
            // Arrange
            var cliente = new ClienteBD();

            // Act
            var result = cliente.DataCadastro;

            // Assert
            Assert.True(result.Date == DateTime.Now.Date);
        }
    }
}
