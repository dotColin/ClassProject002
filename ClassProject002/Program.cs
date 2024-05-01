﻿namespace ClassProject002;

public class Program
{
    private static Customers customers;
    private static List<Appointment> appointments;
    private static List<CustomerAppointment> customerAppointments;
    private static Customer authenticatedCustomer;

    static void Main(string[] args)
    {
        Console.WriteLine("Initializing Room Reservation System...");
        Initialize();
        Menu();
    }

    static void Initialize()
    {
        var c1 = new Customer
        {
            FirstName = "Kambiz",
            LastName = "Saffari",
            Username = "kambiz",
            Password = "1234"
        };

        var c2 = new Customer
        {
            FirstName = "Jeremy",
            LastName = "Lee",
            Username = "jlee",
            Password = "9876"
        };

        var a1 = new Appointment();
        var a2 = new Appointment();
        var a3 = new Appointment();

        var ca1 = new CustomerAppointment(c1, a1);
        var ca2 = new CustomerAppointment(c1, a2);
        var ca3 = new CustomerAppointment(c2, a3);

        customers = new Customers();
        customers.customerList.Add(c1);
        customers.customerList.Add(c2);

        customerAppointments = new List<CustomerAppointment>();
        customerAppointments.Add(ca1);
        customerAppointments.Add(ca2);
        customerAppointments.Add(ca3);

        appointments = new List<Appointment>();
        appointments.Add(a1);
        appointments.Add(a2);
        appointments.Add(a3);

    }

    static void Menu()
    {
        System.Console.WriteLine("Welcome to the library room reservation system!");
        bool done = false;

        while (!done)
        {
            Console.WriteLine("Options: Login: 1, Logout: 2, Sign Up: 3, Room Reservations: 4, Make Reservation: 5, Quit: q");
            Console.Write("Choice: ");
            string choice = Console.ReadLine();

            switch(choice)
            {
                case "1":
                    LoginMenu();
                    break;
                case "2":
                    LogOutMenu();
                    break;
                case "3":
                    SignUpMenu();
                    break;
                case "4":
                    AppointmentsMenu();
                    break;
                case "5":
                    MakeAppointment();
                    break;                    
                case "q":
                    done = true;
                    break;
                default:
                    Console.WriteLine("Invalid command!");
                    break;
            }

        }


    }

    static void LoginMenu()
    {
        if(authenticatedCustomer == null)
        {
            Console.Write("Enter your Marquette Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your Marquette Password: ");
            string password = Console.ReadLine();

            authenticatedCustomer = customers.Authenticate(username, password);
            if (authenticatedCustomer != null)
            {
                Console.WriteLine($"Welcome back, {authenticatedCustomer.FirstName}!");
            }
            else
            {
                Console.WriteLine("Invalid Marquette Username or Password!");
            }
        }


    }

    static void LogOutMenu()
    {
        authenticatedCustomer = null;
        Console.WriteLine("Logged out!");
    }

    static void SignUpMenu()
    {
        if (authenticatedCustomer != null)
        {
            Console.WriteLine("Please logout first!");
            return;
        }
        else
        {
            Console.Write("First Name: ");
            string firstname = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastname = Console.ReadLine();
            Console.Write("Marquette Username: ");
            string username = Console.ReadLine();
            Console.Write("Marquette Password: ");
            string password = Console.ReadLine();

            var newCustomer = new Customer
            {
                FirstName = firstname,
                LastName = lastname,
                Username = username,
                Password = password
            };
            customers.customerList.Add(newCustomer);
            Console.WriteLine("Library room reservation system profile created!");
        }
    }

    static void AppointmentsMenu()
    {
        if (authenticatedCustomer == null)
        {
            Console.WriteLine("Please login with your Marquette credentials first!");
            return;
        }

        var appointmentList = customerAppointments.Where(o => o.c.Username == authenticatedCustomer.Username);

        if(appointmentList.Count() == 0)
        {
            Console.WriteLine("0 room reservations found.");
        }
        else
        {
            foreach(var appointment in appointmentList)
            {
                Console.WriteLine($"Date & Time: {appointment.a.dateTime}");
                System.Console.WriteLine($"Room Number: {appointment.a.roomNumber}");
            }
        }
        
    }

    static void MakeAppointment()
    {
        if (authenticatedCustomer == null)
        {
            Console.WriteLine("Please login with your Marquette credentials first!");
            return;
        }
        else
        {
            System.Console.WriteLine("What hour do you wish to make your room reservation?");
            int h = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("What minute do you wish to make your room reservation?");
            int min = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("What month do you wish to make your room reservation?");
            int m = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("What day do you wish to make your room reservation?");
            int d = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("Which room number do you want?");
            int r = Convert.ToInt32(Console.ReadLine());
            DateTime dt = new DateTime(2024, m, d, h, min, 00); //year, month, day, hour, minute, second
            
            var newAppointment = new Appointment
            {
                roomNumber = r,
                dateTime = dt
            };
            appointments.Add(newAppointment);

            Console.WriteLine($"Library room reservation created for room number {r} at {h}:{min} on {m}/{d}");
        }
    }

}