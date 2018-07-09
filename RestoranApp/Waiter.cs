using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;

namespace RestoranApp
{
    public partial class Waiter : Form
    {

        List<Product> products = new List<Product>();
        Form form = new Form();

        public Waiter(Form _form):this()
        {
            form = _form;
        }

        public Waiter()
        {
            InitializeComponent();

            this.BackgroundImage = Properties.Resources.background;

            products.AddRange(new Product[] {
                new Product(){
                    name="Dolma",
                    price=5,
                    id=1,
                    category=Category.MainEaten
                },
                new Product(){
                    name="Kabab",
                    price=4,
                    id=2,
                    category=Category.MainEaten
                },
                new Product(){
                    name="Pilov",
                    price=3,
                    id=3,
                    category=Category.MainEaten
                },
                new Product(){
                    name="Coban Salati",
                    price=3,
                    id=4,
                    category=Category.Salad
                },
                new Product(){
                    name="Sezar Salati",
                    price=4,
                    id=5,
                    category=Category.Salad
                },
                new Product(){
                    name="Ereb Salati",
                    price=6,
                    id=6,
                    category=Category.Salad
                },
                new Product(){
                    name="Pendir Salati",
                    price=2,
                    id=7,
                    category=Category.Salad
                },
                new Product(){
                    name="Lobya Salati",
                    price=5,
                    id=8,
                    category=Category.Salad
                },
                new Product(){
                    name="Merci Supu",
                    price=2,
                    id=9,
                    category=Category.Soup
                },
                new Product(){
                    name="Toyuq Supu",
                    price=3,
                    id=10,
                    category=Category.Soup
                },
                new Product(){
                    name="Eriste Supu",
                    price=4,
                    id=11,
                    category=Category.Soup
                },
                new Product(){
                    name="Fanta",
                    price=1,
                    id=12,
                    category=Category.Drink
                },
                new Product(){
                    name="Kola",
                    price=1,
                    id=13,
                    category=Category.Drink
                },
                 new Product(){
                    name="Ayran",
                    price=2,
                    id=14,
                    category=Category.Drink
                }
            });
            comboBox_category.DataSource = Enum.GetNames(typeof(Category));
        }

        private void comboBox_category_SelectedValueChanged(object sender, EventArgs e)
        {
            string selectedValue = (sender as ComboBox).SelectedValue.ToString();

            Category newSelectedValue = (Category)Enum.Parse(typeof(Category), selectedValue);

            List<Product> selectedProduct = new List<RestoranApp.Product>();

            foreach (Product pro in products)
            {
                if (pro.category==newSelectedValue)
                {
                    selectedProduct.Add(pro);
                }
            }

            comboBox_eaten.DataSource = selectedProduct;
            comboBox_eaten.DisplayMember = "name";
        }

        Product selectPro;

        private void comboBox_eaten_SelectedValueChanged(object sender, EventArgs e)
        {
            selectPro = (Product)(sender as ComboBox).SelectedValue;
            textBox_price.Text = selectPro.price.ToString();

            textBox_price.Text = textBox_price.Text + " AZN";
        }

        private void textBox_count_Enter(object sender, EventArgs e)
        {
            textBox_count.Text = "";
        }

        private void textBox_count_Leave(object sender, EventArgs e)
        {
            if (textBox_count.Text == "")
            {
                textBox_count.Text = "1";
            }
        }

        Basket basket = new Basket();
        int number = 1;

        private void button_order_Click(object sender, EventArgs e)
        {
            
            ProductOfBasket productOfBasket = new ProductOfBasket
            {
                name = selectPro.name,
                category = selectPro.category,
                price = selectPro.price,
                count = Convert.ToByte(textBox_count.Text),
                id=selectPro.id,
                totalPrice=selectPro.price* Convert.ToByte(textBox_count.Text)
            };

            basket.products.Add(productOfBasket);

            ListViewItem list = new ListViewItem(number.ToString());
            list.SubItems.Add(productOfBasket.name);
            list.SubItems.Add(productOfBasket.category.ToString());
            list.SubItems.Add(productOfBasket.price.ToString()+" AZN");
            list.SubItems.Add(productOfBasket.count.ToString());
            list.SubItems.Add(productOfBasket.totalPrice.ToString()+ " AZN");

            listView_row.Items.Add(list);

            label_allPrice.Text= SumTotalPrice(basket.products).ToString()+" AZN";

            number++;

            textBox_count.Text = "1";
        }
        

        private decimal SumTotalPrice(List<ProductOfBasket> products)
        {
            decimal Sum = 0;
            foreach (ProductOfBasket pro in products)
            {
                Sum += pro.totalPrice;
            }
            return Sum;
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            listView_row.Items.Clear();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if(listView_row.SelectedItems.Count!=0)
            listView_row.SelectedItems[0].Remove();

            //but dont write update form
        }

        private void Waiter_FormClosing(object sender, FormClosingEventArgs e)
        {
            form.Close();
        }

        private void button_otherRoles_Click(object sender, EventArgs e)
        {
            this.Hide();
            form.Show();
        }

        private void textBox_count_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
