using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Life : MonoBehaviour { 

    private int hp;
    public TextMeshProUGUI lifeText;

    // Start is called before the first frame update
    void Start()
    {
        hp = 10;
        setLifeText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void lifeMinusOne()
    {
        if(hp > 0) {
           hp -= 1;
        }
        
    }

    void setLifeText()
    {
        lifeText.text = "HP: " + hp.ToString();
    }
}
