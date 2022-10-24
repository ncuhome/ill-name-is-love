using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveNum : MonoBehaviour
{
    public TextMeshProUGUI Text;

    void Start()
    {
        Text = transform.GetComponent<TextMeshProUGUI>();
        // TxtCurrentTime = GetComponent<TextMeshPro> ();
    }
   

    // Update is called once per frame
    void Update()
    {   
        Text.text = Move.num.ToString();
    }
}
