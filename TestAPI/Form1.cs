using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Windows.Forms;

namespace TestAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private List<Student> Students;
        private int id = 0;
        private string url = "http://localhost:56038/api/";
        private List<Student> GetStudents()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(url)
            };
            HttpResponseMessage response = client.GetAsync("student").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<Student>>().Result;
            }
            else
                return null;
        }
        private bool Them(Student student)
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(url)
            };
            HttpResponseMessage response = client.PostAsJsonAsync("student", student).Result;
            return response.IsSuccessStatusCode;
        }
        private bool Sua(Student student)
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(url)
            };
            HttpResponseMessage response = client.PutAsJsonAsync("student", student).Result;
            return response.IsSuccessStatusCode;
        }
        private bool Xoa(int id)
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(url)
            };
            HttpResponseMessage response = client.DeleteAsync($"student?id={id}").Result;
            return response.IsSuccessStatusCode;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HienThi();
        }
        public void HienThi()
        {
            Students = GetStudents();
            dgvStudent.DataSource = Students;
        }
        private void dgvStudent_SelectionChanged(object sender, EventArgs e)
        {
            var row = dgvStudent.CurrentRow;
            if (row != null)
            {
                id = Convert.ToInt32(row.Cells[0].Value.ToString());
                txtMaSV.Text = row.Cells[1].Value.ToString();
                txtHovaTen.Text = row.Cells[2].Value.ToString();
                dpk.Value = Convert.ToDateTime(row.Cells[3].Value);
                radNam.Checked = Convert.ToBoolean(row.Cells[4].Value);
                radNu.Checked = !Convert.ToBoolean(row.Cells[4].Value);
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            Student student = new Student();
            student.Code = txtMaSV.Text;
            student.FullName = txtHovaTen.Text;
            student.BirthDate = dpk.Value;
            student.Gender = radNam.Checked;
            if (Them(student))
            {
                MessageBox.Show("Thêm thành công", "Thành công",MessageBoxButtons.OK,MessageBoxIcon.Information);
                HienThi();               
            } else
                MessageBox.Show("Không thêm được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void ClearForm()
        {
            txtMaSV.Clear();
            txtHovaTen.Clear();
            dpk.Value = DateTime.Now;
            radNam.Checked = false;
            radNu.Checked = false;
        }
        private void btlClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(id==0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa", "Chưa chọn dòng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Student student = new Student()
            {
                ID = id,
                Code = txtMaSV.Text,
                FullName = txtHovaTen.Text,
                BirthDate = dpk.Value,
                Gender = radNam.Checked
            };
        
            if (Sua(student))
            {
                MessageBox.Show("Sửa thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HienThi();
                return;
            }
            else
                MessageBox.Show("Không sửa được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (id == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xoá", "Chưa chọn dòng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult result = MessageBox.Show("Chắc chưa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (Xoa(id))
                {
                    MessageBox.Show("Xoá thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HienThi();
                    return;
                }
                else
                    MessageBox.Show("Không xoá được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                
        }

        private void btnTimKiemTheoTen_Click(object sender, EventArgs e)
        {
            string keyWord = txtHovaTen.Text;
            if(!string.IsNullOrEmpty(keyWord))
            {
                List<Student> result = new List<Student>();
                foreach (Student student in Students) 
                {
                    if (student.FullName.ToLower().Contains(keyWord.ToLower()))
                    {
                        result.Add(student);
                    }
                }
                dgvStudent.DataSource = result;
                MessageBox.Show($"Đã tìm thấy {result.Count} kết quả có chứa từ khoá '{keyWord}'.", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Vui lòng nhập từ khoá!","Không có từ khoá", MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void btnTimKiemTheoMa_Click(object sender, EventArgs e)
        {
            string keyWord = txtMaSV.Text;
            if (!string.IsNullOrEmpty(keyWord))
            {
                List<Student> result = new List<Student>();
                foreach (Student student in Students)
                {
                    if (student.Code.ToLower().Contains(keyWord.ToLower()))
                    {
                        result.Add(student);
                    }
                }
                dgvStudent.DataSource = result;
                MessageBox.Show($"Đã tìm thấy {result.Count} kết quả có chứa từ khoá '{keyWord}'.", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Vui lòng nhập từ khoá!", "Không có từ khoá", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
