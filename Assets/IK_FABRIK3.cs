using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IK_FABRIK3 : MonoBehaviour
{


    struct myTransform
    {
        public MyVec pos;
        public MyQuat rotation;
    }


    public Transform[] joints;
    public Transform target;

    private MyVec[] copy;
    private float[] distances;
    private bool done;

    float treshold_condition = 0.001f;

    void Start()
    {
        distances = new float[joints.Length - 1];
        copy = new MyVec[joints.Length];
    }

    void Update()
    {
        // Copy the joints positions to work with
        for (int i = 0; i < joints.Length; i++)
        {
            copy[i] = new MyVec(joints[i].position.x, joints[i].position.y, joints[i].position.z); //Copy the joints
            if (i < joints.Length - 1)
            {
                distances[i] = MyVec.Distance(joints[i + 1].position, joints[i].position); //Calculate the distances
            }
        }

        done = (copy[copy.Length - 1] - new MyVec(target.position.x, target.position.y, target.position.z)).magnitude < treshold_condition;

        if (!done)
        {
            float targetRootDist = MyVec.Distance(copy[0], new MyVec(target.position.x, target.position.y, target.position.z));

            // Update joint positions
            if (targetRootDist > distances.Sum())
            {
                // The target is unreachable
                for (int i = 0; i < copy.Length - 1; i++)
                {
                    float r = (new MyVec(target.position.x, target.position.y, target.position.z) - copy[i]).magnitude;
                    float lambda = distances[i] / r;
                    copy[i + 1] = copy[i] * (1 - lambda) + new MyVec(target.position.x, target.position.y, target.position.z) * lambda;
                }
            }
            else
            {
                MyVec b = copy[0];
                float difA = (copy[copy.Length - 1] - new MyVec(target.position.x, target.position.y, target.position.z)).magnitude;

                // The target is reachable
                while (difA > treshold_condition)
                {
                    // STAGE 1: FORWARD REACHING
                    copy[copy.Length - 1] = new MyVec(target.position.x, target.position.y, target.position.z);
                    for (int i = copy.Length - 2; i > 0; i--)
                    {
                        float r = (copy[i + 1] - copy[i]).magnitude;
                        float lambda = distances[i] / r;
                        copy[i] = copy[i + 1] * (1 - lambda) + copy[i] * lambda;
                    }

                    // STAGE 2: BACKWARD REACHING
                    copy[0] = b;
                    for (int i = 0; i < copy.Length - 1; i++)
                    {
                        float r = (copy[i + 1] - copy[i]).magnitude;
                        float lambda = distances[i] / r;
                        copy[i + 1] = copy[i] * (1 - lambda) + copy[i + 1] * lambda;
                    }

                    difA = (copy[copy.Length - 1] - new MyVec(target.position.x, target.position.y, target.position.z)).magnitude;
                }
            }



            // Update original joint rotations
            for (int i = 0; i < joints.Length - 1; i++)
            {
                //TODO 
                MyVec A = new MyVec(joints[i + 1].position - joints[i].position);
                MyVec B = copy[i + 1] - copy[i];

                float cosa = MyVec.Dot(MyVec.Normalize(A), MyVec.Normalize(B));
                float sina = MyVec.Cross(MyVec.Normalize(A), MyVec.Normalize(B)).magnitude;

                float alpha = Mathf.Atan2(sina, cosa) * Mathf.Rad2Deg;

                MyVec myAxis = MyVec.Normalize(MyVec.Cross(A, B));
                //Vector3 axis = new Vector3(myAxis.x, myAxis.y, myAxis.z);

                MyQuat myQuat = MyQuat.AngleAxis(alpha, ref myAxis);

                Quaternion quat = new Quaternion(myQuat.x, myQuat.y, myQuat.z, myQuat.w);
                
                
                joints[i].rotation = quat * joints[i].rotation;
                //joints[i].rotation = Quaternion.AngleAxis(alpha, axis) * joints[i].rotation;
                joints[i + 1].position = new Vector3(copy[i + 1].x, copy[i + 1].y, copy[i + 1].z);
                if(i == 2)
                {
                  //  print(joints[i].rotation.z);
                }
               
                if ((joints[i].rotation.z > 0.5f || joints[i].rotation.z < -0.5f) && i >0)
                {
                    joints[i].rotation = joints[i - 1].rotation;
                    //joints[i].rotation = new Quaternion(joints[i].rotation.x, joints[i].rotation.y, , joints[i - 1].rotation.w);
                }

            }
        }
    }

}