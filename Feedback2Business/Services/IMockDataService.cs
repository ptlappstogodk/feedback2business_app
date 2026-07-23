using Feedback2Business.Models;

namespace Feedback2Business.Services;

public interface IMockDataService
{
    List<OrganizationModel> GetOrganizations();
    List<BrandModel> GetBrands(int? organizationId = null);
    List<SurveyModel> GetSurveys(int? brandId = null);
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

    void CreateOrganization(OrganizationModel org);
    void CreateBrand(BrandModel brand);
    void CreateSurvey(SurveyModel survey);
    void CreateUser(UserModel user);
    void CreateTemplate(TemplateModel template);
    void CreateVariable(VariableModel variable);
    void CreateRole(RoleModel role);
}


