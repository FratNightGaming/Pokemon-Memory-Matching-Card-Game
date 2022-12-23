using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePokemon : MonoBehaviour
{
    public static int pokemonCardTotal = 8;

    public GameObject pokemonCard;

    public Transform gameBoard;

    public static InstantiatePokemon instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(this);
        }

        InstantiatePokemonCards();
    }

    public void InstantiatePokemonCards()
    {
        for (int i = 0; i < pokemonCardTotal; i++)
        {
            GameObject pokemonButton = Instantiate(pokemonCard, gameBoard);
            pokemonButton.name = i.ToString();
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
