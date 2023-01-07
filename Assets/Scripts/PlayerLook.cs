using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public void FixedUpdate()
    {
        HandleLookAt();
    }

    Plane backPlane = new Plane(new Vector3(0, 0, -1), new Vector3(0, 0, 0));
    private void HandleLookAt()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, 1 << 31))
        {
            var lookPosition = hit.point;
            lookPosition.y = transform.position.y;
            var lookDirection = (lookPosition - transform.position).normalized;
            var lookRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = lookRotation;
        }
    }
}
