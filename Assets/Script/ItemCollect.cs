using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemCollect : MonoBehaviour
{
    private int point = 0;
    [SerializeField] private Text pointText;
    [SerializeField] private AudioSource Item;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Reward"))
        {
            Item.Play();
            Destroy(collision.gameObject);
            point++;
            pointText.text = ": " + point;
        }
    }
}
