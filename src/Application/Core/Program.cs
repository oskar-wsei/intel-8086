using Application.Core.Forms;

namespace Application.Core;

public class Program
{
    public void Start()
    {
        System.Windows.Forms.Application.EnableVisualStyles();
        System.Windows.Forms.Application.Run(new AppForm());
    }

    static void Main(string[] args)
    {
        new Program().Start();
    }
}
