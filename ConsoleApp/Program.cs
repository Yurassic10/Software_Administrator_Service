// See https://aka.ms/new-console-template for more information
using BLL.IServices;
using BLL.Services;
using DataAccessLogic.ADO;
using DataAccessLogic.Interfaces;
using DTO.Model;
using System.Security.Cryptography;
using System.Text;

Console.WriteLine("Hello, World!");
// Yura123 пароль мабуть

IServiceAction serviceAction = new ServiceAction();
IServiceActivity serviceActivity = new ServiceActivity();
IServiceAdmin serviceAdmin = new ServiceAdmin();
IServiceRole serviceRole = new ServiceRole();
IServiceSuperAdmin serviceSuperAdmin = new ServiceSuperAdmin();
IServiceStatus serviceStatus = new ServiceStatus();

// Запитуємо вхідні дані від користувача
//Console.WriteLine("Введіть FirstName:");
//string FirstName = Console.ReadLine();

//Console.WriteLine("Введіть LastName:");
//string LastName = Console.ReadLine();

//Console.WriteLine("Введіть email:");
//string email = Console.ReadLine();

//Console.WriteLine("Введіть password:");
//string password = Console.ReadLine();

//Console.WriteLine("Введіть email:");
//Console.WriteLine("Введіть email:");
//string email = Console.ReadLine();
//string email = Console.ReadLine();
//Console.WriteLine("Введіть пароль:");
//string password = Console.ReadLine();

// Перевіряємо вхід
//bool isLoginSuccessful = serviceSuperAdmin.IsLogin(email, password);

//if (isLoginSuccessful)
//{
//    Console.WriteLine("Вхід успішний!");
//}
//else
//{
//    Console.WriteLine("Невірний email або пароль.");
//}

Console.ReadKey();
//var adminobj = serviceSuperAdmin.GetByEmail();

List<User> Users = serviceSuperAdmin.GetProducts();
Console.WriteLine("\tSuper Admin");
foreach (var product in Users)
{
    string passwordAsString = Convert.ToBase64String(product.Password);
    Console.WriteLine($"FirstName:{product.FirstName}\t LastName:{product.LastName} StatusId:{product.StatusId}\t RoleId:{product.RoleId}\t Password:{passwordAsString}");
}
Console.WriteLine("\tAdmin");
foreach (var product in serviceAdmin.GetProducts())
{
    string passwordAsString = Convert.ToBase64String(product.Password);
    Console.WriteLine($"FirstName:{product.FirstName}\t LastName:{product.LastName} StatusId:{product.StatusId}\t RoleId:{product.RoleId} \t Password:{passwordAsString}");
}
Console.WriteLine("\tActions");
foreach (var product in serviceAction.GetProducts())
{
    Console.WriteLine($"ID:{product.Id}\t Name:{product.Name} ");
}


// Запитуємо дані для нового користувача
Console.Write("Введіть ім'я: ");
string firstName = Console.ReadLine();
Console.Write("Введіть прізвище: ");
string lastName = Console.ReadLine();
Console.Write("Введіть email: ");
string email = Console.ReadLine();
Console.Write("Введіть пароль: ");
string passwordInput = Console.ReadLine();


// Генеруємо сіль для пароля
var salt = Guid.NewGuid();
byte[] passwordHash = HashPassword(passwordInput.ToString(), salt);

User newUser = new User
{
    FirstName = firstName,
    LastName = lastName,
    Email = email,
    Password = passwordHash,
    Salt = salt,
    RoleId = 3, // Вкажіть відповідну роль
    StatusId = 1, // Наприклад, активний статус
    CreatedAt = DateTime.Now,
    UpdatedAt = DateTime.Now
};

// Додаємо користувача через сервіс
serviceSuperAdmin.Add(newUser);
Console.WriteLine("Новий користувач успішно доданий.");
    

// Метод хешування пароля з сіллю
static byte[] HashPassword(string password, Guid salt)
{
    using (var algorithm = SHA512.Create())
    {
        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(password + salt.ToString()));
    }
}



Console.ReadKey();


Console.WriteLine("\tAdmin");
foreach (var product in serviceAdmin.GetProducts())
{
    string passwordAsString = Convert.ToBase64String(product.Password);
    Console.WriteLine($"FirstName:{product.FirstName}\t LastName:{product.LastName} StatusId:{product.StatusId}\t RoleId:{product.RoleId} \t Password:{passwordAsString}");
}

Console.ReadKey();