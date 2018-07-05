using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace GPS_Challenge.Domain.Model.Entities
{
    public class PersistenciaJuego
    {
        List<Vehiculo> list = new List<Vehiculo>();

        public PersistenciaJuego()
        {
            list.Add(Vehiculo.Auto());
            list.Add(Vehiculo.Moto());
            list.Add(Vehiculo.CuatroPorCuatro());
        }

        public void persistirJuego(Juego juego)
        {
            string nombre_archivo = juego.Jugador.Nombre + ".xml";

            using (XmlWriter writer = XmlWriter.Create(nombre_archivo))
            {
                writer.WriteStartDocument();

                int posicion_vehiculoX = 0;
                int posicion_vehiculoY = 0;
                Mapa mapa = juego.Mapa;
                for (int x = 0; x < mapa.GetTamanio(); x++)

                    for (int y = 0; y < mapa.GetTamanio(); y++)
                    {
                        if (mapa.GetEsquina(x, y) == juego.Jugador.Vehiculo.Esquina)
                        {
                            posicion_vehiculoX = x;
                            posicion_vehiculoY = y;
                        }
                    }

                Vehiculo vehiculo = juego.Jugador.Vehiculo;
                writer.WriteStartElement("jugador");
                writer.WriteElementString("nombre", juego.Jugador.Nombre);
                writer.WriteElementString("movimientos", juego.Jugador.Vehiculo.Movimientos.ToString());
                writer.WriteElementString("tipo", vehiculo.Tipo.Nombre());
                writer.WriteElementString("x", posicion_vehiculoX.ToString());
                writer.WriteElementString("y", posicion_vehiculoY.ToString());
                writer.WriteElementString("dificultad", juego.LimiteDeMovimientos.ToString());

                writer.WriteEndElement();

            }

            StreamReader sr = new StreamReader("PartidasGuardadas.txt");
            String line;
            List<String> jugadores = new List<String>();

            while ((line = sr.ReadLine()) != null)
            {
                if (line == juego.Jugador.Nombre)
                    jugadores.Add(juego.Jugador.Nombre);
                else
                    jugadores.Add(line);
            }

            sr.Close();

            if (!jugadores.Contains(juego.Jugador.Nombre))
            {
                StreamWriter sw = new StreamWriter("PartidasGuardadas.txt", true);
                sw.WriteLine(juego.Jugador.Nombre);
                sw.Close();
            }
        }

        public Juego cargarJuego(string nombreJugador)
        {

            using (XmlReader reader = XmlReader.Create(nombreJugador + ".xml"))
            {
                String nombre;
                int movimientos = 0;
                int posicionX = 0;
                int posicionY = 0;
                Juego juego = new JuegoFacil(new Jugador("a", new Vehiculo()));
                String tipo;
                Vehiculo vehiculo = Vehiculo.Auto();
                while (reader.Read())
                {
                    // Only detect start elements.
                    if (reader.IsStartElement())
                    {

                        if (reader.Name == "nombre")
                        {
                            reader.Read();
                            nombre = reader.Value.Trim();
                        }

                        if (reader.Name == "movimientos")
                        {
                            reader.Read();
                            movimientos = Convert.ToInt32(reader.Value.Trim());
                        }

                        if (reader.Name == "x")
                        {
                            reader.Read();
                            posicionX = Convert.ToInt32(reader.Value.Trim());
                        }

                        if (reader.Name == "y")
                        {
                            reader.Read();
                            posicionY = Convert.ToInt32(reader.Value.Trim());
                        }

                        if (reader.Name == "tipo")
                        {
                            reader.Read();
                            tipo = reader.Value.Trim();

                            foreach (Vehiculo tipo_de_vehiculo in list)
                            {
                                if (tipo_de_vehiculo.Tipo.Nombre() == tipo)
                                {
                                    vehiculo = tipo_de_vehiculo;
                                    vehiculo.AgregarMovimientos(movimientos);
                                }
                            }
                        }

                        if (reader.Name == "dificultad")
                        {
                            reader.Read();
                            int dificultad = Convert.ToInt32(reader.Value.Trim());

                            if (new JuegoDificil(new Jugador("a", new Vehiculo())).LimiteDeMovimientos == dificultad)
                            {
                                juego = new JuegoDificil(new Jugador(nombreJugador, vehiculo));
                            }
                            if (new JuegoModerado(new Jugador("a", new Vehiculo())).LimiteDeMovimientos == dificultad)
                            {
                                juego = new JuegoModerado(new Jugador(nombreJugador, vehiculo));
                            }
                            if (new JuegoFacil(new Jugador("a", new Vehiculo())).LimiteDeMovimientos == dificultad)
                            {
                                juego = new JuegoFacil(new Jugador(nombreJugador, vehiculo));
                            }
                            Esquina esquina = juego.Mapa.GetEsquina(posicionX, posicionY);
                            vehiculo.Esquina = esquina;
                        }
                    }
                }
                return juego;
            }
        }
    }
}
