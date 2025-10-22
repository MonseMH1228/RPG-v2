using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class movCamara : MonoBehaviour
{
    public Camera camara;

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "portal1")
        {
            Vector3 posicioncamara = new Vector3(56.1f, 85.8f, -10);
            camara.transform.position = posicioncamara;
            Vector3 posicionPlayer = new Vector3(55.9f, 82.1f, 0);
            this.transform.position = posicionPlayer;
        }

        if (obj.gameObject.tag == "portal1r")
        {
            Vector3 posicioncamara = new Vector3(1.8f, 30.3f, -10);
            camara.transform.position = posicioncamara;
            Vector3 posicionPlayer = new Vector3(1.5f, 31.7f, 0);
            this.transform.position = posicionPlayer;
        }
        // if(obj.gameObject.tag == "portal1")
        // {
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        // }

        if (obj.gameObject.tag == "portal2")
        {
            Vector3 posicioncamara = new Vector3(-68.5f, 80.1f, -10);
            camara.transform.position = posicioncamara;
            Vector3 posicionPlayer = new Vector3(-68.6f, 72.2f, 0);
            this.transform.position = posicionPlayer;
        }

        if (obj.gameObject.tag == "portal2r")
        {
            Vector3 posicioncamara = new Vector3 (53.1f, 106.4f, -10);
            camara.transform.position = posicioncamara;
            Vector3 posicionPlayer = new Vector3(53.6f, 110.2F, 0);
            this.transform.position = posicionPlayer;
        }

    }
}
