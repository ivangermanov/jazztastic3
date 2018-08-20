using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeIndeactivator : MonoBehaviour {

    public void deactive_itself()
    {
        this.gameObject.SetActive(false);
    }
}
