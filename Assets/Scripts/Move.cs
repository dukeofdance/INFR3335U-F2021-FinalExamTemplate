using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class Move : MonoBehaviour
{
    private Rigidbody rb;
    private float movespeed,jumpspeed;
    private float dirX, dirZ, dirY;
    bool grounded;

    public Transform cam;

    Vector3 direction;
    Vector3 moveDir;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public PhotonView view;

    public Joystick movestick;
    public Joystick camStick;

    protected float CamAngle;
    protected float CamAngleSpeed = 1f;

    private string message = "bruh";

    private CinemachineFreeLook cin;
    private void CameraMovement()
    {
        CamAngle += camStick.Horizontal * CamAngleSpeed;

        cam.position = transform.position + Quaternion.AngleAxis(CamAngle, Vector3.up) * new Vector3(0, 3.54f, -9.4f);
        cam.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - cam.position, Vector3.up);

        cin.m_XAxis.Value += camStick.Horizontal * CamAngleSpeed;
        cin.m_YAxis.Value += camStick.Vertical * (float)(CamAngleSpeed*0.01);

    }
    // Start is called before the first frame update
    void Start()
    {
        movespeed = 5f;
        jumpspeed = 5f;
        rb = GetComponent<Rigidbody>();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            PlayerMovement();
            CameraMovement();
        }
    }

    private void PlayerMovement()
    {
        dirX = movestick.Horizontal * movespeed;
        dirZ = movestick.Vertical * movespeed;
        //if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        //{
        //    rb.AddForce(Vector3.up * jumpspeed, ForceMode.Impulse);
        //    grounded = false;
        //}
        direction = new Vector3(dirX, 0f, dirZ).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //}
        }
    }

    private void FixedUpdate()
    {
        if (direction.magnitude >= 0.1f)
            rb.velocity = moveDir * movespeed;//new Vector3(dirX*moveDir.normalized.x *joystick.Horizontal,0, dirZ*moveDir.normalized.z*joystick.Vertical);//(dirX, rb.velocity.y, dirZ);
        else
            rb.velocity = new Vector3(dirX, rb.velocity.y, dirZ);
    }


    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.tag == "Enemy")
        //{
        //    Destroy(collision.collider.gameObject);
        //}
        if (collision.collider.tag == "Ground")
        {
            grounded = true;
        }

    }

    public void SetJoysticks(GameObject camera)
    {
        Joystick[] tempJoystickList = camera.GetComponentsInChildren<Joystick>();
        foreach (Joystick temp in tempJoystickList)
        {
            if (temp.tag == "Movement Stick")
                movestick = temp;
            else if (temp.tag == "Camera Stick")
                camStick = temp;
        }
        cam = camera.transform;

        cin =cam.GetComponentInChildren<CinemachineFreeLook>();
        cin.Follow = GameObject.FindWithTag("Player").transform;
        cin.LookAt = GameObject.Find("Neck").transform;
    }
    public void Bruh()
    {
        Debug.Log(message);
    }
}
