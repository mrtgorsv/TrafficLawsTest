﻿using System;
using System.Linq;
using TrafficLawsTest.DataSource.Context;
using TrafficLawsTest.DataSource.Models;

namespace TrafficLawsTest.Security.Services
{
    public interface IUserService
    {
        User Get(int id);
        User Get(string login);
        void LoadUsers(string[] users);
    }

    public class UserService  : IUserService
    {
        private readonly IDomainContext _domainContext;
        public UserService(IDomainContext domainContext)
        {
            _domainContext = domainContext;
        }

        public User Get(int id)
        {
            return _domainContext.Users.FirstOrDefault(u => u.Id.Equals(id));
        }

        public User Get(string login)
        {
            return _domainContext.Users.FirstOrDefault(u => u.Login.Equals(login));
        }

        public void LoadUsers(string[] users)
        {
            foreach (string user in users)
            {
                if (!_domainContext.Users.Any(u => u.Login.Equals(user)))
                {
                    _domainContext.Users.Add(new User() { Login = user , Password = string.Empty});
                }
            }

            _domainContext.SaveChanges();
        }
    }
}
