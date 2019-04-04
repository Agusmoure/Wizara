using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public string npcName;

    [TextArea(3, 10)]
    public string[] sentences;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.ReturnUIManager().EnableDialogueBox(npcName, sentences);
        }
    }
}
