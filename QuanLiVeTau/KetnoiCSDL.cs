using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace QuanLiVeTau
{
    class KetnoiCSDL
    {
        public SqlConnection cnn;
        public SqlCommand cmd;
        public DataTable dta;
        public SqlDataAdapter ada;

        public void KetNoi_Dulieu()
        {
            string strKetNoi = @"Data Source=MSI; Initial Catalog=Quanlivetau;Integrated Security=True";
            cnn = new SqlConnection(strKetNoi);
            cnn.Open();
        }

        public void HuyKetNoi()
        {
            if (cnn.State == ConnectionState.Open)
                cnn.Close();
        }

        public DataTable LayBang(string Sql)
        {
            KetNoi_Dulieu();
            ada = new SqlDataAdapter(Sql, cnn);
            dta = new DataTable();
            ada.Fill(dta);
            return dta;
        }

        public void ThucThiDuLieu(string Sql)
        {
            KetNoi_Dulieu();
            cmd = new SqlCommand(Sql, cnn);
            cmd.ExecuteNonQuery();
            HuyKetNoi();
        }

        internal DataTable LayDuLieu(string v)
        {
            throw new NotImplementedException();
        }
    }


}
