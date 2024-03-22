
using BookStore_Mock_Project.Model;

namespace BookStore_Mock_Project.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Kiểm tra xem liệu cơ sở dữ liệu đã được khởi tạo chưa
            if (context.Books.Any() || context.BookCategories.Any() || context.Users.Any() ||
                context.Roles.Any() || context.Orders.Any() || context.OrderDetails.Any())
            {
                return; // Nếu đã có dữ liệu, không cần khởi tạo lại
            }

            // Thêm dữ liệu vào bảng BookCategories
            var categories = new BookCategory[]
            {
                new BookCategory { CategoryId = Guid.NewGuid(), Name = "Fiction", Description = "Books of fiction genre" },
                new BookCategory { CategoryId = Guid.NewGuid(), Name = "Non-Fiction", Description = "Books of non-fiction genre" },
                new BookCategory { CategoryId = Guid.NewGuid(), Name = "Science-Fiction", Description = "Books of science fiction genre" },
                new BookCategory { CategoryId = Guid.NewGuid(), Name = "Fantasy", Description = "Books of fantasy genre" },
                new BookCategory { CategoryId = Guid.NewGuid(), Name = "Biography", Description = "Books of biography genre" }
            };
            context.BookCategories.AddRange(categories);
            context.SaveChanges();

            // Thêm dữ liệu vào bảng Books
            var books = new Book[]
        {
            new Book { BookId = Guid.NewGuid(), Title = "1984", Author = "George Orwell", Price = 10.99m, image = "1984.jpg", Description = "Nineteen Eighty-Four, often referred to as 1984, is a dystopian social science fiction novel by the English novelist George Orwell.", CategoryId = categories[0].CategoryId, Quantity = 102 },
            new Book { BookId = Guid.NewGuid(), Title = "To Kill a Mockingbird", Author = "Harper Lee", Price = 12.99m, image = "mockingbird.jpg", Description = "To Kill a Mockingbird is a novel by Harper Lee published in 1960. It was immediately successful, winning the Pulitzer Prize, and has become a classic of modern American literature.", CategoryId = categories[0].CategoryId, Quantity = 242 },
            new Book { BookId = Guid.NewGuid(), Title = "Dune", Author = "Frank Herbert", Price = 14.99m, image = "dune.jpg", Description = "Dune is a science fiction novel by American author Frank Herbert, originally published as two separate serials in Analog magazine.", CategoryId = categories[2].CategoryId, Quantity = 99 },
            new Book { BookId = Guid.NewGuid(), Title = "The Hobbit", Author = "J.R.R. Tolkien", Price = 9.99m, image = "hobbit.jpg", Description = "The Hobbit, or There and Back Again, is a children's fantasy novel by English author J. R. R. Tolkien. It was published on 21 September 1937 to wide critical acclaim.", CategoryId = categories[3].CategoryId, Quantity = 0 },
            new Book { BookId = Guid.NewGuid(), Title = "Steve Jobs", Author = "Walter Isaacson", Price = 15.99m, image = "steve_jobs.jpg", Description = "Steve Jobs is the authorized self-titled biography of Steve Jobs, co-founder of Apple Inc.", CategoryId = categories[4].CategoryId, Quantity = 0 },
            new Book { BookId = Guid.NewGuid(), Title = "Jurassic Park", Author = "Michael Crichton", Price = 11.99m, image = "jurassic_park.jpg", Description = "Jurassic Park is a 1990 science fiction novel written by Michael Crichton, divided into seven sections, with numerous chapters in each.", CategoryId = categories[2].CategoryId, Quantity = 102 },
            new Book { BookId = Guid.NewGuid(), Title = "Harry Potter and the Sorcerer's Stone", Author = "J.K. Rowling", Price = 13.99m, image = "harry_potter.jpg", Description = "Harry Potter and the Philosopher's Stone is a fantasy novel written by British author J. K. Rowling.", CategoryId = categories[3].CategoryId, Quantity = 103 },
            new Book { BookId = Guid.NewGuid(), Title = "The Da Vinci Code", Author = "Dan Brown", Price = 12.49m, image = "da_vinci_code.jpg", Description = "The Da Vinci Code is a 2003 mystery thriller novel by Dan Brown.", CategoryId = categories[1].CategoryId, Quantity = 0 },
            new Book { BookId = Guid.NewGuid(), Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Price = 10.49m, image = "great_gatsby.jpg", Description = "The Great Gatsby is a 1925 novel by American writer F. Scott Fitzgerald.", CategoryId = categories[0].CategoryId, Quantity = 20 }
        };

            context.Books.AddRange(books);
            context.SaveChanges();

            // Thêm dữ liệu vào bảng Roles
            var roles = new Role[]
            {
                new Role { RoleId = Guid.NewGuid(), Name = "Admin" },
                new Role { RoleId = Guid.NewGuid(), Name = "User" }
            };
            context.Roles.AddRange(roles);
            context.SaveChanges();

            // Thêm dữ liệu vào bảng Users
            var users = new User[]
            {
                new User { UserId = Guid.NewGuid(), Username = "admin", Password = "admin123", RoleId = roles[0].RoleId },
                new User { UserId = Guid.NewGuid(), Username = "user1", Password = "user123", RoleId = roles[1].RoleId }
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            // Thêm dữ liệu vào bảng Orders
            var orders = new Order[]
            {
                new Order { OrderId = Guid.NewGuid(), OrderDate = DateTime.Now, UserId = users[0].UserId },
                new Order { OrderId = Guid.NewGuid(), OrderDate = DateTime.Now, UserId = users[1].UserId }
            };
            context.Orders.AddRange(orders);
            context.SaveChanges();

            // Thêm dữ liệu vào bảng OrderDetails
            var orderDetails = new OrderDetail[]
            {
                new OrderDetail { OrderDetailId = Guid.NewGuid(), Quantity = 2, Price = 21.98m, OrderId = orders[0].OrderId, BookId = books[0].BookId },
                new OrderDetail { OrderDetailId = Guid.NewGuid(), Quantity = 1, Price = 12.99m, OrderId = orders[1].OrderId, BookId = books[1].BookId }
            };
            context.OrderDetails.AddRange(orderDetails);
            context.SaveChanges();
        }
    }
}
