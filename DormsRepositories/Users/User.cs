namespace WpfDormitories.Model.Users
{
    public class User : IUser
    {
        private string _login;
        private string _password;

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public User(string login, string password)
        {
            _login = login;
            _password = password;
        }



    }
}
