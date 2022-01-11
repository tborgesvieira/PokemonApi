using Pokemon.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pokemon.Domain.Tests.Helpers
{
    public class StringUtilsTests
    {
        [Fact]
        public void StringUtils_ApenasNumeros_Nulo()
        {
            //Arrange
            string str = null;

            //Act
            var retorno = str.ApenasNumeros();

            //Assert
            Assert.Null(retorno);
        }

        [Theory]
        [InlineData("abc123", "123")]
        [InlineData("abc", "")]
        [InlineData("123", "123")]
        public void StringUtils_ApenasNumeros_NaoNulo(string texto, string retorno)
        {
            //Arrange            
            //Act
            var txt = texto.ApenasNumeros();

            //Assert
            Assert.Equal(retorno, txt);
        }
    }
}
