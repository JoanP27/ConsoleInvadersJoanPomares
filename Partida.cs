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
        salir = false;
        Console.SetWindowSize(79, 23);
        Nave nave = new Nave();
        Disparo naveDisparo = new Disparo(1, 1, -1);
        BloqueDeEnemigos bloqueEn = new BloqueDeEnemigos();
        Disparo[] disparoEnemigos = new Disparo[5];
        TorreDefensiva[] torres = new TorreDefensiva[3];



        for (int i = 0; i < disparoEnemigos.Length; i++) disparoEnemigos[i] = new Disparo(1, 1, 1);
        torres[0] = new TorreDefensiva(10, 15);
        torres[1] = new TorreDefensiva(35, 15);
        torres[2] = new TorreDefensiva(62, 15);

        do
        {
            ComprobarCollisiones(nave, naveDisparo, bloqueEn, disparoEnemigos, torres);
            DibujarElementos(nave, naveDisparo, bloqueEn, disparoEnemigos, torres);
            Thread.Sleep(100);

            if (Console.KeyAvailable) { salir = ComprobarTeclas(nave, naveDisparo); }
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
    private void DibujarElementos(Nave nave, Disparo naveDisparo, BloqueDeEnemigos bloqueEn, Disparo[] disparoEnemigos, TorreDefensiva[] torres)
    {
        if (naveDisparo.GetActivo() == true) { naveDisparo.Dibujar(); }
        nave.Dibujar();
        bloqueEn.Dibujar();
        
        for (int i = 0; i < disparoEnemigos.Length; i++)
        {
            if (disparoEnemigos[i].GetActivo() == true) { disparoEnemigos[i].Dibujar(); }
        }
        for (int i = 0; i < torres.Length; i++)
        {
            torres[i].Dibujar_Partes();
        }
    }
    private void ComprobarCollisiones(Nave nave, Disparo naveDisparo, BloqueDeEnemigos bloqueEn, Disparo[] disparoEnemigos, TorreDefensiva[] torres)
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
        if (bloqueEn.GetUltimoEnemigo().GetX() == maxX - 3 || bloqueEn.GetPrimerEnemigo().GetX() == 1) 
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
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {

                if (disparoEnemigos[i].CollisionaCon(nave) == true)
                {
                    salir = true;
                }

                for (int k = 0; k < 3; k++)
                {
                    for (int l = 0; l < 7; l++)
                    {
                        if (disparoEnemigos[i].CollisionaCon(torres[j].GetPartes()[k, l]) == true && torres[j].GetPartes()[k, l].GetActivo() == true) 
                        {
                            disparoEnemigos[i].SetActivo(false);
                            disparoEnemigos[i].Desaparecer();
                            torres[j].GetPartes()[k, l].SetActivo(false);
                            torres[j].GetPartes()[k, l].Desaparecer();
                        }
                    }
                }
            }
        }
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (bloqueEn.GetEnemigos()[i,j].CollisionaCon(nave) == true)
                {
                    salir = true;
                }
                for (int k = 0; k < 3; k++)
                {
                    for (int l = 0; l < 3; l++)
                    {
                        for (int m = 0; m < 7; m++)
                        {
                            if (bloqueEn.GetEnemigos()[i, j].CollisionaCon(torres[k].GetPartes()[l, m]) == true && torres[k].GetPartes()[l, m].GetActivo() == true)
                            {
                                torres[k].GetPartes()[l, m].SetActivo(false);
                                torres[k].GetPartes()[l, m].Desaparecer();
                            }
                        }
                    }
                }
            }
        }


    }
}