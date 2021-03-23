using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassTurn : MonoBehaviour
{
    // скрипт для пасса хода. Я знаю про Button, но у меня с ними отношения не очень

    public delegate void ChangeEnable();
    public static event ChangeEnable Enabled;

    public Text Button;

    private void Start()
    {
        Button.enabled = false;

        Enabled += () =>
        {
            Button.enabled = true;
        };
    }

    private void OnMouseDown()
    {
        TileControlle.PassTheMove();
        Button.enabled = false;
    }

    // Метод для вызова ивента из других скриптов
    public static void CallEnabled()
    {
        Enabled();
    }
}
