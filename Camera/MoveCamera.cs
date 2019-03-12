using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] float C_BoundXPos;
    [SerializeField] float C_BoundXNeg;
    [SerializeField] float C_BoundYPos;
    [SerializeField] float C_BoundYNeg;
    [SerializeField] GameObject C_Pl;
    [SerializeField] bool IsVertical;
    void Start()
    {
    }

    void LateUpdate()
    {
            MoveCameraHorizonal();
            MoveCameraVertical();
    }

    void MoveCameraHorizonal()
    {
        if (C_Pl.transform.position.x < C_BoundXPos && C_Pl.transform.position.x > C_BoundXNeg)
        {
            gameObject.transform.position = new Vector3(C_Pl.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }

    void MoveCameraVertical()
    {
        if (C_Pl.transform.position.y < C_BoundYPos && C_Pl.transform.position.y > C_BoundYNeg)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, C_Pl.transform.position.y, gameObject.transform.position.z);
        }
    }
}
