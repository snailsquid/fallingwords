using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FallingWordItem : MonoBehaviour
{
    public float speed = -1.0f;
    public float stop = 1.0f;
    public string word = "example";
    [SerializeField] private TMP_Text text;
    FallingWordManager fallingWordManager;

    void Start()
    {
        fallingWordManager = ServiceLocator.Instance.fallingWordManager;
    }
    public void Set(string word, float speed)
    {
        this.word = word;
        this.speed = speed;
        text.text = word;
    }
    void Update()
    {
        transform.Translate(Vector3.down * speed * stop * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Despawner"))
        {
            Despawn();
            Game game = ServiceLocator.Instance.gameModeManager.game;
            if (game is EndlessMode endlessMode)
            if(endlessMode.Shield == true)
            {
                endlessMode.ShieldActive(false);
            }
            else
            {
                if (WordGenerator.Is(word, WordGenerator.WordType.PowerUp) || WordGenerator.Is(word, WordGenerator.WordType.Trap))
                {
                    Debug.Log(word);
                    
                }
                else
                {
                    endlessMode.ChangeLife(-1);
                }
            }
            
        }
    }
    public void Despawn()
    {
        fallingWordManager.wordsContainer.RemoveWord(word);
        fallingWordManager.RemoveWordItem(this);
        Destroy(gameObject);
    }
}
