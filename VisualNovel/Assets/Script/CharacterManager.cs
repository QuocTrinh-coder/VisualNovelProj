using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// spawn character as we need and contain as a list for us to access them as we need

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance; // so we can access this from any other without any manual variable assignment 

    public RectTransform characterPanel; // all character must be attache in the character panel 
    public List<Character> characters = new List<Character>(); // A list of all characters currently in our scene so we can keep track
    // use this to lookup the character
    // this dictionary will take the character name string then find its corressponding index int in the character list
    public Dictionary<string, int> characterDictionary = new Dictionary<string, int>(); 
    
    void Awake(){
        instance = this;
    }

    //function is to return an instance of desired character if it's already exist in the scene
    // if the index of our character is in the dictionary, we take the integer value paired with that name and get the character from the list at that index
    public Character getCharacter(string characterName, bool createCharacterIfDoesNotExist = true)
    {
            int index = -1;
            if (characterDictionary.TryGetValue(characterName, out index))
            {
                return characters[index];
            } 
            else if (createCharacterIfDoesNotExist) {
                return createCharacter(characterName);
            }

            return null;
    }

    // function that create character if does not exist
    public Character createCharacter(string characterName)
    {
        Character newCharacter = new Character(characterName);

        characterDictionary.Add(characterName, characters.Count);
        characters.Add (newCharacter);

        return newCharacter;
    }
}
