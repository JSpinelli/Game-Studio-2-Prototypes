using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorHover : MonoBehaviour
{
   [SerializeField] private Image customImage;

   void OnMouseOver(Collider other)
   {
      if (other.CompareTag("Interactable"))
      {
         customImage.enabled = true;
      }
      else
      {
         customImage.enabled = false;
      }
   } 
}
