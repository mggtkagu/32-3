using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSR4.Core;
using WSR4.Entity;

namespace WSR4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Enter(LoginTextBox.Text, PasswordTextBox.Text);
        }
        public async Task Enter(string login, string password)
        {
            try
            {
                using (var context = new Model1())
                {
                    var User =  context.Person.FirstOrDefault(person => person.Login.Equals(login) && person.Password.Equals(password));
                    if (User == null)
                        throw new Exception("Нет такого пользователя");
                    Singleton<User>.Instance().CreateFromPerson(User);
                    if (Role.Manager == Singleton<User>.Instance().Role)
                    {
                        this.Hide();
                        new ManagerForm().ShowDialog();
                        this.Close();
                    }
                    else 
                    {
                        this.Hide();
                        new ExecutorForm().ShowDialog();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    }
}
