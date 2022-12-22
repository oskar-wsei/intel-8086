using System;
using System.Windows.Forms;

namespace Application.Core.Controls;

public class LabelInteractive : Label, IControlInteractive
{
    public Func<string> GetValue;

    public LabelInteractive(Func<string> getValue)
    {
        GetValue = getValue;
        UpdateValue();
    }

    public void UpdateValue()
    {
        Text = GetValue(); 
    }
}
