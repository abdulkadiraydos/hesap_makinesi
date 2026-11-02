namespace hesap_makinesi;

public partial class StandardPage : ContentPage
{
    double _firstNumber = 0;
    double _secondNumber = 0;
    string _operator = "";
    bool _isOperatorPressed = false;

    public StandardPage()
    {
        InitializeComponent();
    }

  
    private void OnNumberClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string pressed = button.Text;

        if (CalcDisplay.Text == "0" || _isOperatorPressed)
        {
            CalcDisplay.Text = pressed;
            _isOperatorPressed = false;
        }
        else
        {
            CalcDisplay.Text += pressed;
        }
    }

   
    private void OnDecimalClicked(object sender, EventArgs e)
    {
        if (_isOperatorPressed)
        {
            CalcDisplay.Text = "0.";
            _isOperatorPressed = false;
            return;
        }

        if (!CalcDisplay.Text.Contains("."))
        {
            CalcDisplay.Text += ".";
        }
    }

    
    private void OnOperatorClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        // Önceki işlemi hafızada tut
        if (double.TryParse(CalcDisplay.Text, out double result))
        {
            _firstNumber = result;
        }

        _operator = button.Text;
        _isOperatorPressed = true;
    }

   
    private void OnCalculateClicked(object sender, EventArgs e)
    {
        if (double.TryParse(CalcDisplay.Text, out double secondNumber))
        {
            _secondNumber = secondNumber;
        }

        double finalResult = 0;

        try
        {
            switch (_operator)
            {
                case "+":
                    finalResult = _firstNumber + _secondNumber;
                    break;
                case "-":
                    finalResult = _firstNumber - _secondNumber;
                    break;
                case "×":
                    finalResult = _firstNumber * _secondNumber;
                    break;
                case "÷":
                   
                    if (_secondNumber == 0)
                    {
                        DisplayAlert("Hata", "Sıfıra bölünemez!", "Tamam");
                        CalcDisplay.Text = "0";
                        return;
                    }
                    finalResult = _firstNumber / _secondNumber;
                    break;
            }

            CalcDisplay.Text = finalResult.ToString();
            _isOperatorPressed = true;
        }
        catch (Exception)
        {
            DisplayAlert("Hata", "Geçersiz işlem!", "Tamam");
            CalcDisplay.Text = "0";
        }
    }

    
    private void OnClearClicked(object sender, EventArgs e)
    {
        CalcDisplay.Text = "0";
        _firstNumber = 0;
        _secondNumber = 0;
        _operator = "";
        _isOperatorPressed = false;
    }

    
    private void OnClearEntryClicked(object sender, EventArgs e)
    {
        CalcDisplay.Text = "0";
    }

    
    private void OnBackSpaceClicked(object sender, EventArgs e)
    {
        string text = CalcDisplay.Text;
        if (text.Length > 1)
        {
            CalcDisplay.Text = text.Substring(0, text.Length - 1);
        }
        else
        {
            CalcDisplay.Text = "0";
        }
    }

    
    private void OnPlusMinusClicked(object sender, EventArgs e)
    {
        if (double.TryParse(CalcDisplay.Text, out double value))
        {
            value = -value;
            CalcDisplay.Text = value.ToString();
        }
    }
}
