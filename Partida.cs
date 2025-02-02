using System;
using System.ComponentModel.Design;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;
class Partida
{
    protected const int maxX = 79;
    protected const int maxY = 23;

    ConsoleKeyInfo tecla;
    bool salir;
    int enemigosDireccion = 1;
    // delayEnemigos es un contador que al llegar un numero permitira que el bloque de enemigos se mueva y compruebe si collisiona con la consola,
    // sirve para hacerlos mas lentos sin afectar a la velocidad del juego
    int velocidadEnemigos = 0;
    int maxVeocidadEnemigos = 7; // es el numero al que tiene que llegar el delay de enemigos para permitir el movimiento

    private int puntuacionTotal = 0;

    Marcador marcador;

    public void SetMarcador(Marcador marcador)
    {
        this.marcador = marcador;
    }
    public void Lanzar()
    {
        salir = false;
        puntuacionTotal = 0;
        Console.SetWindowSize(79, 23);
        Nave nave = new Nave();
        Disparo naveDisparo = new Disparo(1, 1, -1, 0);
        BloqueDeEnemigos bloqueEn = new BloqueDeEnemigos();
        Disparo[] disparoEnemigos = new Disparo[5];
        TorreDefensiva[] torres = new TorreDefensiva[3];
        Disparo disparoOvni = new Disparo(1, 1, 1, 3, ConsoleColor.Red);
        disparoOvni.SetImagen("o");
        disparoOvni.SetPerfora(true);
        Ovni ovni = new Ovni();

        enemigosDireccion = 1;
        velocidadEnemigos = 0;
        maxVeocidadEnemigos = 7;
        

        for (int i = 0; i < disparoEnemigos.Length; i++) disparoEnemigos[i] = new Disparo(1, 1, 1, 0);
        
        torres[0] = new TorreDefensiva(10, 15);
        torres[1] = new TorreDefensiva(35, 15);
        torres[2] = new TorreDefensiva(62, 15);

        do
        {
            // Llama a la funcion que dibuja la decoracion y datos del juego en pantalla
            DibujarPantalla(nave);

            ComprobarCollisiones(nave, naveDisparo, bloqueEn, disparoEnemigos, torres, ovni, disparoOvni); 

            if (Console.KeyAvailable) { ComprobarTeclas(nave, naveDisparo); }   // Comprueba las teclas pulsadas por el jugador

            DibujarElementos(nave, naveDisparo, bloqueEn, disparoEnemigos, torres, ovni, disparoOvni); // Dibuja los elementos en pantalla
            
            Thread.Sleep(50);

            // Comprueba si el contador de la velocidad de enemigos llega al maximo, si llega al maximo entonces los enemigos se moveran
            if (velocidadEnemigos >= maxVeocidadEnemigos) { bloqueEn.Mover(enemigosDireccion); velocidadEnemigos = 0; } 
            else { velocidadEnemigos++; } 

            // Llama a la funcion disparo del bloque de enemigos
            bloqueEn.DisparoEnemigo(disparoEnemigos);

            // Mueve a los respectivos enemigos y disparos
            ovni.Mover();
            if (disparoOvni.GetActivo() == true) { disparoOvni.Mover(); }
            if (naveDisparo.GetActivo() == true) { naveDisparo.Mover(); }
            for (int i = 0; i < 5; i++)
            {
                if (disparoEnemigos[i].GetActivo() == true) { disparoEnemigos[i].Mover(); }
            }

            // Comprueba que el ovni puede disparar y en ese caso llama a la funcion disparo del ovni
            if (ovni.GetActivo() == true && ovni.ProbabilidadDisparo() == true && disparoOvni.GetActivo() == false) { ovni.Disparar(disparoOvni); }

            // Cambia el aspecto del jugador dependiendo del modo siendo usado
            if (nave.GetModo() == 1)
            {
                nave.SetImagen("/x\\");
            }
            else
            {
                nave.SetImagen("/0\\");
            }

            // Comprueba si el jugador a perdido todas sus vidas o a matado a todos los enemigos para salir del juego
            if (nave.GetVidas() <= 0) { salir = true; }
            if (bloqueEn.ComprobarMuertos() == true) { salir = true; }

        } while (salir == false);

        // Pide los datos al jugador
        AnyadirInfoJugador();
    }

    private void ComprobarTeclas(Nave nave, Disparo naveDisparo)
    {
        tecla = Console.ReadKey();
        if (tecla.Key == ConsoleKey.X) { salir = true; }
        else if (tecla.Key == ConsoleKey.LeftArrow && nave.GetX() > 1) { nave.MoverIzquierda(); }
        else if (tecla.Key == ConsoleKey.RightArrow && nave.GetX() < maxX - 3) { nave.MoverDerecha(); }
        else if (tecla.Key == ConsoleKey.Spacebar && naveDisparo.GetActivo() == false) {nave.Disparar(naveDisparo); }
        else if (tecla.Key == ConsoleKey.Z) { nave.SetModo(nave.GetModo() + 1); }

        while (Console.KeyAvailable) { Console.ReadKey(true); }
    }
    private void DibujarElementos(Nave nave, Disparo naveDisparo, BloqueDeEnemigos bloqueEn, Disparo[] disparoEnemigos, TorreDefensiva[] torres, Ovni ovni, Disparo disparoOvni)
    {
        if (naveDisparo.GetActivo() == true) { naveDisparo.Dibujar(); }

        bloqueEn.Dibujar();

        for (int i = 0; i < disparoEnemigos.Length; i++)
        {
            if (disparoEnemigos[i].GetActivo() == true) { disparoEnemigos[i].Dibujar(); }
        }

        for (int i = 0; i < torres.Length; i++)
        {
            torres[i].Dibujar_Partes();
        }

        nave.Dibujar();

        if (ovni.GetActivo() == true) { ovni.Dibujar(); }
        if (disparoOvni.GetActivo() == true) { disparoOvni.Dibujar(); };
        
    }
    private void ComprobarCollisiones(Nave nave, Disparo naveDisparo, BloqueDeEnemigos bloqueEn, Disparo[] disparoEnemigos, TorreDefensiva[] torres, Ovni ovni, Disparo disparoOvni)
    {
        // Comprueba si el disparo de la nave ha llegado al limite de la consola
        if (naveDisparo.GetY() == 0 && naveDisparo.GetActivo() == true)
        {
            naveDisparo.SetActivo(false);
            naveDisparo.Desaparecer();
        }
        // Comprueba si la bomba del ovni ha caido en la posicion Y del jugador, en ese caso explotara
        if (disparoOvni.GetY() == nave.GetY() && disparoOvni.GetActivo() == true)
        {
            disparoOvni.Detonar(nave.GetY(), maxX);
            disparoOvni.SetActivo(false);
            disparoOvni.Desaparecer();
            nave.SetVidas(nave.GetVidas() - 1);
        }
        for (int i = 0; i < 5; i++)
        {
            if (disparoEnemigos[i].GetY() > maxY - 2 && disparoEnemigos[i].GetActivo() == true)
            {
                disparoEnemigos[i].SetActivo(false);
                disparoEnemigos[i].Desaparecer();
            }
        }
        if (naveDisparo.CollisionaCon(disparoOvni) == true)
        {
            disparoOvni.SetActivo(false);
            disparoOvni.Desaparecer();
        }
        // Comprueba si el ultimo enemigo de la matriz o el primero han tocado borde, en ese caso la direccion del movimiento del bloque se revertira
        // velocidad enemigos entra aqui para que el bloque no se mueva varias veces hacia abajo al estar el borde varias pasadas de bucle principal
        if ((bloqueEn.GetUltimoEnemigo().GetX() == maxX - 5 || bloqueEn.GetPrimerEnemigo().GetX() == 1) && velocidadEnemigos >= maxVeocidadEnemigos)
        {
            enemigosDireccion *= -1;
            bloqueEn.Mover(enemigosDireccion);
            bloqueEn.MoverAbajo();
            maxVeocidadEnemigos -= 1;
            velocidadEnemigos = 0;

        }
        // Comprueba la collision entre la bala de nave y los enemigos
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (bloqueEn.GetEnemigos()[i, j].GetY() == maxY) { salir = true; }
                if (naveDisparo.CollisionaCon(bloqueEn.GetEnemigos()[i, j]) == true && naveDisparo.GetActivo() == true )
                {
                    if (naveDisparo.GetPerfora() == false)
                    {
                        naveDisparo.SetActivo(false);
                        naveDisparo.Desaparecer();
                    }
                    bloqueEn.GetEnemigos()[i, j].SetActivo(false);
                    bloqueEn.GetEnemigos()[i, j].Desaparecer();
                    puntuacionTotal += bloqueEn.GetEnemigos()[i, j].GetPuntuacionMorir();
                }
            }
        }
        // Collision entre los disparos del array disparo de enemigos y la nave
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (disparoEnemigos[i].CollisionaCon(nave) == true && disparoEnemigos[i].GetActivo()==true)
                {
                    disparoEnemigos[i].SetActivo(false);
                    disparoEnemigos[i].Desaparecer();
                    nave.SetVidas(nave.GetVidas() - 1);
                }
            }
        }
        // Collision entre el disparo de enemigos y los trozos de la torre
        for (int i = 0; i < torres.Length; i++)
        {
            for (int j = 0; j < disparoEnemigos.Length; j++) { torres[i].CollisionCon(disparoEnemigos[j], true); }
        }

        // Collision entre los enemigos y la torre
        for (int i = 0; i < torres.Length; i++) { bloqueEn.CollisionaCon(torres[i]); }

        // Collision entre el disparo de la nave y el trozo de torre, perforar comprueba si cuando collisionen la bala de
        // la nave debe desaparecer o no segun el modo del disparo
        for (int i = 0; i < torres.Length; i++) {
            if (naveDisparo.GetPerfora() == true) { torres[i].CollisionCon(naveDisparo, false); }
            if (naveDisparo.GetPerfora() == false) { torres[i].CollisionCon(naveDisparo, true); }
        }

        // Collisiones del ovni con el borde de pantalla y con el disparo de nave
        if (ovni.GetActivo() == true && ovni.GetX() == maxX - 10) { ovni.SetActivo(false); ovni.Desaparecer(); }
        if (naveDisparo.CollisionaCon(ovni) == true && naveDisparo.GetActivo() == true) { ovni.SetActivo(false); ovni.Desaparecer(); naveDisparo.SetActivo(false); naveDisparo.Desaparecer(); }

    }
    
    private void AnyadirInfoJugador()
    {

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("-- GAME OVER --");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("------ Ingresa los datos del Jugador ------");

        Console.Write("Nombre: ");
        marcador.AddPuntuacion(Console.ReadLine(), puntuacionTotal);
    }
    private void DibujarPantalla(Nave nave)
    {
        Console.ForegroundColor= ConsoleColor.White;
        Console.SetCursorPosition(0, maxY - 2);

        for (int i=0; i<maxX-1; i++)
        {
            Console.Write("-");
        }
        
        Console.SetCursorPosition(0, maxY - 1);

        for (int i = 0; i < maxX-1; i++)
        {
            Console.Write(" ");
        }


        Console.SetCursorPosition(0, maxY - 1);
        Console.Write(" Vidas:  ");
        for (int i=0; i<nave.GetVidas(); i++)
        {
            Console.Write(" " + nave.GetImg());
        }
        Console.SetCursorPosition(50, maxY - 1);
        Console.Write("Puntos: " + this.puntuacionTotal);

    }
}