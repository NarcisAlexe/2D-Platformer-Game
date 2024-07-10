using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGround : MonoBehaviour
{
    [Header("To Disappear")]
    [SerializeField] private GameObject[] groundToDisappear;

    [Header("Key")]
    [SerializeField] private GameObject key;

    [Header("Audio Parameters")]
    [SerializeField] private AudioClip sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(sound);

        if (collision.tag == "Player")
        {
            //Disappear ground
            for (int i = 0; i < groundToDisappear.Length; i++)
            {
                groundToDisappear[i].SetActive(false);
                key.SetActive(true);
            }
        }
    }
}
