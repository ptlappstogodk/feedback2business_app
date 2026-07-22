using SurveyHub.Models;

namespace SurveyHub.Services;

public interface IMockDataService
{
    List<OrganizationModel> GetOrganizations();
    List<BrandModel> GetBrands();
    List<SurveyModel> GetSurveys();
    List<SurveyQuestionModel> GetSection1Questions();
    List<SurveyQuestionModel> GetSection2Questions();
    List<SurveyQuestionModel> GetSection3Questions();
    List<UserModel> GetUsers();
    List<TemplateModel> GetTemplates();
    List<VariableModel> GetVariables();
    List<MediaItemModel> GetMediaItems();
    List<RoleModel> GetRoles();
    List<ActivityEventModel> GetActivityEvents();
    MobilePreviewModel GetPreview();
}


