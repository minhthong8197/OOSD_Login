using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace OOSD_Login
{
    class Controller
    {
        DataDataContext Data = new DataDataContext();//đối tượng kết nối CSDL
        List<User> users = new List<User>();
        List<User_Right> user_rights = new List<User_Right>();
        List<Right_tb> rights = new List<Right_tb>();
        List<UserGroup> user_groups = new List<UserGroup>();
        List<Group_Right> group_rights = new List<Group_Right>();
        List<String> list_user_rights;
        List<String> list_group_rights;
        //getter - setter
        public List<String> getList_user_rights()
        {
            return this.list_user_rights;
        }
        public List<String> getList_group_rights()
        {
            return this.list_group_rights;
        }
        public Controller()//constructor
        {
            getData();
        }
        public bool checkConnection()//kết nối đến server
        {
            try
            {
                if (Data.Connection.State == ConnectionState.Open)
                    Data.Connection.Close();
                Data.Connection.Open();
                return true;
            }
            catch
            {
                Console.WriteLine("Bắt lỗi không kết nối với server đc");
                return false;
            }
        }
        //kiểm tra đăng nhập: trả về 0 nếu sai tài khoản hoặc pass,
        //về 1 nếu đăng nhập thành công, về 2 nếu hiện tại tài khoản ko có quyền đăng nhập
        public int compareAccount(String ipusername, String ippassword)
        {
            //kiểm tra user này có tồn tại ko
            foreach(User u in users)
            {
                //nếu có tồn tại tài khoản giống thông tin nhập vào
                if (u.username == ipusername && u.password == ippassword)
                {
                    //lần lượt kiểm tra ds quyền riêng là quyền group của user
                    //lấy ra ds quyền riêng của user này
                    this.list_user_rights = new List<string>();
                    getList_user_rights(u.username);
                    //lấy ra ds quyền trong group của user này
                    this.list_group_rights = new List<string>();
                    getList_group_rights(u.GroupID);
                    //kiểm tra xem hiện tại có quyền riêng là login ko
                    foreach (String tenquyen in list_user_rights)
                    {
                        //nếu có quyền "Login"
                        if (tenquyen == "Login")
                        {
                            return 1;
                        }
                    }
                    //kiểm tra xem hiện tại có quyền trong group là login ko
                    foreach (String tenquyen in list_group_rights)
                    {
                        //nếu có quyền "Login"
                        if (tenquyen == "Login")
                        {
                            return 1; 
                        }
                    }
                    //ktra hết ds quyền của user => ko có quyền login
                    return 2;
                }
            }
            //kiểm tra hết ds user => sai tài khoản hoặc pass
            return 0;
        }
        //các hàm lấy cơ sở dữ liệu lên
        private void getUser()//lấy dữ liệu từ bảng User
        {
            this.users = Data.Users.ToList();
        }
        private void getRight()//lấy dữ liệu từ bảng Right_tb
        {
            this.rights = Data.Right_tbs.ToList();
        }
        private void getUser_Group()//lấy dữ liệu từ bảng UserGroup
        {
            this.user_groups = Data.UserGroups.ToList();
        }
        private void getUser_Right()//lấy dữ liệu từ bảng User_Right
        {
            this.user_rights = Data.User_Rights.ToList();
        }
        private void getGroup_Right()//lấy dữ liệu từ bảng Group_Right
        {
            this.group_rights = Data.Group_Rights.ToList();
        }
        private void getData()//hàm thực thi tổng hợp các hàm lấy dữ liệu trên
        {
            getUser();
            getUser_Group();
            getRight();
            getUser_Right();
            getGroup_Right();
        }
        private void getList_user_rights(string ipusername)//lấy ds quyền riêng cho user
        {
            //duyệt hết ds quyền gán riêng cho user
            foreach (User_Right ur in user_rights)
            {
                //chọn ra những quyền gắn với tài khoản truyền vào
                if(ur.username == ipusername)
                    //với mỗi mã quyền gắn với tài khoản truyền vào đó lấy ra tên quyền tương ứng
                    foreach(Right_tb r in rights)
                    {
                        if(ur.RightID == r.RightID) this.list_user_rights.Add(r.RightName);
                    }
            }
        }
        private void getList_group_rights(string ipgroupID)//lấy ds quyền của group cho user
        {
            //duyệt hết ds quyền gán cho các group
            foreach (Group_Right gr in group_rights)
            {
                //chọn ra những quyền gắn group truyền vào
                if (gr.GroupID == ipgroupID)
                    //với mỗi mã quyền gắn với group truyền vào đó lấy ra tên quyền tương ứng
                    foreach (Right_tb r in rights)
                    {
                        if (gr.RightID == r.RightID) this.list_group_rights.Add(r.RightName);
                    }
            }
        }
    }
}
