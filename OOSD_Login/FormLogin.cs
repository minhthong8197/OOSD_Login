using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOSD_Login
{
    public partial class FormLogin : Form
    {
        Controller controller = new Controller();
        //các biến hỗ trợ kéo rê thanh tiêu đề
        //(form tự làm nên phải tự làm cái này)
        Boolean drag = false;
        int mousex;
        int mousey;
        public FormLogin()//constructor
        {
            InitializeComponent();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.lblResult.Text = "- - - - -";
            //kiểm tra điền đủ các ô
            if (this.txtUsername.Text == "" || this.txtPassword.Text == "")
            {
                this.lblResult.Text = "Vui lòng nhập đủ thông tin!";
            }
            //đã điền đủ
            else
            {
                //tiến hành kiểm tra kết nối
                if (controller.checkConnection() == true)
                {
                    //kiểm tra tài khoản đăng nhập
                    switch (controller.compareAccount(this.txtUsername.Text, this.txtPassword.Text))
                    {
                        case 0:
                            this.lblResult.Text = "Sai tài khoản hoặc mật khẩu!";
                            break;
                        case 1:
                            this.lblResult.Text = "Đăng nhập thành công!";
                            String showcontent = "";
                            showcontent += "List User Right\n";
                            foreach (string s in controller.getList_user_rights()) showcontent += "+" + s.ToString();
                            showcontent += "\nList Group Right\n";
                            foreach (string s in controller.getList_group_rights()) showcontent += "+" + s.ToString();
                            MessageBox.Show(showcontent,"Form chức năng");
                            break;
                        case 2:
                            this.lblResult.Text = "Tài khoản hiện đang bị khóa!";
                            break;
                    }
                }
                else MessageBox.Show("Không thể kết nối đến server!");
            }
        }
        //các hàm hỗ trợ kéo rê thanh tiêu đề
        //(form tự làm nên phải tự làm cái này)
        private void pnlTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag == true)
            {
                this.Top = Cursor.Position.Y - mousey;
                this.Left = Cursor.Position.X - mousex;
            }
        }
        private void pnlTop_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            mousex = Cursor.Position.X - this.Left;
            mousey = Cursor.Position.Y - this.Top;
        }
        private void pnlTop_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }
    }
}
