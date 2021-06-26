using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Bussines
{


    public class AgenciaManager
    {
        private DbSet<Entities.Usuario> misUsuarios;
        private DbSet<Entities.Hotel> hoteles;
        private DbSet<Entities.Cabania> cabanias;
        private DbSet<Entities.Reserva> reservas;
        private MyContext contexto;

        public AgenciaManager()
        {
            inicializarAtributos();
        }



        private void inicializarAtributos()
        {
            try
            {
                contexto = new MyContext();

                contexto.Usuario.Load();
                contexto.Hotel.Load();
                contexto.Cabania.Load();
                contexto.Reserva.Load();

                misUsuarios = contexto.Usuario;
                hoteles = contexto.Hotel;
                cabanias = contexto.Cabania;
                reservas = contexto.Reserva;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }



        // ----------------------- METODOS ALOJAMIENTOS -----------------------

        public List<List<string>> obtenerAlojamientos(string ciudad)
        {
            var queryHotel = from hotel in hoteles
                             where hotel.ciudad.nombre == ciudad
                             select hotel;

            var queryCabania = from cabania in cabanias
                               where cabania.ciudad.nombre == ciudad
                               select cabania;

            List<List<string>> alojamientos = new List<List<string>>();
            foreach (Entities.Hotel hotel in queryHotel)
            {
                alojamientos.Add(new List<string> { hotel.barrio, hotel.estrellas, hotel.cantidadDePersonas.ToString(), hotel.ciudad.nombre.ToString(), hotel.precio_por_persona.ToString() });
            }

            foreach (Entities.Cabania cabania in queryCabania)
            {
                alojamientos.Add(new List<string> { cabania.barrio, cabania.estrellas, cabania.cantidadDePersonas.ToString(), cabania.ciudad.ToString(), cabania.cantidadDeBanios.ToString(), cabania.habitaciones.ToString(), cabania.precio_por_dia.ToString() });
            }

            return alojamientos;
        }

        public List<List<string>> obtenerAlojamientos()
        {
            var queryHotel = from hotel in hoteles
                             select hotel;

            var queryCabania = from cabania in cabanias
                               select cabania;

            List<List<string>> alojamientos = new List<List<string>>();
            foreach (Entities.Hotel hotel in queryHotel)
            {
                alojamientos.Add(new List<string> { hotel.barrio, hotel.estrellas, hotel.cantidadDePersonas.ToString(), hotel.ciudad.nombre.ToString(), hotel.precio_por_persona.ToString() });
            }

            foreach (Entities.Cabania cabania in queryCabania)
            {
                alojamientos.Add(new List<string> { cabania.barrio, cabania.estrellas, cabania.cantidadDePersonas.ToString(), cabania.ciudad.ToString(), cabania.cantidadDeBanios.ToString(), cabania.habitaciones.ToString(), cabania.precio_por_dia.ToString() });
            }

            return alojamientos;
        }

        public List<List<string>> buscarAlojamientos(string Ciudad, string Pdesde, string Phasta, string cantPersonas, string tipo)
        {

            List<List<string>> alojamientos = new List<List<string>>();

            if (string.IsNullOrEmpty(cantPersonas))
            {
                cantPersonas = "0";
            }

            if (tipo == "Hotel")
            {
                var queryHotel = from hotel in hoteles
                                 where hotel.precio_por_persona <= double.Parse(Phasta)
                                 && hotel.precio_por_persona >= double.Parse(Pdesde)
                                 && hotel.cantidadDePersonas >= int.Parse(cantPersonas)
                                 && hotel.reservado == false
                                 select hotel;

                foreach (Entities.Hotel hotel in queryHotel)
                {
                    alojamientos.Add(new List<string> { hotel.barrio, hotel.estrellas, hotel.cantidadDePersonas.ToString(), hotel.ciudad.nombre.ToString(), hotel.precio_por_persona.ToString() });
                }
            }
            else
            {
                var queryCabania = from cabania in cabanias
                                   where cabania.cantidadDePersonas <= double.Parse(Phasta)
                                   && cabania.precio_por_dia >= double.Parse(Pdesde)
                                   && cabania.reservado == false
                                   select cabania;

                foreach (Entities.Cabania cabania in queryCabania)
                {
                    alojamientos.Add(new List<string> { cabania.barrio, cabania.estrellas, cabania.cantidadDePersonas.ToString(), cabania.ciudad.ToString(), cabania.cantidadDeBanios.ToString(), cabania.habitaciones.ToString(), cabania.precio_por_dia.ToString() });
                }
            }

            return alojamientos;
        }






        public List<Entities.Ciudades> getCiudades()
        {
            List<Entities.Ciudades> ciudades = new List<Entities.Ciudades>();

            var query = from ciudad in ciudades
                        select ciudad;
            return ciudades;

            foreach (Entities.Ciudades ciudad in query)
            {
                ciudades.Add(ciudad);
            }
        }

        public bool agregarAlojamiento(string tipo, string ciudad, string barrio, string estrellas, string cantPersonas, bool tv, string precio, string habitaciones, string banios) //Parametro Datos del Alojamiento ¿?
        {
            bool result;
            /*try
            {
                if (tipo == "Hotel")
                {
                    Entities.Alojamiento Alojamiento = new Entities.Alojamiento(
                    barrio, estrellas, int.Parse(cantPersonas), tv, int.Parse(ciudad), 0,
                    0, double.Parse(precio), 1, true);
                    alojamientos.Add(Alojamiento);
                    contexto.SaveChanges();
                }
                else
                {
                    Entities.Alojamiento Alojamiento = new Entities.Alojamiento(
                    barrio, estrellas, int.Parse(cantPersonas), tv, int.Parse(ciudad), int.Parse(habitaciones),
                    double.Parse(precio), 0, int.Parse(banios), false);
                    alojamientos.Add(Alojamiento);
                    contexto.SaveChanges();
                }
                result = true;
            }
            catch
            {
                result = false;
            }*/

            return false;
        }

        public bool modificarAlojamiento(string codigoInstancia, string ciudad, string barrio, string estrellas, string cantPersonas, bool tv, string precioxDia,
                                         string habitaciones, string banios, string precioxPersona)
        {
            bool result;
            if (string.IsNullOrEmpty(precioxDia))
            {
                precioxDia = "0";
            }
            if (string.IsNullOrEmpty(habitaciones))
            {
                habitaciones = "0";
            }
            if (string.IsNullOrEmpty(banios))
            {
                banios = "0";
            }
            if (string.IsNullOrEmpty(precioxPersona))
            {
                precioxPersona = "0";
            }

            /*
            try
            {
                var alojamiento = contexto.Alojamiento.Find(int.Parse(codigoInstancia));
                alojamiento.id_ciudad = int.Parse(ciudad);
                alojamiento.barrio = barrio;
                alojamiento.estrellas = estrellas;
                alojamiento.tv = tv;

                if (alojamiento.esHotel == true)
                {
                    alojamiento.precio_por_persona = int.Parse(precioxPersona);
                }
                else
                {
                    alojamiento.cantidadDeBanios = int.Parse(banios);
                    alojamiento.precio_por_dia = double.Parse(precioxDia);
                    alojamiento.cantidad_de_habitaciones = int.Parse(habitaciones);
                }                             

                contexto.Alojamiento.Update(alojamiento);
                contexto.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }*/



            return false;
        }

        public bool quitarAlojamiento(int codigo)
        {
            bool result;
            try
            {
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }


        // ----------------------- METODOS RESERVAS -----------------------
        public List<List<string>> buscarReservas(string dni, string fdesde, string fhasta)
        {
            return null;
        }



        public List<List<string>> getTodasLasReservas()
        {
            return null;
        }
        public List<List<string>> getReservasPorCliente(String dni)
        {
            return null;
        }

        public bool reservar(int codAloj, string dniUsuario, DateTime Fdesde, DateTime Fhasta)
        {
            //PENDIENTE

            return false;
        }

        public bool modificarReserva(int ID, DateTime FDesde, DateTime FHasta, Entities.TipoAlojamiento propiedad, Entities.Usuario persona, float precio)//Parametro Datos de Reserva ¿?
        {
            bool modificada = false;
            //PENDIENTE

            return modificada;
        }

        public bool eliminarReserva(int id)
        {
            //PENDIENTE
            return false;
        }


        // ----------------------- METODOS USUARIOS -----------------------

        public List<List<string>> obtenerUsuarios()
        {
            List<List<string>> salida = new List<List<string>>();

            foreach (Entities.Usuario u in misUsuarios)
                salida.Add(new List<string> { null, null, null, u.DNI.ToString(), u.mail, u.bloqueado.ToString(), u.nombre, u.esAdmin.ToString(), u.pass, u.id.ToString() });
            return salida;
        }


        public List<string> buscarUsuarioxNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                nombre = "";
            }

            var query = from usuarioDB in misUsuarios
                        where usuarioDB.nombre == nombre
                        select usuarioDB;

            List<string> usuario = new List<string>();

            foreach (Entities.Usuario u in query)
            {
                usuario.Add(u.DNI.ToString());
                usuario.Add(u.nombre);
                usuario.Add(u.mail);
                usuario.Add(u.pass.ToString());
                usuario.Add(u.esAdmin.ToString());
                usuario.Add(u.bloqueado.ToString());
                usuario.Add(u.intentosLogueo.ToString());
            }

            return usuario;
        }




        public bool agregarUsuario(int DNI, string nombre, string mail, string pass, bool esAdmin, bool bloqueado)
        {

            try
            {
                Entities.Usuario usuario = new Entities.Usuario(DNI, nombre, mail, pass, esAdmin, bloqueado);
                misUsuarios.Add(usuario);
                contexto.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool modificarUsuario(int dni, string nombre, string mail, string pass, bool esAdmin, bool bloqueado)
        {
            bool respuesta = false;
            foreach (Entities.Usuario u in contexto.Usuario)
            {
                if (u.DNI == dni)
                {
                    u.nombre = nombre;
                    u.mail = mail;
                    u.pass = pass;
                    u.esAdmin = esAdmin;
                    u.bloqueado = bloqueado;
                    contexto.Usuario.Update(u);
                    respuesta = true;
                }
            }
            if (respuesta)
            {
                contexto.SaveChanges();
            }
            return respuesta;
        }

        public bool eliminarUsuario(int id)
        {
            bool respuesta = false;

            try
            {
                var usuario = contexto.Usuario.Find(id);
                contexto.Usuario.Remove(usuario);
                contexto.SaveChanges();
                respuesta = true;

                return respuesta;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool autenticar(string nombre, string password)
        {
            bool respuesta = false;
            var query = from usuarioDB in misUsuarios
                        where usuarioDB.nombre == nombre
                        select usuarioDB;

            if (misUsuarios != null)
            {
                foreach (Entities.Usuario u in query)
                {
                    if (password == u.pass)
                    {
                        respuesta = true;
                    }
                    else
                    {
                        u.intentosLogueo++;
                        if (u.intentosLogueo >= 3)
                        {
                            u.bloqueado = true;
                        }
                        contexto.Usuario.Update(u);
                    }
                }
            }

            return respuesta;
        }


        public bool desbloquearUsuario(int id, bool valido)
        {
            bool desbloqueado = false;

            if (valido == true)
            {
                var usuario = contexto.Usuario.Find(id);
                usuario.bloqueado = false;
                contexto.Usuario.Update(usuario);
                contexto.SaveChanges();
                desbloqueado = true;
            }

            return desbloqueado;
        }

        public int recuperoDni(string usuario, string contrasenia)
        {

            // SE USA EN LA VIEW DE CAMBAIR CONTRASEÑA

            int resultado = 0;
            var query = (from usuarioDB in misUsuarios
                         where usuarioDB.nombre == usuario && usuarioDB.pass == contrasenia
                         select usuarioDB).FirstOrDefault();

            resultado = query.DNI;
            return resultado;
        }


        public string cambiarContrasenia(int DNI, string oldPass, string newPass1, string newPass2)
        {

            //REVISAR EL FOREACH

            bool respuesta = false;
            string mensaje = "";
            var query = from usuario in misUsuarios
                        where usuario.DNI == DNI
                        select usuario;

            //Usuario asd = query.First<>; ALA LA LA

            foreach (Entities.Usuario u in query)
            {
                if (u.pass == oldPass)
                {
                    if (newPass1 == newPass2)
                    {
                        u.pass = newPass1;
                        contexto.Usuario.Update(u);
                        respuesta = true;
                        mensaje = "La contraseña ha sido modificada con exito";

                    }
                    else
                    {
                        mensaje = "Las contraseñas ingresadas no coinciden ";
                    }
                }
                else
                {
                    mensaje = "La contraseña ingresada es incorrecta ";
                }
            }
            if (respuesta)
            {
                contexto.SaveChanges();
            }

            return mensaje;
        }



        public void cerrar()
        {
            contexto.Dispose();
        }

    }


}
