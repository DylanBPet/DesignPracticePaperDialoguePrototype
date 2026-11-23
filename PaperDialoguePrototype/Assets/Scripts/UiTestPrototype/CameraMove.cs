using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject moveCamera;

    Vector3 cameraStartPos;
    Vector3 cameraRotationPos;

    public GameObject textCanvas;

    float t = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CenterCameraStart(Vector3 centeredObject)
    {
        cameraStartPos = mainCamera.transform.position;
        StartCoroutine(CenterCamera(centeredObject));
    }
    public IEnumerator CenterCamera(Vector3 centeredObject)
    {
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 0.5f;
            //center camera at an object
            mainCamera.transform.position = Vector3.Lerp(cameraStartPos, new Vector3(centeredObject.x, cameraStartPos.y, centeredObject.z), t);
            yield return null;
        }
        CameraSwitchPerspectivesStart();
    }



    public void CameraSwitchPerspectivesStart()
    {
        cameraStartPos = mainCamera.transform.position;
        cameraRotationPos = mainCamera.transform.eulerAngles;
        StartCoroutine(CameraSwitchPOV());
    }
    public IEnumerator CameraSwitchPOV()
    {
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 1.2f;
            //center camera at an object
            mainCamera.transform.position = Vector3.Lerp(cameraStartPos, moveCamera.transform.position, t);
            mainCamera.transform.eulerAngles = Vector3.Lerp(cameraRotationPos, moveCamera.transform.eulerAngles, t);
            yield return null;
        }
         textCanvas.SetActive(true);
}
}
