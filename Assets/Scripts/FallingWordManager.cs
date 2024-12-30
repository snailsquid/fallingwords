using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWordManager : MonoBehaviour
{
    public float initialSpeed = 1.0f;
    public float speedVariation = 0.1f;
    public Transform fallingWordPrefab, spawnArea;

    void InstantiateWord(WordGenerator.Theme theme)
    {
        string word = WordGenerator.GetRandomWord(theme);
        float speed = initialSpeed * (1 + Random.Range(-speedVariation, speedVariation));

        Vector2 spawnPos = new Vector2(spawnArea.localScale.x * Random.Range(-1.0f, 1.0f) * 0.5f, spawnArea.position.y);
        Transform wordInstance = Instantiate(fallingWordPrefab, spawnPos, Quaternion.identity);
        wordInstance.GetComponent<FallingWordItem>().Set(word, speed);
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
    static string[][] everydayItems = new string[][]
            {
            new string[] { "Pen", "Bag", "Key", "Cup", "Mug", "Bed", "Fan", "Box", "Lid", "Pad" }, // 3 letters
            new string[] { "Book", "Lamp", "Soap", "Comb", "Fork", "Bowl", "Chair", "Desk", "Shoe", "Glue" }, // 4 letters
            new string[] { "Plate", "Brush", "Towel", "Clock", "Frame", "Glass", "Paper", "Knife", "Phone", "Chair" }, // 5 letters
            new string[] { "Remote", "Drawer", "Cushion", "Pillow", "Basket", "Marker", "Mirror", "Charger", "Bottle", "Switch" } // 6 letters
            };

    // Foods
    static string[][] foods = new string[][]
    {
            new string[] { "Pie", "Tea", "Egg", "Nut", "Yam", "Ham", "Bun", "Dip", "Fig", "Pea" }, // 3 letters
            new string[] { "Cake", "Soup", "Milk", "Meat", "Corn", "Rice", "Bean", "Tart", "Roll", "Lamb" }, // 4 letters
            new string[] { "Bread", "Steak", "Sushi", "Pasta", "Honey", "Apple", "Chili", "Lemon", "Mango", "Bacon" }, // 5 letters
            new string[] { "Burger", "Cheese", "Cookie", "Cereal", "Orange", "Tomato", "Potato", "Peanut", "Butter", "Salad" } // 6 letters
    };

    // Animals
    static string[][] animals = new string[][]
    {
            new string[] { "Cat", "Dog", "Fox", "Bat", "Rat", "Bee", "Ant", "Owl", "Elk", "Ape" }, // 3 letters
            new string[] { "Wolf", "Lion", "Fish", "Frog", "Hawk", "Dove", "Bear", "Swan", "Crab", "Duck" }, // 4 letters
            new string[] { "Tiger", "Horse", "Snake", "Zebra", "Whale", "Shark", "Eagle", "Otter", "Lemur", "Camel" }, // 5 letters
            new string[] { "Rabbit", "Turtle", "Donkey", "Parrot", "Salmon", "Spider", "Falcon", "Giraffe", "Kitten", "Weasel" } // 6 letters
    };

    // Technology
    static string[][] technology = new string[][]
    {
            new string[] { "App", "Bit", "Fax", "Web", "URL", "CPU", "GPU", "RAM", "VPN", "GPS" }, // 3 letters
            new string[] { "Code", "Tech", "Game", "Wifi", "Sync", "Disk", "Data", "Chip", "JPEG", "HTML" }, // 4 letters
            new string[] { "Robot", "Laser", "Cloud", "Input", "Output", "Cache", "Bytes", "Modern", "Print", "Phone" }, // 5 letters
            new string[] { "Server", "Binary", "Phyton", "Laptop", "Script", "Router", "Mobile", "Syntax", "Upload", "Widget" } // 6 letters
    };

    // Geography and places
    static string[][] geography = new string[][]
    {
            new string[] { "Sea", "Bay", "USA", "UAE", "Pau", "Rio", "Lake", "City", "Cave", "Hill" }, // 3 letters
            new string[] { "West", "East", "Dune", "Pole", "Land", "Bali", "Lyon", "Pisa", "Oslo", "Rome" }, // 4 letters
            new string[] { "Paris", "Italy", "India", "Japan", "China", "River", "Beach", "Valley", "Tower", "Cliff" }, // 5 letters
            new string[] { "Canyon", "Lagoon", "Island", "Harbor", "Forest", "Brazil", "Sweden", "Canada", "Padang", "Bekasi" } // 6 letters
    };

    static readonly Dictionary<Theme, string[][]> wordBank = new Dictionary<Theme, string[][]>{
        { Theme.EverydayItems, everydayItems },
        { Theme.Foods, foods },
        { Theme.Animals, animals },
        { Theme.Technology, technology },
        { Theme.Geography, geography }
    };
    public static string GetRandomWord(Theme theme)
    {
        int randLen = Random.Range(0, 3);
        int randWord = Random.Range(0, 9);
        return wordBank[theme][randLen][randWord];
    }
}
