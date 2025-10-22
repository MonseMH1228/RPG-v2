using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public static int vidaEnemigo = 1;
    private float freAtaque = 2.5f, tiempoSigAtaque = 0, iniciaConteo;

    public Transform personaje;
    private NavMeshAgent agente;
    public Transform[] puntoRuta;
    private int indiceRuta = 0;
    private bool playerEnRango = false;
    [SerializeField] private float distanciaDeteccionPlayer;
    private SpriteRenderer spriteEnemigo;
    private Transform mirarHacia;
    
    // ✅ NUEVO: Tolerancia para llegar al punto
    [SerializeField] private float distanciaMinimaPunto = 0.5f;

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        spriteEnemigo = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        vidaEnemigo = 1;
        agente.updateRotation = false;
        agente.updateUpAxis = false;
        
        // ✅ NUEVO: Establecer primer destino al iniciar
        if (puntoRuta.Length > 0)
        {
            agente.SetDestination(puntoRuta[indiceRuta].position);
        }
    }

    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        float distancia = Vector3.Distance(personaje.position, this.transform.position);

        // ✅ CORREGIDO: Detección del jugador
        if (distancia < distanciaDeteccionPlayer)
        {
            playerEnRango = true;
        }
        else
        {
            playerEnRango = false;
        }

        // ✅ CORREGIDO: Movimiento entre puntos SOLO cuando no hay jugador
        if (!playerEnRango && puntoRuta.Length > 0)
        {
            // ✅ Usar distancia en lugar de igualdad exacta
            float distanciaAlPunto = Vector3.Distance(transform.position, puntoRuta[indiceRuta].position);
            
            if (distanciaAlPunto <= distanciaMinimaPunto)
            {
                CambiarSiguientePunto();
            }
        }

        // ✅ CORREGIDO: Lógica de seguimiento SIEMPRE activa
        SiguePlayer(playerEnRango);
        RotaEnemigo();

        // ✅ CORREGIDO: Lógica de ataque separada
        if (tiempoSigAtaque > 0)
        {
            tiempoSigAtaque = freAtaque + iniciaConteo - Time.time;
        }
    }

    // ✅ NUEVO: Método para cambiar puntos de ruta
    private void CambiarSiguientePunto()
    {
        if (indiceRuta < puntoRuta.Length - 1)
        {
            indiceRuta++;
        }
        else
        {
            indiceRuta = 0;
        }
        
        // Establecer nuevo destino
        agente.SetDestination(puntoRuta[indiceRuta].position);
    }

    // ✅ CORREGIDO: Método de seguimiento siempre activo
    private void SiguePlayer(bool playerEnRango)
    {
        if (playerEnRango)
        {
            agente.SetDestination(personaje.position);
            mirarHacia = personaje;
        }
        else if (puntoRuta.Length > 0) // ✅ Solo si hay puntos de ruta
        {
            agente.SetDestination(puntoRuta[indiceRuta].position);
            mirarHacia = puntoRuta[indiceRuta];
        }
    }

    private void RotaEnemigo()
    {
        if (mirarHacia != null)
        {
            if (this.transform.position.x > mirarHacia.position.x)
            {
                spriteEnemigo.flipX = true;
            }
            else
            {
                spriteEnemigo.flipX = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            tiempoSigAtaque = freAtaque;
            iniciaConteo = Time.time;
            obj.transform.GetComponentInChildren<VidasPlayer>().TomarDaño(1);
        }
    }

    public void TomarDaño(int daño)
    {
        vidaEnemigo -= daño;
        if (vidaEnemigo <= 0)
        {
            Destroy(gameObject);
        }
    }
}