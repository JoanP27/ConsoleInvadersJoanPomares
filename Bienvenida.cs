using System;
using System.Runtime.CompilerServices;
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
        marcador.AddPuntuacion("Joan", 3500);
        string[] puntuaciones = marcador.GetPuntuaciones();
        marcador.OrdenarPuntuaciones();
        bool salirBienvenida = false;
        do
        {
            
            salir = true;
            Console.WriteLine("< BIENVENIDO A CONSOLE INVADERS > < Lee las intrucciones antes de jugar porfavor >");
            Console.WriteLine();

            Console.WriteLine(" - Mejores Puntuaciones - ");


            for (int i = 0; i < puntuaciones.Length; i++)
            {
                Console.WriteLine(" ------------------");
                Console.WriteLine(i + 1 + "| " + puntuaciones[i]);
            }

            Console.WriteLine();

            Console.WriteLine("< ENTER  PARA [JUGAR] > | < ESCAPE PARA [SALIR] > | < I PARA INSTRUCCIONES>");

            ConsoleKeyInfo tecla;
            tecla = Console.ReadKey();

            if (tecla.Key == ConsoleKey.Escape) { salir = true; salirBienvenida = true; }
            else if (tecla.Key == ConsoleKey.Enter) { salir = false; salirBienvenida = true; }
            else if (tecla.Key == ConsoleKey.I) { Instrucciones(); }
        }while (salirBienvenida == false);
    
    }
    public bool GetSalir()
    {
        return salir;
    }

    private void Instrucciones()
    {
        Console.Clear();

        Console.WriteLine(" [ CONTROLES ]");
        Console.WriteLine(" -> | Mover nave a la derecha");
        Console.WriteLine(" <- | Mover nave a la izquierda");
        Console.WriteLine(" Espacio | disparar");
        Console.WriteLine(" Z | Cambiar modo");
        Console.WriteLine(" X | Salir del juego");

        Console.WriteLine("--------------------------------");
        Console.WriteLine(" ");

        Console.WriteLine(" [ JUGABIIDAD ] ");
        Console.WriteLine("1. Los enemigos van aumento la velocidad conforme mas bajan");
        Console.WriteLine("2. De vez en cuando aparece un ovni muy rapido que da 1000 puntos");
        Console.WriteLine("3. El ovni es capaz de lanzar una bomba roja que si cae al suelo explotara y te quitra 1 vida");
        Console.WriteLine("4. La bomba se puede disparar para evitar que caiga");
        Console.WriteLine("5. La nave tiene un modo que dispara balas perforadoras, mucho mas lentas pero no se desrullen con nada");

        Console.WriteLine();
        Console.WriteLine("Presionar ESPACIO para volver");
        ConsoleKeyInfo tecla = Console.ReadKey();
        Console.Clear();

    }
}