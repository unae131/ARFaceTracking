using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARFaceSwitch : MonoBehaviour
{
    private ARFaceManager arFaceManager;

    public Material[] materials;

    private int switchCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        arFaceManager = GetComponent<ARFaceManager>(); // GetComponent : access and bring other component in the same game object(ex: AR Session Origin?)
        arFaceManager.facePrefab.GetComponent<MeshRenderer>().material = materials[switchCount];
    }

    void SwitchFaces()
    {
        foreach (ARFace face in arFaceManager.trackables) // arFaceManager가 추적가능한 모든 얼굴
        {
            face.GetComponent<MeshRenderer>().material = materials[switchCount];
        }

        switchCount = (switchCount + 1) % materials.Length;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            SwitchFaces();
        }
    }
}
