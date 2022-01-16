using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;

    void LateUpdate()
    {
        if (target != null)
            transform.position = Vector3.Lerp(this.transform.position, new Vector3(target.position.x, this.transform.position.y, target.position.z), 2f);
    }
    public void SwitchTarget(Transform newTarget) { target = newTarget; }
}
