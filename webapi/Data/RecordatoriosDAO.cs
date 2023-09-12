using MySqlConnector;
using webapi.Model;
using webapi.Util;

namespace webapi.Data
{
    public class RecordatoriosDAO
    {
        //DESARROLLAR AQUI EL METODO ASIGNADO EN CLASE
        public ulong Editar(Recordatorios rec)
        {
            var ad = new AccesoDatos();
            using (ad)
            {
                ad.parameters.Add(new MySqlParameter("@idRecordatorios", rec.idRecordatorios));
                ad.parameters.Add(new MySqlParameter("@notitas_id", rec.notitas_id));
                ad.parameters.Add(new MySqlParameter("@fecha_recordatorio", rec.fecha_recordatorio));
                ad.sentencia = "UPDATE recordatorios " +
                                "SET fecha_recordatorio = @fecha_recordatorio" +
                                "WHERE idRecordatorios = @idRecordatorios";
                return (ulong)ad.ejecutarSentencia(TIPOEJECUCIONSQL.SENTENCIASQL);
            }
        }

        public Recordatorios GetOneById(int idRecordatorio)
        {
            var ad = new AccesoDatos();
            Recordatorios recordatorios = null;

            using (ad)
            {
                ad.parameters.Add(new MySqlParameter("@idRecordatorios", idRecordatorio));
                ad.sentencia = "SELECT * FROM Recordatorios WHERE idRecordatorios = @idRecordatorios";

                var reader = (MySqlDataReader)ad.ejecutarSentencia(TIPOEJECUCIONSQL.CONSULTA);

                while (reader.Read())
                {
                    recordatorios = new Recordatorios
                    {
                        idRecordatorios = reader.GetUInt64("idRecordatorios"),
                        notitas_id = reader.GetInt32("notitas_id"),
                        fecha_recordatorio = reader.GetString("fecha_recordatorio"),
                    };
                }
            }

            return recordatorios;
        }

        //EDUARDO LARA AYALA
        internal IEnumerable<Recordatorios> getAll()
        {
            List<Recordatorios> recordatorios = new List<Recordatorios>();
            //var jiji = new AccesoDatos();

            using (var jiji = new AccesoDatos())
            {
                jiji.sentencia = "SELECT * FROM RECORDATORIOS";
                MySqlDataReader reader = (MySqlDataReader)jiji.ejecutarSentencia(TIPOEJECUCIONSQL.CONSULTA);

                while (reader.Read())
                {
                    Recordatorios recordatorio = new Recordatorios
                    {
                        idRecordatorios = reader.GetUInt64("idrecordatorios"),
                        notitas_id = (int)(reader.IsDBNull(reader.GetOrdinal("notitas_id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("notitas_id"))),
                        fecha_recordatorio = reader.IsDBNull(reader.GetOrdinal("fecha_recordatorio")) ? string.Empty : reader.GetString("fecha_recordatorio"),
                    };
                    recordatorios.Add( recordatorio );
                }
                
            }
            return recordatorios;
        }


    }
}
