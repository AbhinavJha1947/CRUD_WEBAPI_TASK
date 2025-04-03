using CRUD_WEBAPI_TASK.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CRUD_WEBAPI_TASK.DAL
{
    public class EmployeeDAL
    {
        private readonly string _connectionString;

        public EmployeeDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBConnection");
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllEmployees", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    employees.Add(new Employee
                    {
                        EmployeeID = Convert.ToInt32(dr["EmployeeID"]),
                        Name = dr["Name"].ToString(),
                        Surname = dr["Surname"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        Department = dr["Department"].ToString(),
                        Email = dr["Email"].ToString(),
                        Password = dr["Password"].ToString()
                    });
                }
            }
            return employees;
        }

        public Employee GetEmployeeById(int id)
        {
            Employee emp = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetEmployeeById", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EmployeeID", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    emp = new Employee
                    {
                        EmployeeID = Convert.ToInt32(dr["EmployeeID"]),
                        Name = dr["Name"].ToString(),
                        Surname = dr["Surname"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        Department = dr["Department"].ToString(),
                        Email = dr["Email"].ToString(),
                        Password = dr["Password"].ToString()
                    };
                }
            }
            return emp;
        }

        public void AddEmployee(Employee emp)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddEmployee", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Surname", emp.Surname);
                cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                cmd.Parameters.AddWithValue("@Department", emp.Department);
                cmd.Parameters.AddWithValue("@Email", emp.Email);
                cmd.Parameters.AddWithValue("@Password", emp.Password);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateEmployee(Employee emp)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateEmployee", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EmployeeID", emp.EmployeeID);
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Surname", emp.Surname);
                cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                cmd.Parameters.AddWithValue("@Department", emp.Department);
                cmd.Parameters.AddWithValue("@Email", emp.Email);
                cmd.Parameters.AddWithValue("@Password", emp.Password);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteEmployee", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EmployeeID", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
