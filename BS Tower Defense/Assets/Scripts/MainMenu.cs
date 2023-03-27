using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    /* Options menu references from 603 game 2 */

    private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource2;

    [SerializeField] private AudioClip _buttonSFX;

    [SerializeField] private Scrollbar _optionsSlider;

    [SerializeField] private GameObject _mainMenu;

    [SerializeField] private GameObject _optionMenu;

    [SerializeField] private GameObject _miscMenu;


    private void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        audioSource2.clip = _buttonSFX;
        audioSource2.Play();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();

        audioSource2.clip = _buttonSFX;
        audioSource2.Play();
    }


    public void OnOptionSliderChange()
    {
        AudioListener.volume = _optionsSlider.value;
    }

    public void OnButtonOptionClicked()
    {
        _mainMenu.SetActive(!_mainMenu.activeInHierarchy);
        _miscMenu.SetActive(!_miscMenu.activeInHierarchy);
        _optionMenu.SetActive(!_optionMenu.activeInHierarchy);

        audioSource2.clip = _buttonSFX;
        audioSource2.Play();
    }

}
