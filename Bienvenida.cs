using System;
class Bienvenida
{
    bool salir;
    
    public void Lanzar()
    {
        salir = false;
        Console.WriteLine("< BIENVENIDO A CONSOLE INVADERS >");
        Console.WriteLine();
        Console.WriteLine("< ENTER  PARA [JUGAR] >");
        Console.WriteLine("< ESCAPE PARA [SALIR] >");
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