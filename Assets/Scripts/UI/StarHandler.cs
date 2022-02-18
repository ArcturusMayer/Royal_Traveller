using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarHandler : MonoBehaviour
{
    public Sprite dark;
    public Sprite glow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setStarMode(bool isPassed)
    {
        if (isPassed)
        {
            gameObject.GetComponent<Image>().sprite = glow;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = dark;
        }
    }
}
