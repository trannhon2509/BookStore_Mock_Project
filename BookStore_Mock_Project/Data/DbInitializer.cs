using BookStore_Mock_Project.Model;

namespace BookStore_Mock_Project.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Check if the database has been initialized
            if (context.Books.Any() || context.BookCategories.Any() || context.Users.Any() ||
                context.Roles.Any() || context.Orders.Any() || context.OrderDetails.Any())
            {
                return; // If data already exists, no need to initialize again
            }

            // Sample data for BookCategories
            var categories = new List<BookCategory>
            {
                new BookCategory { Name = "Fiction", Description = "Books of fictional stories" },
                new BookCategory { Name = "Non-Fiction", Description = "Books based on facts and real events" },
                new BookCategory { Name = "Science-Fiction", Description = "Books that explore futuristic or scientific themes" },
                new BookCategory { Name = "Mystery", Description = "Books that involve solving a puzzle or crime" },
                new BookCategory { Name = "Thriller", Description = "Books that provide suspenseful and exciting plots" },
                new BookCategory { Name = "Biography", Description = "Books that tell the life stories of real people" }

                // Add more categories as needed
            };
            context.BookCategories.AddRange(categories);
            context.SaveChanges();

            // Sample data for Books
            var books = new List<Book>
            {
                new Book
                {
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    Price = 15.99m,
                    Description = "A classic novel set in the American South",
                    Quantity = 10,
                    CategoryId = categories.First(c => c.Name == "Fiction").CategoryId // Assigning a category
                },
                new Book
    {
        Title = "1984",
        Author = "George Orwell",
        Price = 12.50m,
        Description = "A dystopian novel about surveillance and government control",
        Quantity = 15,
        CategoryId = categories.First(c => c.Name == "Science-Fiction").CategoryId // Assigning a category
    },
    new Book
    {
        Title = "The Da Vinci Code",
        Author = "Dan Brown",
        Price = 9.99m,
        Description = "A mystery thriller involving secret societies and religious symbolism",
        Quantity = 20,
        CategoryId = categories.First(c => c.Name == "Mystery").CategoryId // Assigning a category
    },
    new Book
    {
        Title = "Pride and Prejudice",
        Author = "Jane Austen",
        Price = 12.99m,
        Description = "A classic romantic novel set in Georgian England",
        Quantity = 25,
        CategoryId = categories.First(c => c.Name == "Fiction").CategoryId // Assigning a category
    },
    new Book
    {
        Title = "The Catcher in the Rye",
        Author = "J.D. Salinger",
        Price = 11.50m,
        Description = "A coming-of-age novel following the life of Holden Caulfield",
        Quantity = 18,
        CategoryId = categories.First(c => c.Name == "Fiction").CategoryId // Assigning a category
    },
    new Book
    {
        Title = "Pride and Prejudice",
        Author = "Jane Austen",
        Price = 12.99m,
        Description = "A classic romantic novel set in Georgian England",
        Quantity = 25,
        CategoryId = categories.First(c => c.Name == "Fiction").CategoryId // Assigning a category
    },
    new Book
    {
        Title = "The Catcher in the Rye",
        Author = "J.D. Salinger",
        Price = 11.50m,
        Description = "A coming-of-age novel following the life of Holden Caulfield",
        Quantity = 18,
        CategoryId = categories.First(c => c.Name == "Fiction").CategoryId // Assigning a category
    },
    // Add 10 more books
    new Book
    {
        Title = "The Great Gatsby",
        Author = "F. Scott Fitzgerald",
        Price = 10.99m,
        Description = "A novel depicting the glamour and decadence of the Jazz Age",
        Quantity = 20,
        CategoryId = categories.First(c => c.Name == "Fiction").CategoryId // Assigning a category
    },
    new Book
    {
        Title = "Moby-Dick",
        Author = "Herman Melville",
        Price = 14.75m,
        Description = "A tale of Captain Ahab's obsession with hunting the white whale",
        Quantity = 15,
        CategoryId = categories.First(c => c.Name == "Fiction").CategoryId // Assigning a category
    },
    new Book
    {
        Title = "Jane Eyre",
        Author = "Charlotte Brontë",
        Price = 11.25m,
        Description = "A Gothic novel following the experiences of a young orphan",
        Quantity = 22,
        CategoryId = categories.First(c => c.Name == "Fiction").CategoryId // Assigning a category
    },
    new Book
    {
        Title = "The Hobbit",
        Author = "J.R.R. Tolkien",
        Price = 13.99m,
        Description = "A fantasy adventure novel about Bilbo Baggins' quest to reclaim treasure guarded by a dragon",
        Quantity = 19,
        CategoryId = categories.First(c => c.Name == "Fiction").CategoryId // Assigning a category
    },
    new Book
    {
        Title = "War and Peace",
        Author = "Leo Tolstoy",
        Price = 17.50m,
        Description = "An epic historical novel set during the Napoleonic Wars",
        Quantity = 30,
        CategoryId = categories.First(c => c.Name == "Fiction").CategoryId // Assigning a category
    },
    new Book
    {
        Title = "The Lord of the Rings",
        Author = "J.R.R. Tolkien",
        Price = 20.25m,
        Description = "A high fantasy epic trilogy depicting the struggle to destroy the One Ring",
        Quantity = 25,
        CategoryId = categories.First(c => c.Name == "Fiction").CategoryId // Assigning a category
    },
    new Book
    {
        Title = "Anna Karenina",
        Author = "Leo Tolstoy",
        Price = 13.75m,
        Description = "A tragic tale of love and infidelity in Imperial Russia",
        Quantity = 16,
        CategoryId = categories.First(c => c.Name == "Fiction").CategoryId // Assigning a category
    },
    new Book
    {
        Title = "The Picture of Dorian Gray",
        Author = "Oscar Wilde",
        Price = 12.50m,
        Description = "A philosophical novel exploring the nature of beauty and morality",
        Quantity = 17,
        CategoryId = categories.First(c => c.Name == "Fiction").CategoryId // Assigning a category
    },
     // Fiction books
    new Book
    {
        Title = "1984",
        Author = "George Orwell",
        Price = 10.99m,
        Description = "A dystopian novel about a totalitarian regime and surveillance",
        Quantity = 15,
        CategoryId = categories.First(c => c.Name == "Fiction").CategoryId
    },
    new Book
    {
        Title = "The Great Gatsby",
        Author = "F. Scott Fitzgerald",
        Price = 12.50m,
        Description = "A story of love, greed, and the American Dream in the Roaring Twenties",
        Quantity = 20,
        CategoryId = categories.First(c => c.Name == "Fiction").CategoryId
    },
    // Add more Fiction books...

    // Non-Fiction books
    new Book
    {
        Title = "Sapiens: A Brief History of Humankind",
        Author = "Yuval Noah Harari",
        Price = 14.99m,
        Description = "Explores the history of Homo sapiens from the Stone Age to the present",
        Quantity = 25,
        CategoryId = categories.First(c => c.Name == "Non-Fiction").CategoryId
    },
    new Book
    {
        Title = "Educated",
        Author = "Tara Westover",
        Price = 11.25m,
        Description = "A memoir recounting the author's journey from a survivalist upbringing to higher education",
        Quantity = 18,
        CategoryId = categories.First(c => c.Name == "Non-Fiction").CategoryId
    },
    // Add more Non-Fiction books...

    // Science-Fiction books
    new Book
    {
        Title = "Dune",
        Author = "Frank Herbert",
        Price = 13.75m,
        Description = "A space opera epic set in a distant future amidst political intrigue and desert planets",
        Quantity = 20,
        CategoryId = categories.First(c => c.Name == "Science-Fiction").CategoryId
    },
    new Book
    {
        Title = "The Martian",
        Author = "Andy Weir",
        Price = 12.99m,
        Description = "A Science-Fiction novel about an astronaut stranded on Mars and his struggle for survival",
        Quantity = 22,
        CategoryId = categories.First(c => c.Name == "Science-Fiction").CategoryId
    },
    // Add more Science-Fiction books...

    // Mystery books
    new Book
    {
        Title = "The Girl with the Dragon Tattoo",
        Author = "Stieg Larsson",
        Price = 9.99m,
        Description = "A gripping mystery novel featuring journalist Mikael Blomkvist and hacker Lisbeth Salander",
        Quantity = 17,
        CategoryId = categories.First(c => c.Name == "Mystery").CategoryId
    },
    new Book
    {
        Title = "Gone Girl",
        Author = "Gillian Flynn",
        Price = 11.50m,
        Description = "A psychological thriller about a woman who goes missing and the suspicions surrounding her husband",
        Quantity = 21,
        CategoryId = categories.First(c => c.Name == "Mystery").CategoryId
    },
    // Add more Mystery books...

    // Thriller books
    new Book
    {
        Title = "The Girl on the Train",
        Author = "Paula Hawkins",
        Price = 10.75m,
        Description = "A psychological thriller about a woman who becomes entangled in a missing persons investigation",
        Quantity = 19,
        CategoryId = categories.First(c => c.Name == "Thriller").CategoryId
    },
    new Book
    {
        Title = "The Silent Patient",
        Author = "Alex Michaelides",
        Price = 13.25m,
        Description = "A psychological thriller about a woman who murders her husband and then stops speaking",
        Quantity = 16,
        CategoryId = categories.First(c => c.Name == "Thriller").CategoryId
    },
    // Add more Thriller books...

    // Biography books
    new Book
    {
        Title = "Steve Jobs",
        Author = "Walter Isaacson",
        Price = 12.50m,
        Description = "A biography of Apple co-founder Steve Jobs, based on more than forty interviews with him",
        Quantity = 23,
        CategoryId = categories.First(c => c.Name == "Biography").CategoryId
    },
    new Book
    {
        Title = "Becoming",
        Author = "Michelle Obama",
        Price = 14.99m,
        Description = "A memoir by former First Lady Michelle Obama, discussing her life, her journey, and her inspirations",
        Quantity = 18,
        CategoryId = categories.First(c => c.Name == "Biography").CategoryId
    }
                // Add more books as needed
            };
            context.Books.AddRange(books);
            context.SaveChanges();

            // Sample data for Roles
            var roles = new List<Role>
            {
                new Role { Name = "Admin" },
                new Role { Name = "User" }
                // Add more roles as needed
            };
            context.Roles.AddRange(roles);
            context.SaveChanges();

            // Sample data for Users
            var users = new List<User>
            {
                new User
                {
                    Username = "admin",
                    Password = "admin123",
                    Status = true,
                    RoleId = roles.First(r => r.Name == "Admin").RoleId
                },
                new User
                {
                    Username = "user",
                    Password = "user123",
                    Status = true,
                    RoleId = roles.First(r => r.Name == "User").RoleId
                },
                // Add more users as needed
            };
            context.Users.AddRange(users);
            context.SaveChanges();
            /*
            // Sample data for Orders
            var orders = new List<Order>
{
                new Order
                {
                    OrderDate = DateTime.Now,
                    Status = true,
                    UserId = users.First().UserId, // Assigning a user
                    User = users.First() // Set the User property
                },
                // Add more orders as needed
};
            context.Orders.AddRange(orders);
            context.SaveChanges();

            // Sample data for OrderDetails
            var orderDetails = new List<OrderDetail>
            {
                new OrderDetail
                {
                    Quantity = 2,
                    Price = 30.00m,
                    OrderId = orders.First().OrderId, // Assigning an order
                    BookId = books.First().BookId // Assigning a book
                },
                // Add more order details as needed
            };
            context.OrderDetails.AddRange(orderDetails);
            context.SaveChanges();
            */
        }
    }
}
