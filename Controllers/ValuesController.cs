
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Configuration;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace MyApp.Controllers
{

    //[ApiController]
    // [Route("api/[controller]")]

    public class ValuesController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["webapi_conn"].ConnectionString);
        Employee emp = new Employee();
        //Get api/values
        public List<Employee> Get()
        {
            SqlDataAdapter da = new SqlDataAdapter("usp_GetAllEmployees", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Employee> listEmployee = new List<Employee>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)

                {
                    Employee emp = new Employee();
                    emp.Name = dt.Rows[i]["Name"].ToString();
                    emp.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    emp.Age = Convert.ToString(dt.Rows[i]["Age"]);
                    emp.Active = Convert.ToInt32(dt.Rows[i]["Active"]);
                    listEmployee.Add(emp);
                }

            }
            if (listEmployee.Count > 0)
            {
                return listEmployee;
            }
            else
            {
                return null;
            }
        }
        //Get api/values/5

        public Employee Get(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter("usp_GetEmployeeById", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", id);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Employee emp = new Employee();
                emp.Name = dt.Rows[0]["Name"].ToString();
                emp.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                emp.Age = Convert.ToString(dt.Rows[0]["Age"]);
                emp.Active = Convert.ToInt32(dt.Rows[0]["Active"]);

                return emp;
            }
            else
            {
                return null;
            }
        }






        //POST api/values
        public string Post(Employee employee)


        {
            string msg = "";

            if (employee != null)
            {
                SqlCommand cmd = new SqlCommand("usp_AddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Age", employee.Age);
                cmd.Parameters.AddWithValue("@Active", employee.Active);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i > 0)
                {
                    msg = "Data has been inserted successfully";
                }
                else
                {
                    msg = "Error";
                }

            }

            return msg;
        }

        //PUT api/values
        public string Put(int id, Employee employee)
        {
            string msg = "";
            if (employee != null)
            { 
                SqlCommand cmd = new SqlCommand("usp_UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Age", employee.Age);
                cmd.Parameters.AddWithValue("@Active", employee.Active);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i > 0)
                {
                    msg = "Data has been updated successfully";
                }
                else
                {
                    msg = "Error";
                }

            }

            return msg;
        }
    

        //DELETE api/values/5
        public string Delete(int id)
        {

        string msg = "";
       
        
            SqlCommand cmd = new SqlCommand("usp_DeleteEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
         
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0)
            {
                msg = "Data has been deleted";
            }
            else
            {
                msg = "Error";
            }
        return msg;
    }

        

    }
     }
