using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace Library_Management
{
    public static class Function1
    {
        static int rentalid = 0;

        [FunctionName("AddBook")]
        public static async Task<IActionResult> AddBook(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<BookDetail>(requestBody);


            BookDetail book = new BookDetail()
            {
                BookId = data.bookId,
                BookTitle = data.bookTitle,
                BookDescription = data.bookDescription,
                Author = data.author,
                Stream = data.stream,
                Quantity = data.quantity,
                RentPrice = (float) Convert.ToDouble(data.rentprice),
                Status = data.quantity>0?"Available":"Not-Available"
            };

            db.ActionTypes.InsertOnSubmit(book);
            db.SubmitChanges();
            return new OkObjectResult("OK");
        }

        [FunctionName("SearchBookById")]
        public static async Task<IActionResult> SearchBookById(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var books = from a in BookDetails
                        where a.BookId == data.bookId
                        select a;
            
            return new OkObjectResult(books);
        }

        [FunctionName("SearchBookByTitle")]
        public static async Task<IActionResult> SearchBookByTitle(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var books = from a in BookDetails
                        where a.BookTitle == data.bookTitle
                        select a;

            return new OkObjectResult(books);
        }

        [FunctionName("SearchBookByMedium")]
        public static async Task<IActionResult> SearchBookByMedium(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var books = from a in BookDetails
                        where a.BookMedium == data.bookMedium
                        select a;

            return new OkObjectResult(books);
        }

        [FunctionName("SearchBookByStream")]
        public static async Task<IActionResult> SearchBookByStream(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var books = from a in BookDetails
                        where a.Stream == data.bookStream
                        select a;

            return new OkObjectResult(books);
        }

        [FunctionName("BookIssue")]
        public static async Task<IActionResult> BookIssue(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var book = from a in BookDetails
                       where a.BookId == data.bookId
                       select a;

            if(book.quantity<=0)
            {
                return new OkObjectResult("No books available currently.");
            }

            book.quantity = book.quantity-1;
            if (book.quantity <= 0)
            {
                book.status = "Not-Available";
            }
            double duration = Convert.ToDouble(data.duration);
            float totalRentalCost = book.RentPrice * duration;
            DateTime today = DateTime.Now;
            DateTime returnDate = today.AddDays(duration);

            // update book in the db

            return new OkObjectResult("Book issued successfully.");
        }
    }
}
