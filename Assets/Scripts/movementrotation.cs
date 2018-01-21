using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movementrotation : MonoBehaviour {
    public MyVec move;
    public float x, y, z;
    public MyQuat init;
    MyQuat final;
    MyQuat rot;
    MyVec angularD;

    public MyVec drag = new MyVec(0, 0, 0);
    public float gravity;
    public MyVec magnuss = new MyVec(0, 0, 0);

    public Text text;

    float mass = 0.1f;

    public float MagnusX;

    public float DragX, DragY, DragZ;

    public float angularY;

    public KeyManager keys;

    float cross;
    public MyVec acc;
    public MyVec velocity;
    public MyVec positions;
    [SerializeField]
    private float proportionalCoef;
    [SerializeField]
    private float magnussCoef;
    [SerializeField]
    private float airDens;
    [SerializeField]
    private float radius;
    [SerializeField]
    private float crossSectionalArea;
    public Vector3 uolo;
    struct perdigo
    {
        public MyVec pos;
        public MyVec vel;
        public MyVec angularV;
        public MyVec forces;
        public MyQuat rotation;
        public float mass;
        public float radi;
    }

    perdigo perd = new perdigo();
    // Use this for initialization
    void Start () {

        Time.timeScale = 0.01f;

        magnussCoef = 0.15f;
        airDens = 1;
        radius = 0.5f;
        proportionalCoef = 0.15f;
        crossSectionalArea = (Mathf.Pow(radius, 2)) * Mathf.PI;

        keys = GameObject.Find("InputManager").GetComponent<KeyManager>();
        perd.pos = new MyVec(transform.position.x, transform.position.y, transform.position.z);
        perd.vel = new MyVec(0, 0, 20) + keys.vent;
        perd.angularV = new MyVec(0, angularY, 0);
        uolo = new Vector3( perd.angularV.x,perd.angularV.y,perd.angularV.z);
        gravity = 9.8f * mass;
        //perd.forces = drag + gravity + magnuss;
        move = new MyVec(transform.position.x, transform.position.y, transform.position.z);
        init = new MyQuat(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);

    }

    // Update is called once per frame
    void Update () {

        //acceleration & forces
        perd.vel = new MyVec(magnuss.x, -gravity, perd.vel.z);
        velocity = perd.vel;

        magnuss = ((MyVec.Cross(perd.angularV, perd.vel)) * magnussCoef * airDens * crossSectionalArea * radius) * 0.5f;

        drag = (MyVec.Scale(perd.vel, perd.vel) * proportionalCoef * crossSectionalArea * airDens) * -0.5f;

        acc = (magnuss + drag + new MyVec(0,-gravity*mass,0))/mass;

        MagnusX = magnuss.x;

        DragX = drag.x;
        DragY = drag.y;
        DragZ = drag.z;

        perd.pos.x += (magnuss.x - drag.x) * Time.deltaTime;

        perd.pos.y -= (gravity - drag.y) * (Time.deltaTime);

        perd.pos.z += (perd.vel.z - drag.z) * Time.deltaTime;

        positions = perd.pos;

        //rotation
        float angle = perd.angularV.magnitude*Time.deltaTime;
        angularD = perd.angularV;
        rot = new MyQuat(angularD.x * Mathf.Sin(angle/2), angularD.y  * Mathf.Sin(angle / 2), angularD.z * Mathf.Sin(angle / 2), Mathf.Cos(angle/2));
        final = rot *new MyQuat(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        transform.rotation = new Quaternion(final.x, final.y, final.z, final.w);
        transform.position = new Vector3(perd.pos.x, perd.pos.y, perd.pos.z);
    }
}
