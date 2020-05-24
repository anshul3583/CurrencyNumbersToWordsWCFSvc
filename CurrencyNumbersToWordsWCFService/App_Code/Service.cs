using System;
using System.Linq;

public class Service : IService
{
    public CurrencyNumbersToWords ConvertCurrencyNumbersToWords(string currencyNumber)
    {
        CurrencyNumbersToWords response = new CurrencyNumbersToWords();
        string isNegative = "";
        try
        {
            string number = currencyNumber.Replace(',', '.').Trim().Replace(" ", "");

            if (number.Contains('.'))
            {
                number = Convert.ToDouble(number).ToString(".00");
            }
            else
            {
                number = Convert.ToDouble(number).ToString();
            }

            if (number.Contains("-"))
            {
                isNegative = "Minus ";
                number = number.Substring(1, number.Length - 1);
            }
            if (number == "0")
            {
                response.Result = "zero dollar";
            }
            else
            {
                response.Result = (isNegative + ConvertToWords(number)).ToLower();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return response;
    }


    private string ConvertWholeNumber(string Number)
    {
        string word = "";
        try
        {
            bool beginsZero = false;
            bool isDone = false;
            double dblAmt = (Convert.ToDouble(Number));

            if (dblAmt > 0)
            {
                beginsZero = Number.StartsWith("0");

                int numDigits = Number.Length;
                int pos = 0;
                string place = "";
                switch (numDigits)
                {
                    case 1: //ones' range    

                        word = ones(Number);
                        isDone = true;
                        break;
                    case 2: //tens' range    
                        word = tens(Number);
                        isDone = true;
                        break;
                    case 3: //hundreds' range    
                        pos = (numDigits % 3) + 1;
                        place = " Hundred ";
                        break;
                    case 4: //thousands' range    
                    case 5:
                    case 6:
                        pos = (numDigits % 4) + 1;
                        place = " Thousand ";
                        break;
                    case 7: //millions' range    
                    case 8:
                    case 9:
                        pos = (numDigits % 7) + 1;
                        place = " Million ";
                        break;
                    case 10: //Billions's range    
                    case 11:
                    case 12:

                        pos = (numDigits % 10) + 1;
                        place = " Billion ";
                        break;
                    default:
                        isDone = true;
                        break;
                }
                if (!isDone)
                {
                    if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                    {
                        try
                        {
                            word = ConvertWholeNumber(Number.Substring(0, pos)) + place + ConvertWholeNumber(Number.Substring(pos));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                    else
                    {
                        word = ConvertWholeNumber(Number.Substring(0, pos)) + ConvertWholeNumber(Number.Substring(pos));
                    }
                }
                //ignore digit grouping names    
                if (word.Trim().Equals(place.Trim())) word = "";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return word.Trim();
    }

    private string tens(string Number)
    {
        int _Number = Convert.ToInt32(Number);
        string name = null;
        switch (_Number)
        {
            case 10:
                name = "Ten";
                break;
            case 11:
                name = "Eleven";
                break;
            case 12:
                name = "Twelve";
                break;
            case 13:
                name = "Thirteen";
                break;
            case 14:
                name = "Fourteen";
                break;
            case 15:
                name = "Fifteen";
                break;
            case 16:
                name = "Sixteen";
                break;
            case 17:
                name = "Seventeen";
                break;
            case 18:
                name = "Eighteen";
                break;
            case 19:
                name = "Nineteen";
                break;
            case 20:
                name = "Twenty";
                break;
            case 30:
                name = "Thirty";
                break;
            case 40:
                name = "Fourty";
                break;
            case 50:
                name = "Fifty";
                break;
            case 60:
                name = "Sixty";
                break;
            case 70:
                name = "Seventy";
                break;
            case 80:
                name = "Eighty";
                break;
            case 90:
                name = "Ninety";
                break;
            default:
                if (_Number > 0)
                {
                    var val = tens(Number.Substring(0, 1) + "0");
                    if (string.IsNullOrEmpty(val))
                    {
                        name = ones(Number.Substring(1));
                    }
                    else
                    {
                        name = val + "-" + ones(Number.Substring(1));
                    }

                }
                break;
        }
        return name;
    }

    private string ones(string Number)
    {
        int _number = Convert.ToInt32(Number);
        string name = "";
        switch (_number)
        {

            case 1:
                name = "One";
                break;
            case 2:
                name = "Two";
                break;
            case 3:
                name = "Three";
                break;
            case 4:
                name = "Four";
                break;
            case 5:
                name = "Five";
                break;
            case 6:
                name = "Six";
                break;
            case 7:
                name = "Seven";
                break;
            case 8:
                name = "Eight";
                break;
            case 9:
                name = "Nine";
                break;
        }
        return name;
    }

    // Convert Decimal
    private string ConvertToWords(string numb)
    {
        string val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
        string endStr = "dollars";
        if (numb == "1")
        {
            endStr = "dollar";
        }

        try
        {
            int decimalPlace = numb.IndexOf(".");
            if (decimalPlace > 0)
            {
                wholeNo = numb.Substring(0, decimalPlace);
                points = numb.Substring(decimalPlace + 1);
                if (Convert.ToInt32(points) > 0)
                {
                    andStr = endStr + " and "; // separate whole numbers from points/cents    
                    endStr = "cents";//Cents    
                    pointStr = ConvertDecimals(points);
                }
            }
            val = String.Format("{0} {1}{2}{3}", ConvertWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return val;
    }

    private string ConvertDecimals(string number)
    {
        var output = ConvertCurrencyNumbersToWords(number);
        return output.Result.Replace("dollars", "");
    }
}
