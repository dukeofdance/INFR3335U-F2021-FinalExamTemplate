using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemaLook : MonoBehaviour
{
    public GameObject Player;
    public Transform followTarget;
    private CinemachineFreeLook cam;

    public Joystick camStick;
    public float CamAngleSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineFreeLook>();
        Player = GameObject.FindWithTag("Player");
        followTarget = Player.transform;
        cam.LookAt = followTarget;
        cam.Follow = followTarget;
    }

    private void Update()
    {
        cam.m_XAxis.m_InputAxisValue += camStick.Horizontal * 200 * CamAngleSpeed;
        cam.m_YAxis.m_InputAxisValue += camStick.Vertical * CamAngleSpeed;

    }
}
