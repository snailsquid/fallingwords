using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class FallingWordManager : MonoBehaviour
{
    public float initialSpeed = 1.0f;
    public float speedVariation = 0.1f;
    public float rate = 1.0f;
    public static float specialChance = 0.9f;
    public static float powerupChance = 0.7f;
    public Transform fallingWordPrefab, spawnArea;
    public WordGenerator.Theme theme;
    public WordsContainer wordsContainer;
    score score;
    public Dictionary<string, FallingWordItem> wordItems = new Dictionary<string, FallingWordItem>();
    static public Typing typing;
    static public FallingWordItem fallingWordItem;
    void Start()
    {
        typing = ServiceLocator.Instance.typing;
        score = GameObject.FindWithTag("Player").GetComponent<score>();
    }
    public void StartGame(WordGenerator.Theme theme)
    {
        this.theme = theme;
        wordsContainer = new WordsContainer(theme);
        score.setScore(0);
        StartCoroutine(StartGameCoroutine());
    }
    IEnumerator StartGameCoroutine()
    {
        while (GameStateManager.Instance.gameState == GameStateManager.GameState.Playing)
        {
            float time = Time.time;
            float deltaTime = 0;
            string word = wordsContainer.GetRandomWord();
            InstantiateWord(word);
            yield return new WaitForSeconds(TimeDelay(deltaTime) * rate);
            deltaTime = Time.time - time;
        }
    }
    float TimeDelay(float currentTime)
    {
        return 1 - (Mathf.Exp(currentTime - 4) / ((Mathf.Exp(currentTime - 4) + 1) * 2));
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
        wordItems.Add(wordItem.word, wordItem);
    }
    public void RemoveWordItem(FallingWordItem wordItem)
    {
        wordItems.Remove(wordItem.word);
    }
    public void Reset()
    {
        foreach (FallingWordItem wordItem in wordItems.Values)
        {
            Destroy(wordItem.gameObject);
        }
        wordItems.Clear();
        wordsContainer.Reset(theme);
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
    static List<string> powerUp = new List<string> { "Slow", "Freeze", "2xBonus", "3xBonus", "Clear" };
    static List<string> trap = new List<string> { "Fast", "Minus", "Halfx", "Blind", "Hurt", "Sick" };
    static List<string> powerUpEndless = new List<string> { "Slow", "Freeze", "2xBonus", "3xBonus", "Clear", "Shield", "Heal", "Vigor" };
    static readonly Dictionary<Theme, List<List<string>>> wordBank = new Dictionary<Theme, List<List<string>>>{
        { Theme.EverydayItems, everydayItems },
        { Theme.Foods, foods },
        { Theme.Animals, animals },
        { Theme.Technology, technology },
        { Theme.Geography, geography }
    };
    public enum WordType
    {
        PowerUp, Trap, Normal
    }
    public static List<List<string>> GetWordBank(Theme theme)
    {
        return wordBank[theme].ToList();
    }
    public static List<string> GetWordPowerUp(bool isEndless)
    {
        if (isEndless)
            return trap.Concat(powerUpEndless).ToList();
        return powerUp.ToList();
    }
    public static List<string> GetWordTrap()
    {
        return trap.ToList();
    }
    public static bool Is(string word, WordType wordType)
    {
        if (wordType == WordType.PowerUp)
        {
            return powerUp.Contains(word);
        }
        else if (wordType == WordType.Trap)
        {
            return trap.Contains(word);
        }
        else
        {
            return !powerUp.Contains(word) && !trap.Contains(word);
        }

    }
}
public class WordsContainer
{

    public Dictionary<string, bool> wordsOnScreen;
    public List<List<string>> availableWords;
    public List<string> powerUpWords;
    public List<string> trapWords;
    Typing typing;
    public WordsContainer(WordGenerator.Theme theme)
    {
        Reset(theme);
    }
    public void Reset(WordGenerator.Theme theme)
    {
        availableWords = WordGenerator.GetWordBank(theme);
        powerUpWords = WordGenerator.GetWordPowerUp(ServiceLocator.Instance.gameModeManager.game is EndlessMode);
        trapWords = WordGenerator.GetWordTrap();
        wordsOnScreen = new Dictionary<string, bool>();
    }
    public string GetRandomWord()
    {
        int special = Random.Range(0, 100);
        float chance = FallingWordManager.specialChance;
        //float specialChance = new FallingWordManager().specialChance;
        if (special >= 100f * chance)
        {
            int randLen = GetNonEmptyIndex();
            int randWord = Random.Range(0, availableWords[randLen].Count - 1);
            string word = availableWords[randLen][randWord];
            string sumWords = "";
            foreach (List<string> item in availableWords)
            {
                foreach (string wordItem in item)
                {
                    sumWords += wordItem + " ";
                }
                sumWords += "\n";
            }
            Debug.Log(sumWords);
            availableWords[randLen].RemoveAt(randWord);
            Debug.Log(sumWords);
            AddWord(word);
            return word;
        }
        else
        {
            int powerUpWord = Random.Range(0, 100);
            //float powerupChance = new FallingWordManager().powerupChance;
            if (powerUpWord <= 100f * FallingWordManager.powerupChance)
            {
                int randWord = Random.Range(0, powerUpWords.Count - 1);
                string word = powerUpWords[randWord];
                powerUpWords.RemoveAt(randWord);
                AddWord(word);
                return word;
            }
            else
            {
                int randWord = Random.Range(0, trapWords.Count - 1);
                string word = trapWords[randWord];
                trapWords.RemoveAt(randWord);
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
        if (FallingWordManager.typing.theWords.ContainsKey(word)) { }
        else
        {
            FallingWordManager.typing.addTheWords(word);
        }
    }
    public void RemoveWord(string word)
    {
        Debug.Log(WordGenerator.Is(word, WordGenerator.WordType.PowerUp));
        Debug.Log(WordGenerator.Is(word, WordGenerator.WordType.Trap));
        Debug.Log(WordGenerator.Is(word, WordGenerator.WordType.Normal));
        if (WordGenerator.Is(word, WordGenerator.WordType.PowerUp) && !powerUpWords.Contains(word))
        {
            powerUpWords.Add(word);
        }
        else if (WordGenerator.Is(word, WordGenerator.WordType.Trap) && !trapWords.Contains(word))
        {
            trapWords.Add(word);
        }
        else if (WordGenerator.Is(word, WordGenerator.WordType.Normal) && !availableWords[word.Length - 3].Contains(word))
        {
            availableWords[word.Length - 3].Add(word);
        }
        FallingWordManager.typing.removeTheWords(word);
    }
}