using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class Voonm : MonoBehaviour
{
    public float health = 100f;
    private AudioClip audioClip;
    private string device;

    public bool Damagable = false;
    public TMP_Text messageText;
    private int healthInt;

    // Add these variables for movement

    public float speed = 2.0f;
    public float distance = 2.0f; // Distance to move left and right from the center
    private Vector3 center; // Center point

    public SpriteRenderer _spritePlayer;
    public Rigidbody2D voonm;


    void Start()
    {
        device = Microphone.devices[0];
        audioClip = Microphone.Start(device, true, 999, 44100);
        Damagable = false;
        center = transform.position;
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        //health mechanics -----------------------------
        float loudness = GetAverageVolume() * 15;
        if ((loudness > 1f) && Damagable)
        {
            health -= loudness / 100;
        }
        else if (loudness <= 1f && health < 100)
        {
            health += 0.5f * Time.deltaTime;
        }

        healthInt = (int)health;
        messageText.SetText(healthInt.ToString());

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        //boss patterns --------------------------------
        float newX = center.x + Mathf.PingPong(Time.time * speed, distance * 2) - distance;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    float GetAverageVolume()
    {
        float[] data = new float[256];
        int offset = Microphone.GetPosition(device) - 256 + 1;
        if (offset < 0)
        {
            return 0;
        }
        audioClip.GetData(data, offset);

        float a = 0;
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }

    // Modify this coroutine for continuous movement
}
