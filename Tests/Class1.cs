using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSR4;
using WSR4.Core;
using WSR4.Entity;

namespace Tests
{
    [TestFixture]
    public class EnterTest
    {
        User user;
        Person person;
        
        [SetUp]
        public void SetUp()
        {
            user = new User();
            
            person = new Person();
            person.Id = 1;
        }
        
        [Test]
        public  void IdCurrentUSer()
        {
            user.CreateFromPerson(person);
            Assert.AreEqual(person.Id,user.Id);
          //  Assert.Throws<Exception>(() => new User().CreateFromPerson("")) ;
          //  Assert.Throws<Exception>(() => new Form1().Enter("123", "123")) ;
        }

        [Test]
        public void SingletonTest()
        {
            user = Singleton<User>.Instance().CreateFromPerson(person);
            Assert.AreSame(user,Singleton<User>.Instance());
            Assert.AreEqual(true, Singleton<User>.Instance() is User);
        }
    }
}
