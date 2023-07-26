using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ComponentManagerBusiness : MonoBehaviour
{
    [SerializeField]
    public GameObject Pos;
    [SerializeField]
    public GameObject Cash;
    [SerializeField]
    public GameObject Strisciata;
    [SerializeField]
    public TMP_Text WhiteC;
    [SerializeField]
    public TMP_Text BlackC;
    [SerializeField]
    public TMP_Text BlackPerc;
    [SerializeField]
    public TMP_Text MintFor;


    private void Update()
    {
        SetWhiteC();
        SetBlackC();
        SetBlackPerc();
        SetMintFor();
    }

    private void SetWhiteC()
    {
        double.TryParse(ResetComma(Strisciata.GetComponent<TMP_InputField>().text), out double strisciataValue);
        double.TryParse(ResetComma(Pos.GetComponent<TMP_InputField>().text), out double posValue);

        var whiteCcontrol = string.Empty;
        if ((strisciataValue - posValue) < 0)
        {
            whiteCcontrol = "Err Bat Pos";
        }
        if ((strisciataValue - posValue) == 0)
        {
            whiteCcontrol = "Bat Solo Pos";

        }

        WhiteC.text = $"{(strisciataValue - posValue).ToString("0.##")} {whiteCcontrol}";
    }
    private void SetBlackC()
    {
        double.TryParse(ResetComma(Cash.GetComponent<TMP_InputField>().text), out double cashValue);
        double.TryParse(ResetComma(WhiteC.text), out double whiteCValue);

        var blackCcontrol = string.Empty;
        var result = (cashValue - whiteCValue).ToString("0.##");
        if ((cashValue - whiteCValue) < 0)
        {
            blackCcontrol = "Battuto più di incassato";
        }
        if ((cashValue - whiteCValue) == 0)
        {
            blackCcontrol = "Battuto tutto";

        }
        BlackC.text = $"{result} {blackCcontrol}";
    }
    private void SetBlackPerc()
    {
        double.TryParse(ResetComma(Cash.GetComponent<TMP_InputField>().text), out double cashValue);
        double.TryParse(ResetComma(Strisciata.GetComponent<TMP_InputField>().text), out double strisciataValue);
        double.TryParse(ResetComma(Pos.GetComponent<TMP_InputField>().text), out double posValue);

        BlackPerc.text = (100 * (1 - (strisciataValue / (posValue + cashValue)))).ToString("0.##");
    }
    private void SetMintFor()
    {
        double.TryParse(ResetComma(Cash.GetComponent<TMP_InputField>().text), out double cashValue);
        double.TryParse(ResetComma(Strisciata.GetComponent<TMP_InputField>().text), out double strisciataValue);
        double.TryParse(ResetComma(Pos.GetComponent<TMP_InputField>().text), out double posValue);

        var result = ((0.7 * (posValue + cashValue)) - strisciataValue).ToString("0.##");

        var mintForControl = string.Empty;

        if(((0.7 * (posValue + cashValue)) - strisciataValue) < 0)
        {
            mintForControl = "Battuto Troppo";
        }
        if (((0.7 * (posValue + cashValue)) - strisciataValue) == 0)
        {
            mintForControl = "Battuto Correttamente";
        }

        MintFor.text = $"{result} {mintForControl}";
    }

    private string ResetComma(string toReplace)
    {
        if (toReplace.Contains("."))
            return toReplace.Replace(".", ",");
        return toReplace;
    }

}
