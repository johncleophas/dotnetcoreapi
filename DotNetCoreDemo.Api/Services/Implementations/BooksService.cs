using DotNetCoreDemo.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreDemo.Api.Services
{
    public class BooksService : IBooksService
    {

        private DataContext context;

        public BooksService(DataContext context)
        {
            this.context = context;
        }

        public void GenerateSampleBooks()
        {
            var books = new List<Book>() {
                new Book(){
                    Name = "Science of Running: Analyze your Technique, Prevent Injury, Revolutionize your Training",
                    Text = "Discover the hard science that will help you run faster, endure for longer, and avoid injury. Analyze your running style and learn how to enhance your gait for optimum efficiency and safety. Transform your performance with exercises targeting strength, flexibility, and recovery - each exercise annotated to reveal the muscle mechanics so you know you're getting it right.",
                    Price = 99.99
                },
                new Book(){
                    Name = "Born to Run: A Hidden Tribe, Superathletes, and the Greatest Race the World Has Never Seen",
                    Text = "The astonishing national bestseller and hugely entertaining story that completely changed the way we run. An epic adventure that began with one simple question: Why does my foot hurt?",
                    Price = 59.99
                },
                new Book(){
                    Name = "The Lost Art of Running: A Journey to Rediscover the Forgotten Essence of Human Movement",
                    Text = "One man's journey across six continents to rediscover the lost art of running. The Lost Art of Running is an opportunity to join running technique analyst coach and movement guru, Shane Benzie, on his incredible journey of discovery across six continents as he trains with and analyzes the running style of some of the most naturally gifted athletes on the planet.",
                    Price = 299.99
                },
                new Book(){
                    Name = "80/20 Running: Run Stronger and Race Faster By Training Slower",
                    Text = "TRAIN EASIER TO RUN FASTER. This revolutionary training method has been embraced by elite runners—with extraordinary results—and now you can do it, too.",
                    Price = 199.99
                },
                new Book(){
                    Name = "80/20 Running: Run Stronger and Race Faster By Training Slower",
                    Text = "TRAIN EASIER TO RUN FASTER. This revolutionary training method has been embraced by elite runners—with extraordinary results—and now you can do it, too.",
                    Price = 69.99
                },
                new Book(){
                    Name = "The Incomplete Book of Running",
                    Text = "Peter Sagal, the host of NPR’s Wait Wait...Don’t Tell Me! and a popular columnist for Runner’s World, shares lessons, stories, advice, and warnings gleaned from running the equivalent of once around the Earth. At the verge of turning 40, Peter Sagal - brainiac Harvard grad, short, bald Jew with a disposition toward heft, and a sedentary star of public radio - started running seriously. And much to his own surprise, he kept going, faster and further, running 14 marathons and logging tens of thousands of miles on roads, sidewalks, paths, and trails all over the US and the world, including the 2013 Boston Marathon, where he crossed the finish line moments before the bombings.",
                    Price = 69.99
                },
                new Book(){
                    Name = "North",
                    Text = "From the author of the best seller Eat and Run, a thrilling new memoir about his grueling, exhilarating, and immensely inspiring 46 day run to break the speed record for the Appalachian Trail. Scott Jurek is one of the world's best known and most beloved ultrarunners. Renowned for his remarkable endurance and speed, accomplished on a vegan diet, he's finished first in nearly all of ultrarunning's elite events over the course of his career. But after two decades of racing, training, speaking, and touring, Jurek felt an urgent need to discover something new about himself. He embarked on a wholly unique challenge, one that would force him to grow as a person and as an athlete: breaking the speed record for the Appalachian Trail. North is the story of the 2,189 mile journey that nearly shattered him.",
                    Price = 499.99
                }
            };

            this.context.Books.AddRange(books);
            this.context.SaveChanges();

        }

        public ServiceResult<List<Book>> GetBooks()
        {
            return new ServiceResult<List<Book>>()
            {
                Result = context.Books.ToList(),
                Status = System.Net.HttpStatusCode.OK
            };
        }
    }
}
