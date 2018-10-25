using ProductManager.Models;
using ProductManager.Presenters;
using ProductManager.Views;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProductManager.Client
{
    public partial class MainForm : Form, IProductView
    {
        private readonly ProductPresenter productPresenter;
        public MainForm()
        {
            InitializeComponent();
            this.productPresenter = new ProductPresenter(this);
            this.productPresenter.Initialize();
        }

        public event EventHandler<ProductSelectedEventArgs> ProductSelected;

        public void BindProductList(IEnumerable<Product> products)
        {
            this.dataGridView1.DataSource = products;
        }

        public void BindProductName(IEnumerable<string> names)
        {
            this.comboBox1.DataSource = names;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string productName = (string)this.comboBox1.SelectedItem;
            ProductSelectedEventArgs eventArgs = new ProductSelectedEventArgs { ProductName = productName };
            ProductSelected?.Invoke(sender, eventArgs);
        }
    }
}
