using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class FumoCount : MonoBehaviour
{
    public int fumocount2;

    public Text fumotext2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fumotext2.text = fumocount2.ToString();
        print(fumocount2);

    }
}
