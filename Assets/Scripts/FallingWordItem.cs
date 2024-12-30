using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FallingWordItem : MonoBehaviour
{
    public float speed = 1.0f;
    public string word = "example";
    [SerializeField] private TMP_Text text;

    public void Set(string word, float speed)
    {
        this.word = word;
        this.speed = speed;
        text.text = word;
    }
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Despawner"))
        {
            Destroy(gameObject);
            Despawn();
        }
    }
    void Despawn()
    {

    }
}
