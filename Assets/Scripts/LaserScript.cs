using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour
{
    public LineRenderer laserLineRenderer;
    public float laserWidth = 0.1f;
    public float laserMaxLength = 5f;
    public movementrotation move;
    public Vector3 dirMagnuss;
    //public Vector3 dirMagnuss;
    public Material forces;
    public Material velocities;
    public Material positions;
    public int type;

    public KeyManager keymanager;

  

    void Start()
    {
        keymanager = GameObject.Find("InputManager").GetComponent<KeyManager>();
        Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        laserLineRenderer.SetPositions(initLaserPositions);
        laserLineRenderer.SetWidth(laserWidth, laserWidth);
    }

    void Update()
    {
       
        switch (type)
        {
            case 0:
                if (keymanager.rClicked)
                {
                    laserLineRenderer.enabled = true;
                    laserLineRenderer.material = forces;
                    dirMagnuss = new Vector3(move.magnuss.x, move.magnuss.y, move.magnuss.z);
                    ShootLaserFromTargetPosition(transform.position, dirMagnuss, dirMagnuss.magnitude);
                }
                else
                {
                    laserLineRenderer.enabled = false;
                }
                break;
            case 1:
                if (keymanager.gClicked)
                {
                    laserLineRenderer.enabled = true;
                    laserLineRenderer.material = velocities;
                    dirMagnuss = new Vector3(move.velocity.x, move.velocity.y, move.velocity.z);
                    ShootLaserFromTargetPosition(transform.position, dirMagnuss, dirMagnuss.magnitude);
                }
                else
                {
                    laserLineRenderer.enabled = false;
                }
                break;
            case 2:
                if (keymanager.bClicked)
                {
                    laserLineRenderer.enabled = true;
                    laserLineRenderer.material = positions;
                    // dirMagnuss = transform.forward.normalized;
                    ShootLaserFromTargetPosition(transform.position, transform.forward, transform.forward.magnitude);
                }
                else
                {
                    laserLineRenderer.enabled = false;
                }
                break;
            case 3:
                if (keymanager.bClicked)
                {
                    laserLineRenderer.enabled = true;
                    laserLineRenderer.material = positions;
                    ShootLaserFromTargetPosition(transform.position, transform.up, transform.up.magnitude);
                }
                else
                {
                    laserLineRenderer.enabled = false;
                }
                break;
            case 4:
                if (keymanager.bClicked)
                {
                    laserLineRenderer.enabled = true;
                    laserLineRenderer.material = positions;
                    ShootLaserFromTargetPosition(transform.position, transform.right, transform.right.magnitude);
                }
                else
                {
                    laserLineRenderer.enabled = false;
                }
                break;
            case 5:
                if (keymanager.rClicked)
                {
                    laserLineRenderer.enabled = true;
                    laserLineRenderer.material = forces;
                    dirMagnuss = new Vector3(0, -move.gravity, 0).normalized;
                    ShootLaserFromTargetPosition(transform.position, dirMagnuss, dirMagnuss.magnitude);
                }
                else
                {
                    laserLineRenderer.enabled = false;
                }
                break;
            case 6:
                if (keymanager.rClicked)
                {
                    laserLineRenderer.enabled = true;
                    laserLineRenderer.material = forces;
                    dirMagnuss = new Vector3(-move.velocity.x, move.drag.y, move.drag.z);
                    ShootLaserFromTargetPosition(transform.position, dirMagnuss, dirMagnuss.magnitude);
                }
                else
                {
                    laserLineRenderer.enabled = false;
                }
                break;
            case 7:
                if (keymanager.rClicked)
                {
                    laserLineRenderer.enabled = true;
                    laserLineRenderer.material = forces;
                    dirMagnuss = new Vector3(move.acc.x, move.acc.y, move.acc.z);
                    ShootLaserFromTargetPosition(transform.position, dirMagnuss, dirMagnuss.magnitude);
                }
                else
                {
                    laserLineRenderer.enabled = false;
                }
                break;

        }
        
            //ShootLaserFromTargetPosition(transform.position, transform.forward, laserMaxLength, 1);
        //ShootLaserFromTargetPosition(transform.position, dirMagnuss, laserMaxLength,3);
        //ShootLaserFromTargetPosition(transform.position, dirMagnuss, laserMaxLength,3);
        //velocities
        //ShootLaserFromTargetPosition(transform.position, dirMagnuss, laserMaxLength, 0);
        //ShootLaserFromTargetPosition(transform.position, dirMagnuss, laserMaxLength, 3);
        //ShootLaserFromTargetPosition(transform.position, dirMagnuss, laserMaxLength, 3);
        //positions
        //ShootLaserFromTargetPosition(transform.position, dirMagnuss, laserMaxLength, 0);
        //ShootLaserFromTargetPosition(transform.position, dirMagnuss, laserMaxLength, 3);
        //ShootLaserFromTargetPosition(transform.position, dirMagnuss, laserMaxLength, 0);
    }

    void ShootLaserFromTargetPosition(Vector3 targetPosition, Vector3 direction, float length)
    {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);

        if (Physics.Raycast(ray, out raycastHit, length))
        {
            endPosition = raycastHit.point;
        }

        laserLineRenderer.SetPosition(0, targetPosition);
        laserLineRenderer.SetPosition(1, endPosition);
        
       
    }
}