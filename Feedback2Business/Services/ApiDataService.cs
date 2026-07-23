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
    public List<BrandModel> GetBrands() => Get<List<BrandModel>>("brands");
    public List<SurveyModel> GetSurveys() => Get<List<SurveyModel>>("surveys");
    public List<SurveyQuestionModel> GetSection1Questions() => Get<List<SurveyQuestionModel>>("surveys/questions?section=1");
    public List<SurveyQuestionModel> GetSection2Questions() => Get<List<SurveyQuestionModel>>("surveys/questions?section=2");
    public List<SurveyQuestionModel> GetSection3Questions() => Get<List<SurveyQuestionModel>>("surveys/questions?section=3");
    public List<UserModel> GetUsers() => Get<List<UserModel>>("users");
    public List<TemplateModel> GetTemplates() => Get<List<TemplateModel>>("templates");
    public List<VariableModel> GetVariables() => Get<List<VariableModel>>("variables");
    public List<MediaItemModel> GetMediaItems() => Get<List<MediaItemModel>>("media");
    public List<RoleModel> GetRoles() => Get<List<RoleModel>>("roles");
    public List<ActivityEventModel> GetActivityEvents() => Get<List<ActivityEventModel>>("activitylog");
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

    public void CreateOrganization(OrganizationModel org) => Post("organizations", org);
    public void CreateBrand(BrandModel brand) => Post("brands", brand);
    public void CreateSurvey(SurveyModel survey) => Post("surveys", survey);
    public void CreateUser(UserModel user) => Post("users", user);
    public void CreateTemplate(TemplateModel template) => Post("templates", template);
    public void CreateVariable(VariableModel variable) => Post("variables", variable);
    public void CreateRole(RoleModel role) => Post("roles", role);
}
