using System.Data;
using System.Data.SqlClient;

using Domain;

namespace Infrastructure;
public class EmpleadosDbContext
{
    private readonly string _connectionString;
    public EmpleadosDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Empleado> List()
    {
        var data = new List<Empleado>();

        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("SELECT [Id],[Nombre],[Edad],[Foto] FROM [Empleado]", con);
        try
        {
            con.Open();
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                data.Add(new Empleado
                {
                    Id = (Guid)dr["Id"],
                    Nombre = (string)dr["Nombre"],
                    Edad = (byte)dr["Edad"],
                    Foto = dr.IsDBNull("Foto") ? null : (string)dr["Foto"]
                });
            }
            return data;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
    }

    public Empleado Details(Guid id)
    {
        var data = new Empleado();

        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("SELECT [Id],[Nombre],[Edad],[Foto] FROM [Empleado] WHERE [Id] = @Id", con);
        cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;
        try
        {
            con.Open();
            var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                data.Id = (Guid)dr["Id"];
                data.Nombre = (string)dr["Nombre"];
                data.Edad = (byte)dr["Edad"];
                data.Foto = dr.IsDBNull("Foto") ? null : (string)dr["Foto"];
            }
            return data;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
    }

    public void Create(Empleado data)
    {
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("INSERT INTO [Empleado] ([Nombre],[Edad],[Foto]) VALUES (@Nombre,@Edad,@Foto)", con);
        cmd.Parameters.Add("Nombre", SqlDbType.NVarChar, 128).Value = data.Nombre;
        cmd.Parameters.Add("Edad", SqlDbType.TinyInt).Value = data.Edad;
        cmd.Parameters.Add("Foto", SqlDbType.NVarChar).Value = (object)data.Foto ?? DBNull.Value;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
    }

    public void Edit(Empleado data)
    {
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("UPDATE [Empleado] SET [Nombre] = @Nombre, [Edad] = @Edad, [Foto] = @Foto WHERE [Id] = @Id", con);
        cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = data.Id;
        cmd.Parameters.Add("Nombre", SqlDbType.NVarChar, 128).Value = data.Nombre;
        cmd.Parameters.Add("Edad", SqlDbType.Int).Value = data.Edad;
        cmd.Parameters.Add("Foto", SqlDbType.NVarChar).Value = (object)data.Foto ?? DBNull.Value;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
    }

    public void Delete(Guid id)
    {
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("DELETE FROM [Empleado] WHERE [Id] = @Id", con);
        cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = id;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
    }
}
