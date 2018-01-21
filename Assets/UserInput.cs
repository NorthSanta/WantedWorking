using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    Vector3 v;
    float ZPos;
    float count;
    MyVec firstPos;
    MyVec lastPos;
    public GameObject perdigo;
    bool isdown;
    public Transform init;
    public List<GameObject> bullets;
    public KeyManager keys;
    // Use this for initialization
    void Start()
    {
        ZPos = 0.8f;
        count = 0;
        keys = GameObject.Find("InputManager").GetComponent<KeyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            for(int i = 0; i < bullets.Count; i++)
            {
               Destroy(bullets[i]);
            }
        }

        v = Input.mousePosition;
        v.z = ZPos;
        if (Input.GetKey(KeyCode.W))
            ZPos += 0.05f;
        else if (Input.GetKey(KeyCode.S))
            ZPos -= 0.05f;
        v = Camera.main.ScreenToWorldPoint(v);
        transform.position = v;
        if (isdown)
        {
            count += Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0))
        {
            isdown = true;   
            firstPos = new MyVec(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            //print(firstPos.x);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isdown = false;
            //count += Time.deltaTime;
           
            lastPos = new MyVec(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            //print(lastPos.x);
            MyVec finalPos = (lastPos - firstPos);
            finalPos.Normalize();
            float vel = finalPos.magnitude / count;
            if (GameObject.Find("perdigo") == null)
            {
                GameObject nou = Instantiate(perdigo, init.position, init.rotation);
                vel = -Input.GetAxis("Mouse X") * 10f;
                nou.GetComponent<movementrotation>().angularY =vel;
                bullets.Add(nou);
              
                   
                
            }
            //print(vel);
           
        }
        //MyVec v =new MyVec( Input.mousePosition.x, Input.mousePosition.y, -9.15f);
    }
}
