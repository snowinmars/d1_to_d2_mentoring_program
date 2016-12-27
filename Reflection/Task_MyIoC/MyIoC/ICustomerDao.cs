namespace MyIoC
{
    public interface ICustomerDao
    {
    }

    [Export(typeof(ICustomerDao))]
    public class CustomerDao : ICustomerDao
    { }
}