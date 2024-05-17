using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip button_Au;
    [SerializeField] AudioSource AudioSource_effect;
    [SerializeField] List<GameObject> CanvasList;


    List<Button> Button_list = new List<Button>();
    void OnEnable()
    {
        Button_list.AddRange(FindObjectsOfType<Button>());
        for (int i = 0; i < Button_list.Count; i++)
        {
            Button_list[i].onClick.AddListener(() => Buzz());
        }
        for (int i = 1; i < CanvasList.Count; i++)
        {
            CanvasList[i].SetActive(false);
        }

    }
    void Buzz()
    {
        AudioSource_effect.PlayOneShot(button_Au);
    }
}
