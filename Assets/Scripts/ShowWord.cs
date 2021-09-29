using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowWord : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //exibe a palavra descoberta
        GetComponent<Text>().text = PlayerPrefs.GetString("lastWord");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
