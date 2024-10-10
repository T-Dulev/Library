using System.Collections.Generic;
using System.Data.SQLite;

Console.WriteLine("Да бъде ли създадена нова база данни Library.db (Y/N): ");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
string userInput = Console.ReadLine().ToUpper();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

if (userInput == "Y")
{
    string filePath = @"../../../../Library.db";
    File.Delete(filePath);

    SQLiteConnection sqliteConnection;
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

static SQLiteConnection CreateConnection()
{
    SQLiteConnection sqliteConn;
    sqliteConn = new SQLiteConnection("Data source=../../../../Library.db; version = 3; New = True; Conpress = True");
    sqliteConn.Open();

    return sqliteConn;
}

static void CreateTable(SQLiteConnection conn)
{
    SQLiteCommand sqliteCommand;
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

    
}