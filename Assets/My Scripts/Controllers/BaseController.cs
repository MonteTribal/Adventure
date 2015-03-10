using UnityEngine;
using System.Collections;

//base controllers from enemies

public class BaseController : MonoBehaviour {

    public virtual bool getStat(string stat)
    {
        return false; // redefined in emeny controllers
    }

}
