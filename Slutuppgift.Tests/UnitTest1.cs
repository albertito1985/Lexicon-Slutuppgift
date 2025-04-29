using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using Lexicon_Slutuppgift;
using Lexicon_Slutuppgift.Core;
using Lexicon_Slutuppgift.Menus;
using Slutuppgift.Utils;

namespace Slutuppgift.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("TestAuthor", "TestTitle", "1234567894561",true)]
        [InlineData("TestAuthor", "TestTitle", null, false)]
        [InlineData("TestAuthor", null, "1234567894561", false)]
        [InlineData(null, "TestTitle", "1234567894561", false)]
        public void AddBook_shouldAddBookIfInputIsCorrect(string author, string title, string isbn, bool output)
        {
            //ARRANGE
            Book newBook = new Book();
            newBook.Author = author;
            newBook.Title = title;
            newBook.Isbn13 = isbn;

            //ACT
            bool answer = Library.AddBook(newBook);

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
            Book newBook = new Book();
            newBook.Author = "MARIO VARGAS LLOSA";
            newBook.Title = "LA CASA VERDE";
            newBook.Isbn13 = "1234567894561";
            Library.AddBook(newBook);

            //ACT
            Book outputBook = Library.SelectBook(inputString);

            //ASSERT
            if(inputString== "öslkdjhf")
            {
                Assert.Null(outputBook);
            }
            else
            {
                Assert.True(outputBook.Title == inputString.ToUpper() || outputBook.Isbn13 == inputString);
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void RemoveBook_ShouldRemoveBookIfInputIsCorrect(bool input)
        {
            //ARRANGE
            Book newBook = new Book();
            newBook.Author = "MARIO VARGAS LLOSA";
            newBook.Title = "LA CASA VERDE";
            newBook.Isbn13 = "1234567894561";
            Library.AddBook(newBook);
            bool output;

            if (!input)
            {
                Book otherBook = new Book();
                newBook.Author = "GABRIEL GARCIA MARQUEZ";
                newBook.Title = "CIEN ANOS DE SOLEDAD";
                newBook.Isbn13 = "4964735821468";
                //ACT
                output = Library.RemoveBook(otherBook);
            }
            else
            {
                //ACT
                output = Library.RemoveBook(newBook);
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
            Book newBook = new Book();
            newBook.Author = "MARIO VARGAS LLOSA";
            newBook.Title = "LA CASA VERDE";
            newBook.Isbn13 = "1234567894561";
            Library.AddBook(newBook);

            //ACT
            bool output = Library.LoanBook(inputString);

            //ASSERT
            Assert.Equal(expectedOutput, output);
        }
    }
}