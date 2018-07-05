using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GPS_Challenge.Domain.Model.Exceptions;

namespace GPS_Challenge.Domain.Model.Entities
{
    public class Mapa
    {
        private Esquina[,] mapa;
        private int tamanio;

        public Mapa(int tam) 
        {
            this.tamanio = tam;
            this.mapa = new Esquina[tam, tam];

            for (int i = 0; i < tam; i++)
                for (int j = 0; j < tam; j++)
                    this.mapa[i, j] = new Esquina();

            for (int i = 0; i < tam; i++)
                for (int j = 0; j < tam; j++)
                {
                    if (i > 0)
                        this.mapa[i, j].SetEsquina(Orientacion.Norte, this.mapa[i-1,j]);
                    if (i < tam-1)
                        this.mapa[i, j].SetEsquina(Orientacion.Sur, this.mapa[i + 1, j]);
                    if (j < tam-1)
                        this.mapa[i, j].SetEsquina(Orientacion.Este, this.mapa[i, j + 1]);
                    if (j > 0)
                        this.mapa[i, j].SetEsquina(Orientacion.Oeste, this.mapa[i, j - 1]); 
                }

            AgregarCuadras();
        }

        private void AgregarCuadras() 
        {
            Cuadra cuadra;
            Random random = new Random();
            for (int i = 0; i < this.tamanio - 1; i++)
            {
                for (int j = 0; j < this.tamanio - 1; j++)
                {
                    cuadra = GeneradorDeCuadra(random);
                    this.mapa[i, j].SetCuadra(Orientacion.Este, cuadra);
                    this.mapa[i, j + 1].SetCuadra(Orientacion.Oeste, cuadra);

                    cuadra = GeneradorDeCuadra(random);
                    this.mapa[i, j].SetCuadra(Orientacion.Sur, cuadra);
                    this.mapa[i + 1, j].SetCuadra(Orientacion.Norte, cuadra);
                }
            }
            
            for (int i = 0; i < this.tamanio - 1; i++)
            {
                cuadra = GeneradorDeCuadra(random);
                this.mapa[this.tamanio - 1, i].SetCuadra(Orientacion.Este, cuadra);
                this.mapa[this.tamanio - 1, i + 1].SetCuadra(Orientacion.Oeste, cuadra);

                cuadra = GeneradorDeCuadra(random);
                this.mapa[i, this.tamanio - 1].SetCuadra(Orientacion.Sur, cuadra);
                this.mapa[i + 1, this.tamanio - 1].SetCuadra(Orientacion.Norte, cuadra);
            }       
        }

        private Cuadra GeneradorDeCuadra(Random random)
        {
            Sorpresa[] sorpresasRandom = new Sorpresa[3];
            sorpresasRandom[0] = new SorpresaFavorable();
            sorpresasRandom[1] = new SorpresaDesfavorable();
            sorpresasRandom[2] = new SorpresaCambioVehiculo();

            Obstaculo[] obstaculosRandom = new Obstaculo[3];
            obstaculosRandom[0] = new Pozo();
            obstaculosRandom[1] = new Piquete();
            obstaculosRandom[2] = new ControlPolicial();

            Sorpresa sorpresaElegida = null;
            Obstaculo obstaculoElegido = null;
            if (random.Next(10) < 4)
                sorpresaElegida = sorpresasRandom[random.Next(3)];
            if (random.Next(10) < 4)
                obstaculoElegido = obstaculosRandom[random.Next(3)];

            
            return new Cuadra(sorpresaElegida, obstaculoElegido);
        }

        public Esquina GetEsquina(int i, int j) 
        {
            if (i < 0 || i > this.tamanio - 1 || j < 0 || j > this.tamanio - 1)
                throw new PosicionFueraDeRangoException();

            return this.mapa[i, j];
        }

        public int GetTamanio()
        {
            return tamanio;
        }

    }
}
