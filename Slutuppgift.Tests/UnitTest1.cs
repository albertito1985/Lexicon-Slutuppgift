using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using Lexicon_Slutuppgift;
using Lexicon_Slutuppgift.Core;
using Lexicon_Slutuppgift.Core.Collections;
using Slutuppgift.Utils;

namespace Slutuppgift.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("TestAuthor", "TestTitle", "1234567894561",true)]
        [InlineData("TestAuthor", "TestTitle", null, false)]
        [InlineData("TestAuthor", null, "1234567894561", false)]
        [InlineData(null, "TestTitle", "1234567894561", true)]
        public void AddBook_shouldAddBookIfInputIsCorrect(string author, string title, string isbn, bool output)
        {
            //ARRANGE
            BooksHandler library = new("library");
            Book newBook = new Book();
            newBook.Author = author;
            newBook.Name = title;
            newBook.IdNr = isbn;

            //ACT
            bool answer = library.Add(newBook);

            //ASSERT
            Assert.Equal(output, answer);
        }

        [Theory]
        [InlineData("La casa verde")]
        [InlineData("1234567894561")]
        [InlineData("öslkdjhf")]
        public void SelectBook_ShouldReturnBookIfInputIsCorrect(string inputString)
        {
            //ARRANGE
            BooksHandler library = new("library");
            Book newBook = new Book();
            newBook.Author = "MARIO VARGAS LLOSA";
            newBook.Name = "LA CASA VERDE";
            newBook.IdNr = "1234567894561";
            library.Add(newBook);

            //ACT
            Identifiable outputBook = library.Select(inputString);

            //ASSERT
            if(inputString== "öslkdjhf")
            {
                Assert.Null(outputBook);
            }
            else
            {
                Assert.True(outputBook.Name == inputString.ToUpper() || outputBook.IdNr == inputString);
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void RemoveBook_ShouldRemoveBookIfInputIsCorrect(bool input)
        {
            //ARRANGE
            BooksHandler library = new("library");
            Book newBook = new Book();
            newBook.Author = "MARIO VARGAS LLOSA";
            newBook.Name = "LA CASA VERDE";
            newBook.IdNr = "1234567894561";
            library.Add(newBook);
            bool output;

            if (!input)
            {
                Book otherBook = new Book();
                newBook.Author = "GABRIEL GARCIA MARQUEZ";
                newBook.Name = "CIEN ANOS DE SOLEDAD";
                newBook.IdNr = "4964735821468";
                //ACT
                output = library.Remove(otherBook);
            }
            else
            {
                //ACT
                output = library.Remove(newBook);
            }

            //ASSERT
            Assert.Equal(input,output);
        }

        [Theory]
        [InlineData("1234567894561", true)]
        [InlineData("1234567894560", false)]
        public void LoanBook_ShouldChangeTheStatusOfTheBookIfInputIsCorrect(string inputString, bool expectedOutput)
        {
            //ARRANGE
            BooksHandler library = new("library");
            Book newBook = new Book();
            newBook.Author = "MARIO VARGAS LLOSA";
            newBook.Name = "LA CASA VERDE";
            newBook.IdNr = "1234567894561";
            library.Add(newBook);

            //ACT
            bool output = library.Loan(inputString);

            //ASSERT
            Assert.Equal(expectedOutput, output);
        }
    }
}