namespace hesap_makinesi;

public partial class ScientificPage : ContentPage
{
    double _firstNumber = 0;
    double _secondNumber = 0;
    string _operator = "";
    bool _isOperatorPressed = false;
    bool _isDegreeMode = true; 

    public ScientificPage()
    {
        InitializeComponent();
    }

    private void OnAngleModeClicked(object sender, EventArgs e)
    {
        _isDegreeMode = !_isDegreeMode;
        BtnAngleMode.Text = _isDegreeMode ? "DEG" : "RAD";
    }

    private void OnNumberClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        if (SciDisplay.Text == "0" || _isOperatorPressed)
        {
            SciDisplay.Text = button.Text;
            _isOperatorPressed = false;
        }
        else
        {
            SciDisplay.Text += button.Text;
        }
    }

    private void OnDecimalClicked(object sender, EventArgs e)
    {
        if (_isOperatorPressed)
        {
            SciDisplay.Text = "0.";
            _isOperatorPressed = false;
            return;
        }
        if (!SciDisplay.Text.Contains("."))
        {
            SciDisplay.Text += ".";
        }
    }

    private void OnOperatorClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        if (double.TryParse(SciDisplay.Text, out double result))
        {
            _firstNumber = result;
        }
        _operator = button.Text;
        _isOperatorPressed = true;
    }

    private void OnConstantClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        if (button.Text == "π") SciDisplay.Text = Math.PI.ToString();
        else if (button.Text == "e") SciDisplay.Text = Math.E.ToString();
        _isOperatorPressed = true;
    }

    
    private void OnTrigClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        if (double.TryParse(SciDisplay.Text, out double value))
        {
            double result = 0;
            string func = button.Text;
            double angleInRadians = _isDegreeMode ? (value * Math.PI / 180.0) : value;

            if (func == "sin") result = Math.Sin(angleInRadians);
            else if (func == "cos") result = Math.Cos(angleInRadians);
            else if (func == "tan") result = Math.Tan(angleInRadians);
            else if (func == "cot")
            {
                double tanVal = Math.Tan(angleInRadians);
                if (tanVal == 0) { DisplayAlert("Hata", "Kotanjant tanımlı değil (Sıfıra bölünme)!", "Tamam"); return; }
                result = 1 / tanVal; // Cot(x) = 1 / Tan(x)
            }

            SciDisplay.Text = result.ToString();
            _isOperatorPressed = true;
        }
    }

    private void OnScientificFunctionClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        if (double.TryParse(SciDisplay.Text, out double value))
        {
            double result = 0;
            string func = button.Text;

            if (func == "√")
            {
                if (value < 0) { DisplayAlert("Hata", "Negatif sayının karekökü alınamaz!", "Tamam"); return; }
                result = Math.Sqrt(value);
            }
            else if (func == "∛x") result = Math.Cbrt(value);
            else if (func == "x²") result = Math.Pow(value, 2);
            else if (func == "1/x")
            {
                if (value == 0) { DisplayAlert("Hata", "Sıfıra bölünemez!", "Tamam"); return; }
                result = 1 / value;
            }
            else if (func == "|x|") result = Math.Abs(value);
            else if (func == "log")
            {
                if (value <= 0) { DisplayAlert("Hata", "Logaritma sıfır veya negatif olamaz!", "Tamam"); return; }
                result = Math.Log10(value);
            }
            else if (func == "ln")
            {
                if (value <= 0) { DisplayAlert("Hata", "Doğal logaritma sıfır veya negatif olamaz!", "Tamam"); return; }
                result = Math.Log(value);
            }
            else if (func == "10^x") result = Math.Pow(10, value);
            else if (func == "n!") result = Factorial((int)value);

            SciDisplay.Text = result.ToString();
            _isOperatorPressed = true;
        }
    }

    private double Factorial(int n)
    {
        if (n < 0) return 0;
        if (n == 0 || n == 1) return 1;
        double fact = 1;
        for (int i = 2; i <= n; i++) fact *= i;
        return fact;
    }

    private void OnCalculateClicked(object sender, EventArgs e)
    {
        if (double.TryParse(SciDisplay.Text, out double secondNumber))
        {
            _secondNumber = secondNumber;
        }

        double finalResult = 0;
        try
        {
            switch (_operator)
            {
                case "+": finalResult = _firstNumber + _secondNumber; break;
                case "-": finalResult = _firstNumber - _secondNumber; break;
                case "×": finalResult = _firstNumber * _secondNumber; break;
                case "÷":
                    if (_secondNumber == 0)
                    {
                        DisplayAlert("Hata", "Sıfıra bölünemez!", "Tamam");
                        SciDisplay.Text = "0";
                        return;
                    }
                    finalResult = _firstNumber / _secondNumber;
                    break;
                case "x^y": finalResult = Math.Pow(_firstNumber, _secondNumber); break;
                case "mod": finalResult = _firstNumber % _secondNumber; break;
                case "exp": finalResult = _firstNumber * Math.Pow(10, _secondNumber); break;
            }
            SciDisplay.Text = finalResult.ToString();
            _isOperatorPressed = true;
        }
        catch (Exception)
        {
            DisplayAlert("Hata", "Geçersiz işlem!", "Tamam");
            SciDisplay.Text = "0";
        }
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        SciDisplay.Text = "0";
        _firstNumber = 0;
        _secondNumber = 0;
        _operator = "";
        _isOperatorPressed = false;
    }

    private void OnBackSpaceClicked(object sender, EventArgs e)
    {
        string text = SciDisplay.Text;
        if (text.Length > 1) SciDisplay.Text = text.Substring(0, text.Length - 1);
        else SciDisplay.Text = "0";
    }

    private void OnPlusMinusClicked(object sender, EventArgs e)
    {
        if (double.TryParse(SciDisplay.Text, out double val))
            SciDisplay.Text = (-val).ToString();
    }
}