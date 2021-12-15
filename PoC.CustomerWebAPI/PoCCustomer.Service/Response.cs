using PocCustomer.Model;

namespace PoCCustomer.Service
{
    public class Response<T>
    {
        public T Customer { get; set; }
        public bool Exists { get; set; }
    }
}
