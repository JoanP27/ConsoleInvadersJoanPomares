using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Marcador
{
    int listaSize = 0;
    struct TipoListaPuntuacion
    {
        public string nombre;
        public int puntuacion;
    }

    TipoListaPuntuacion[] listaDePuntuaciones = new TipoListaPuntuacion[10];

    public Marcador()
    {

    }

    public void AddPuntuacion(string nombre, int puntuacion)
    {
        if (listaSize < listaDePuntuaciones.Length) {
            listaDePuntuaciones[listaSize].nombre = nombre;
            listaDePuntuaciones[listaSize].puntuacion = puntuacion;
            listaSize++;
        }
        
    }

    public void OrdenarPuntuaciones()
    {
        for (int i=0; i<listaSize - 1;i++)
        {
            for (int j = i + 1; j < listaSize; j++)
            {

                if (listaDePuntuaciones[i].puntuacion < listaDePuntuaciones[j].puntuacion)
                {
                    TipoListaPuntuacion temporal = listaDePuntuaciones[i];
                    listaDePuntuaciones[i] = listaDePuntuaciones[j];
                    listaDePuntuaciones[j] = temporal;
                }
            }
        }
    }

    public string[] GetPuntuaciones() 
    {
        string[] puntuaciones = new string[listaSize];

        for (int i = 0; i < puntuaciones.Length; i++)
        {
            puntuaciones[i] = listaDePuntuaciones[i].nombre + " | " + listaDePuntuaciones[i].puntuacion + " puntos";
        }

        return puntuaciones;
    }


}

