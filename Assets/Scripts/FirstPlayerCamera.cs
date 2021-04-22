// ************************************************ Flocking Behavior Project built for bachelor graduation ***************************//
//***************************************************** Main Authur for this Scripts is Mahdi Bentaleb ***************************
//****************************************************** Co-workers Tina Abdelaziz and Zendagui Rafik **************************
//************************************************************ supervisor Dr. Mebarek Bouceta **************************************

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayerCamera : MonoBehaviour
{
    // this variable is used to control the mouseSensitivity
    [Range(1, 1000)]
    public float mouseSensitivity;

    // this variable is used to control the moveSensitivity
    [Range(1, 7)]
    public float moveSensitivity;

    // this variables are used to control the rotation in the Axis X , Y
    private float xRotation = 0;
    private float yRotation = 0;


    // Start is called before the first frame update
    void Start()
    {
        // this code line is called to lock the cursor from appearing in our scene
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called every fixed frame - check https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html for more details
    void FixedUpdate()
    {

        float h = Input.GetAxis("Horizontal") * moveSensitivity * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * moveSensitivity * Time.deltaTime;

        if (v != 0 || h != 0)
        {
            Vector3 vector3 = new Vector3(h, 0, v);
            transform.Translate(vector3);
        }

        float x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += x;
        
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
