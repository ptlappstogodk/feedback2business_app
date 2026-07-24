using Feedback2Business.Models;

namespace Feedback2Business.Services;

public interface IMockDataService
{
    List<OrganizationModel> GetOrganizations();
    List<BrandModel> GetBrands(int? organizationId = null);
    List<SurveyModel> GetSurveys(int? brandId = null);
    List<SurveyQuestionModel> GetQuestionsForSurvey(int surveyId);
    List<SurveyQuestionModel> GetSection1Questions();
    List<SurveyQuestionModel> GetSection2Questions();
    List<SurveyQuestionModel> GetSection3Questions();
    List<UserModel> GetUsers(int? organizationId = null);
    List<TemplateModel> GetTemplates();
    List<VariableModel> GetVariables(int? organizationId = null);
    List<MediaItemModel> GetMediaItems();
    List<RoleModel> GetRoles(int? organizationId = null);
    List<ActivityEventModel> GetActivityEvents(int? organizationId = null);
    AppSettingModel GetAppSettings(int organizationId);
    void SaveAppSettings(AppSettingModel settings);
    void SaveRole(RoleModel role);
    MobilePreviewModel GetPreview();

    void CreateOrganization(OrganizationModel org);
    void CreateBrand(BrandModel brand);
    void CreateSurvey(SurveyModel survey);
    void CreateUser(UserModel user);
    void CreateTemplate(TemplateModel template);
    void CreateVariable(VariableModel variable);
    void CreateRole(RoleModel role);
}


