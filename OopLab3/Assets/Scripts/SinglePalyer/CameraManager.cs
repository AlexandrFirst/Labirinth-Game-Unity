using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera vcam;
    public GameObject tPlayer;
    public Transform tFollowTarget;

    public float duration = 1f;
    float elapsed = 0.0f;
    public bool transition = false;

    [SerializeField]
    UI_Manager UiManager;

    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        UiManager = FindObjectOfType<UIManager>();
        if(UiManager==null)
            UiManager = FindObjectOfType<MultiplayerUIManager>();

        vcam.m_Lens.OrthographicSize = 5f;
        UiManager.ZoomLevel.value = vcam.m_Lens.OrthographicSize;
        UiManager.CameraChange = ChangeCameraZoom;
    }


    void ChangeCameraZoom()
    {
        vcam.m_Lens.OrthographicSize = UiManager.ZoomLevel.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (tPlayer == null)
        {
            tPlayer = FindObjectOfType<ControlManager>().gameObject;
        }
        tFollowTarget = tPlayer.transform;
        vcam.LookAt = tFollowTarget;
        vcam.Follow = tFollowTarget;
       

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            vcam.m_Lens.OrthographicSize = vcam.m_Lens.OrthographicSize + 5 * Time.deltaTime;
            if (vcam.m_Lens.OrthographicSize > 10f)
                vcam.m_Lens.OrthographicSize = 10f;

            UiManager.ZoomLevel.value = vcam.m_Lens.OrthographicSize;
        }


        if (Input.GetKey(KeyCode.LeftControl))
        {
            vcam.m_Lens.OrthographicSize = vcam.m_Lens.OrthographicSize - 5 * Time.deltaTime;
            if (vcam.m_Lens.OrthographicSize < 3f)
                vcam.m_Lens.OrthographicSize = 3f;

            UiManager.ZoomLevel.value = vcam.m_Lens.OrthographicSize;
        }
    }
}
