using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private int lifes;  //numero de vidas
    private int score;  //pontuacao

    public GameObject letter;   //prefab das letras
    public GameObject center; //referencia centro da tela

    private string wordHide = ""; // palavra a ser descoberta
    private int wordHideSize; //tamanho da palavra a ser descoberta
    char [] letterHide; // letras da palavra a ser descoberta
    bool [] letterShow; // indicador letras conhecidas

    // Start is called before the first frame update
    void Start()
    {
        center = GameObject.Find("centerScreen");
        InitGame();
        InitLetter();
        lifes = 5;
        UpdateLifes();
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        checkKeyboard();
    }

    //Carrega as letras de acordo com a palavra sorteada
    void InitLetter(){
        int numLetters = wordHideSize;
        for (int i=0;i<numLetters;i++){
            Vector3 newPosition;
            newPosition = new Vector3(center.transform.position.x + ((i-numLetters/2.0f) * 80),center.transform.position.y, center.transform.position.z);
            GameObject l = (GameObject) Instantiate(letter, newPosition,Quaternion.identity);
            l.name = "letter" + (i +1);
            l.transform.SetParent(GameObject.Find("Canvas").transform);

        }
    }

    void InitGame(){
        wordHide = GetWord(); // definir palavra a ser descoberta
        wordHideSize = wordHide.Length; //salvar tamanho da palavra oculta
        wordHide = wordHide.ToUpper(); //transformar palavra em maiúscula
        letterHide = new char [wordHideSize]; // iniciar array 
        letterShow = new bool [wordHideSize]; // iniciar array
        letterHide = wordHide.ToCharArray(); // atribuir valores ao array
    }


    void checkKeyboard()
    {
        //verifica se uma tecla é digitada
        if (Input.anyKeyDown){
            char letterKey = Input.inputString.ToCharArray()[0];
            int letterKeyToInt = System.Convert.ToInt32(letterKey);
            //Verifica se a tecla digitada e uma letra
            if (letterKeyToInt >=97 && letterKeyToInt<=122){
                bool condition = false; // variavel que armazena informacao se a letra digitada estava ou nao na palavra oculta
                for (int i=0; i<wordHideSize; i++)
                {
                    if(!letterShow[i])
                    {
                        letterKey = System.Char.ToUpper(letterKey);
                        //verifica se a letra digitada existe na palavra oculta
                        if (letterHide[i] == letterKey)
                        {
                            condition = true;
                            letterShow[i] = true;
                            GameObject.Find("letter" + (i+1)).GetComponent<Text>().text = letterKey.ToString();
                            score = PlayerPrefs.GetInt("score");
                            score++;
                            PlayerPrefs.SetInt("score", score);
                            UpdateScore();
                            CheckWin();
                        }
                    }
                }
                //se o player nao acertou uma letra desconta uma vida
                if (!condition)
                {
                    lifes--;
                    UpdateLifes();
                }
                //se as vidas acabarem exibe a cena de game over
                if (lifes == 0)
                {
                    SceneManager.LoadScene("Lab1_gameover");
                }
            }
        }
    }
    //Atualiza o texto da quantidade de vidas do player
    void UpdateLifes()
    {
        GameObject.Find("lifeUI").GetComponent<Text>().text = "VIDAS: " +lifes;
    }
    //Atualiza o texto do score do player
    void UpdateScore()
    {
        GameObject.Find("scoreUI").GetComponent<Text>().text = "Score: " + score;
    }

    //Verifica se todas as letras foram descobertas
    void CheckWin()
    {
        bool condition = true;
        for (int i=0; i< wordHideSize; i++)
        {
            condition = condition && letterShow[i];
        }
        if (condition)
        {
            PlayerPrefs.SetString("lastWord", wordHide);
            SceneManager.LoadScene("Lab1_congrats");
        }
    }

    //sorteia uma palavra entre as palavras encontradas no arquivo words
    string GetWord()
    {
        TextAsset t1 = (TextAsset)Resources.Load("words", typeof(TextAsset));
        string s = t1.text;
        string[] words = s.Split(' ');
        int randomWord = Random.Range(0, words.Length);
        return (words[randomWord]);
    }
}
