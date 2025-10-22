using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidasPlayer : MonoBehaviour
{
    public Image vidaPlayer;
    private float anchoVidaPlayer;
    public static int vida;
    private bool haMuerto;
    public GameObject gameOver;
    private const int vidasINI = 5;
    public static int perderVida = 1; //Marca cada cuanto puede perder vida el jugador

    // Start is called before the first frame update
    void Start()
    {
        anchoVidaPlayer = vidaPlayer.GetComponent<RectTransform>().sizeDelta.x;
        haMuerto = false;
        vida = vidasINI;
        gameOver.SetActive(false);
    }
    public void TomarDaño(int daño)
    {
        if (vida > 0 && perderVida == 1)
        {
            perderVida = 0;
            vida-= daño;
            DibujaVida(vida);
        }
        if (vida <= 0 && !haMuerto) {
            haMuerto = true;
            StartCoroutine(EjecutaMuerte());
        }
    }

    private void DibujaVida(int vida)
    {
        RectTransform transformImagen = vidaPlayer.GetComponent<RectTransform>();//Modifica el ancho de las imageners
        transformImagen.sizeDelta = new Vector2(anchoVidaPlayer * (float)vida / (float)vidasINI, transformImagen.sizeDelta.y);
    }

    IEnumerator EjecutaMuerte()
    {
        yield return new WaitForSeconds(1.2f);
        gameOver.SetActive(true);
    }

}
