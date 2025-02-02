using System;
class Bienvenida
{
    bool salir;

    Marcador marcador;
    public void SetMarcador(Marcador marcador)
    {
        this.marcador = marcador;
    }
    public void Lanzar()
    {
        Console.ForegroundColor = ConsoleColor.White;
        salir = false;
        Console.WriteLine("< BIENVENIDO A CONSOLE INVADERS >");
        Console.WriteLine();

        Console.WriteLine(" - Mejores Puntuaciones - ");
        marcador.OrdenarPuntuaciones();
        string[] puntuaciones = marcador.GetPuntuaciones();
        
        for (int i = 0; i < puntuaciones.Length; i++)
        {
            Console.WriteLine(" ------------------");
            Console.WriteLine(i+1 + "| " + puntuaciones[i]);
        }

        Console.WriteLine();

        Console.WriteLine("< ENTER  PARA [JUGAR] > | < ESCAPE PARA [SALIR] >");

        ConsoleKeyInfo tecla;
        
        
        tecla = Console.ReadKey();

        if (tecla.Key == ConsoleKey.Escape) { salir = true; }
        else if (tecla.Key == ConsoleKey.Enter) { salir = false; }

        
    }
    public bool GetSalir()
    {
        return salir;
    }
}