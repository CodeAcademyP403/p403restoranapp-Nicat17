using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranApp
{
    public partial class Register : Form
    {
        Form form = new Form();

        List<Person> NewAllPerson;
        public AllPerson AllOwn;
        public AllWaiter AllWaiter;
        public AllMaderator AllMaderator;
        public AllAdmin AllAdmin;

        public Register(Form _form) : this()
        {
            form = _form;
            
        }

        public Register()
        {
            InitializeComponent();

            textBox_password.PasswordChar = '*';
            textBox_password.MaxLength = 15;

            comboBox_roles.DataSource = Enum.GetNames(typeof(Role));

            NewAllPerson = new List<RestoranApp.Person>();
            AllOwn = new RestoranApp.AllPerson();
            AllWaiter = new RestoranApp.AllWaiter();
            AllMaderator = new RestoranApp.AllMaderator();
            AllAdmin = new RestoranApp.AllAdmin();
        }

        private void Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            form.Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            form.Show();

            textBox_username.Text = "";
            textBox_email.Text = "";
            textBox_password.Text = "";
        }

        Role roleItem;
        string itemValue;

        private void comboBox_roles_SelectedValueChanged(object sender, EventArgs e)
        {
            itemValue = (sender as ComboBox).SelectedValue.ToString();

            roleItem = (Role)Enum.Parse(typeof(Role), itemValue);
        }

        private void button_sign_Click(object sender, EventArgs e)
        {
            if (textBox_username.Text == "" || textBox_email.Text == "" || textBox_password.Text == "")
            {
                MessageBox.Show("Please, Include Correct Information");
            }
            else
            {
                if (ControlAdminAndMader(AllOwn.People, itemValue) == "Admin" || ControlAdminAndMader(AllOwn.People, itemValue) == "Moderator")
                {
                    if (ControlAdminAndMader(AllOwn.People, itemValue) == "Admin")
                    {
                        MessageBox.Show("Don't Be Second The Admin");
                    }
                    else if (ControlAdminAndMader(AllOwn.People, itemValue) == "Moderator")
                    {
                        MessageBox.Show("Don't Be Second The Maderator");
                    }
                }
                else if (!Control(AllOwn.People, textBox_username.Text))
                {
                    MessageBox.Show("Username Is Used");
                }
                else
                {
                    Person person = new Person
                    {
                        username = textBox_username.Text,
                        email = textBox_email.Text,
                        password = textBox_password.Text,
                        role = roleItem
                    };

                    AllOwn.People.Add(person);

                    if ((person.role | Role.Waiter) == Role.Waiter)
                    {
                        Waiter waiter = new Waiter(form);
                        AllWaiter.AllPersonWaiters.Add(person);
                        AllWaiter.AllWaiters.Add(waiter);
                        this.Hide();
                        form.Show();
                    }
                    else if ((person.role | Role.Moderator) == Role.Moderator)
                    {
                        Maderator maderator = new Maderator(form);
                        AllMaderator.AllPersonMaderators.Add(person);
                        AllMaderator.AllMaderators.Add(maderator);
                        this.Hide();
                        form.Show();

                    }
                    else if ((person.role | Role.Admin) == Role.Admin)
                    {
                        Admin admin = new RestoranApp.Admin(form);
                        AllAdmin.AllPersonAdmins.Add(person);
                        AllAdmin.AllAdmins.Add(admin);
                        this.Hide();
                        form.Show();
                    }
                }
            }

            textBox_username.Text = "";
            textBox_email.Text = "";
            textBox_password.Text = "";
        }
        private bool Control(List<Person> _personList, string _personUsername)
        {
            foreach (Person _per in AllOwn.People)
            {
                if (_per.username == _personUsername)
                {
                    return false;
                }
            }
            return true;
        }
        private string ControlAdminAndMader(List<Person> _personList, string _personRole)
        {
            foreach (Person _per in AllOwn.People)
            {
                if (_per.role.ToString() == _personRole)
                {
                    if (_per.role.ToString() == "Admin")
                    {
                        return "Admin";
                    }
                    else if (_per.role.ToString()== "Moderator")
                    {
                        return "Moderator";
                    }
                }
            }
            return "Not";
        }

    }
}