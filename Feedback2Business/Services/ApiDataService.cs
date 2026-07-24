using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using Feedback2Business.Models;

namespace Feedback2Business.Services;

public class ApiDataService : IMockDataService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://feedback2business-g9bafwfuetdxdcds.denmarkeast-01.azurewebsites.net/api/";

    public ApiDataService()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };
    }

    private T Get<T>(string endpoint)
    {
        var response = _httpClient.GetAsync(endpoint).GetAwaiter().GetResult();
        if (!response.IsSuccessStatusCode)
        {
            var errorBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            System.Diagnostics.Debug.WriteLine($"API Get failed with status {response.StatusCode}. Details: {errorBody}");
            throw new Exception($"API Get failed with status {response.StatusCode}. Details: {errorBody}");
        }
        return response.Content.ReadFromJsonAsync<T>().GetAwaiter().GetResult() ?? Activator.CreateInstance<T>();
    }

    public List<OrganizationModel> GetOrganizations() => Get<List<OrganizationModel>>("organizations");
    public List<BrandModel> GetBrands(int? organizationId = null) => Get<List<BrandModel>>(organizationId.HasValue ? $"brands?organizationId={organizationId.Value}" : "brands");
    public List<SurveyModel> GetSurveys(int? brandId = null) => Get<List<SurveyModel>>(brandId.HasValue ? $"surveys?brandId={brandId.Value}" : "surveys");
    public List<SurveyQuestionModel> GetQuestionsForSurvey(int surveyId) => Get<List<SurveyQuestionModel>>($"surveys/questions?surveyId={surveyId}");
    public List<SurveyQuestionModel> GetSection1Questions() => Get<List<SurveyQuestionModel>>("surveys/questions?section=1");
    public List<SurveyQuestionModel> GetSection2Questions() => Get<List<SurveyQuestionModel>>("surveys/questions?section=2");
    public List<SurveyQuestionModel> GetSection3Questions() => Get<List<SurveyQuestionModel>>("surveys/questions?section=3");
    public List<UserModel> GetUsers(int? organizationId = null) => Get<List<UserModel>>(organizationId.HasValue ? $"users?organizationId={organizationId.Value}" : "users");
    public List<TemplateModel> GetTemplates() => Get<List<TemplateModel>>("templates");
    public List<VariableModel> GetVariables(int? organizationId = null) => Get<List<VariableModel>>(organizationId.HasValue ? $"variables?organizationId={organizationId.Value}" : "variables");
    public List<MediaItemModel> GetMediaItems() => Get<List<MediaItemModel>>("media");
    public List<RoleModel> GetRoles(int? organizationId = null) => Get<List<RoleModel>>(organizationId.HasValue ? $"roles?organizationId={organizationId.Value}" : "roles");
    public List<ActivityEventModel> GetActivityEvents(int? organizationId = null) => Get<List<ActivityEventModel>>(organizationId.HasValue ? $"activitylog?organizationId={organizationId.Value}" : "activitylog");
    public AppSettingModel GetAppSettings(int organizationId) => Get<AppSettingModel>($"appsettings?organizationId={organizationId}");
    public void SaveAppSettings(AppSettingModel settings) => Put("appsettings", settings);
    public void SaveRole(RoleModel role) => Put($"roles/{role.Id}", role);
    public MobilePreviewModel GetPreview() => Get<MobilePreviewModel>("preview");

    private void Post<T>(string endpoint, T data)
    {
        var response = _httpClient.PostAsJsonAsync(endpoint, data).GetAwaiter().GetResult();
        if (!response.IsSuccessStatusCode)
        {
            var errorBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            System.Diagnostics.Debug.WriteLine($"API Post failed with status {response.StatusCode}. Details: {errorBody}");
            throw new Exception($"API Post failed with status {response.StatusCode}. Details: {errorBody}");
        }
    }

    private void Put<T>(string endpoint, T data)
    {
        var response = _httpClient.PutAsJsonAsync(endpoint, data).GetAwaiter().GetResult();
        if (!response.IsSuccessStatusCode)
        {
            var errorBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            System.Diagnostics.Debug.WriteLine($"API Put failed with status {response.StatusCode}. Details: {errorBody}");
            throw new Exception($"API Put failed with status {response.StatusCode}. Details: {errorBody}");
        }
    }

    public void CreateOrganization(OrganizationModel org) => Post("organizations", org);
    public void CreateBrand(BrandModel brand) => Post("brands", brand);
    public void CreateSurvey(SurveyModel survey) => Post("surveys", survey);
    public void CreateUser(UserModel user) => Post("users", user);
    public void CreateTemplate(TemplateModel template) => Post("templates", template);
    public void CreateVariable(VariableModel variable) => Post("variables", variable);
    public void CreateRole(RoleModel role) => Post("roles", role);
}
