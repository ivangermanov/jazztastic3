using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wrong_credentials_entered : MonoBehaviour {

    public void disable_boolean()
    {
        (this.GetComponent("Animator") as Animator).SetBool("GoIn",false);
    }
}
