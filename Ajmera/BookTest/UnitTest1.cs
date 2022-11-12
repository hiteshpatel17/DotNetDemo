using BookTest.Demo;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace BookTest
{
    public class UnitTest1
    {
        private HttpClient _httpClient = new HttpClient();
        private string URL = "https://localhost:44306/api/Book/";

        [Fact]
        public void Get()
        {
            
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml,application/xml");
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var responseMessage = _httpClient.GetAsync($"{URL}GetAll").Result;
            Assert.Equal(System.Net.HttpStatusCode.OK,responseMessage.StatusCode);

            var responseContent =  responseMessage.Content.ReadAsStringAsync();
            var book = JsonConvert.DeserializeObject<List<BookModel>>(responseContent.Result);
            Assert.IsType<List<BookModel>>(book);
            Assert.Equal(5, book.Count);
        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200", "ab2bd817-98cd-4cf3-a80a-53ea0cd9c111")]
        public void GetBookByIdTest(string guid1, string guid2)
        {
            //Arrange
            var validGuid = new Guid(guid1);
            var invalidGuid = new Guid(guid2);

            //Act
            var responseMessageInValid = _httpClient.GetAsync($"{URL}GetByID/{invalidGuid}").Result;
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, responseMessageInValid.StatusCode);


            var responseMessageValid = _httpClient.GetAsync($"{URL}GetByID/{validGuid}").Result;
            Assert.Equal(System.Net.HttpStatusCode.OK, responseMessageValid.StatusCode);

            var responseContent = responseMessageValid.Content.ReadAsStringAsync();
            var book = JsonConvert.DeserializeObject<BookModel>(responseContent.Result);

            //Now, let us check the value itself.
            Assert.Equal(validGuid, book.Id);
            Assert.Equal("Managing Oneself", book.Name);
        }

        [Fact]
        public void AddBookTest()
        {
            //OK RESULT TEST START

            //Arrange
            var book = new BookModel()
            {
                Name="Book1",
                AuthorName = "Author1"
            };

            //Act
            var json = JsonConvert.SerializeObject(book);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var createdResponse = _httpClient.PostAsync($"{URL}Create", httpContent).Result;

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, createdResponse.StatusCode);


            //OK RESULT TEST END

            //BADREQUEST AND MODELSTATE ERROR TEST START

            //Arrange
            var incompleteBook = new BookModel()
            {
                AuthorName = "Author2"
            };

            //Act
            //_controller.ModelState.AddModelError("Title", "Title is a requried filed");
            var injson = JsonConvert.SerializeObject(incompleteBook);
            StringContent httpContentin = new StringContent(injson, Encoding.UTF8, "application/json");
            var in_createdResponse = _httpClient.PostAsync($"{URL}Create", httpContentin).Result;

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, in_createdResponse.StatusCode);



            //BADREQUEST AND MODELSTATE ERROR TEST END
        }
    }
}
