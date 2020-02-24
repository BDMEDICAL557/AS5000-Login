using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AS5000_Login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            

            dataGridView1.BackgroundColor = Color.White;

            authenticate();
            loaddropdowns();
            loadalluser();
        }

        private void authenticate()
        {

            tabControl1.Visible = false;
            tabControl2.Visible = true;




        }

        private void loaddropdowns()
        {
            comboBox1.Items.Add("Admin");
            comboBox1.Items.Add("CM");
            comboBox1.Items.Add("DM");
            comboBox1.Items.Add("ICC");
            comboBox1.Items.Add("NSD");
            comboBox1.Items.Add("PD");
            comboBox1.Items.Add("RM");
            comboBox1.Items.Add("TM");
            comboBox1.Items.Add("Senior Scientist");
            comboBox1.Items.Add("Analytics / Marketing");

            comboBox2.Items.Add("Admin");
            comboBox2.Items.Add("User");
        }


        private void loadalluser()
        {
            List<string> parameters = new List<string>();
            List<string> values = new List<string>();

            //parameters.Add("@id");
            //values.Add("550");

            DataSet ds = Dataaccess("SelectAllUser", 1, parameters, values);

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Refresh();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string firstname;
            string lastname;
            string email;

            string title;
            string userlevel;
            string usertype;

            string password;

            List<string> parameter = new List<string>();
            List<string> value = new List<string>();

            DataSet ds = new DataSet();

            firstname = textBox1.Text;
            lastname = textBox2.Text;
            email = textBox3.Text;

            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Missing input fields!");
                return;
            }
            if (comboBox2.SelectedIndex < 0)
            {
                MessageBox.Show("Missing input fields!");
                return;
            }

            title = comboBox1.SelectedItem.ToString();
            userlevel = comboBox2.SelectedItem.ToString();

            if (firstname == "" && lastname == "" && email == "" && title == "" && userlevel == "")
            {
                MessageBox.Show("Missing input fields!");
                return;
            }

            if(!checkBox1.Checked)
            {
                email = email + "-old";
            }

            if(userlevel=="Admin")
            {
                usertype = "2";
            }
            else
            {
                usertype = "1";
            }

            parameter.Add("@firstname");
            parameter.Add("@lastname");
            parameter.Add("@email");
            parameter.Add("@title");
            parameter.Add("@userlevel");

            value.Add(firstname);
            value.Add(lastname);
            value.Add(email);
            value.Add(title);
            value.Add(usertype);

            try
            {
                ds = Dataaccess("AddNewUser", 1, parameter, value);
                password = ds.Tables[0].Rows[0]["Password"].ToString();
                textBox4.Text = password;

                MessageBox.Show("Successfully added user: " + firstname + " " + lastname);
            }
            catch
            {
                MessageBox.Show("Failed adding new user. Try again");
            }

            loadalluser();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            List<string> parameter = new List<string>();
            List<string> value = new List<string>();

            if (textBox3.Text == "")
            {
                MessageBox.Show("Email is required!");
                return;
            }


            parameter.Add("@email");
            parameter.Add("@type");

            value.Add(textBox3.Text);
            value.Add("0");

            try
            {
                Dataaccess("ChangeUserDetail", 1, parameter, value);
                MessageBox.Show("Successfully deactivated user: " + textBox3.Text);
            }
            catch
            {
                MessageBox.Show("Failed. Try again!");
            }

            loadalluser();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            List<string> parameter = new List<string>();
            List<string> value = new List<string>();

            if (textBox3.Text == "")
            {
                MessageBox.Show("Email is required!");
                return;
            }

            parameter.Add("@email");
            parameter.Add("@type");

            value.Add(textBox3.Text);
            value.Add("1");

            try
            {
                Dataaccess("ChangeUserDetail", 1, parameter, value);
                MessageBox.Show("Successfully activated user: " + textBox3.Text);
            }
            catch
            {
                MessageBox.Show("Failed. Try again!");
            }
           

            loadalluser();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            List<string> parameter = new List<string>();
            List<string> value = new List<string>();

            if (textBox3.Text == "")
            {
                MessageBox.Show("Email is required!");
                return;
            }

            parameter.Add("@email");
            parameter.Add("@type");

            value.Add(textBox3.Text);
            value.Add("2");

            try
            {
                Dataaccess("ChangeUserDetail", 1, parameter, value);
                MessageBox.Show("Successfully deleted user: " + textBox3.Text);
            }
            catch
            {
                MessageBox.Show("Failed. Try again!");
            }
            

            loadalluser();
        }


        private DataSet Dataaccess(string fullcommand, int type, List<string> parameters, List<string> values)
        {
            string ConnectionString = null;
            string sql = null;

            SqlConnection connection;
            SqlCommand command;
            SqlDataAdapter adapter;

            DataSet ds = new DataSet(); 
            
            ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            //ConnectionString = "Data Source = arcticsun5000.ccnsypazwmb3.us - east - 2.rds.amazonaws.com; Initial Catalog = Bard; User ID = Administrator; Password = t!JCwU = (9.nxb - tHn)5XOqgFGR.KABg2";

            sql = fullcommand;
            connection = new SqlConnection(ConnectionString);

            int i = 0;

            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);

                if (type == 1)
                {
                    command.CommandType = CommandType.StoredProcedure;
                    

                    foreach(string s in parameters)
                    {
                        command.Parameters.AddWithValue(s, values[i]);

                        i++;
                    }

                    
                }
                else
                {

                }
                

                
                adapter = new SqlDataAdapter(command);

                adapter.Fill(ds);

                adapter.Dispose();               
                command.Dispose();
                connection.Close();

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return ds;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "arcticcircle5000!")
            {
                tabControl1.Visible = true;
                tabControl2.Visible = false;
                loadalluser();
            }
            else
            {
                tabControl1.Visible = false;
                tabControl2.Visible = true;
                MessageBox.Show("Wrong Password. Try again!");
            }
        }
    }
}
