using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Katalog.Models;

namespace Katalog
{
    public partial class Katalog : Form
    {
        string connection_string;
        SqlConnection connection;

        List<Categories> categories;
        List<Producer> producers;
        List<Product> products;



        public Katalog()
        {
            InitializeComponent();
            connection_string = @"Data Source=DESKTOP-8BI5I70\SQLEXPRESS;Initial Catalog=Katalog;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            connection = new SqlConnection(connection_string);
            categories = new List<Categories>();
            producers = new List<Producer>();
            products = new List<Product>();

            LoadCategories();
            LoadProducers();
            LoadProducts();
        }

        private void LoadCategories()
        {
            categories.Add(new Categories()
            {
                Id = 0,
                Name = "Все категории"
            });

            connection.Open();
            string sql = "exec getAllCategories";
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                Categories c = new Categories()
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString()
                };
                categories.Add(c);
            }
            connection.Close();
        }

        private void LoadProducers()
        {
            producers.Add(new Producer()
            {
                Id = 0,
                Name = "Все производители"
            });
            connection.Open();
            string sql = "exec getAllProducers";
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                Producer p = new Producer()
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString()
                };
                producers.Add(p);
            }
            connection.Close();
        }

        private void LoadProducts()
        {
            string query = "exec getAllProducts";
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                Product product = new Product()
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    CategoryName = reader["CategoryName"].ToString(),
                    ProducerName = reader["ProducerName"].ToString(),
                    Price = (decimal)reader["Price"],
                    Number = (int)reader["Number"]
                };
                products.Add(product);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach(Categories c in categories)
            {
                CategoryList.Items.Add(c.Name);
            }
            CategoryList.SelectedIndex = 0;

            foreach(Producer p in producers)
            {
                Producerlist.Items.Add(p.Name);
            }
            Producerlist.SelectedIndex = 0;
        }

        private void CategoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string category_name = CategoryList.SelectedItem.ToString();
            var res = products.ToList();
            if(category_name != "Все категории")
            {
                res = res.Where(p => p.CategoryName == category_name).ToList();
            }

            GoodsList.Items.Clear();
            foreach(var p in res)
            {
                var item = GoodsList.Items.Add(p.Id.ToString());
                item.SubItems.Add(p.Name);
                item.SubItems.Add(p.CategoryName);
                item.SubItems.Add(p.ProducerName);
                item.SubItems.Add(p.Price.ToString());
                item.SubItems.Add(p.Number.ToString());
            }
        }

        private void Producerlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            string producer_name = Producerlist.SelectedItem.ToString();
            string category_name = CategoryList.SelectedItem.ToString();
            var res = products.ToList();
            if(producer_name != "Все производители") //&& category_name != "Все категории")
            {
                res = res.Where(p => p.ProducerName == producer_name &&
                    p.CategoryName == category_name).ToList();
            }
            GoodsList.Items.Clear();
            foreach (var p in res)
            {
                var item = GoodsList.Items.Add(p.Id.ToString());
                item.SubItems.Add(p.Name);
                item.SubItems.Add(p.CategoryName);
                item.SubItems.Add(p.ProducerName);
                item.SubItems.Add(p.Price.ToString());
                item.SubItems.Add(p.Number.ToString());
            }
        }
    }
}
