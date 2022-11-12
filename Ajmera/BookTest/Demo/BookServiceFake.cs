using System;
using System.Collections.Generic;
using System.Text;

namespace BookTest.Demo
{
    public class BookServiceFake
    {
        private readonly List<BookModel> _bookModels;

        public BookServiceFake()
        {
            _bookModels = new List<BookModel>(){  new BookModel()
            {
                Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                Name = "Orange Juice",
                AuthorName = "Orange Tree"
            },
                new BookModel()
                {
                    Id = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),
                    Name = "Diary Milk",
                    AuthorName = "Cow"
                },
                new BookModel()
                {
                    Id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad"),
                    Name = "Frozen Pizza",
                    AuthorName = "Uncle Mickey"
                }
          };

        }
    }
}
