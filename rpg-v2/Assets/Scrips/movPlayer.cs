using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movPlayer : MonoBehaviour
{

    private Vector2 dirMovimiento;
    public float velMovimiento;
    public Rigidbody2D rb;
    public Animator anim;

    [SerializeField] private string capaIde;
    [SerializeField] private string capaCaminar; 
    private bool PlayerMoviendose = false;
    private float ultimoMovX, ultimoMovY;
    // Permite realizar calculos cuadro a cuadro
    void FixedUpdate()
    {
        Movimiento();
        AnimacionesMago();
    }

    private void Movimiento()
    {
        float movimientoX = Input.GetAxisRaw("Horizontal");
        float movimientoY = Input.GetAxisRaw("Vertical");
        dirMovimiento = new Vector2(movimientoX, movimientoY).normalized;
        rb.velocity = new Vector2(dirMovimiento.x * velMovimiento,
        dirMovimiento.y * velMovimiento);

        if (movimientoX == 0 && movimientoY == 0)//idle
        {
            PlayerMoviendose = false;
        }
        else //caminar
        {
            PlayerMoviendose = true;
            ultimoMovX = movimientoX;
            ultimoMovY = movimientoY;

        }

        ActualizarCapa();

    }
    private void AnimacionesMago()
    {
        anim.SetFloat("movimientoX", ultimoMovX);
        anim.SetFloat("movimientoY", ultimoMovY);
    }
    private void ActualizarCapa()
    {
        if (PlayerMoviendose)
        {
            activaCapa(capaCaminar);
            Debug.Log("Camianar");
        }
        else
        {
            activaCapa(capaIde);
            Debug.Log("ide");

        }
    }
    
    private void activaCapa(string nombre)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);//Ambos layer los mandamos a 0
        }
        anim.SetLayerWeight(anim.GetLayerIndex(nombre), 1);//solo se encendera al que se le de el peso, se swichearan la opcion
    }
    
}
