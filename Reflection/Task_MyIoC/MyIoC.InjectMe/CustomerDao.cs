using MyIoCAttributes;

namespace MyIoC
{
    [Export(typeof(ICustomerDao))]
    public class CustomerDao : ICustomerDao
    { }
}