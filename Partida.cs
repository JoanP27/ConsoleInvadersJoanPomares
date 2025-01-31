using System;
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
    int delayEnemigos = 0;
    int maxDelayEnemigos = 7; // es el numero al que tiene que llegar el delay de enemigos para permitir el movimiento


    public void Lanzar()
    {
        salir = false;
        Console.SetWindowSize(79, 23);
        Nave nave = new Nave();
        Disparo naveDisparo = new Disparo(1, 1, -1);
        BloqueDeEnemigos bloqueEn = new BloqueDeEnemigos();
        Disparo[] disparoEnemigos = new Disparo[5];
        TorreDefensiva[] torres = new TorreDefensiva[3];
        Ovni ovni = new Ovni();

        enemigosDireccion = 1;


        for (int i = 0; i < disparoEnemigos.Length; i++) disparoEnemigos[i] = new Disparo(1, 1, 1);
        torres[0] = new TorreDefensiva(10, 15);
        torres[1] = new TorreDefensiva(35, 15);
        torres[2] = new TorreDefensiva(62, 15);

        do
        {

            ComprobarCollisiones(nave, naveDisparo, bloqueEn, disparoEnemigos, torres, ovni);
            DibujarElementos(nave, naveDisparo, bloqueEn, disparoEnemigos, torres, ovni);
            Thread.Sleep(50);

            if (Console.KeyAvailable) { salir = ComprobarTeclas(nave, naveDisparo); }
            if (delayEnemigos >= maxDelayEnemigos) { bloqueEn.Mover(enemigosDireccion); delayEnemigos = 0; }
            else { delayEnemigos++; }
            bloqueEn.DisparoEnemigo(disparoEnemigos);

            ovni.Mover();

            if (naveDisparo.GetActivo() == true) { naveDisparo.Mover(); }
            for (int i = 0; i < 5; i++)
            {
                if (disparoEnemigos[i].GetActivo() == true) { disparoEnemigos[i].Mover(); }
            }
        } while (salir == false);
    }

    private bool ComprobarTeclas(Nave nave, Disparo naveDisparo)
    {
        tecla = Console.ReadKey();
        if (tecla.Key == ConsoleKey.Escape) { return true; }
        else if (tecla.Key == ConsoleKey.LeftArrow && nave.GetX() > 1) { nave.MoverIzquierda(); }
        else if (tecla.Key == ConsoleKey.RightArrow && nave.GetX() < maxX - 2) { nave.MoverDerecha(); }
        else if (tecla.Key == ConsoleKey.Spacebar && naveDisparo.GetActivo() == false) { nave.Disparar(naveDisparo); }

        while (Console.KeyAvailable) { Console.ReadKey(true); }
        return false;
    }
    private void DibujarElementos(Nave nave, Disparo naveDisparo, BloqueDeEnemigos bloqueEn, Disparo[] disparoEnemigos, TorreDefensiva[] torres, Ovni ovni)
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
    }
    private void ComprobarCollisiones(Nave nave, Disparo naveDisparo, BloqueDeEnemigos bloqueEn, Disparo[] disparoEnemigos, TorreDefensiva[] torres, Ovni ovni)
    {

        if (naveDisparo.GetY() == 0 && naveDisparo.GetActivo() == true)
        {
            naveDisparo.SetActivo(false);
            naveDisparo.Desaparecer();
        }
        for (int i = 0; i < 5; i++)
        {
            if (disparoEnemigos[i].GetY() > maxY - 2 && disparoEnemigos[i].GetActivo() == true)
            {
                disparoEnemigos[i].SetActivo(false);
                disparoEnemigos[i].Desaparecer();
            }
        }
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (bloqueEn.GetEnemigos()[i, j].GetY() == maxY) { salir = true; }
            }
        }
        if ((bloqueEn.GetUltimoEnemigo().GetX() == maxX - 5 || bloqueEn.GetPrimerEnemigo().GetX() == 1) && delayEnemigos >= maxDelayEnemigos) 
        { 
            enemigosDireccion *= -1;
            bloqueEn.Mover(enemigosDireccion);
            bloqueEn.MoverAbajo();
            maxDelayEnemigos -= 1;
            delayEnemigos = 0;

        }
        for (int i=0; i<3;i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (naveDisparo.CollisionaCon(bloqueEn.GetEnemigos()[i,j]) == true)
                {
                    naveDisparo.SetActivo(false);
                    naveDisparo.Desaparecer();
                    bloqueEn.GetEnemigos()[i, j].SetActivo(false);
                    bloqueEn.GetEnemigos()[i, j].Desaparecer();
                }
            }
        }
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (disparoEnemigos[i].CollisionaCon(nave) == true)
                {
                    salir = true;
                }
            }
        }
        for (int i = 0; i < torres.Length; i++)
        {
            for (int j = 0; j < disparoEnemigos.Length; j++) { torres[i].CollisionCon(disparoEnemigos[j], true); }
        }
        for (int i = 0; i < torres.Length; i++) { bloqueEn.CollisionaCon(torres[i]); }
        for (int i = 0; i < torres.Length; i++) { torres[i].CollisionCon(naveDisparo, true); }
        if (ovni.GetActivo() == true && ovni.GetX() == maxX - 10) { ovni.SetActivo(false); ovni.Desaparecer(); }
    }
}