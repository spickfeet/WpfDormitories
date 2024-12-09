﻿using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;
using Timer = System.Windows.Threading.DispatcherTimer;
using WpfDormitories.Model.Services.LogIn;
using WpfDormitories.Model.Convertors;

namespace WpfTest.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _login;
        private string _password;
        private string _leaKeyboardLayout;
        private string _capsLockStatus;
        private ILogInService _logInService = new LogInService(new HashCodeConvertor());

        private Timer _timerForWindow = new Timer();
        public Action OnLogInSuccess;

        public string Login
        {
            get => _login;
            set 
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public string LeaKeyboardLayoutText
        {
            get => _leaKeyboardLayout;
            set
            {
                _leaKeyboardLayout = value;
                OnPropertyChanged("LeaKeyboardLayoutText");
            }
        }

        public string CapsLockStatusText
        {
            get => _capsLockStatus;
            set
            {
                _capsLockStatus = value;
                OnPropertyChanged("CapsLockStatusText");
            }
        }

        
        public event PropertyChangedEventHandler? PropertyChanged;

        public LoginViewModel()
        {
            //StartTimer(100);
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void StartTimer(long interval)
        {
            _timerForWindow.Interval = new System.TimeSpan(interval);
            _timerForWindow.Start();
            _timerForWindow.Tick += Timer_Tick;

            //textBlockVersion.Text = "Версия" + assembly.GetName().Version.ToString();
        }



        private void Timer_Tick(object sender, System.EventArgs e)
        {
            if (Console.CapsLock)
                CapsLockStatusText = "Клавиша CapsLock нажата";
            else
                CapsLockStatusText = "";

            LeaKeyboardLayoutText = "Язык ввода " + ParseLanguage(InputLanguageManager.Current.CurrentInputLanguage.DisplayName);
        }

        private string ParseLanguage(string language)
        {         
            int startIndex = language.IndexOf('(');
            int count = language.Length - startIndex;
            language = language.Remove(startIndex, count);

            return Char.ToUpper(language[0]) + language.Substring(1);
        }

        private void Cancel()
        {
            Login = "";
            Password = "";
        }
        private void Enter()
        {
            if(_logInService.TryLogIn(Login,Password))
                OnLogInSuccess?.Invoke();
        }

        public ICommand CancelCommand
        {
            get
            {
                return new DelegateCommand(() => Cancel());
            }
        }
        public ICommand EnterCommand
        {
            get
            {
                return new DelegateCommand(() => Enter());
            }
        }

    }
}