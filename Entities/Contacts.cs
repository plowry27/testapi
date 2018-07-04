using TodoApi.DataAccess;

namespace TodoApi.Contacts
{
    public class Contact
    {
        public int PersonID {get; set;}
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string city { get; set; }

        
        public Contact GetContactInfo()
        {
            var info = DataAccess.PersonsDS.GetPersonDA(firstname,lastname);
            return info;

        }

        public string AddContactInfo()
        {

            DataAccess.PersonsDS.AddContactInfo(PersonID, firstname, lastname, address,city);
            return "1";

        }

    }

}