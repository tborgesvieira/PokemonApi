using Pokemon.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pokemon.Domain.Tests.Helpers
{
    public class ConversorTests
    {
        [Fact]
        public void Conversor_EncodeToBase64_Nulo()
        {
            //Arrange
            string str = null;
            
            //Act
            var base64 = str.EncodeToBase64();
            
            //Assert
            Assert.Null(base64);
        }

        [Fact]
        public void Conversor_EncodeToBase64_Vazio()
        {
            //Arrange
            string str = "";

            //Act
            var base64 = str.EncodeToBase64();

            //Assert
            Assert.Empty(base64);
        }

        [Fact]
        public void Conversor_EncodeToBase64_NaoNulo()
        {
            //Arrange
            string str = "http://teste.com.br";

            //Act
            var base64 = str.EncodeToBase64();

            //Assert
            Assert.Equal("aHR0cDovL3Rlc3RlLmNvbS5icg==", base64);
        }
    }
}