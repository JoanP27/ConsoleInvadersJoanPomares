using System;
class Juego
{
    public void Lanzar()
    {
        Bienvenida bienvenida = new Bienvenida();
        Partida partida = new Partida();
        Marcador marcador = new Marcador();

        partida.SetMarcador(marcador);
        bienvenida.SetMarcador(marcador);
        do
        {
            Console.Clear();
            bienvenida.Lanzar();
            Console.Clear();

            if (bienvenida.GetSalir() == false) { partida.Lanzar(); }
        } while (bienvenida.GetSalir() == false);
        
    }
}