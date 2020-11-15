using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreDemo.Api.Controllers.Mappers
{
    public static class BookMapper
    {
        public static Data.Book MapToService(Book controllerBook)
        {
            return new Data.Book()
            {
                Id = controllerBook.Id,
                Name = controllerBook.Name,
                Price = controllerBook.Price,
                Text = controllerBook.Text
            };
        }

        public static Book MapToController(Data.Book serviceBook)
        {
            return new Book()
            {
                Id = serviceBook.Id,
                Name = serviceBook.Name,
                Price = serviceBook.Price,
                Text = serviceBook.Text
            };
        }

        public static List<Book> MapBooksToController(List<Data.Book> serviceBooks) {

            var books = new List<Book>();

            foreach (var item in serviceBooks)
            {
                books.Add(MapToController(item));
            }

            return books;
        }
    }
}
