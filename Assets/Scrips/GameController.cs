using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameController : MonoBehaviour
{

    public GameObject enemy;

    //Interfaz, derrota y victoria
    public Text gamedefeat;
    public Text gamevictory;
    public Text Score;
    public bool lose;
    public int scr;

    //Movimiento y camara
    public GameObject player;
    public new Camera camera;

    void Start()
    {
        scr = 40;
    }

    void Update()
    {
        if (scr == 0)
        {
            Victory();
        }

        if (player == null)
        {
            Defeat();
            /*if (!lose)
            {
                lose = true;
                gamedefeat.text = "Game Over\nPress R to restart or E to exit";
            }
            if (lose)
            {
                if (Input.GetKeyDown("r"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }

                if (Input.GetKeyDown("e"))
                {
                    SceneManager.LoadScene("MainMenu");
                }
            }*/
        }
        else
        {
            camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, camera.transform.position.z);
        }
    }

    public void AumentoScore()
    {
        scr -= 1;
        Score.text = "Enemies remaining: " + scr;
    }

    public void Victory()
    {
        gamevictory.text = "Victory";
        StartCoroutine(Defeatcrono(6f));
    }

    public void Defeat()
    {
        gamedefeat.text = "Game Over";
        StartCoroutine(Defeatcrono(6f));
    }

    public IEnumerator Defeatcrono(float valcrono)
    {
        yield return new WaitForSeconds(valcrono);
        SceneManager.LoadScene("Main Menú");
    }

}
