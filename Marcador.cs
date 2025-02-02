using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Marcador
{
    int listaTamanyo = 0;
    struct TipoListaPuntuacion
    {
        public string nombre;
        public int puntuacion;
    }

    TipoListaPuntuacion[] listaDePuntuaciones = new TipoListaPuntuacion[100];

    public Marcador()
    {

    }

    public void AddPuntuacion(string nombre, int puntuacion)
    {
        if (listaTamanyo < listaDePuntuaciones.Length) 
        {
            listaDePuntuaciones[listaTamanyo].nombre = nombre;
            listaDePuntuaciones[listaTamanyo].puntuacion = puntuacion;
            listaTamanyo++;
        }
        
        
    }

    public void OrdenarPuntuaciones()
    {

        for (int i=0; i<listaTamanyo - 1;i++)
        {
            for (int j = i + 1; j < listaTamanyo; j++)
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
        int tamanyo = listaTamanyo;
        if (tamanyo > 10) { tamanyo = 10; }
        string[] puntuaciones = new string[tamanyo];

        for (int i = 0; i < puntuaciones.Length; i++)
        {
            puntuaciones[i] = listaDePuntuaciones[i].nombre + " | " + listaDePuntuaciones[i].puntuacion + " puntos";
        }

        return puntuaciones;
    }


}

