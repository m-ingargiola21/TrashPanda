using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    bool flip;
    void Update()
    {
        if (!flip)
            transform.LookAt(Camera.main.transform.position, Vector3.up);
        else
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);

    }
}
