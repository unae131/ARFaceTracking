using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;

public class ARCoreFaceRegionManager : MonoBehaviour
{
    public GameObject nosePrefab; // 처음에 어떤 object를 올릴 것인지
    public GameObject leftHeadPrefab;
    public GameObject rightHeadPrefab;
    
    private ARFaceManager arFaceManager;
    private ARSessionOrigin sessionOrigin;

    private NativeArray<ARCoreFaceRegionData> faceRegions; // arcore를 통해서 얻어진 face region 정보들이 들어갈 것

    private GameObject noseObject; // 실제로 올려줄 object
    private GameObject leftHeadObject;
    private GameObject rightHeadObject;
    
    // Start is called before the first frame update
    void Start()
    {
        arFaceManager = GetComponent<ARFaceManager>();
        sessionOrigin = GetComponent<ARSessionOrigin>();
        // for mapping
    }

    // Update is called once per frame
    void Update()
    {
        ARCoreFaceSubsystem subsystem = (ARCoreFaceSubsystem) arFaceManager.subsystem; // unity 내부 다른 plugin들(ex: arkit)에 붙어서 해당하는 api만 사용할 수 있게 만들어주는 하나의 인터페이스
        
        foreach (ARFace face in arFaceManager.trackables)
        {
            subsystem.GetRegionPoses(face.trackableId, Allocator.Persistent, ref faceRegions); // face, allocator, output

            foreach (ARCoreFaceRegionData faceRegion in faceRegions)
            {
                ARCoreFaceRegion regionType = faceRegion.region;

                if (regionType == ARCoreFaceRegion.NoseTip)
                {
                    if (!noseObject)
                    {
                        noseObject = Instantiate(nosePrefab, sessionOrigin.trackablesParent);
                    }

                    noseObject.transform.localPosition = faceRegion.pose.position;
                    noseObject.transform.localRotation = faceRegion.pose.rotation;
                }
                else if (regionType == ARCoreFaceRegion.ForeheadLeft)
                {
                    if (!leftHeadObject)
                    {
                        leftHeadObject = Instantiate(leftHeadPrefab, sessionOrigin.trackablesParent);
                    }

                    leftHeadObject.transform.localPosition = faceRegion.pose.position;
                    leftHeadObject.transform.localRotation = faceRegion.pose.rotation;
                }
                else if (regionType == ARCoreFaceRegion.ForeheadRight)
                {
                    if (!rightHeadObject)
                    {
                        rightHeadObject = Instantiate(rightHeadPrefab, sessionOrigin.trackablesParent);
                    }

                    rightHeadObject.transform.localPosition = faceRegion.pose.position;
                    rightHeadObject.transform.localRotation = faceRegion.pose.rotation;
                }
            }
        }
    }
}
