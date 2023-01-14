using CalculateFromText;
using Microsoft.AspNetCore.Components;

namespace SummingCalculator.Pages
{
    public partial class Index
    {
        private string TotalSum = "0.00";
        private string InputValue = "";
        private string CurrentValue = "";
        private string RunningSum = "";
        private enum Operator
        {
            Add,
            Subtract,
            Divide,
            Multiply
        }

        private int _prevInputLenght = 0;
        private void OnInputTextChanged(ChangeEventArgs args)
        {
            try
            {
                CurrentValue = "";
                RunningSum = "";
                if (args.Value == null)
                    return;

                var input = args.Value.ToString();
                if (input == null)
                    return;

                var inputLenght = input.Length;

                var numExtractor = new NumberExtractor();

                var lines = input.Split(new char[] { '\n', '\r' });

                double sum = 0;
                foreach (var line in lines)
                {
                    var val = numExtractor.GetNumberFromLine(line);
                    if (val < 0.0000001 && val > -0.0000001)
                    {
                        CurrentValue += Environment.NewLine;
                        RunningSum += Environment.NewLine;
                        continue;
                    }


                    if (line.Contains('/'))
                    {
                        sum /= val;
                        CurrentValue += $"/ {val:# ### ##0.00}" + Environment.NewLine;
                    }
                    else if (line.Contains('*'))
                    {
                        sum *= val;
                        CurrentValue += $"* {val:# ### ##0.00}" + Environment.NewLine;
                    }
                    else if (line.Contains('-'))
                    {
                        sum -= val;
                        CurrentValue += $"- {val:# ### ##0.00}" + Environment.NewLine;
                    }
                    else
                    {
                        sum += val;
                        CurrentValue += $"  {val:# ### ##0.00}" + Environment.NewLine;
                    }

                    RunningSum += $"{sum:# ### ##0.00}" + Environment.NewLine;
                }

                TotalSum = $"{sum:# ### ##0.00}";

                var diffLenght = inputLenght - _prevInputLenght;
                if (diffLenght > 1 && lines.Length > 0 && !string.IsNullOrWhiteSpace(lines[lines.Length - 1]))
                {
                    InputValue = input + Environment.NewLine;
                    inputLenght++;
                    StateHasChanged();
                    //args.Value = input + Environment.NewLine;
                }

                _prevInputLenght = inputLenght;
            }
            catch (Exception ex)
            {
                TotalSum = $"Error: {ex.Message}";
            }
        }
    }
}
