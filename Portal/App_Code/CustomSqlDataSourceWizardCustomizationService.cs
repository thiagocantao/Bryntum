using DevExpress.DataAccess.Web;
using DevExpress.DataAccess.Wizard.Services;

/// <summary>
/// Summary description for CustomSqlDataSourceWizardCustomizationService
/// </summary>
public class CustomSqlDataSourceWizardCustomizationService : ISqlDataSourceWizardCustomizationService
{
    public ICustomQueryValidator CustomQueryValidator
    {
        get { return new CustomValidator(); }
    }

    public bool IsCustomSqlDisabled
    {
        get { return false; }
    }
}