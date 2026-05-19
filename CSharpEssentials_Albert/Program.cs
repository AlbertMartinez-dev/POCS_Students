
using CSharpEssentials_Albert.Entities;

Console.WriteLine("Escriu el nom del guest:");
var guestName = Console.ReadLine();

Console.WriteLine("Escriu el número d'Habitacio: ");
var roomNumber = int.Parse(Console.ReadLine());

var reservation = Reservation.Create(guestName,roomNumber);

Console.WriteLine($"Guest: {reservation.GuestName}");
Console.WriteLine($"ID generado: {reservation.Id.Value}");
