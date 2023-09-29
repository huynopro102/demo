using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace callAPI_buoi6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
    
            HttpClient httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri("https://localhost:7136/api/");
            try{
                HttpResponseMessage response = httpclient.GetAsync("Product").Result;
                if (response.IsSuccessStatusCode) {
                    string jsonData = response.Content.ReadAsStringAsync().Result;
                    // Create a new DataTable with the desired columns
                    DataTable originalDataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(jsonData);
                    DataTable newDataTable = new DataTable();
                    newDataTable.Columns.Add("productid", typeof(string));
                    newDataTable.Columns.Add("productName", typeof(string));
                    newDataTable.Columns.Add("price", typeof(string));
                    newDataTable.Columns.Add("category", typeof(string));
                    foreach (DataRow row in originalDataTable.Rows) {
                        newDataTable.Rows.Add(
                            row["productid"],
                            row["productName"],
                            row["price"],
                            row["category"]
                        );
                    }
                    dataGridView1.DataSource = newDataTable;
                    foreach (DataGridViewRow row in dataGridView1.Rows) {
                        if (row.Index >= 0 && row.Cells.Count >= 3)  {
                            var valueToAdd = row.Cells[3].Value;
                            if (!comboBox1.Items.Contains(valueToAdd))  {
                                comboBox1.Items.Add(valueToAdd);
                            }
                        }
                    }
                    comboBox1.SelectedItem = 0;
                }
                else
                {
                    MessageBox.Show("loi api");
                }
            }
            catch (Exception ex) { }
            txtid.Focus();
        }


        private async void button1_Click(object sender, EventArgs e)
        {
        

            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("https://localhost:7136/api/");

                // Tạo dữ liệu sản phẩm mới
                Product product = new Product();
                product.ProductID = txtid.Text;
                product.ProductName = txtname.Text;
                product.Price = txtgia.Text;
                product.Category = comboBox1.Text;
                string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(product);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Gửi yêu cầu POST để thêm sản phẩm mới và đợi kết quả
                HttpResponseMessage response = await httpClient.PostAsync("Product", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thêm sản phẩm thành công!");
                    Form1_Load(sender,e);
                }
                else
                {
                    MessageBox.Show("Lỗi khi thêm sản phẩm: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                try
                {
                    // Lấy ID của sản phẩm được chọn
                    int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];
                    string productId = selectedRow.Cells["ProductId"].Value.ToString();

                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri("https://localhost:7136/api/");

                    // Gửi yêu cầu DELETE để xóa sản phẩm và đợi kết quả
                    HttpResponseMessage response = await httpClient.DeleteAsync($"Product/{productId}");

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Xóa sản phẩm thành công!");
                        Form1_Load(sender,e);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi xóa sản phẩm: " + response.ReasonPhrase);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                String a = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                String a1 = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                String a2 = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                String a3 = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtid.Text = a;
                txtname.Text = a1;
                txtgia.Text = a2;
                comboBox1.Text = a3;
            
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7136/api/");
           
            
            // giá trị truyền vào khi sử dụng put , và phải convert thành json
            Product product = new Product();
            product.ProductID = txtid.Text;
            product.ProductName = txtname.Text;
            product.Price = txtgia.Text;
            product.Category = comboBox1.Text;

            string jsonContent = JsonConvert.SerializeObject(product);

      
            // Create a StringContent with the JSON data and specify the content type
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            // Gửi yêu cầu POST để thêm sản phẩm mới và đợi kết quả
            HttpResponseMessage response = await httpClient.PutAsync("Product", httpContent);


            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Cập nhật sản phẩm thành công!");
                Form1_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Lỗi khi cập nhật sản phẩm: " + response.ReasonPhrase);
            }

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txttimkiem.Text))
            {
                MessageBox.Show("khong bo rong");

            }
            else
            {

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7136/api/");

            string productId = txttimkiem.Text.Trim();

            // Gửi yêu cầu DELETE để xóa sản phẩm và đợi kết quả
            HttpResponseMessage response = await httpClient.GetAsync($"Product/{productId.Trim()}");

            if (response.IsSuccessStatusCode)
            {
                // Đọc nội dung JSON trả về từ response
                string jsonData = await response.Content.ReadAsStringAsync();

                // Định dạng JSON thành đối tượng hoặc thực hiện xử lý dữ liệu ở đây
                // Ví dụ: Product product = JsonConvert.DeserializeObject<Product>(jsonData);

                // Hiển thị thông tin hoặc thực hiện xử lý dữ liệu dựa trên jsonData
                MessageBox.Show("Dữ liệu trả về từ API: " + jsonData);
            }
            else
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ API: " + response.ReasonPhrase);
            }
            }
         
        }

        private void txtid_Leave(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtid.Text))
            {
                MessageBox.Show("khong de rong id ");
                txtid.Focus();  
            }
        }
    }
}
