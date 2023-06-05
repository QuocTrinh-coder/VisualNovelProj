using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// spawn character as we need and contain as a list for us to access them as we need

public class CharacterManager : MonoBehaviour
{
    // By declaring it as public static, it means that there will be only one instance of CharacterManager accessible globally throughout the program. 
    // Other scripts can access this instance without needing to manually assign a reference to it.
    public static CharacterManager instance; 

    // reference a UI panel that will contain all the character game objects. The RectTransform component allows for positioning and sizing UI elements in a scene.
    // all character must be attache in the character panel 
    public RectTransform characterPanel; 
    
    // serves as a collection to keep track of all the character objects currently in the scene
    // A list of all characters currently in our scene so we can keep track
    public List<Character> characters = new List<Character>(); // A list of all characters currently in our scene so we can keep track

    // use this to lookup the character
    // used to associate a character's name (string) with its corresponding index in the characters list (int). 
    public Dictionary<string, int> characterDictionary = new Dictionary<string, int>(); 

    // instance variable is assigned the value of this, referring to the current instance of CharacterManager
    //  it sets the instance variable to the current instance of CharacterManager. This allows other scripts to access the CharacterManager instance through the instance variable.
    void Awake(){
        instance = this;
    }

    //function is to return an instance of desired character if it's already exist in the scene
    // if the index of our character is in the dictionary, we take the integer value paired with that name and get the character from the list at that index
    public Character getCharacter(string characterName, bool createCharacterIfDoesNotExist = true)
    {
            int index = -1;
            //  find the value associated with the given key (characterName) in the dictionary. If it succeeds, it updates the index variable with the corresponding value and returns true. Otherwise, it returns false.
            if (characterDictionary.TryGetValue(characterName, out index))
            {
                // If the TryGetValue call succeeds (i.e., the character name exists in the dictionary), the method retrieves the Character object from the characters list using the obtained index and returns it.
                return characters[index];
            } 
            // If fail, the method calls the createCharacter method to create a new Character object with the given characterName. 
            else if (createCharacterIfDoesNotExist) {
                return createCharacter(characterName);
            }

            return null;
    }

    // function that called when a character with the given name doesn't exist in the dictionary and needs to be created. It takes characterName as a parameter and returns a new Character object.
    public Character createCharacter(string characterName)
    {
        // A new Character object is created with the given characterName.
        Character newCharacter = new Character(characterName);
        // The characterDictionary is updated by adding an entry with the characterName as the key and the current count of characters in the characters list as the value.
        characterDictionary.Add(characterName, characters.Count);
        // The newly created Character object is added to the characters list.
        characters.Add (newCharacter);

        // /the new Character object is returned.
        return newCharacter;

        // This function allows you to create a new Character object with the given name, update the dictionary and list accordingly, and return the newly created object.
    }

Vector2 targetPosition;
    Coroutine moving;
    bool isMoving { get { return moving != null; } }

    public void Move(Vector2 target, float speed, bool smooth = true)
    {
        StopMoving();
        moving = StartCoroutine(Moving(target, speed, smooth));
    }

    public void StopMoving()
    {
        if (isMoving)
        {
            StopCoroutine(moving);
        }
        moving = null;
    }

    IEnumerator Moving(Vector2 target, float speed, bool smooth = true)
    {
        targetPosition = target;
        // Implement the logic for character movement here
        // You can access the targetPosition and use it for movement calculations

        yield return null; // Placeholder yield statement
    }
}

// Summary :
// Overall, these two methods work together to retrieve a Character object by name from the characterDictionary. 
// If the character exists, it is returned. If the character doesn't exist, you have the option to create it or receive a null value, depending on the value of createCharacterIfDoesNotExist.
