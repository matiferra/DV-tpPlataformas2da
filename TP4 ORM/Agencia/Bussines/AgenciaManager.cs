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
        private DbSet<Entities.Ciudades> ciudades;
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
                contexto.Ciudades.Load();
                contexto.Hotel.Load();
                contexto.Cabania.Load();
                contexto.Reserva.Load();

                misUsuarios = contexto.Usuario;
                ciudades = contexto.Ciudades;
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
                alojamientos.Add(new List<string> { hotel.nombre, hotel.barrio, hotel.estrellas, hotel.cantidadDePersonas.ToString(), hotel.ciudad.nombre.ToString(), hotel.precio_por_persona.ToString() });
            }

            foreach (Entities.Cabania cabania in queryCabania)
            {
                alojamientos.Add(new List<string> { cabania.nombre, cabania.barrio, cabania.estrellas, cabania.cantidadDePersonas.ToString(), cabania.ciudad.ToString(), cabania.cantidadDeBanios.ToString(), cabania.habitaciones.ToString(), cabania.precio_por_dia.ToString() });
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
                alojamientos.Add(new List<string> { hotel.nombre, hotel.barrio, hotel.estrellas, hotel.cantidadDePersonas.ToString(), hotel.ciudad.nombre.ToString(), hotel.precio_por_persona.ToString() });
            }

            foreach (Entities.Cabania cabania in queryCabania)
            {
                alojamientos.Add(new List<string> { cabania.nombre, cabania.barrio, cabania.estrellas, cabania.cantidadDePersonas.ToString(), cabania.ciudad.ToString(), cabania.cantidadDeBanios.ToString(), cabania.habitaciones.ToString(), cabania.precio_por_dia.ToString() });
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
                    alojamientos.Add(new List<string> { hotel.nombre, hotel.barrio, hotel.estrellas, hotel.cantidadDePersonas.ToString(), hotel.ciudad.nombre.ToString(), hotel.precio_por_persona.ToString() });
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
                    alojamientos.Add(new List<string> { cabania.nombre, cabania.barrio, cabania.estrellas, cabania.cantidadDePersonas.ToString(), cabania.ciudad.ToString(), cabania.cantidadDeBanios.ToString(), cabania.habitaciones.ToString(), cabania.precio_por_dia.ToString() });
                }
            }

            return alojamientos;
        }


        public List<Entities.Ciudades> getCiudades()
        {
            List<Entities.Ciudades> ciudades = new List<Entities.Ciudades>();

            var query = from ciudad in ciudades
                        select ciudad;

            foreach (Entities.Ciudades ciudad in query)
            {
                ciudades.Add(ciudad);
            }
            return ciudades;

        }

        public bool agregarAlojamiento(string tipo, string ciudadNombre, string nombre, string barrio, string estrellas, string cantPersonas, bool tv, string precio, string habitaciones, string banios) //Parametro Datos del Alojamiento ¿?
        {
            Entities.Ciudades ciudad = new Entities.Ciudades();
            ciudad.nombre = ciudadNombre;

            bool result = false;
            try
            {
                if (tipo == "Hotel")
                {
                    Entities.Hotel hotel = new Entities.Hotel(nombre,
                        barrio, estrellas, int.Parse(cantPersonas), tv, ciudad, double.Parse(precio), false);
                    hoteles.Add(hotel);
                    contexto.SaveChanges();
                }
                else
                {
                    Entities.Cabania cabania = new Entities.Cabania(nombre,
                    barrio, estrellas, int.Parse(cantPersonas), tv, ciudad, int.Parse(banios), double.Parse(precio), int.Parse(habitaciones),
                     false);
                    cabanias.Add(cabania);
                    contexto.SaveChanges();
                }
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;

        }

        public bool modificarAlojamiento(string ciudadNombre, string nombre, string barrio, string estrellas, string cantPersonas, bool tv, string precioxDia,
                                         string habitaciones, string banios, string precioxPersona, bool reservado)
        {
            bool result = false;

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

            var queryHotel = from hotelDB in hoteles
                             where hotelDB.nombre == nombre
                             select hotelDB;

            var queryCabania = from cabania in cabanias
                               where cabania.nombre == nombre
                               select cabania;

            if (queryHotel != null)
            {
                Entities.Hotel hotel = queryHotel.FirstOrDefault();

                if (hotel != null)
                {
                    hotel.nombre = nombre;
                    hotel.barrio = barrio;
                    hotel.estrellas = estrellas;
                    hotel.cantidadDePersonas = int.Parse(cantPersonas);
                    hotel.tv = tv;
                    hotel.ciudad.nombre = ciudadNombre;
                    hotel.precio_por_persona = double.Parse(precioxPersona);
                    hotel.reservado = reservado;

                    contexto.Hotel.Update(hotel);
                    result = true;
                }
            }
            else if (queryCabania != null)
            {
                Entities.Cabania cabania = queryCabania.FirstOrDefault();

                if (cabania != null)
                {
                    cabania.nombre = nombre;
                    cabania.barrio = barrio;
                    cabania.estrellas = estrellas;
                    cabania.cantidadDePersonas = int.Parse(cantPersonas);
                    cabania.tv = tv;
                    cabania.ciudad.nombre = ciudadNombre;
                    cabania.cantidadDeBanios = int.Parse(banios);
                    cabania.precio_por_dia = double.Parse(precioxDia);
                    cabania.habitaciones = int.Parse(habitaciones);
                    cabania.reservado = reservado;

                    contexto.Cabania.Update(cabania);
                    result = true;
                }

            }
            else
            {
                //NO SE ENCONTRO EL ALOJAMIENTO
                result = false;
            }

            if (result)
            {
                contexto.SaveChanges();
            }
            return result;
        }

        public bool quitarAlojamiento(string nombre)
        {
            bool result = false;

            var queryHotel = from hotelDB in hoteles
                             where hotelDB.nombre == nombre
                             select hotelDB;

            var queryCabania = from cabania in cabanias
                               where cabania.nombre == nombre
                               select cabania;


            if (queryHotel != null)
            {
                Entities.Hotel hotel = queryHotel.FirstOrDefault();

                if (hotel != null)
                {
                    contexto.Hotel.Remove(hotel);
                    contexto.SaveChanges();
                    result = true;
                }
            }
            else if (queryCabania != null)
            {
                Entities.Cabania cabania = queryCabania.FirstOrDefault();

                if (cabania != null)
                {
                    contexto.Cabania.Remove(cabania);
                    contexto.SaveChanges();
                    result = true;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }


        // ----------------------- METODOS RESERVAS -----------------------
        /*public List<List<string>> buscarReservas(int dni, string fdesde, string fhasta)
        {

            List<List<string>> resultado = new List<List<string>>();

            var query = from reservaDB in reservas
                        where reservaDB.usuario.DNI == dni
                        && reservaDB.FDesde >= DateTime.Parse(fdesde)
                        && reservaDB.FHasta <= DateTime.Parse(fhasta)
                        select reservaDB;

            foreach (Entities.Reserva reservas in query)
            {
                resultado.Add(new List<string> { reservas.FDesde.ToString(), reservas.FHasta.ToString(),
                    reservas.tipoAlojamiento.nombre, reservas.usuario.DNI.ToString(), reservas.precio.ToString()});
            }

            return resultado;

        }



        public List<List<string>> getTodasLasReservas()
        {

            List<List<string>> resultado = new List<List<string>>();

            var query = from reservaDB in reservas
                        select reservaDB;

            foreach (Entities.Reserva reservas in query)
            {
                resultado.Add(new List<string> { reservas.FDesde.ToString(), reservas.FHasta.ToString(),
                    reservas.tipoAlojamiento.nombre, reservas.usuario.DNI.ToString(), reservas.precio.ToString()});
            }

            return resultado;
        }
        public List<List<string>> getReservasPorCliente(int dni)
        {
            List<List<string>> resultado = new List<List<string>>();

            var query = from reservaDB in reservas
                        where reservaDB.usuario.DNI == dni
                        select reservaDB;

            foreach (Entities.Reserva reservas in query)
            {
                resultado.Add(new List<string> { reservas.FDesde.ToString(), reservas.FHasta.ToString(),
                    reservas.tipoAlojamiento.nombre, reservas.usuario.DNI.ToString(), reservas.precio.ToString()});
            }

            return resultado;
        }

       /* public bool reservar(string nombre, string dniUsuario, DateTime Fdesde, DateTime Fhasta)
        {


            var queryHotel = from hotelDB in hoteles
                             where hotelDB.nombre == nombre
                             select hotelDB;

            var queryCabania = from cabania in cabanias
                               where cabania.nombre == nombre
                               select cabania;


            if (queryHotel != null)
            {
                Entities.Hotel hotel = queryHotel.FirstOrDefault();

                if (hotel != null)
                {
                    if (hotel.reservado != true)
                    {
                        contexto.Hotel.Remove(hotel);
                        contexto.SaveChanges();
                        result = true;
                    }
                    
                }
            }
            else if (queryCabania != null)
            {
                Entities.Cabania cabania = queryCabania.FirstOrDefault();

                if (cabania != null)
                {
                    contexto.Cabania.Remove(cabania);
                    contexto.SaveChanges();
                    result = true;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }*/

       /* public bool modificarReserva(int ID, DateTime FDesde, DateTime FHasta, Entities.TipoAlojamiento propiedad, Entities.Usuario persona, float precio)//Parametro Datos de Reserva ¿?
        {
            bool modificada = false;

            var query = from reservaDB in reservas
                        where reservaDB.id == ID
                        select reservaDB;

            Entities.Reserva reserva = query.FirstOrDefault();

            if(reserva != null)
            {

            }

            return modificada;
        }

        public bool eliminarReserva(int id)
        {
            bool result = false;


            var query = from reservaDB in reservas
                        where reservaDB.id == id
                        select reservaDB;



            if (query != null)
            {
                Entities.Reserva reserva = query.FirstOrDefault();

                if (reserva != null)
                {
                    contexto.Reserva.Remove(reserva);
                    contexto.SaveChanges();
                    result = true;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }
       */

        // ----------------------- METODOS USUARIOS -----------------------

        public List<List<string>> obtenerUsuarios()
        {

            var query = from usuario in misUsuarios
                        select usuario;

            List<List<string>> usuarios = new List<List<string>>();

            foreach (Entities.Usuario u in query)
            {
                usuarios.Add(new List<string> { u.DNI.ToString(), u.nombre, u.mail, u.pass, u.esAdmin.ToString(), u.bloqueado.ToString() });
            }
            List<List<string>> salida = new List<List<string>>();

            return usuarios;
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

            var query = from usuarioDB in misUsuarios
                        where usuarioDB.DNI == dni
                        select usuarioDB;

            Entities.Usuario u = query.FirstOrDefault();
            if (u != null)
            {
                u.nombre = nombre;
                u.mail = mail;
                u.pass = pass;
                u.esAdmin = esAdmin;
                u.bloqueado = bloqueado;
                contexto.Usuario.Update(u);
                respuesta = true;
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

            var query = from usuarioDB in misUsuarios
                        where usuarioDB.id == id
                        select usuarioDB;

            Entities.Usuario usuario = query.FirstOrDefault();

            try
            {
                if (usuario != null)
                {
                    contexto.Usuario.Remove(usuario);
                    contexto.SaveChanges();
                    respuesta = true;
                }

            }
            catch (Exception)
            {
                respuesta = false;
            }

            return respuesta;
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
