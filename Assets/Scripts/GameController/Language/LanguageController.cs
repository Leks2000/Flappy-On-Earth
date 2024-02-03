﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class LanguageController : MonoBehaviour
{
    public SaveData saveData;
    private int LanguageIndex;
    [SerializeField] private List<Sprite> LanguageSprite;
    [SerializeField] private Image ImageLanguage;
    [SerializeField] private List<Sprite> but1;
    [SerializeField] private Image but_1;
    [SerializeField] private List<Sprite> but2;
    [SerializeField] private Image but_2;
    [SerializeField] private List<Text> texts;

    private void Awake()
    {
        LanguageIndex = PlayerPrefs.GetInt("LanguageInd", 0);
        ImageLanguage.sprite = LanguageSprite[LanguageIndex];
        but_1.sprite = but1[LanguageIndex];
        but_2.sprite = but2[LanguageIndex];
        ChangeLanguage();
    }

    public void ChangeIndex()
    {
        LanguageIndex = (LanguageIndex == 0) ? 1 : 0;
        ChangeLanguage();
    }

    private void ChangeLanguage()
    {
        saveData.SetLanguageIndex(LanguageIndex);
        PlayerPrefs.SetInt("LanguageInd", LanguageIndex);
        ImageLanguage.sprite = LanguageSprite[LanguageIndex];
        but_1.sprite = but1[LanguageIndex];
        but_2.sprite = but2[LanguageIndex];

        string[] Russian = { "Римский воин-пёс", "Базовый Кот", "Кружка пива", "Депрессивный пёс", "Испанский войн-пёс", "Святой пёс", "Не достаточно денег", "Отсутсвует подключение к интернету. Проверьте подключение к интернету и попробуйте снова.", "Ошибка подключения", "Попробовать снова", "Сохранить", "Звук" };
        string[] English = { "Roman Dog Warrior", "Basic Cat", "Beer Mug", "Depressive dog", "Spanish dog warrior", "Holy Dog", "Not enough money", "No internet connection. Check your internet connection and try again.", "Connection error", "Try again", "Save", "Sound" };

        string[] currentLanguageTexts = (LanguageIndex == 0) ? Russian : English;
        for (int i = 0; i < texts.Count; i++)
        {
            if (i < currentLanguageTexts.Length)
            {
                texts[i].text = currentLanguageTexts[i];
            }
            else
            {
                texts[i].text = (LanguageIndex == 0) ? "Ой, отсутствует перевод" : "Woops, there is no translation";
            }
        }
    }
}