using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

Console.WriteLine("Да бъде ли създадена нова база данни Library.db (Y/N): ");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
string userInput = Console.ReadLine().ToUpper();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

if (userInput == "Y")
{
    string filePath = @"../../../../Library.db";
    File.Delete(filePath);

    SqliteConnection sqliteConnection;
    sqliteConnection = CreateConnection();
    CreateTable(sqliteConnection);
}

//string connectionString = "Data Source=../../../../Library.db";

//using (SQLiteConnection connection = new SQLiteConnection(connectionString))
//{
//    connection.Open();

//    using (SQLiteCommand command = new SQLiteCommand(connection))
//    {
//        command.CommandText = "SELECT * FROM Readers";

//        using (SQLiteDataReader reader = command.ExecuteReader())
//        {
//            while (reader.Read())
//            {
//                Console.WriteLine("ID: {0}, FirstName: {1}, LastName: {2}, EGN: {3}, Email: {4}",
//                    reader[0], reader[1], reader[2], reader[3], reader[4]);
//            }
//        }
//    }
//}

static SqliteConnection CreateConnection()
{
    SQLitePCL.Batteries.Init();
    SqliteConnection sqliteConn;
    sqliteConn = new SqliteConnection("Data source=../../../../Library.db;");
    sqliteConn.Open();

    return sqliteConn;
}

static void CreateTable(SqliteConnection conn)
{
    SqliteCommand sqliteCommand;
    //създаване на базата
    string createSQL = "CREATE TABLE IF NOT EXISTS TestLibrary(Col1 VARCHAR(10), Col2 VARCHAR(20))";
    sqliteCommand = conn.CreateCommand();
    sqliteCommand.CommandText = createSQL;
    sqliteCommand.ExecuteNonQuery();

    //създаване на таблица за книгите
    sqliteCommand.CommandText = @"CREATE TABLE IF NOT EXISTS Books (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title varchar(255) NOT NULL,
                    ISBN varchar(50) NOT NULL,
                    Author varchar(255) NOT NULL,
                    Genre varchar(50) NOT NULL,
                    AvailableCount INTEGER NOT NULL DEFAULT 0
                )";
    sqliteCommand.ExecuteNonQuery();

    //създаване на таблица за читателите
    sqliteCommand.CommandText = @"CREATE TABLE IF NOT EXISTS Readers (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    FirstName varchar(50) NOT NULL,
                    LastName varchar(50) NOT NULL,
                    EGN varchar(10) NOT NULL,
                    Email varchar(50) NOT NULL
                )";
    sqliteCommand.ExecuteNonQuery();

    //Създаване на свързващата таблица BookLoans
    sqliteCommand.CommandText = @"CREATE TABLE BookLoans(
    loan_id INTEGER PRIMARY KEY AUTOINCREMENT,
    book_id INT,
    reader_id INT,
    borrowed_date DATE,
    returned_date DATE,
    FOREIGN KEY(book_id) REFERENCES Books(id),
    FOREIGN KEY(reader_id) REFERENCES Readers(id)
    )";
    sqliteCommand.ExecuteNonQuery();

    //попълване на тестови книги
    sqliteCommand.CommandText = @"INSERT INTO Books (Title, ISBN, Author, Genre, AvailableCount)
                    VALUES
                    ('Да убиеш присмехулник', '1789545855975', 'Харпър Лий', 'Fiction', 10),
                    ('Гордост и предразсъдъци', '9189545853975', 'Джейн Остин', 'Fiction', 10),
                    ('1984', '9719545853975', 'Джордж Оруел', 'Fiction', 10),
                    ('Фермата на животните', '9789545853975', 'Джордж Оруел', 'Fiction', 10),
                    ('Мечо Пух', '9781545853975', 'А. А. Милн', 'Fiction', 10),
                    ('Малкият принц', '9789145853975', 'Антоан дьо Сент-Екзюпери', 'Fiction', 10),
                    ('Приказка за стълбата', '9789515853975', 'Христо Смирненски', 'Fiction', 10),
                    ('Бай Ганьо', '9789541853975', 'Алеко Константинов', 'Fiction', 10),
                    ('Тютюн', '9789545153975', 'Димитър Димов', 'Fiction', 10),
                    ('Железният светилник', '9789545813975', 'Димитър Талев', 'Fiction', 10),
                    ('Под игото', '9789545853175', 'Иван Вазов', 'Fiction', 10),
                    ('Тил Уленшпигел', '97895458531975', 'Шарл де Костер', 'Fiction', 10),
                    ('Мадам Бовари', '9789545853915', 'Гюстав Флобер', 'Fiction', 10),
                    ('Престъпление и наказание', '9789545853971', 'Фьодор Достоевски', 'Fiction', 10),
                    ('Анна Каренина', '9789545853972', 'Лев Толстой', 'Fiction', 10),
                    ('Големи надежди', '9789545853925', 'Чарлз Дикенс', 'Fiction', 10),
                    ('Гроздовете на гнева', '9789545853275', 'Джон Стайнбек', 'Fiction', 10),
                    ('Портретът на Дориан Грей', '9789545852975', 'Оскар Уайлд', 'Fiction', 10),
                    ('Преображението', '9789545823975', 'Франц Кафка', 'Fiction', 10),
                    ('Сто години самота', '9789545253975', 'Габриел Гарсия Маркес', 'Fiction', 10);
                ";
    sqliteCommand.ExecuteNonQuery();

    //попълване на тестови книги
    sqliteCommand.CommandText = @"INSERT INTO Readers(FirstName, LastName, EGN, Email)
                    VALUES
                    ('Антон', 'Андонов', '5804063129', 'anton.andonov@gamil.com'),
                    ('Борислава', 'Бончева', '0803206798', 'borislava.boncheva@gamil.com'),
                    ('Валентина', 'Василева', '9009125999', 'valentina.vasileva@gamil.com'),
                    ('Виктория', 'Великова', '3205199372', 'victoria.velikova@gamil.com'),
                    ('Габриела', 'Георгиева', '9705201355', 'gabriela.georgieva@gamil.com'),
                    ('Данаил', 'Добрев', '7709182162', 'danail.dobrev@gamil.com');
                ";
    sqliteCommand.ExecuteNonQuery();

    //Попълване на таблицата BookLoans с 20 тестови записа
    sqliteCommand.CommandText = @"INSERT INTO BookLoans(book_id, reader_id, borrowed_date, returned_date) VALUES
                    (1, 3, '2023-09-01', NULL),
                    (4, 1, '2023-08-15', '2023-09-01'),
                    (7, 2, '2023-07-20', '2023-08-10'),
                    (10, 5, '2023-06-10', NULL),
                    (13, 4, '2023-05-01', '2023-06-01'),
                    (16, 3, '2023-04-15', NULL),
                    (19, 2, '2023-03-20', '2023-04-10'),
                    (2, 1, '2023-02-05', NULL),
                    (5, 5, '2023-01-15', '2023-02-01'),
                    (8, 4, '2022-12-20', NULL),
                    (11, 3, '2022-11-10', '2022-12-05'),
                    (14, 2, '2022-10-01', NULL),
                    (17, 1, '2022-09-15', '2022-10-01'),
                    (20, 5, '2022-08-20', NULL),
                    (3, 4, '2022-07-05', '2022-08-01'),
                    (6, 3, '2022-06-15', NULL),
                    (9, 2, '2022-05-10', '2022-06-05'),
                    (12, 1, '2022-04-01', NULL),
                    (15, 5, '2022-03-15', '2022-04-01'),
                    (18, 4, '2022-02-20', NULL);
                ";
    sqliteCommand.ExecuteNonQuery();
}