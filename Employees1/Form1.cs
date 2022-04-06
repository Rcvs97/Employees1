using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employees1
{
    public partial class Form1 : Form
        
    {

        int from, to;

        public Form1()
        {
            InitializeComponent();
            from = 0;
            to = 0;
        }

        private void employeeBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.employeeBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSetEmployee);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSetEmployee.Employee' table. You can move, or remove it, as needed.
            this.employeeTableAdapter.Fill(this.dataSetEmployee.Employee);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            if (searchById.Checked) //Check if we have sellected this check box 
            {
                //We have try and catch to be able to stop the application from crash if we enter a characters when is selected to filter by ID 
                //We are expecting a intager
                try
                {

                //SELECT Id, [First Name], [Last Name], [Phone Number], Salary, Position
                //FROM Employee
                //WHERE ID  like @ID
                    employeeTableAdapter.SelectFillById(this.dataSetEmployee.Employee, int.Parse(textBox1.Text));
                }
                catch(Exception ex) { }
                
            }

            // This is the query that is used to select the data from table by using the Name
            //SELECT Id, [First Name], [Last Name], [Phone Number], Salary, Position
            //FROM Employee
            //WHERE[First Name] LIKE @Name
            if (searchByFirstName.Checked)      //Check if we have sellected this check box 
                this.employeeTableAdapter.SearchFillByName(this.dataSetEmployee.Employee, textBox1.Text + '%');


            //This is the query for filtering the tabe by name 
            //
            //SELECT Id, [First Name], [Last Name], [Phone Number], Salary, Position
            //FROM Employee
            //WHERE([Last Name] LIKE @Last_Name)
            //
            // SearchFillByName() is a function that i have generate with Table Adapter Wizard 
            if (searchByLastName.Checked)      //Check if we have sellected this check box 
                this.employeeTableAdapter.SearchFillByLastName(this.dataSetEmployee.Employee, textBox1.Text + '%');

            // This is the query that is used to select the data from table by using the Phone Number
            //SELECT Id, [First Name], [Last Name], [Phone Number], Salary, Position
            //FROM Employee
            //WHERE[Phone Number] LIKE @Phone
            if (searchByPhoneNumber.Checked)      //Check if we have sellected this check box 
                this.employeeTableAdapter.SearchFillByPhone(this.dataSetEmployee.Employee, textBox1.Text + '%');

            // This is the query that is used to select the data from table by using the Postiong
            //SELECT Id, [First Name], [Last Name], [Phone Number], Salary, Position
            //FROM Employee
            //WHERE[Position] LIKE @Position
            if (searchByPosition.Checked)      //Check if we have sellected this check box 
                this.employeeTableAdapter.SelectFillByPosition(this.dataSetEmployee.Employee, textBox1.Text + '%');

            //If we dont have anything in the text field we are showing all the data
            if (textBox1.Text == "")            {
                this.employeeTableAdapter.Fill(this.dataSetEmployee.Employee);
            }
        }   
        


        // This is a event function that is clled when ever we write in to a text box for 'From' Salary
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (this.searchBySalary.Checked)                
              FilterSalary();   //Function that is called for popilating the data grid 
        }

        // This is a event function that is clled when ever we write in to a text box for 'To' Salary

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (this.searchBySalary.Checked)
                FilterSalary();
        }


        // Function that is filtering the salary ragne 
        private void FilterSalary() {

            int fromSalary = 0;
            int toSalary = 1000000;

            //We are expecting a intager
            try
            {
                fromSalary = int.Parse(textBox2.Text);
            }
            catch { fromSalary = 0; }

            //We are expecting a intager
            try
            {
                toSalary = int.Parse(textBox3.Text);
            }
            catch { 
                toSalary = 1000000;
            } //Set default value to the Salary range to be 1000000


            // This is the query that is used to select the data from table by using the Salary by using the BETWEEN operator 
            //SELECT Id, [First Name], [Last Name], [Phone Number], Salary, Position 
            //FROM dbo.Employee where Salary
            //BETWEEN @From and @To
            this.employeeTableAdapter.SelectFillBySalary(this.dataSetEmployee.Employee, fromSalary, toSalary);
        }
    }
}
