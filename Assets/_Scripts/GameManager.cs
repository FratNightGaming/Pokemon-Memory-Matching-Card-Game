using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Pokemon Cards")]
    public GameObject[] cards;

    [Header("Sprite Elements")]
    public Sprite[] pokemonPC;
    public List<Sprite> pokemon = new List<Sprite>();
    public Sprite backCardSprite;

    [Header("Audio Elements")]
    public AudioClip[] pokemonSoundsPC;
    public List<AudioClip> pokemonSounds = new List<AudioClip>();

    [Header("Game Stats")]
    public int guesses;
    public int matches;
    public int possibleMatches;
    public bool madeFirstGuess;
    public bool madeSecondGuess;
    public bool match;
    public int firstGuessIndex;
    public int secondGuessIndex;

    private void Awake()
    {
        pokemonPC = Resources.LoadAll<Sprite>("Sprites/Pokemon");
        pokemonSoundsPC = Resources.LoadAll<AudioClip>("Audio/Pokemon Cries");
    }

    void Start()
    {
        cards = GameObject.FindGameObjectsWithTag("PokemonCard");
        possibleMatches = cards.Length / 2;//InstantiatePokemon.pokemonCardTotal / 2;

        RandomizeSpritesAndSoundArray(pokemonPC, pokemonSoundsPC);

        SetCardsFaceDown();
        SetPokemonSprites();

        RandomizeSpritesAndSoundList(pokemon, pokemonSounds);

        ButtonListeners();
    }

    public void RandomizeSpritesAndSoundArray(Sprite[] spriteArray, AudioClip[] audioClipArray)
    {
        //this doesnt shuffle within the pokemon list...
        for (int i = 0; i < spriteArray.Length; i++)
        {
            int random = UnityEngine.Random.Range(i, spriteArray.Length);

            Sprite tempSprite = spriteArray[i];
            AudioClip tempClip = audioClipArray[i];

            spriteArray[i] = spriteArray[random];
            pokemonSoundsPC[i] = audioClipArray[random];

            spriteArray[random] = tempSprite;
            pokemonSoundsPC[random] = tempClip;
        }
    }

    public void RandomizeSpritesAndSoundList(List<Sprite> spriteList, List<AudioClip> audioClipList)
    {
        for (int i = 0; i < spriteList.Count; i++)
        {
            int random = UnityEngine.Random.Range(i, spriteList.Count);

            Sprite tempSprite = spriteList[i];
            AudioClip tempClip = audioClipList[i];

            spriteList[i] = spriteList[random];
            audioClipList[i] = audioClipList[random];

            spriteList[random] = tempSprite;
            audioClipList[random] = tempClip;
        }
    }

    void Update()
    {

    }

    public void SetCardsFaceDown()
    {
        //GameObject[] cards = GameObject.FindGameObjectsWithTag("PokemonCard");
        for (int i = 0; i < cards.Length; i++)
        {
            //pokemonCardButtons.Add(cards[i].GetComponent<Button>());
            //pokemonCardButtons[i].image.sprite = backCardSprite;
            cards[i].GetComponent<Button>().image.sprite = backCardSprite;//this doesnt register...
            //pokemonSounds.Add(cards[i].GetComponent<AudioSource>().clip);//add list of audio clips 
            //pokemonSounds[i] = pokemonCries[i];//set sound from new clip to equal preselected clips from soundlist
        }
    }

    public void SetPokemonSprites()
    {
        int index = 0;

        for (int i = 0; i < cards.Length; i++)
        {
            if (index == cards.Length / 2)
            {
                index = 0;
            }
            pokemon.Add(pokemonPC[index]);
            pokemonSounds.Add(pokemonSoundsPC[index]);
            index++;
        }
    }

    public void ButtonListeners()
    {
        /*foreach (Button button in pokemonCardButtons)
        {
            button.onClick.AddListener(FlipCardOver);
        }*/

        foreach (GameObject card in cards)
        {
            card.GetComponent<Button>().onClick.AddListener(FlipCardOver);
        }
    }

    public void FlipCardOver()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        Debug.Log("This is button: " + name);

        if (madeFirstGuess == false)
        {
            madeFirstGuess = true;
            firstGuessIndex = int.Parse(name);
            cards[firstGuessIndex].GetComponent<Button>().image.sprite = pokemon[firstGuessIndex];
            cards[firstGuessIndex].GetComponent<AudioSource>().PlayOneShot(pokemonSounds[firstGuessIndex]);

            //cards[firstGuessIndex].GetComponent<Button>().interactable = false;
        }

        else if (madeSecondGuess == false)
        {
            secondGuessIndex = int.Parse(name);

            if (firstGuessIndex == secondGuessIndex)
            {
                cards[firstGuessIndex].GetComponent<Button>().image.sprite = backCardSprite;
                madeFirstGuess = false;
                return;
            }

            else
            {
                madeSecondGuess = true;
                guesses++;
                cards[secondGuessIndex].GetComponent<Button>().image.sprite = pokemon[secondGuessIndex];
                cards[secondGuessIndex].GetComponent<AudioSource>().PlayOneShot(pokemonSounds[secondGuessIndex]);

                StartCoroutine(CheckIfPokemonMatch());
            }
        }
    }

    public IEnumerator CheckIfPokemonMatch()
    {

        if (cards[firstGuessIndex].GetComponent<Button>().image.sprite == cards[secondGuessIndex].GetComponent<Button>().image.sprite)

        {
            matches++;

            if(matches == possibleMatches)
            {
                yield return new WaitForSeconds(1f);

                TurnOffButtons();

                Debug.Log("Victory");
                //add victory function
            }

            else
            {
                yield return new WaitForSeconds(1f);

                Debug.Log("match!!!");

                TurnOffButtons();

                //allow player to continue matching
                madeFirstGuess = false;
                madeSecondGuess = false;
            }
        }

        else
        {
            yield return new WaitForSeconds(1f);

            Debug.Log("Not a match!!!");

            //turn cards over
            cards[firstGuessIndex].GetComponent<Button>().image.sprite = backCardSprite;
            cards[secondGuessIndex].GetComponent<Button>().image.sprite = backCardSprite;

            //allow player to continue matching
            madeFirstGuess = false;
            madeSecondGuess = false;
        }
    }

    public void Victory()
    {

    }

    //restart button
    public void RestartGame()
    {

    }

    //main menu button
    public void MainMenu()
    {

    }

    public void TurnOffButtons()
    {
        Color clear = new Color(0, 0, 0, 0);

        //set matched buttons to not be interactable
        cards[firstGuessIndex].GetComponent<Button>().interactable = false;
        cards[secondGuessIndex].GetComponent<Button>().interactable = false;

        //set matched buttons to 0 alpha
        //cards[firstGuessIndex].GetComponent<Button>().image.color = clear;
        //cards[secondGuessIndex].GetComponent<Button>().image.color = clear;
    }

}
