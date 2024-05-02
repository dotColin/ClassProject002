using System.Runtime.Intrinsics.X86;

namespace ClassProject002;

public class Program
{
    private static Customers customers;
    private static List<Appointment> appointments;
    private static List<CustomerAppointment> customerAppointments;
    private static Customer authenticatedCustomer;

    static void Main(string[] args)
    {
        System.Console.WriteLine("Initializing Room Reservation System...");
        System.Console.WriteLine("Version 1.9 of V.A.C.S Room Information System");
        Initialize();
        System.Console.WriteLine("Loading Menu...");
        Menu();
    }

    static void Initialize()
    {
        var c1 = new Customer
        {
            FirstName = "Vincent",
            LastName = "Herff",
            Username = "vherff",
            Password = "1234"
        };

        var c2 = new Customer
        {
            FirstName = "Alex",
            LastName = "Armendariz",
            Username = "alex",
            Password = "5678"
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
// (╯°□°)╯︵ ┻━┻ 
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
                System.Console.WriteLine($"Date & Time: {appointment.a.dateTime}");
                System.Console.WriteLine($"Room Number: {appointment.a.roomNumber}");
            }
        }
    }

    static void MakeAppointment() //the method for making the appointment for the customer
    {
        if (authenticatedCustomer == null) //if the customer is not logged in yet, it rejects the command
        {
            Console.WriteLine("Please login with your Marquette credentials first!");
            return;
        }
        else //if the customer is logged in, it asks the customer when they want the appointment and what room number they want
        {
            System.Console.WriteLine("What hour do you wish to make your room reservation? (00 = 12am | 12 = 12pm)");
            int h = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("What minute do you wish to make your room reservation? (30 = Half Past | 45 = Quarter Till)");
            int min = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("What month do you wish to make your room reservation? (1 = January | 12 = December)");
            int m = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("What day do you wish to make your room reservation? (1-31)");
            int d = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("Which room number do you want?");
            int r = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("What amenities would you like? Options: Whiteboard: 1, AV: 2, Both: 3, None: 4");
            Console.Write("Choice: ");
            string choice1 = Console.ReadLine();
            switch(choice1)
            {
                case "1":
                    Whiteboard();
                    break;
                case "2":
                    AVAmenities();
                    break;
                case "3":
                    BothAmenities();
                    break;
                case "4":
                    NoAmenities();
                    break;                  
                default:
                    Console.WriteLine("Invalid command!");
                    break;
            }
        static void Whiteboard()
        {
            Console.WriteLine("We will make sure your room has a whiteboard!");
        }

        static void AVAmenities()
        {
            Console.WriteLine("We will make sure your room has AV amenities!");
        }
        static void BothAmenities()
        {
            Console.WriteLine("We will make sure your room has both whiteboard and AV amenities!");
        }
        static void NoAmenities()
        {
            Console.WriteLine("Your room will not have any whiteboards or AV amenities.");
        }


            DateTime dt = new DateTime(2024, m, d, h, min, 00); //year, month, day, hour, minute, second
            var newAppointment = new Appointment //creating an appointment object out of the information the user gave the program
            {
                roomNumber = r,
                dateTime = dt
            };        
            var ca = new CustomerAppointment(authenticatedCustomer, newAppointment); //adds a new customer appointment to the customer appointment list
            customerAppointments.Add(ca);
            System.Console.WriteLine($"Date & Time for your reservation: {newAppointment.dateTime}");
            System.Console.WriteLine($"Room Number for your reservation: {newAppointment.roomNumber}");
        }
    }
}