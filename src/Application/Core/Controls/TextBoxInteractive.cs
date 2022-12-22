using System;
using System.Windows.Forms;

namespace Application.Core.Controls;

public class TextBoxInteractive : TextBox, IControlInteractive
{
    public Func<string> GetValue;

    public TextBoxInteractive(Func<string> getValue)
    {
        GetValue = getValue;
        UpdateValue();
    }

    public void UpdateValue()
    {
        Text = GetValue();
    }
}
