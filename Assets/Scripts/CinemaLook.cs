using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemaLook : MonoBehaviour
{
    public GameObject Player;
    public Transform followTarget;
    //private CinemachineVirtualCamera cam;
    private CinemachineFreeLook cam;

    public Joystick camStick;
    public float CamAngleSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //cam = GetComponent<CinemachineVirtualCamera>();
        cam = GetComponent<CinemachineFreeLook>();
        Player = GameObject.FindWithTag("Player");
        followTarget = Player.transform;
        cam.Follow = followTarget;
        cam.LookAt = GameObject.Find("Neck").transform;
    }

    private void Update()
    {
            cam.m_XAxis.Value += camStick.Horizontal*200 * CamAngleSpeed;
            cam.m_YAxis.Value += camStick.Vertical*CamAngleSpeed;

    }
}