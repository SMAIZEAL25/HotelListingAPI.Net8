namespace HotelListingAPI.Exceptions
{
    public class NotfoundException : ApplicationException
    {
        public NotfoundException(string name, object key) :base ($"{name} ({key}) was not found")
        {
            
        }
    }
}
