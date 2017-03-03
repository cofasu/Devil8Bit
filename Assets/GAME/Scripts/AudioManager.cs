using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource _gameAudio;
    public static Queue SOUNDCOMMAND = new Queue();
    public AudioClip sfx1;
    public AudioClip sfx2;
    public AudioClip sfx3;
    public AudioClip sfx4;
    public AudioClip sfx5;
    public AudioClip sfx6;
    public AudioClip sfx7;
    public AudioClip sfx8;
    void Awake()
    {
        _gameAudio = GetComponent<AudioSource>();

        //example
        SOUNDCOMMAND.Enqueue("Play");
    }
    void FixedUpdate()
    {
        if (SOUNDCOMMAND.Count > 0)
        {
            string tempString = (string)SOUNDCOMMAND.Dequeue();

            switch (tempString)
            {
                case "":
                    break;
                case "snd1":
                    {
                        _gameAudio.PlayOneShot(sfx1);

                        break;
                    }
                case "snd2":
                    {
                        _gameAudio.PlayOneShot(sfx2);

                        break;
                    }
                case "snd3":
                    {
                        _gameAudio.PlayOneShot(sfx3);

                        break;
                    }
                case "snd4":
                    {
                        _gameAudio.PlayOneShot(sfx4);

                        break;
                    }
                case "stop":
                    {
                        _gameAudio.Stop();
                        break;
                    }
                case "Play":
                    {
                        _gameAudio.Play();
                        break;
                    }
                default:
                    break;
            }
        }
    }

}

