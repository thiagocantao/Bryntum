//using Alias = Microsoft.Office.Interop.PowerPoint; 

/// <summary>
/// Summary description for Presentation
/// </summary>
public class Presentation
{
    //Alias.Application _powerPoint;
    //Alias.Presentation _presentation;

    ~Presentation()
    {
        //_powerPoint = null;
        //_presentation = null;
        //GC.Collect();
        //GC.WaitForPendingFinalizers();
        //GC.Collect();
        //GC.WaitForPendingFinalizers();
    }

    public Presentation()
    {
        //_powerPoint = new Alias.Application();
        //_presentation = _powerPoint.Presentations.Add(
        //                WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
    }

    private int _slideNo = 0;
    public void AddPage(string fileName)
    {
        //_slideNo++;
        //var slide = _presentation.Slides.Add(_slideNo,
        //    Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutBlank);
        //slide.Shapes.AddPicture(fileName,
        //                    Microsoft.Office.Core.MsoTriState.msoFalse,
        //                    Microsoft.Office.Core.MsoTriState.msoTrue, 0, 0);
    }

    public void SaveAs(string filename)
    {
        //if (File.Exists(filename))
        //    File.Delete(filename);

        //_presentation.SaveAs(filename);
        //_presentation.Close();
        //_powerPoint.Quit();
    }
}
