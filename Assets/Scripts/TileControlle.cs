using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileControlle : MonoBehaviour
{
    // Скрипт для игрового поля. Такой скрипт висит на каждой клетке

    //Ивент для передачи хода
    public delegate void Turn();
    public static Turn PassTheMove;

    //Все клетки
    static TileControlle[] tiles;

    //Пешка котрая стоит на этой клетке
    [SerializeField]
    public CheckerControlle checker;

    // Списки необходимых клеток. Близлежащие, по диагонали через клетку и т.д.
    [SerializeField]
    List<TileControlle> contiguousTile;
    [SerializeField]
    List<TileControlle> diagonalTile;
    [SerializeField]
    List<TileControlle> additionalDiagonalsTile;
    [SerializeField]
    List<TileControlle> additionalContiguousTile;

    // Для режима игры с диагональными прыжками
    bool diagonal;

    private void Start()
    {
        if (PlayerPrefs.HasKey("diagonal"))
        {
            diagonal = System.Convert.ToBoolean(PlayerPrefs.GetString("diagonal"));
        }

        AddTile();

    }

    // Если пешка выбрана, то вызывется этот метод
    private void OnMouseDown()
    {
        if (checker == null)
        {
            CheckList();
            CheckJump();
        }
    }


    // Метод проверяющий близлежащие Клетки и клетки по диагонали
    CheckerControlle CheckList ()
    {
        if (diagonal)
        {
            foreach (TileControlle ch in additionalDiagonalsTile)
            {
                if (ch.checker != null)
                {
                    if (ch.checker.Choose)
                    {
                        foreach (TileControlle tcTwo in ch.diagonalTile)
                        {
                            foreach (TileControlle tcThree in diagonalTile)
                            {
                                if (tcTwo == tcThree && tcTwo.checker != null)
                                {
                                    ch.checker.Choose = false;
                                    ch.checker.transform.position = new Vector3(transform.position.x, transform.position.y);
                                    checker = ch.checker;
                                    CheckerControlle chTwo = ch.checker;
                                    ch.checker = null;
                                    PassTurn.CallEnabled();

                                    return chTwo;
                                }
                            }
                        }
                    }
                }
            }
            
        }

        foreach (TileControlle ch in contiguousTile)
        {
            if (ch.checker != null)
            {
                if (ch.checker.Choose)
                {
                    ch.checker.Choose = false;
                    ch.checker.transform.position = new Vector3(transform.position.x, transform.position.y);
                    checker = ch.checker;
                    CheckerControlle chTwo = ch.checker;
                    ch.checker = null;

                    PassTheMove();

                    return chTwo;
                }
            }
        }

        return new CheckerControlle();
    }

    // Метод проверящий клетки для перепрыгивания НЕ по диагонали
    CheckerControlle CheckJump ()
    {
        foreach (TileControlle tc in additionalContiguousTile)
        {
            if (tc.checker != null)
            {
                if (tc.checker.Choose)
                {
                    foreach (TileControlle tcTwo in tc.contiguousTile)
                    {
                        foreach (TileControlle tcThree in contiguousTile)
                        {
                            if (tcTwo == tcThree && tcTwo.checker != null)
                            {
                                tc.checker.Choose = false;
                                tc.checker.transform.position = new Vector3(transform.position.x, transform.position.y);
                                checker = tc.checker;
                                CheckerControlle chTwo = tc.checker;
                                tc.checker = null;

                                PassTurn.CallEnabled();

                                return chTwo;
                            }
                        }
                    }
                }
            }
        }
        return null;
    }

    // Добавление находящихся рядом клеток в нужные списки. Как говорится "Дьявол плакал и кололся". Но это всё равно лучше чем в ручную добавлять
    // Хотя такого количества проверок я не делал даже когда учился. Да, можно было прогнать через switch, но я его не очень люблю
    void AddTile ()
    {
        int i = int.Parse(name);
        tiles = FindObjectsOfType<TileControlle>();
        foreach (TileControlle tile in tiles)
        {
            if (i + 8 == int.Parse(tile.name))
            {
                contiguousTile.Add(tile);
            }
            if (i - 8 == int.Parse(tile.name))
            {
                contiguousTile.Add(tile);
            }
            if (i - 1 == int.Parse(tile.name) && transform.position.x > tile.transform.position.x)
            {
                contiguousTile.Add(tile);
            }
            if (i + 1 == int.Parse(tile.name) && transform.position.x < tile.transform.position.x)
            {
                contiguousTile.Add(tile);
            }
            if (i + 16 == int.Parse(tile.name))
            {
                additionalContiguousTile.Add(tile);
            }
            if (i - 16 == int.Parse(tile.name))
            {
                additionalContiguousTile.Add(tile);
            }
            if (i + 2 == int.Parse(tile.name) && transform.position.x < tile.transform.position.x)
            {
                additionalContiguousTile.Add(tile);
            }
            if (i - 2 == int.Parse(tile.name) && transform.position.x > tile.transform.position.x)
            {
                additionalContiguousTile.Add(tile);
            }


            if (diagonal)
            {
                if (i + 9 == int.Parse(tile.name) && transform.position.x < tile.transform.position.x)
                {
                    diagonalTile.Add(tile);
                }
                if (i - 9 == int.Parse(tile.name) && transform.position.x > tile.transform.position.x)
                {
                    diagonalTile.Add(tile);
                }
                if (i + 7 == int.Parse(tile.name) && transform.position.x > tile.transform.position.x)
                {
                    diagonalTile.Add(tile);
                }
                if (i - 7 == int.Parse(tile.name) && transform.position.x < tile.transform.position.x)
                {
                    diagonalTile.Add(tile);
                }


                if (i + 18 == int.Parse(tile.name) && transform.position.x < tile.transform.position.x)
                {
                    additionalDiagonalsTile.Add(tile);
                }
                if (i - 18 == int.Parse(tile.name) && transform.position.x > tile.transform.position.x)
                {
                    additionalDiagonalsTile.Add(tile);
                }
                if (i + 14 == int.Parse(tile.name) && transform.position.x > tile.transform.position.x)
                {
                    additionalDiagonalsTile.Add(tile);
                }
                if (i - 14 == int.Parse(tile.name) && transform.position.x < tile.transform.position.x)
                {
                    additionalDiagonalsTile.Add(tile);
                }
            }
        }
    }
}
