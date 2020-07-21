using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyPickup : MonoBehaviour
{
    public static int crystallCount;
    private Text crystallConter;
    // Start is called before the first frame update
    void Start()
    {
        crystallConter = GetComponent<Text>();
        crystallCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        crystallConter.text = "X" + crystallCount;
    }
}
