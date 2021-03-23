using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerControlle : MonoBehaviour
{

    //Скрипт для пешек

    public static CheckerControlle[] Checkers;

    // Цвет пешки
    public enum Color
    {
        White,
        Black
    }

    public Color CheckerColor;

    
    //Указывает на выбранную пешка
    public bool Choose;

    CheckerControlle controller;

    private void Start()
    {
        controller = GetComponent<CheckerControlle>();
        Checkers = FindObjectsOfType<CheckerControlle>();
    }

    //При тапе на пешку она выбирается (если сейчас соответсвующий ход)
    private void OnMouseDown()
    {
        if (MainController.Turn == MainController.WhoTurn.White && CheckerColor == Color.White)
        {
            foreach (CheckerControlle ch in Checkers)
            {
                if (ch != controller)
                {
                    ch.Choose = false;
                }
                else
                {
                    Choose = true;
                }
            }
        }
        else if(MainController.Turn == MainController.WhoTurn.Black && CheckerColor == Color.Black)
        {
            foreach (CheckerControlle ch in Checkers)
            {
                if (ch != controller)
                {
                    ch.Choose = false;
                }
                else
                {
                    Choose = true;
                }
            }
        }
    }
}
