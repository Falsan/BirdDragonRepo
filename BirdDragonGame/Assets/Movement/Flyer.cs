using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonoBehaviour {

    float speed;
    Rigidbody rigidBody;
    float increaseSpeedBy;
    float decreaseSpeedBy;
    float maxSpeed;
    float maxReverse;
    float rotationSpeed;
    float mouseRotationSpeed;
    GameObject rotationVector;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        increaseSpeedBy = 2.0f;
        decreaseSpeedBy = -2.0f;
        maxReverse = -10.0f;
        maxSpeed = 18.0f;
        rotationSpeed = 8.0f;
        mouseRotationSpeed = 2.0f;
    }

    // Update is called once per frame
    void Update() {

        if ((Input.mouseScrollDelta.y > 0 || Input.mouseScrollDelta.y < 0) || (Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Joystick1Button4)))
        {

            int mouseScroll = 0;

            if (Input.mouseScrollDelta.y > 0 || Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                mouseScroll = 1;
            }
            else if (Input.mouseScrollDelta.y < 0 || Input.GetKeyDown(KeyCode.Joystick1Button4))
            {
                mouseScroll = -1;
            }

            if (speed > maxSpeed && mouseScroll > 0)
            {
               
            }
            else if (speed < maxReverse && mouseScroll < 0)
            {
                
            }
            else if (mouseScroll < 0)
            {
                ModifyForwardSpeed(decreaseSpeedBy);
            }
            else if (mouseScroll > 0)
            {
                ModifyForwardSpeed(increaseSpeedBy);
            }

           // rigidBody.AddRelativeForce(0.0f, 0.0f, speed, ForceMode.Force);
        }

        rigidBody.AddRelativeForce(0.0f, 0.0f, speed);

        if (Input.GetMouseButton(0))
        {
            AngleTowardsMousePointer();
        }
        if((Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Pitch") > 1) || (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") < 0 || Input.GetAxis("Pitch") < 1))
        {
            AngleTowardsAxisDirection();
        }
        if (Input.GetAxis("Pitch") > 0)
        {
            Debug.Log(Input.GetAxis("Pitch"));
        }

    }

    void AngleTowardsAxisDirection()
    {

     if (transform.GetChild(0).transform.rotation.y > 20.0f)
     {
        Quaternion fixedRotation = new Quaternion(transform.GetChild(0).transform.rotation.x, 20.0f, transform.GetChild(0).transform.rotation.z, transform.GetChild(0).transform.rotation.w);

        transform.GetChild(0).transform.SetPositionAndRotation(transform.GetChild(0).transform.position, fixedRotation);
     }
     Vector3 rotationDirection = new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

     transform.GetChild(0).transform.RotateAround(gameObject.transform.position, rotationDirection, 10 * Time.deltaTime);

     Quaternion targetRotation = Quaternion.LookRotation(transform.GetChild(0).transform.position - transform.position);

     transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }

    void AngleTowardsMousePointer()
    {

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);

        Quaternion targetRotation = Quaternion.LookRotation(mouseWorldPosition - transform.position);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, mouseRotationSpeed * Time.deltaTime);

    }


    void ModifyForwardSpeed(float mouseScroll)
    {
        speed += mouseScroll;
        Debug.Log(speed);
    }
}
