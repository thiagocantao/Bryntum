using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Wizard.Services;

/// <summary>
/// Summary description for CustomValidator
/// </summary>
public class CustomValidator : ICustomQueryValidator
{
    public CustomValidator()
    {

    }

    public bool Validate(DataConnectionParametersBase connectionParameters, string sql, ref string message)
    {
        return true;
    }
}