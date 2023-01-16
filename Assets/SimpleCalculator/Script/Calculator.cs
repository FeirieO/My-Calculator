using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    #region Fields
    public TextMeshProUGUI _input;
    public TextMeshProUGUI _result;

    //private float result;
    private float input;
    private float input2;
    private string operators;

    #endregion Fields

    #region Methods
    //Initialization
    private void Start()
    {
        ResetAll();
    }

    //Handle values coming from button clicks
    public void SetInputValue(string s)
    {
        //Clear fields if _input contains letters
        if (_input.text.ToString().Any(x => char.IsLetter(x)))
        {
            ResetAll();
        }

        //Handles button input values
        switch (s)
        {
            case "+":
            case "-":
            case "/":
            case "*":
            case "=":
                Calculate(s);
                break;
            case "c":
                ResetAll();
                break;
            default:
                AddCharacter(s);
                break;
        }
    }

    //Prepare values before the calculation
    private void Calculate(string s)
    {
        //If result field is empty
        if (_result.text == "" && s != "=")
        {
            input = Convert.ToSingle(_input.text);
            operators = s;
            _result.text = _input.text + s;
            _input.text = "|";
            return;
        }

        //Handles division by 0
        if (operators == "/" && _input.text == "|")
        {
            ResetAll();
            _input.text = "Cannot divide by zero";
            return;
        }

        //Calculates float result
        input2 = Convert.ToSingle(_input.text);

        if (s == "=")
        {
            ClearField();
            _input.text = CalculatePair(input, input2, operators).ToString();
        }
        else
        {
            input = Convert.ToSingle(CalculatePair(input, input2, operators));
            _result.text = input.ToString() + " " + s;
            operators = s;
            _input.text = "|";
        }
    }

    //Return the result of an operation between 2 floats
    private float CalculatePair(float x, float y, string op)
    {
        float result = 0.0f;

        switch (op)
        {
            case "+":
                result = x + y;
                break;
            case "-":
                result = x - y;
                break;
            case "*":
                result = x * y;
                break;
            case "/":
                result = x / y;
                break;
        }

        return result;
    }

    //Add character input to input text string
    private void AddCharacter(string s)
    {
        //Add only one decimal point
        if (s == ".")
        {
            if (!_input.text.ToString().Contains("."))
            {
                _input.text += s;
            }
            //Remove 0 in front of number
        }
        else if (_input.text == "|")
        {
            _input.text = s;
            //Concatenate the input string
        }
        else
        {
            _input.text += s;
        }
    }

    //Clear all fields when AC is pressed
    public void ClearField()
    {
        _result.text = "";
        _input.text = "|";
    }

    //Reset fields and member variables
    public void ResetAll()
    {
        ClearField();
        input = 0.0f;
        input2 = 0.0f;
        operators = "";
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _input.text = "|";
            yield return new WaitForSeconds(0.35f);
            _input.text = "";
            yield return new WaitForSeconds(0.35f);
        }
    }
}

    #endregion Methods

    #region Events

    #endregion Events

