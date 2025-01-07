using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWordManager : MonoBehaviour
{
    public float initialSpeed = 1.0f;
    public float speedVariation = 0.1f;
    public float rate = 1.0f;
    public float specialChance = 0.1f;
    public float powerupChance = 0.7f;
    //public float trapChance = 0.3f;
    public Transform fallingWordPrefab, spawnArea;
    public WordGenerator.Theme theme;
    public WordsContainer wordsContainer;
    public List<FallingWordItem> wordItems = new List<FallingWordItem>();
    public void StartGame(WordGenerator.Theme theme)
    {
        Debug.Log("test");
        this.theme = theme;
        wordsContainer = new WordsContainer(theme);
        StartCoroutine(StartGameCoroutine());
    }
    IEnumerator StartGameCoroutine()
    {
        while (GameStateManager.Instance.gameState == GameStateManager.GameState.Playing)
        {
            string word = wordsContainer.GetRandomWord();
            InstantiateWord(word);
            yield return new WaitForSeconds(1.0f / rate);
        }
    }
    void InstantiateWord(string word)
    {
        float speed = initialSpeed * (1 + Random.Range(-speedVariation, speedVariation));

        Vector2 spawnPos = new Vector2(spawnArea.localScale.x * Random.Range(-1.0f, 1.0f) * 0.5f, spawnArea.position.y);
        Transform wordInstance = Instantiate(fallingWordPrefab, spawnPos, Quaternion.identity);
        wordInstance.GetComponent<FallingWordItem>().Set(word, speed);
        AddWordItem(wordInstance.GetComponent<FallingWordItem>());
    }
    public void AddWordItem(FallingWordItem wordItem)
    {
        wordItems.Add(wordItem);
    }
    public void RemoveWordItem(FallingWordItem wordItem)
    {
        wordItems.Remove(wordItem);
    }
}
public static class WordGenerator
{
    public enum Theme
    {
        EverydayItems,
        Foods,
        Animals,
        Technology,
        Geography
    }
    static List<List<string>> everydayItems = new List<List<string>>
            {
            new List<string>{ "Pen", "Bag", "Key", "Cup", "Mug", "Bed", "Fan", "Box", "Lid", "Pad" }, // 3 letters
            new List<string>{ "Book", "Lamp", "Soap", "Comb", "Fork", "Bowl", "Desk", "Shoe", "Glue" }, // 4 letters
            new List<string>{ "Plate", "Brush", "Towel", "Clock", "Frame", "Glass", "Paper", "Knife", "Phone", "Chair" }, // 5 letters
            new List<string>{ "Remote", "Drawer", "Cushion", "Pillow", "Basket", "Marker", "Mirror", "Charger", "Bottle", "Switch" } // 6 letters
            };

    // Foods
    static List<List<string>> foods = new List<List<string>>
    {
            new List<string>{ "Pie", "Tea", "Egg", "Nut", "Yam", "Ham", "Bun", "Dip", "Fig", "Pea" }, // 3 letters
            new List<string>{ "Cake", "Soup", "Milk", "Meat", "Corn", "Rice", "Bean", "Tart", "Roll", "Lamb" }, // 4 letters
            new List<string>{ "Bread", "Steak", "Sushi", "Pasta", "Honey", "Apple", "Chili", "Lemon", "Mango", "Bacon" }, // 5 letters
            new List<string>{ "Burger", "Cheese", "Cookie", "Cereal", "Orange", "Tomato", "Potato", "Peanut", "Butter", "Salad" } // 6 letters
    };

    // Animals
    static List<List<string>> animals = new List<List<string>>
    {
            new List<string>{ "Cat", "Dog", "Fox", "Bat", "Rat", "Bee", "Ant", "Owl", "Elk", "Ape" }, // 3 letters
            new List<string>{ "Wolf", "Lion", "Fish", "Frog", "Hawk", "Dove", "Bear", "Swan", "Crab", "Duck" }, // 4 letters
            new List<string>{ "Tiger", "Horse", "Snake", "Zebra", "Whale", "Shark", "Eagle", "Otter", "Lemur", "Camel" }, // 5 letters
            new List<string>{ "Rabbit", "Turtle", "Donkey", "Parrot", "Salmon", "Spider", "Falcon", "Giraffe", "Kitten", "Weasel" } // 6 letters
    };

    // Technology
    static List<List<string>> technology = new List<List<string>>
    {
            new List<string>{ "App", "Bit", "Fax", "Web", "URL", "CPU", "GPU", "RAM", "VPN", "GPS" }, // 3 letters
            new List<string>{ "Code", "Tech", "Game", "Wifi", "Sync", "Disk", "Data", "Chip", "JPEG", "HTML" }, // 4 letters
            new List<string>{ "Robot", "Laser", "Cloud", "Input", "Output", "Cache", "Bytes", "Modern", "Print", "Phone" }, // 5 letters
            new List<string>{ "Server", "Binary", "Phyton", "Laptop", "Script", "Router", "Mobile", "Syntax", "Upload", "Widget" } // 6 letters
    };

    // Geography and places
    static List<List<string>> geography = new List<List<string>>
    {
            new List<string>{ "Sea", "Bay", "USA", "UAE", "Pau", "Rio", "Lake", "City", "Cave", "Hill" }, // 3 letters
            new List<string>{ "West", "East", "Dune", "Pole", "Land", "Bali", "Lyon", "Pisa", "Oslo", "Rome" }, // 4 letters
            new List<string>{ "Paris", "Italy", "India", "Japan", "China", "River", "Beach", "Valley", "Tower", "Cliff" }, // 5 letters
            new List<string>{ "Canyon", "Lagoon", "Island", "Harbor", "Forest", "Brazil", "Sweden", "Canada", "Padang", "Bekasi" } // 6 letters
    };
    static List<string> powerUp = new List<string>{"Slow","Freeze","2xBonus","3xBonus","Clear","Shield","Heal","Vigor"};
    static List<string> trap = new List<string>{"Fast","Minus","Halfx","Blind","Shuffle","Hurt","Sick"};

    static readonly Dictionary<Theme, List<List<string>>> wordBank = new Dictionary<Theme, List<List<string>>>{
        { Theme.EverydayItems, everydayItems },
        { Theme.Foods, foods },
        { Theme.Animals, animals },
        { Theme.Technology, technology },
        { Theme.Geography, geography }
    };
    public static List<List<string>> GetWordBank(Theme theme)
    {
        return wordBank[theme];
    }
    public static List<string> GetWordPowerUp()
    {
        return powerUp;
    }
    public static List<string> GetWordTrap()
    {
        return trap;
    }
}
public class WordsContainer
{

    public Dictionary<string, bool> wordsOnScreen = new Dictionary<string, bool>();
    public List<List<string>> availableWords;
    public List<string> powerUpWords;
    public List<string> trapWords;
    public Typing typing;
    public WordsContainer(WordGenerator.Theme theme)
    {
        availableWords = WordGenerator.GetWordBank(theme);
        powerUpWords = WordGenerator.GetWordPowerUp();
        trapWords = WordGenerator.GetWordTrap();
    }
    public string GetRandomWord()
    {
        int special = Random.Range(0,100);
        //float specialChance = new FallingWordManager().specialChance;
        if(special >= 100f*0.1)
        {
            int randLen = GetNonEmptyIndex();
            int randWord = Random.Range(0, availableWords[randLen].Count - 1);
            string word = availableWords[randLen][randWord];
            availableWords[randLen].RemoveAt(randWord);
            AddWord(word);
            return word;
        }
        else
        {
            int powerUpWord = Random.Range(0,100);
            //float powerupChance = new FallingWordManager().powerupChance;
            if(powerUpWord <= 100f*0.7f)
            {
                int randWord = Random.Range(0, powerUpWords.Count - 1);
                string word = powerUpWords[randWord];
                AddWord(word);
                return word;
            }
            else
            {
                int randWord = Random.Range(0, trapWords.Count - 1);
                string word = trapWords[randWord];
                AddWord(word);
                return word;
            }
        }
    }
    int GetNonEmptyIndex()
    {
        int randLen = Random.Range(0, 3);
        if (availableWords[randLen].Count == 0)
        {
            randLen = GetNonEmptyIndex();
        }
        return randLen;
    }
    public void AddWord(string word)
    {
        if(wordsOnScreen.ContainsKey(word)){}
        else 
        {
            wordsOnScreen.Add(word, false);
            //typing.addTheWords(word);
        }
    }
    public void RemoveWord(string word)
    {
        wordsOnScreen.Remove(word);
        if(powerUpWords.Contains(word)){}
        else if(trapWords.Contains(word)){}
        else 
        {
            availableWords[word.Length - 3].Add(word);
            //typing.removeTheWords(word);
        }
    }
}