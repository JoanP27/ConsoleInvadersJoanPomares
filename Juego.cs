using System;
class Juego
{
    public void Lanzar()
    {
        Bienvenida bienvenida = new Bienvenida();
        Partida partida = new Partida();

        do
        {
            Console.Clear();
            bienvenida.Lanzar();
            Console.Clear();
            if (bienvenida.GetSalir() == false) { partida.Lanzar(); }
        } while (bienvenida.GetSalir() == false);
        
    }
}