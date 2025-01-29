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

    public void Lanzar()
    {
        Console.SetWindowSize(79, 23);
        Nave nave = new Nave();
        Disparo naveDisparo = new Disparo(1, 1, -1);
        BloqueDeEnemigos bloqueEn = new BloqueDeEnemigos();
        Disparo[] disparoEnemigos = new Disparo[5];
        for (int i = 0; i < disparoEnemigos.Length; i++) disparoEnemigos[i] = new Disparo(1, 1, 1);

        do
        {
            DibujarElementos(nave, naveDisparo, bloqueEn, disparoEnemigos);
            Thread.Sleep(100);

            if (Console.KeyAvailable) { salir = ComprobarTeclas(nave, naveDisparo); }
            ComprobarCollisiones(nave, naveDisparo, bloqueEn, disparoEnemigos);
            bloqueEn.Mover(enemigosDireccion);
            bloqueEn.DisparoEnemigo(disparoEnemigos);


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
    private void DibujarElementos(Nave nave, Disparo naveDisparo, BloqueDeEnemigos bloqueEn, Disparo[] disparoEnemigos)
    {
        if (naveDisparo.GetActivo() == true) { naveDisparo.Dibujar(); }
        nave.Dibujar();
        bloqueEn.Dibujar();
        for (int i = 0; i < disparoEnemigos.Length; i++)
        {
            if (disparoEnemigos[i].GetActivo() == true) { disparoEnemigos[i].Dibujar(); }
        }
    }
    private void ComprobarCollisiones(Nave nave, Disparo naveDisparo, BloqueDeEnemigos bloqueEn, Disparo[] disparoEnemigos)
    {
        if (naveDisparo.GetY() == 0 && naveDisparo.GetActivo() == true)
        {
            naveDisparo.SetActivo(false);
            naveDisparo.Desaparecer();
        }
        for (int i = 0; i < 5; i++)
        {
            if (disparoEnemigos[i].GetY() > maxY && disparoEnemigos[i].GetActivo() == true)
            {
                disparoEnemigos[i].SetActivo(false);
                disparoEnemigos[i].Desaparecer();
            }
        }
        if (bloqueEn.GetUltimoEnemigo().GetX() == maxX - 1 || bloqueEn.GetPrimerEnemigo().GetX() == 1) 
        { 
            enemigosDireccion *= -1;
            bloqueEn.MoverAbajo();
            
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

    }
}