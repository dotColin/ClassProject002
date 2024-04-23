namespace ClassProject002;

public class CustomerAppointment
{
    public Customer c {get; set;}
    public Appointment a {get; set;}
    public CustomerAppointment(Customer c, Appointment a){
        this.c = c;
        this.a = a;
    }
}
