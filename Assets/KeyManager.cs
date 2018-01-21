using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour {
    public bool rClicked;
    public bool gClicked;
    public bool bClicked;

    public bool paused;

    public Text text;
    public MyVec vent;
    // Use this for initialization
    void Start () {
        vent = new MyVec(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        text.text = "Wind Velocity: X(" + vent.x + ")," + "Y(" + vent.y + ");" + "Z(" + vent.z + ")";

    }
	
	// Update is called once per frame
	void Update () {
       
        if (Input.GetKeyDown(KeyCode.R))
        {
            rClicked = !rClicked;
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            gClicked = !gClicked;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            bClicked = !bClicked;
        }else if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }else if (Input.GetKeyDown(KeyCode.Space))
        {
            vent = new MyVec(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
            text.text = "Wind Velocity: X(" + vent.x + ")," + "Y(" + vent.y + ");" + "Z(" + vent.z + ")";
        }
        if (paused)
        {
            Time.timeScale = 0;
        }else
        {
            Time.timeScale = 0.01f;
        }
    }
}
