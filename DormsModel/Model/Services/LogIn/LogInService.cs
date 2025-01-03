﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.User;
using WpfDormitories.Model.Convertors;

namespace WpfDormitories.Model.Services.LogIn
{
    public class LogInService : ILogInService
    {
        private IConvertor<string,string> _convertor;

        public LogInService(IConvertor<string, string> convertor)
        {
            _convertor = convertor;
        }

        /// <summary>
        /// Попытаться войти.
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <exception cref="ArgumentException"></exception>
        public bool TryLogIn(string login, string password)
        {
            foreach (IUserData userData in DataManager.GetInstance().UsersRepository.Read())
            {
                if (userData.User.Login == login && userData.User.Password == _convertor.Convert(password))
                {
                    DataManager.GetInstance().CurrentUser = userData;
                    return true;
                }
            }
            return false;
        }
    }
}
