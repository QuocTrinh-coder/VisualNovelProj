using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public Character Audrey; 
    public Vector2 moveTarget;
    public float moveSpeed;
    public bool smooth;
    // Start is called before the first frame update
    void Start()
    {
        Audrey = CharacterManager.instance.getCharacter("Audrey");
        Audrey.Move (moveTarget, moveSpeed, smooth ) ;
    }



    // void Update()
    // {
    //     if ( Input.GetKey(KeyCode.M))
    //     {
    //         Audrey.Move (moveTarget, moveSpeed, smooth ) ;
    //     }
    //     if (Input.GetKey(KeyCode.S))
    //     {
    //         Audrey.StopMoving (true) ;
    //     }
    // }

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
