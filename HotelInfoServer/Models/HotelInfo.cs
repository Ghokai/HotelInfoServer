namespace HotelInfoServer.Models
{
    public class HotelInfo
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Stars { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Uri { get; set; }

        public HotelInfo()
        {

        }

        public HotelInfo(string Name, string Address, int Stars, string Contact, string Phone, string Uri)
        {
            this.Name = Name;
            this.Address = Address;
            this.Stars = Stars;
            this.Contact = Contact;
            this.Phone = Phone;
            this.Uri = Uri;
        }
    }
}
