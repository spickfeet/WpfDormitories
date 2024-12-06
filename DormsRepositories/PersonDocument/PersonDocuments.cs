using WpfDormitories.Model.PersonDocument.Passport;

namespace WpfDormitories.Model.PersonDocument
{
    public class PersonDocuments : IPersonDocuments
    {
        public string RegistrationNumber { get; set; }
        public IPassport Passport { get; set; }

        public PersonDocuments(string registrationNumber, IPassport passport) 
        {
            RegistrationNumber = registrationNumber;
            Passport = passport;
        }
    }
}
