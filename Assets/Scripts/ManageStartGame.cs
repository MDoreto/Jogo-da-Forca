using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManageStartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("score",0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //abre a cena principal do jogo
    public void StartWorldGame()
    {
        SceneManager.LoadScene("Lab1");
    }
}
