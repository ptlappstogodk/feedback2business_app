using Feedback2Business.Models;

namespace Feedback2Business.Services;

public interface IMockDataService
{
    List<OrganizationModel> GetOrganizations(int? userId = null);
    UserModel? Login(string email, string password);
    UserModel? Register(string name, string email, string password, string organizationName);
    List<BrandModel> GetBrands(int? organizationId = null);
    List<SurveyModel> GetSurveys(int? brandId = null);
    void DeleteSurvey(int surveyId);
    List<SurveyQuestionModel> GetQuestionsForSurvey(int surveyId);
    void SaveSurveyQuestions(int surveyId, List<SurveyQuestionModel> questions);
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
    void SaveSurvey(SurveyModel survey);
    MobilePreviewModel GetPreview();

    void CreateOrganization(OrganizationModel org, int? creatorUserId = null);
    void CreateBrand(BrandModel brand);
    void CreateSurvey(SurveyModel survey);
    void CreateUser(UserModel user);
    void CreateTemplate(TemplateModel template);
    void CreateVariable(VariableModel variable);
    void CreateRole(RoleModel role);
}


