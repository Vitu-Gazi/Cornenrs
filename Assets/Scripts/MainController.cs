using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    // Этот скрипт нужен для контроля ходов, и некоторого дополнительного управления ходом игры

    // Отслеживание хода игрока
    public enum WhoTurn
    {
        White,
        Black
    };
    public static WhoTurn Turn;


    // Клетки с которых начинают ходить белые и чёрные
    [SerializeField]
    List<TileControlle> whiteTile, blackTile;




    // Количество проделанных ходов
    static int turnNumber = 0;

    // Для вывода информации чей ход и сколько было сделано ходов
    [SerializeField]
    Text text, turnText;

    // Нужно для отключения баттона. Хотя можно юыло сделать через ивент, но я делал это в три часа ночи
    [SerializeField]
    PassTurn pass;

    private void Start()
    {
        Turn = WhoTurn.White;

        // Привязка к событию
        TileControlle.PassTheMove += () =>
        {
            pass.Button.enabled = false;


            Win();


            if (Turn == WhoTurn.White)
            {
                Turn = WhoTurn.Black;
                text.text = "Black";
                if (turnNumber == 80)
                {
                    text.text = "Ничья";
                    Destroy(GetComponent<MainController>());
                }
            }
            else if (Turn == WhoTurn.Black)
            {
                Turn = WhoTurn.White;
                turnNumber++;
                text.text = "White";
                
            }
            turnText.text = turnNumber.ToString();
        }; 
    }

    // Скрипт победы
    void Win()
    {
        int score = 0;
        foreach (TileControlle tl in whiteTile)
        {
            if (tl.checker != null)
            {
                if (tl.checker.CheckerColor == CheckerControlle.Color.Black)
                {
                    score++;
                }
            }
        }
        if (score == 9)
        {
            text.text = "Black Win";
        }
        else
        {
            score = 0;
            foreach (TileControlle tl in blackTile)
            {
                if (tl.checker != null)
                {
                    if (tl.checker.CheckerColor == CheckerControlle.Color.White)
                    {
                        score++;
                    }
                }
            }
            if (score == 9)
            {
                text.text = "White Win";
            }
        }
    }

}
