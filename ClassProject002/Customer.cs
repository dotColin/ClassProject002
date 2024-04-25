using System.Data.Common;
using System.Dynamic;

namespace ClassProject002;

public class Customer
{
    public int Id {get; set;}
    public String FirstName {get; set;}
    public String LastName {get; set;}
    public String Username {get; set;}
    public String Password {get; set;}

    public Customer(int Id, String FirstName,String LastName,String Username,String Password)
    {
        this.Id = Id;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Username = Username;
        this.Password = Password;
    }

}
