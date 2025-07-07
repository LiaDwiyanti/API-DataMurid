using Manajemen_Data_Murid.Helpers;
using Manajemen_Data_Murid.Models;
using Npgsql;

namespace Manajemen_Data_Murid.Models
{
    public class PersonContext
    {
        private string __constr;
        private string __errorMsqg;

        public PersonContext(string pObs)
        {
            __constr = pObs;
        }

        public List<Person> ListPerson()
        {
            List<Person> list1 = new List<Person>();

            string query = string.Format(@"SELECT * FROM person");
            sqlDBHelper db = new sqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list1.Add(new Person()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Name = reader["nama"].ToString(),
                        Alamat = reader["alamat"].ToString(),
                        Email = reader["email"].ToString(),
                        Password = reader["password"].ToString(),
                        Role = reader["role"].ToString()
                    });
                }
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __errorMsqg = ex.Message;
            }

            return list1;
        }
        public Person GetPersonByEmail(string email)
        {
            Person person = null;

            string query = string.Format(@"SELECT * FROM person WHERE email = @email");
            sqlDBHelper db = new sqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@email", email);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    person = new Person()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Name = reader["nama"].ToString(),
                        Alamat = reader["alamat"].ToString(),
                        Email = reader["email"].ToString(),
                        Password = reader["password"].ToString(),
                        Role = reader["role"].ToString()
                    };
                }
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __errorMsqg = ex.Message;
            }

            return person;
        }

        public bool RegisterPerson(Person person)
        {
            bool result = false;
            string query = string.Format(@"INSERT INTO person (nama, alamat, email, password, role) 
                                  VALUES (@nama, @alamat, @email, @password, @role)");
            sqlDBHelper db = new sqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama", person.Name);
                cmd.Parameters.AddWithValue("@alamat", person.Alamat);
                cmd.Parameters.AddWithValue("@email", person.Email);
                cmd.Parameters.AddWithValue("@password", person.Password);
                cmd.Parameters.AddWithValue("@role", person.Role);

                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;

                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __errorMsqg = ex.Message;
                Console.WriteLine("Error in RegisterPerson: " + ex.Message);
            }

            return result;
        }

    }
}