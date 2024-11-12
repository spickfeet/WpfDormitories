using WpfDormitories.Model.FullName;

namespace WpfDormitories.Model.PersonDocument.Passport
{
    public class Passport : IPassport
    {
        public IFullName FullName { get; set; }
        public string Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public uint Series { get; set; }
        public uint Number { get; set; }
        public DateOnly DateOfIssue { get; set; }
        public string WhoGave { get; set; }

        public Passport(IFullName fullName, string gender, DateOnly dateOfBirth, uint series, uint number, DateOnly dateOfIssue, string whoGave) 
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
