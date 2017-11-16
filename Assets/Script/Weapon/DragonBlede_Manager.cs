using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonBlede_Manager : MonoBehaviour
{
    public GameObject DragonBlede;
    public GameObject Model;
    private float Durability;
    private float DamageDegree;
    private float Reply;
    public Slider DurabilitySlider;
    private SteamVR_TrackedObject TrackdeObject;
    public AudioSource AudioSource;
    public AudioClip[] SwordAudio;

    // Use this for initialization
    void Start ()
	{
	    TrackdeObject = this.transform.parent.GetComponent<SteamVR_TrackedObject>();
        Durability = 100.0f;
	    Reply = 8.0f;
	    DamageDegree = 10.0f;
	    DurabilitySlider.value = DurabilitySlider.maxValue = Durability;
	    AudioSource = GetComponent<AudioSource>();
	}

    void Update()
    {
        Fracture();
    }

    void OnCollisionEnter(Collision other)
    {
        var device = SteamVR_Controller.Input((int)TrackdeObject.index);
        if (other.gameObject.tag == "Enemy")
        {
            Durability -= DamageDegree;
            DurabilitySlider.value = Durability;
            device.TriggerHapticPulse(3000);
            int n = Random.Range(1, SwordAudio.Length);
            AudioSource.clip = SwordAudio[n];
            AudioSource.PlayOneShot(SwordAudio[n]);
            SwordAudio[n] = SwordAudio[0];
            SwordAudio[0] = AudioSource.clip;
        }
    }

    void Fracture()
    {
        if (Durability <= 0)
        {
            DragonBlede.SetActive(false);
            Model.SetActive(true);
        }

        if (Durability < 100)
        {
            Durability += Reply * Time.deltaTime;
        }
    }

    public void SetNumber(int Number)
    {
        this.Durability = Number;
    }
}
