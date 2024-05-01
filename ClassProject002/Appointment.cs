namespace ClassProject002;

public class Appointment
{
    private static int autoIncrement;
    public int Id {get;}
    public DateTime dateTime {get; set;}
    public int roomNumber {get; set;}

    public Appointment()
    {
        autoIncrement++;
        Id = autoIncrement;
    }

}