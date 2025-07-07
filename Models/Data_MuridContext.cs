using Npgsql;
using Manajemen_Data_Murid.Helpers;

namespace Manajemen_Data_Murid.Models
{
    public class Data_MuridContext
    {
        private string __constr;
        private string __ErrorMsg;
        public Data_MuridContext(string pConstr)
        {
            __constr = pConstr;
        }
        public List<Data_Murid> ListDataMurid()
        {
            List<Data_Murid> list1 = new List<Data_Murid>();
            string query = string.Format(@"Select muridID, nama, alamat, peminatan
                from Data_Murid;");
            sqlDBHelper db = new sqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list1.Add(new Data_Murid()
                    {
                        muridID = int.Parse(reader["muridID"].ToString()),
                        nama = reader["nama"].ToString(),
                        alamat = reader["alamat"].ToString(),
                        peminatan = reader["peminatan"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }
            return list1;
        }

        
        public void addDataMurid(Data_Murid dMurid)
        {
            string query = string.Format(@"Insert into Data_Murid (nama, alamat, peminatan) VALUES (@nama, @alamat, @peminatan);");
            sqlDBHelper db = new sqlDBHelper(this.__constr);
            try
            {
                using(NpgsqlCommand cmd = db.getNpgsqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@nama", dMurid.nama);
                    cmd.Parameters.AddWithValue("@alamat", dMurid.alamat);
                    cmd.Parameters.AddWithValue("@peminatan", dMurid.peminatan);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }
        }

        public void updateDataMurid(int muridID, Data_Murid dMurid)
        {
            string query = string.Format(@"update Data_Murid set nama=@nama, alamat=@alamat, peminatan=@peminatan where muridID=@muridID;");
            sqlDBHelper db = new sqlDBHelper(this.__constr);
            try
            {
                using(NpgsqlCommand cmd = db.getNpgsqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@muridID", muridID);
                    cmd.Parameters.AddWithValue("@nama", dMurid.nama);
                    cmd.Parameters.AddWithValue("@alamat", dMurid.alamat);
                    cmd.Parameters.AddWithValue("@peminatan", dMurid.peminatan);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }
        }

        public void delDataMurid(int muridID)
        {
            string query = string.Format(@"delete from Data_Murid where muridID=@muridID;");
            sqlDBHelper db = new sqlDBHelper(this.__constr);
            try
            {
                using (NpgsqlCommand cmd = db.getNpgsqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@muridID", muridID);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }
        }
    }
}
