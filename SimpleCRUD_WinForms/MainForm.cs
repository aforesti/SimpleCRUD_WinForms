namespace SimpleCRUD_WinForms
{
    using System.Data;
    using Dapper;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using SimpleCRUD_WinForms.Models;

    public partial class MainForm : Form
    {
        private readonly string _connectionString;
        private List<Product> _products = new();

        public MainForm(IConfiguration config)
        {
            InitializeComponent();
            _connectionString = config.GetConnectionString("Default");
        }

        private void RefreshData(IDbConnection con)
        {
            _products = con.Query<Product>("select * from Product").ToList();
            ProductList.DataSource = _products;
        }


        private void CreateButton_Click(object sender, EventArgs e)
        {
            var product = new Product
            {
                Code = ProductCodeTextBox.Text,
                Name = ProductNameTextBox.Text,  
                Description = ProductDescriptionTextBox.Text,
                Date = DateTime.Now
            };

            using var con = new SqlConnection(_connectionString);
            con.Execute("insert into Product values (@Code, @Name, @Description, @Date)", product);
            RefreshData(con);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (ProductList.CurrentRow is null)
                return;
            
            var product = new Product
            {
                Id = _products[ProductList.CurrentRow.Index].Id,
                Code = ProductCodeTextBox.Text,
                Name = ProductNameTextBox.Text,  
                Description = ProductDescriptionTextBox.Text,
                Date = DateTime.Now
            };


            using var con = new SqlConnection(_connectionString);
            con.Execute(
                "update Product set " +
                "code = @Code, name = @Name, description = @Description, date = @Date " +
                "where id = @id", 
                product
            );
            RefreshData(con);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (ProductList.CurrentRow is null)
                return;

            using var con = new SqlConnection(_connectionString);
            con.Execute(
                "delete Product where id = @id", 
                new { id = _products[ProductList.CurrentRow.Index].Id }
            );
            RefreshData(con);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            using var con = new SqlConnection(_connectionString);
            RefreshData(con);
        }
    }
}