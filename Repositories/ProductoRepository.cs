using System;
using Microsoft.AspNetCore.Mvc;
using EspacioProducto;
using Microsoft.Data.Sqlite;

public class ProductoRepository
{
    private string connectionString = "Data Source=DB/MiTienda.db";


    //ALTA DE UN PRODUCTO
    public int Alta(Producto producto)
    {
        int nuevoId = 0;
        using (var conexion = new SqliteConnection(connectionString))
        {
            conexion.Open();
            string consulta = "INSERT INTO Producto (Descripcion, Precio) VALUES(@Descripcion, @Precio); SELECT last_insert_rowid();";
            using var comando = new SqliteCommand(consulta, conexion);
            comando.Parameters.Add(new SqliteParameter("@Descripcion", producto.Descripcion));
            comando.Parameters.Add(new SqliteParameter("@Precio", producto.Precio));
            nuevoId = Convert.ToInt32(comando.ExecuteScalar());
            // comando.ExecuteNonQuery();
        }
        return nuevoId;
    }



    //MODIFICAR UN PRODUCTO 
    public bool Modificar(int id, Producto producto)
    {
        using (var conexion = new SqliteConnection(connectionString))
        {
            conexion.Open();
            string consulta = "UPDATE Producto SET Descripcion = @Descripcion, Precio = @Precio WHERE IdProducto = @Id";
            using var comando = new SqliteCommand(consulta, conexion);
            comando.Parameters.Add(new SqliteParameter("@Descripcion", producto.Descripcion));
            comando.Parameters.Add(new SqliteParameter("@Precio", producto.Precio));
            comando.Parameters.Add(new SqliteParameter("@Id", id));

            int filas = comando.ExecuteNonQuery();
            if (filas > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }



    //LISTAR PRODUCTOS
    public List<Producto> ListarProductos()
    {
        var productos = new List<Producto>();
        using (var conexion = new SqliteConnection(connectionString))
        {
            conexion.Open();
            string consulta = "SELECT * FROM Producto";
            using var comando = new SqliteCommand(consulta, conexion);

            using var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                var miProducto = new Producto(Convert.ToInt32(lector["IdProducto"]), lector["Descripcion"].ToString(), Convert.ToDouble(lector["Precio"]));
                productos.Add(miProducto);
            }
        }
        return productos;
    }



    //OBTENER DETALLES DE UN SOLO PRODUCTO
    public Producto ObtenerDetalles(int id)
    {

        using (var conexion = new SqliteConnection(connectionString))
        {
            conexion.Open();
            string consulta = "SELECT * FROM Producto WHERE IdProducto = @Id";
            using var comando = new SqliteCommand(consulta, conexion);
            comando.Parameters.Add(new SqliteParameter("@Id", id));

            using var lector = comando.ExecuteReader();

            if (lector.Read())
            {
                int idRecuperado = Convert.ToInt32(lector["IdProducto"]);
                string descripcionRecuperada = lector["Descripcion"].ToString();
                double precioRecuperado = Convert.ToDouble(lector["Precio"]);
                Producto producto = new Producto(idRecuperado, descripcionRecuperada, precioRecuperado);

                return producto;
            }
        }

        return null;
    }


    //ELIMINAR UN PRODUCTO
    public bool EliminarProducto(int id)
    {
        using var conexion = new SqliteConnection(connectionString);
        conexion.Open();

        string consulta = "DELETE FROM Producto WHERE IdProducto = @Id";
        using var comando = new SqliteCommand(consulta, conexion);

        comando.Parameters.Add(new SqliteParameter("@Id", id));

        int filas = comando.ExecuteNonQuery();

        if(filas > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



}