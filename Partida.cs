﻿using System;
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
    int delayEnemigos = 0;
    int maxDelayEnemigos = 7; // es el numero al que tiene que llegar el delay de enemigos para permitir el movimiento

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
        Disparo disparoOvni = new Disparo(1, 1, 1, 3, true, ConsoleColor.Red);
        disparoOvni.SetImagen("o");
        disparoOvni.SetPerfora(true);
        Ovni ovni = new Ovni();

        enemigosDireccion = 1;
        delayEnemigos = 0;
        maxDelayEnemigos = 7;
        

        for (int i = 0; i < disparoEnemigos.Length; i++) disparoEnemigos[i] = new Disparo(1, 1, 1, 0);
        
        torres[0] = new TorreDefensiva(10, 15);
        torres[1] = new TorreDefensiva(35, 15);
        torres[2] = new TorreDefensiva(62, 15);

        do
        {
            DibujarPantalla(nave);
            ComprobarCollisiones(nave, naveDisparo, bloqueEn, disparoEnemigos, torres, ovni, disparoOvni);
            if (Console.KeyAvailable) { ComprobarTeclas(nave, naveDisparo); }  
            DibujarElementos(nave, naveDisparo, bloqueEn, disparoEnemigos, torres, ovni, disparoOvni);
            Thread.Sleep(50);

            if (delayEnemigos >= maxDelayEnemigos) { bloqueEn.Mover(enemigosDireccion); delayEnemigos = 0; }
            else { delayEnemigos++; }
            bloqueEn.DisparoEnemigo(disparoEnemigos);

            ovni.Mover();
            if (ovni.GetActivo() == true && ovni.ProbabilidadDisparo() == true && disparoOvni.GetActivo() == false) { ovni.Disparar(disparoOvni); }
            if (disparoOvni.GetActivo() == true) { disparoOvni.Mover(); };
            if (naveDisparo.GetActivo() == true) { naveDisparo.Mover(); }
            for (int i = 0; i < 5; i++)
            {
                if (disparoEnemigos[i].GetActivo() == true) { disparoEnemigos[i].Mover(); }
            }

            if (nave.GetModo() == 1)
            {
                nave.SetImagen("/x\\");
            }
            else
            {
                nave.SetImagen("/0\\");
            }

            if (nave.GetVidas() <= 0) { salir = true; }
            if (bloqueEn.ComprobarMuertos() == true) { salir = true; }

        } while (salir == false);

        AnyadirInfoJugador();
    }

    private bool ComprobarTeclas(Nave nave, Disparo naveDisparo)
    {
        tecla = Console.ReadKey();
        if (tecla.Key == ConsoleKey.X) { salir = true; }
        else if (tecla.Key == ConsoleKey.LeftArrow && nave.GetX() > 1) { nave.MoverIzquierda(); }
        else if (tecla.Key == ConsoleKey.RightArrow && nave.GetX() < maxX - 3) { nave.MoverDerecha(); }
        else if (tecla.Key == ConsoleKey.Spacebar && naveDisparo.GetActivo() == false) {nave.Disparar(naveDisparo); }
        else if (tecla.Key == ConsoleKey.Z) { nave.SetModo(nave.GetModo() + 1); }

        while (Console.KeyAvailable) { Console.ReadKey(true); }
        return false;
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
        
        if (naveDisparo.GetY() == 0 && naveDisparo.GetActivo() == true)
        {
            naveDisparo.SetActivo(false);
            naveDisparo.Desaparecer();
        }
        if (disparoOvni.GetY() == nave.GetY() && disparoOvni.GetActivo() == true)
        {
            disparoOvni.Detonar(nave.GetY(), maxX);
            disparoOvni.SetActivo(false);
            disparoOvni.Desaparecer();
            nave.SetVidas(nave.GetVidas() - 1);
        }
        if (naveDisparo.CollisionaCon(disparoOvni) == true)
        {
            disparoOvni.SetActivo(false);
            disparoOvni.Desaparecer();
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
        if ((bloqueEn.GetUltimoEnemigo().GetX() == maxX - 5 || bloqueEn.GetPrimerEnemigo().GetX() == 1) && delayEnemigos >= maxDelayEnemigos) 
        { 
            enemigosDireccion *= -1;
            bloqueEn.Mover(enemigosDireccion);
            bloqueEn.MoverAbajo();
            maxDelayEnemigos -= 1;
            delayEnemigos = 0;

        }
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
        for (int i = 0; i < torres.Length; i++)
        {
            for (int j = 0; j < disparoEnemigos.Length; j++) { torres[i].CollisionCon(disparoEnemigos[j], true); }
        }
        for (int i = 0; i < torres.Length; i++) { bloqueEn.CollisionaCon(torres[i]); }
        for (int i = 0; i < torres.Length; i++) {
            if (naveDisparo.GetPerfora() == true) { torres[i].CollisionCon(naveDisparo, false); }
            if (naveDisparo.GetPerfora() == false) { torres[i].CollisionCon(naveDisparo, true); }
        }
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
        Console.Write("Modo: " + nave.GetModo());

    }
}