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

    public List<OrganizationModel> GetOrganizations(int? userId = null) => Get<List<OrganizationModel>>(userId.HasValue ? $"organizations?userId={userId.Value}" : "organizations");

    public UserModel? Login(string email, string password)
    {
        try
        {
            var response = _httpClient.PostAsJsonAsync("users/login", new { Email = email, Password = password }).GetAwaiter().GetResult();
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Login failed with status {response.StatusCode}");
            }
            return response.Content.ReadFromJsonAsync<UserModel>().GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Login call failed: {ex.Message}");
            return null;
        }
    }

    public UserModel? Register(string name, string email, string password, string organizationName)
    {
        var response = _httpClient.PostAsJsonAsync("users/register", new { Name = name, Email = email, Password = password, OrganizationName = organizationName }).GetAwaiter().GetResult();
        if (!response.IsSuccessStatusCode)
        {
            var errorBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            throw new Exception($"Registration failed: {errorBody}");
        }
        return response.Content.ReadFromJsonAsync<UserModel>().GetAwaiter().GetResult();
    }
    public List<BrandModel> GetBrands(int? organizationId = null) => Get<List<BrandModel>>(organizationId.HasValue ? $"brands?organizationId={organizationId.Value}" : "brands");
    public List<SurveyModel> GetSurveys(int? brandId = null) => Get<List<SurveyModel>>(brandId.HasValue ? $"surveys?brandId={brandId.Value}" : "surveys");
    public void DeleteSurvey(int surveyId) => Delete($"surveys/{surveyId}");
    public List<SurveyQuestionModel> GetQuestionsForSurvey(int surveyId) => Get<List<SurveyQuestionModel>>($"surveys/questions?surveyId={surveyId}");
    public void SaveSurveyQuestions(int surveyId, List<SurveyQuestionModel> questions) => Post($"surveys/{surveyId}/questions", questions);
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
    public void SaveBrand(BrandModel brand)
    {
        if (brand.Id <= 0)
        {
            CreateBrand(brand);
            return;
        }

        try
        {
            Put($"brands/{brand.Id}", brand);
        }
        catch (Exception ex) when (ex.Message.Contains("NotFound") || ex.Message.Contains("404"))
        {
            CreateBrand(brand);
        }
    }
    public void SaveSurvey(SurveyModel survey)
    {
        if (survey.Id <= 0)
        {
            CreateSurvey(survey);
            return;
        }

        try
        {
            Put($"surveys/{survey.Id}", survey);
        }
        catch (Exception ex) when (ex.Message.Contains("NotFound") || ex.Message.Contains("404"))
        {
            CreateSurvey(survey);
        }
    }
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

    private void Delete(string endpoint)
    {
        var response = _httpClient.DeleteAsync(endpoint).GetAwaiter().GetResult();
        if (!response.IsSuccessStatusCode)
        {
            var errorBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            System.Diagnostics.Debug.WriteLine($"API Delete failed with status {response.StatusCode}. Details: {errorBody}");
            throw new Exception($"API Delete failed with status {response.StatusCode}. Details: {errorBody}");
        }
    }

    public void CreateOrganization(OrganizationModel org, int? creatorUserId = null) => Post(creatorUserId.HasValue ? $"organizations?creatorUserId={creatorUserId.Value}" : "organizations", org);
    public void CreateBrand(BrandModel brand) => Post("brands", brand);
    public void CreateSurvey(SurveyModel survey)
    {
        var created = PostWithResult<SurveyModel, SurveyModel>("surveys", survey);
        if (created != null && created.Id > 0)
        {
            survey.Id = created.Id;
        }
    }
    public void CreateUser(UserModel user) => Post("users", user);
    public void CreateTemplate(TemplateModel template) => Post("templates", template);
    public void CreateVariable(VariableModel variable) => Post("variables", variable);
    public void CreateRole(RoleModel role) => Post("roles", role);

    private TResult? PostWithResult<TData, TResult>(string endpoint, TData data)
    {
        var response = _httpClient.PostAsJsonAsync(endpoint, data).GetAwaiter().GetResult();
        if (!response.IsSuccessStatusCode)
        {
            var errorBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            System.Diagnostics.Debug.WriteLine($"API Post failed with status {response.StatusCode}. Details: {errorBody}");
            throw new Exception($"API Post failed with status {response.StatusCode}. Details: {errorBody}");
        }
        return response.Content.ReadFromJsonAsync<TResult>().GetAwaiter().GetResult();
    }
}
