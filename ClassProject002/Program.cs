using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace ClassProject002;

public class Program
{
    private static Customers customers;
    private static List<Appointment> appointments;
    private static List<CustomerAppointment> customerAppointments;
    private static Customer authenticatedCustomer;

    static void Main(String[] args)
    {
        System.Console.WriteLine("Initializing");
        Initialize();
        Menu();
    }

    static void Initialize()
    {
        var c1 = new Customer()
        {
            FirstName = "Kambiz", LastName = "Saffari", Username = "kambiz", Password = "1234"
        };

        var c2 = new Customer()
        {
            FirstName = "Jeremy", LastName = "Lee", Username = "jlee", Password = "5678"
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

        appointments.Add(a1);
        appointments.Add(a2);
        appointments.Add(a3);

    }

    static void Menu()
    {
        bool done = false;
        while (!done)
        {
            System.Console.WriteLine("Options: Login: 1, Logout: 2, Sign Up: 3, Appointments: 4, Quit: 5");
            System.Console.Write("Choice: ");
            String choice = Console.ReadLine();
            switch(choice)
            {
                case "1":
                    LoginMenu();
                    break;
                case "2":
                    Logout();
                    break;
                case "3":
                    SignUpMenu();
                    break;
                case "4":
                    ViewAppointments();
                    break;
                case "q":
                    done = true;
                    break;
                default:
                    System.Console.WriteLine("Invalid Command");
                    break;
            }
        }
    }

    static void LoginMenu()
    {
        if (authenticatedCustomer == null)
        {
            System.Console.WriteLine("Enter username:");
            String username = Console.ReadLine();
            System.Console.WriteLine("Enter password:");
            String password = Console.ReadLine();
            authenticatedCustomer = customers.Authenticate(username, password);
            if (authenticatedCustomer != null)
            {
                System.Console.WriteLine($"Welcome {authenticatedCustomer.FirstName}");
            }
            else
            {
                System.Console.WriteLine("Invalid username or password");
            }
        }
        
    }

    static void Logout()
    {
        authenticatedCustomer = null;
        System.Console.WriteLine("Logged out");
    }

    static void SignUpMenu()
    {
        System.Console.WriteLine("Enter first name");
        String firstname = Console.ReadLine();
        System.Console.WriteLine("Enter last name");
        String lastname = Console.ReadLine();
        System.Console.WriteLine("Enter username");
        String username = Console.ReadLine();
        System.Console.WriteLine("Enter password");
        String password = Console.ReadLine();

        var newCustomer = new Customer
        {
            FirstName = firstname
            LastName = lastname
            Username = username
            Password = password
        };
        customers.customerList.Add(newCustomer);
        System.Console.WriteLine("Profile Created");
    }

    static void ViewAppointments()
    {
        if (authenticatedCustomer == null)
        {
            System.Console.WriteLine("Please log in first");
            return;
        }
        var appointmentList = customerAppointments.Where(o => o.c.Username == authenticatedCustomer);

        if (appointmentList.Count() == 0)
        {
            System.Console.WriteLine("Zero Appointments Found");
        }
        else
        {
            foreach(var appointment in appointmentList)
            {
                System.Console.WriteLine(appointment.a.dateTime);
            }
        }
    }
}
