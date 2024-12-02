using WpfDormitories.Model.FullName;

namespace WpfDormitories.Model.PersonDocument.Passport
{
    public class Passport : IPassport
    {
        public IFullName FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public DateTime DateOfIssue { get; set; }
        public string WhoGave { get; set; }

        public Passport(IFullName fullName, string gender, DateTime dateOfBirth, string series, string number, DateTime dateOfIssue, string whoGave) 
        {
            FullName = fullName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Series = series;
            Number = number;
            DateOfIssue = dateOfIssue;
            WhoGave = whoGave;
        }

    }
}
