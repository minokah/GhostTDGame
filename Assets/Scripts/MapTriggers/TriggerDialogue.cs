using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text ghostDialogue;

    private void OnTriggerEnter(Collider collisionObject)
    {
        if (collisionObject.gameObject.CompareTag("Enemy") && this.name == "EarlyDetection1")
        {
            this.gameObject.SetActive(false);
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "Oh damm, those humans are already coming in. They're moving quicker then I expected too. I have to keep them away from my home, until I finish my concealment spell.";
            WaitUtility.Wait(5, () => { 
                dialoguePanel.SetActive(false); 
                ghostDialogue.text = ""; }
            );
        }
        else if (collisionObject.gameObject.CompareTag("Enemy") && this.name == "MidDetection1")
        {
            this.gameObject.SetActive(false);
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "Hey they're starting to come pretty close... I should probably stop them before they get to close.";
            WaitUtility.Wait(5, () => {
                dialoguePanel.SetActive(false);
                ghostDialogue.text = "";
            }
            );
        }
        else if (collisionObject.gameObject.CompareTag("Enemy") && this.name == "LateDetection1")
        {
            this.gameObject.SetActive(false);
            dialoguePanel.SetActive(true);
            ghostDialogue.text = "Now they are way too close for comfort... I've got to stop them before they mess up my house!";
            WaitUtility.Wait(5, () => {
                dialoguePanel.SetActive(false);
                ghostDialogue.text = "";
            }
            );
        }
    }
}
