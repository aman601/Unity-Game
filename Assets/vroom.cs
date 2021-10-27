using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vroom : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float m_Thrust = 25f;
    Vector3 currentVelocity;
    public bool grounded = true;
    public bool q;
    public bool e;
    RaycastHit hit;

    public bool switchG;
    public bool ramping;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void OnTriggerExit(Collider other) {
        if (other.tag == "ramp") {
            ramping = false;
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.tag == "ramp") {
            ramping = true;
        }
    }

    void Update() {
        //Debug.Log(car.transform.position.ToString());
        if (Input.GetKeyDown("q")) {
            if (grounded) {
                q = true;
            }
        }
        if (Input.GetKeyDown("e")) {
            if (grounded) {
                e = true;
            }
        }
        //if (Input.GetKey("a")) {
            //Vector3 currentRotation = transform.eulerAngles;
            //currentRotation.y = Mathf.Lerp(currentRotation.y, currentRotation.y - 10f, Time.fixedDeltaTime);
            //transform.eulerAngles = currentRotation;
        //}
    }
    void FixedUpdate()
    {

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), (float).2)
        && !(ramping)) {
            grounded = true;
            m_Rigidbody.transform.position = new Vector3(m_Rigidbody.transform.position.x, (float)-.85, m_Rigidbody.transform.position.z);
            m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        } else {
            grounded = false;
            m_Rigidbody.constraints = RigidbodyConstraints.None;
        }






        if (Input.GetKey("w")) {
            if (grounded) {
                m_Rigidbody.AddForce(transform.forward * m_Thrust);
                //Debug.Log("FORWARD!");
            } else {
                m_Rigidbody.transform.Rotate(3f, 0f, 0f, Space.Self);
            }
        }
        if (Input.GetKey("s")) {
            if (grounded) {
                m_Rigidbody.AddForce(transform.forward * m_Thrust * -1);
                //Debug.Log("FORWARD!");
            } else {
                m_Rigidbody.transform.Rotate(-3f, 0f, 0f, Space.Self);
            }
        }
        if (Input.GetKey("d")) {
            if (true) {
                m_Rigidbody.transform.Rotate(0f, 1f + (m_Rigidbody.velocity.y/300), 0f, Space.World);
                /*
                Vector3 desiredDirection = transform.forward; // set this to the direction you want.
                currentVelocity = m_Rigidbody.velocity;
                Vector3 newVelocity = desiredDirection.normalized * currentVelocity.magnitude;
                //Debug.Log(currentVelocity);
                //Debug.Log(newVelocity);

                //THIS IS AN ISSUE, CAN SPAM SPEED IN THE AIR
                m_Rigidbody.velocity = new Vector3(newVelocity.x, currentVelocity.y, newVelocity.z);*/
                if (!Input.GetKey("left shift")) {
                    Vector3 desiredDirection = transform.forward; // set this to the direction you want.
                    //Debug.Log(m_Rigidbody.velocity);
                    currentVelocity = m_Rigidbody.velocity;
                    float currentVelocityx = m_Rigidbody.velocity.x;
                    float currentVelocityz = m_Rigidbody.velocity.z;
                    Vector3 goodVelocity = new Vector3(currentVelocityx,0,currentVelocityz);
                    //Debug.Log(goodVelocity);
                    Vector3 newVelocity = desiredDirection.normalized * goodVelocity.magnitude;
                    //Debug.Log(newVelocity);
                    m_Rigidbody.velocity = new Vector3(newVelocity.x, currentVelocity.y, newVelocity.z);
                    //Debug.Log(m_Rigidbody.velocity);
                }
            } //else {
              //  m_Rigidbody.transform.Rotate(0f, 0f, 3f, Space.World);
            //}
        }
        if (Input.GetKey("a")) {
            if (true) {
                //Vector3 currentRotation = transform.eulerAngles;
                //currentRotation.y = Mathf.Lerp(currentRotation.y, currentRotation.y - 1000f, Time.deltaTime);
                //transform.eulerAngles = currentRotation;

                m_Rigidbody.transform.Rotate(0f, -1f + (m_Rigidbody.velocity.y/300), 0f, Space.World);

                //transform.rotation = Quaternion.Slerp(Quaternion.AngleAxis(-3 + transform.eulerAngles.y, Vector3.up), Quaternion.LookRotation(transform.position), Time.deltaTime);
                //transform.rotation = Quaternion.Euler(new Vector3(0f, transform.rotation.eulerAngles.y, 0f));
                /*Vector3 desiredDirection = transform.forward; // set this to the direction you want.
                currentVelocity = m_Rigidbody.velocity;
                Vector3 newVelocity = desiredDirection.normalized * currentVelocity.magnitude;
                //Debug.Log(currentVelocity);
                //Debug.Log(newVelocity);
                m_Rigidbody.velocity = new Vector3(newVelocity.x, currentVelocity.y, newVelocity.z);*/
                if (!Input.GetKey("left shift")) {
                    Vector3 desiredDirection = transform.forward; // set this to the direction you want.
                    //Debug.Log(m_Rigidbody.velocity);
                    currentVelocity = m_Rigidbody.velocity;
                    float currentVelocityx = m_Rigidbody.velocity.x;
                    float currentVelocityz = m_Rigidbody.velocity.z;
                    Vector3 goodVelocity = new Vector3(currentVelocityx,0,currentVelocityz);
                    //Debug.Log(goodVelocity);
                    Vector3 newVelocity = desiredDirection.normalized * goodVelocity.magnitude;
                    //Debug.Log(newVelocity);
                    m_Rigidbody.velocity = new Vector3(newVelocity.x, currentVelocity.y, newVelocity.z);
                    //Debug.Log(m_Rigidbody.velocity);
                }
            } //else {
                //m_Rigidbody.transform.Rotate(0f, 0f, -3f, Space.World);
            //}
        }
        if (Input.GetKey("q")) {
            if (!grounded) {
                m_Rigidbody.transform.Rotate(0f, 0f, -3f, Space.Self);
            }
        }
        if (Input.GetKey("e")) {
            if (!grounded) {
                m_Rigidbody.transform.Rotate(0f, 0f, 3f, Space.Self);
            }
        }
        if (q) {
            if (grounded) {
                m_Rigidbody.transform.Rotate(0f, -90f, 0f, Space.World);
                Vector3 desiredDirection = transform.forward; // set this to the direction you want.
                Vector3 currentVelocity = m_Rigidbody.velocity;
                Vector3 newVelocity = desiredDirection.normalized * currentVelocity.magnitude;
                //Debug.Log(currentVelocity);
                //Debug.Log(newVelocity);
                
                m_Rigidbody.velocity = new Vector3(newVelocity.x, currentVelocity.y, newVelocity.z);
                q = false;
            }
        }
        if (e) {
            if (grounded) {
                m_Rigidbody.transform.Rotate(0f, 90f, 0f, Space.World);
                Vector3 desiredDirection = transform.forward; // set this to the direction you want.
                currentVelocity = m_Rigidbody.velocity;
                Vector3 newVelocity = desiredDirection.normalized * currentVelocity.magnitude;
                //Debug.Log(currentVelocity);
                //Debug.Log(newVelocity);
                m_Rigidbody.velocity = new Vector3(newVelocity.x, currentVelocity.y, newVelocity.z);
                e = false;
            }
        }
        //if (grounded && ((m_Rigidbody.transform.eulerAngles.x >= 90 && m_Rigidbody.transform.eulerAngles.x <= 270)
        //|| 
        //(m_Rigidbody.transform.eulerAngles.z >= 90 && m_Rigidbody.transform.eulerAngles.z <= 270))) {
        //    Debug.Log(m_Rigidbody.transform.eulerAngles.x);
        //    Debug.Log(m_Rigidbody.transform.eulerAngles.y);
        //    m_Rigidbody.transform.eulerAngles = new Vector3(0, m_Rigidbody.transform.eulerAngles.y, 0);

        //    switchG = false;
        //}

        if (grounded && ((m_Rigidbody.transform.eulerAngles.x > 0 && m_Rigidbody.transform.eulerAngles.x < 180))) {
            //Debug.Log(m_Rigidbody.transform.eulerAngles.x);
            //Debug.Log(m_Rigidbody.transform.eulerAngles.y);
            if (m_Rigidbody.transform.eulerAngles.x - 1 < 0 || m_Rigidbody.transform.eulerAngles.x + 1 > 0) {
                m_Rigidbody.transform.eulerAngles = new Vector3(0, m_Rigidbody.transform.eulerAngles.y, m_Rigidbody.transform.eulerAngles.z);
            } else {
                m_Rigidbody.transform.eulerAngles = new Vector3(-1, m_Rigidbody.transform.eulerAngles.y, m_Rigidbody.transform.eulerAngles.z);
            }
            

            //switchG = false;
        } else if (grounded && ((m_Rigidbody.transform.eulerAngles.x > 179))) {
            //Debug.Log(m_Rigidbody.transform.eulerAngles.x);
            //Debug.Log(m_Rigidbody.transform.eulerAngles.y);
            if (m_Rigidbody.transform.eulerAngles.x - 1 < 0 || m_Rigidbody.transform.eulerAngles.x + 1 > 0) {
                m_Rigidbody.transform.eulerAngles = new Vector3(0, m_Rigidbody.transform.eulerAngles.y, m_Rigidbody.transform.eulerAngles.z);
            } else {
                m_Rigidbody.transform.eulerAngles = new Vector3(1, m_Rigidbody.transform.eulerAngles.y, m_Rigidbody.transform.eulerAngles.z);
            }
            
            //switchG = false;
        } else if (grounded && ((m_Rigidbody.transform.eulerAngles.z > 0 && m_Rigidbody.transform.eulerAngles.z < 180))) {
            //Debug.Log(m_Rigidbody.transform.eulerAngles.x);
            //Debug.Log(m_Rigidbody.transform.eulerAngles.y);
            if (m_Rigidbody.transform.eulerAngles.z - 1 < 0 || m_Rigidbody.transform.eulerAngles.z + 1 > 0) {
                m_Rigidbody.transform.eulerAngles = new Vector3(m_Rigidbody.transform.eulerAngles.z, m_Rigidbody.transform.eulerAngles.y, 0);
            } else {
                m_Rigidbody.transform.eulerAngles = new Vector3(m_Rigidbody.transform.eulerAngles.z, m_Rigidbody.transform.eulerAngles.y, -1);
            }

            //switchG = false;
        } else if (grounded && ((m_Rigidbody.transform.eulerAngles.z > 179))) {
            //Debug.Log(m_Rigidbody.transform.eulerAngles.x);
            //Debug.Log(m_Rigidbody.transform.eulerAngles.y);
            if (m_Rigidbody.transform.eulerAngles.z - 1 < 0 || m_Rigidbody.transform.eulerAngles.z + 1 > 0) {
                m_Rigidbody.transform.eulerAngles = new Vector3(m_Rigidbody.transform.eulerAngles.z, m_Rigidbody.transform.eulerAngles.y, 0);
            } else {
                m_Rigidbody.transform.eulerAngles = new Vector3(m_Rigidbody.transform.eulerAngles.z, m_Rigidbody.transform.eulerAngles.y, 1);
            }

            //switchG = false;
        }
    }
    /*
    void FixedUpdate() {
        if (Input.GetKey("w")) {
            //m_Rigidbody.AddForce(transform.forward * m_Thrust);
            //Debug.Log("FORWARD!");
            m_Rigidbody.velocity = new Vector3(0f, 0f, 1f);
        }
    }
    */
}
