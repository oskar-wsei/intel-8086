namespace Application.Core.Misc;

public class MemoryView
{
    public readonly int Size;
    public readonly int Rows;
    public readonly int BytesPerRow;
    public readonly int BytesPerPage;
    public readonly int Pages;

    public int Page { get; private set; } = 0;

    public MemoryView(int size, int rows = 16, int bytesPerRow = 16)
    {
        Size = size;
        Rows = rows;
        BytesPerRow = bytesPerRow;
        BytesPerPage = Rows * BytesPerRow;
        Pages = Size / BytesPerPage;
    }

    public void GoTo(int page)
    {
        Page = page % Pages;

        while (Page < 0)
        {
            Page = Pages + Page;
        }
    }

    public void PreviousPage()
    {
        GoTo(Page - 1);
    }

    public void NextPage()
    {
        GoTo(Page + 1);
    }
}
