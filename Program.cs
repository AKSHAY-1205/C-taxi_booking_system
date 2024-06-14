using System;
using System.Collections.Generic;
using System.Linq;

namespace TaxiBookingSystem
{
    public class Taxi
    {
        public int Id { get; set; }
        public string DriverName { get; set; }
        public bool IsAvailable { get; set; }

        public Taxi(int id, string driverName, bool isAvailable)
        {
            Id = id;
            DriverName = driverName;
            IsAvailable = isAvailable;
        }
    }

    public class Booking
    {
        public int BookingId { get; set; }
        public int TaxiId { get; set; }
        public string CustomerName { get; set; }
        public string PickUpLocation { get; set; }
        public string DropOffLocation { get; set; }
        public DateTime PickUpTime { get; set; }
        public DateTime BookingTime { get; set; }

        public Booking(int bookingId, int taxiId, string customerName, string pickUpLocation, string dropOffLocation, DateTime pickUpTime, DateTime bookingTime)
        {
            BookingId = bookingId;
            TaxiId = taxiId;
            CustomerName = customerName;
            PickUpLocation = pickUpLocation;
            DropOffLocation = dropOffLocation;
            PickUpTime = pickUpTime;
            BookingTime = bookingTime;
        }
    }

    class Program
    {
        private static List<Taxi> taxis = new List<Taxi>
        {
            new Taxi(1, "John Doe", true),
            new Taxi(2, "Jane Smith", true),
            new Taxi(3, "Bob Brown", true)
        };

        private static List<Booking> bookings = new List<Booking>();
        private static int bookingCounter = 1;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Taxi Booking System");
                Console.WriteLine("1. List available taxis");
                Console.WriteLine("2. Book a taxi");
                Console.WriteLine("3. View all bookings");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ListAvailableTaxis();
                        break;
                    case "2":
                        BookTaxi();
                        break;
                    case "3":
                        ViewBookings();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void ListAvailableTaxis()
        {
            Console.Clear();
            Console.WriteLine("Available Taxis:");
            foreach (var taxi in taxis.Where(t => t.IsAvailable))
            {
                Console.WriteLine($"ID: {taxi.Id}, Driver: {taxi.DriverName}");
            }
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        static void BookTaxi()
        {
            Console.Clear();
            Console.Write("Enter Taxi ID to book: ");
            if (int.TryParse(Console.ReadLine(), out int taxiId))
            {
                var taxi = taxis.FirstOrDefault(t => t.Id == taxiId && t.IsAvailable);
                if (taxi != null)
                {
                    Console.Write("Enter your name: ");
                    var customerName = Console.ReadLine();
                    Console.Write("Enter pick-up location: ");
                    var pickUpLocation = Console.ReadLine();
                    Console.Write("Enter drop-off location: ");
                    var dropOffLocation = Console.ReadLine();
                    Console.Write("Enter pick-up time (yyyy-MM-dd HH:mm): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime pickUpTime))
                    {
                        taxi.IsAvailable = false;
                        bookings.Add(new Booking(bookingCounter++, taxiId, customerName, pickUpLocation, dropOffLocation, pickUpTime, DateTime.Now));
                        Console.WriteLine("Taxi booked successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid date and time format.");
                    }
                }
                else
                {
                    Console.WriteLine("Taxi not available or invalid ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        static void ViewBookings()
        {
            Console.Clear();
            Console.WriteLine("Bookings:");
            foreach (var booking in bookings)
            {
                Console.WriteLine($"Booking ID: {booking.BookingId}, Taxi ID: {booking.TaxiId}, Customer: {booking.CustomerName}, " +
                                  $"Pick-Up: {booking.PickUpLocation}, Drop-Off: {booking.DropOffLocation}, Pick-Up Time: {booking.PickUpTime}, " +
                                  $"Booking Time: {booking.BookingTime}");
            }
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}
