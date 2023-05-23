using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public Character Audrey; 
    // Start is called before the first frame update
    void Start()
    {
        Audrey = CharacterManager.instance.getCharacter("Audrey");
    }

    public Vector2 moveTarget;
    public float moveSpeed;
    public bool smooth;

    void Update()
    {
        if ( Input.GetKey(KeyCode.M))
        {
            Audrey.MoveTo (moveTarget, moveSpeed, smooth ) ;
        }
    }

    // get the text for character speak and the counting variable for the line
    
    // public string[] speech;
    // int i = 0;

//     // update the text as we go
//     void Update()
// {
//     if (Input.GetKeyDown(KeyCode.Space))
//     {
//         if (i < speech.Length)
//             Audrey.Say(speech[i]);
//         else
//              DialogSystem.instance.Close();

//         i++;
        
//     }
// }
}
