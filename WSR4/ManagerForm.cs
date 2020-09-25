using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSR4.Entity;

namespace WSR4
{
    public partial class ManagerForm : Form
    {
        public ManagerForm()
        {
            InitializeComponent();
            
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add();
        }

        private async Task Add()
        {
            using (var context = new Model1())
            { 
             context.Person.Add(new Person
             { 
               FirstName = FirstNameTextBox.Text,
               SecondName = SecondNameTextBox.Text,
               LastName = LastNameTextBox.Text,
               Login = LoginTextBox.Text,
               Password = PasswordTextBox.Text
             });
             await context.SaveChangesAsync();

                context.Manager.Add(new Manager
                {
                    IdPerson =context.Person.Last().Id,
                    CoefficientId = Int32.Parse(CoefficientComboBox.Text),
                    SalaryId = Int32.Parse(SalaryComboBox.Text)
                }) ;  
                await context.SaveChangesAsync();
            }
        }

        private async void ManagerForm_Load(object sender, EventArgs e)
        {
            using (var context = new Model1())
            {
                await context.Coefficient.LoadAsync();
                await context.Salary.LoadAsync();
                CoefficientComboBox.DataSource = context.Coefficient.Local.Select(coeff => coeff.Id).ToList();
                SalaryComboBox.DataSource = context.Salary.Local.Select(salary => salary.Id).ToList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Update();
        }

        private async Task Update()
        {
            using (var context = new Model1())
            {
                var per = context.Person.FirstOrDefault(person=>person.Login.Equals(LoginTextBox.Text));
                per.FirstName = FirstNameTextBox.Text;
                per.SecondName = SecondNameTextBox.Text;
                per.LastName = LastNameTextBox.Text;
                per.Login = LoginTextBox.Text;
                per.Password = PasswordTextBox.Text;
                await context.SaveChangesAsync();
                var manager = context.Manager.FirstOrDefault(man=>man.IdPerson.Equals(per.Id));
                manager.CoefficientId = Int32.Parse(CoefficientComboBox.Text);
                manager.SalaryId = Int32.Parse(SalaryComboBox.Text);
                await context.SaveChangesAsync();


            }
        }
        private async Task delete()
        {
            using (var context = new Model1())
            {
                Int32 idperson = Int32.Parse(textBox1.Text);
                var manager = context.Manager.FirstOrDefault(man => man.IdPerson==idperson);
                var per = context.Person.FirstOrDefault(person => person.Id==idperson);
                context.Manager.Remove(manager);
                context.Person.Remove(per);         
                await context.SaveChangesAsync();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            delete();
        }
    }
}
