using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class error_window_manager : MonoBehaviour, IPointerClickHandler
{
    public Animator erro_animator;
    public void OnPointerClick(PointerEventData eventData)
    {
       
        erro_animator.SetBool("GoOut",true);
        erro_animator.SetBool("GoIn", false);
    }
}
