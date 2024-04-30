namespace ClassProject002;

public class Customer
{
    private static int autoIncrement;
    public int Id {get;}
    public String Username {get; set;}
    public String Password {get; set;}
    public String FirstName {get; set;}
    public String LastName {get; set;}

    public Customer()
    {
        autoIncrement++;
        Id = autoIncrement;
    }


}
