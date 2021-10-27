using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapCamera : MonoBehaviour
{
    public Transform mainCar;

    void LateUpdate() {
        Vector3 newPosition = mainCar.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, mainCar.eulerAngles.y, 0f);
    }
}
