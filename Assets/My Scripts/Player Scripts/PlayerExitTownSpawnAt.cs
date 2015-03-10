using UnityEngine;
using System.Collections;

public class PlayerExitTownSpawnAt : MonoBehaviour {

    private Vector3 posToBeAtAfterLoad;
    private Vector3 dirToFaceAfterLoad;

    void OnLevelWasLoaded(int level) 
    {
        if (posToBeAtAfterLoad != new Vector3())
        {
            transform.position = posToBeAtAfterLoad;
            posToBeAtAfterLoad = new Vector3();
        }
        if (dirToFaceAfterLoad != new Vector3())
        {
            transform.LookAt(dirToFaceAfterLoad);
        }
    }

	public void setPositionAfterLoad(Vector3 pos, string facing = "zPos")
    {
        posToBeAtAfterLoad = pos;

        switch (facing)
        {
            case "zPos":
                dirToFaceAfterLoad = new Vector3(0, 0, 1);
                break;
            case "zNeg":
                dirToFaceAfterLoad = new Vector3(0, 0, -1);
                break;
            case "xPos":
                dirToFaceAfterLoad = new Vector3(1, 0, 0);
                break;
            case "xNeg":
                dirToFaceAfterLoad = new Vector3(-1, 0, 0);
                break;
        }
        dirToFaceAfterLoad += pos;
    }
}
