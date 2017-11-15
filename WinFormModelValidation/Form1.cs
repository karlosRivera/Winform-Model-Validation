using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;

namespace WinFormModelValidation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //string path = Utilities.IconFromFilePath(true);
            //MessageBox.Show(path);
            //bool x = System.IO.File.Exists(path);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Employee oEmployee = new Employee();
            oEmployee.ID = Convert.ToInt32(txtID.Text.Trim() == "" ? "0" : txtID.Text.Trim());
            oEmployee.Name = txtName.Text;
            oEmployee.Salary = Convert.ToInt32(txtSalary.Text.Trim() == "" ? "0" : txtSalary.Text.Trim());
            oEmployee.Email = txtEmail.Text;

            var validationContext = new ValidationContext(oEmployee, null, null);
            var results = new List<ValidationResult>();

            ErrID.SetError(txtID, null);
            ErrName.SetError(txtName, null);
            ErrSalary.SetError(txtSalary, null);
            ErrEmail.SetError(txtEmail, null);

            if (!Validator.TryValidateObject(oEmployee, validationContext, results, true))
            {
                //Display validation errors
                //These are available in your results.   

                // here i do custom validation to add custom message
                if (txtID.Text.Trim() == "")
                    results.Add(new ValidationResult("ID is required", new List<string>() { "ID" }));

                foreach (var result in results)
                {
                    if (result.MemberNames.Contains("ID"))
                        ErrID.SetError(txtID, result.ErrorMessage);

                    if (result.MemberNames.Contains("Name"))
                        ErrName.SetError(txtName, result.ErrorMessage);


                    if (result.MemberNames.Contains("Salary"))
                        ErrSalary.SetError(txtSalary, result.ErrorMessage);


                    if (result.MemberNames.Contains("Email"))
                        ErrEmail.SetError(txtEmail, result.ErrorMessage);

                };

            }
        }
    }


    public class Employee
    {
        //[Required(ErrorMessage = "Employee ID is required", AllowEmptyStrings = false)]
        //[Range(1, 10000000, ErrorMessage = "ID can't be Zero")]
        public int? ID { get; set; }

        [Required(ErrorMessage = "Employee Name is required")]
        [StringLength(20, ErrorMessage = "Name must be 20 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Employee Salary is required")]
        [Range(3000, 10000000, ErrorMessage = "Salary must be between 3000 and 10000000")]
        public int Salary { get; set; }

        [Required(ErrorMessage = "Employee email is required")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(20)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter valid email")]
        public string Email { get; set; }
    }
}
