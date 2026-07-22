using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using Feedback2Business.Models;

namespace Feedback2Business.Services;

public class ApiDataService : IMockDataService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://feedback2business.azurewebsites.net/api/";

    public ApiDataService()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };
    }

    private T Get<T>(string endpoint)
    {
        try
        {
            return _httpClient.GetFromJsonAsync<T>(endpoint).GetAwaiter().GetResult() ?? Activator.CreateInstance<T>();
        }
        catch
        {
            return Activator.CreateInstance<T>();
        }
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
}
