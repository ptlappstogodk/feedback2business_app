# SurveysToGo — Agent Rules & Guidelines

This file defines the coding standards, repository layout, build workflows, and architectural rules for agents working on the SurveysToGo codebase.

## Codebase Architecture
* **MVVM Setup**: Built on a clean, custom MVVM scaffolding using `ObservableObject` and `RelayCommand` under the `SurveyHub` namespace.
* **Desktop-First Administrative Shell**: The user interface is driven by `DashboardShellPage.xaml` which implements a persistent left-hand sidebar navigation layout (`SidebarView.xaml`) and a dynamic page host.
* **Explicit Content Swapping**: Navigation switches pages dynamically inside the shell by setting `PageHost.Content = page.Content;`. All target views must subclass `ContentPage` so that their internal contents are accessible.
* **Danish Mock Data**: Use `IMockDataService` and `MockDataService` under the `SurveyHub.Services` namespace to populate standard widgets and list controls. Keep text labels and badges translated to Danish (e.g. `"Aktiv"`, `"Inviteret"`, `"Skabeloner"`, `"Butiksinspektion"`).

## Project Directory Map
* `SurveyHub/`: Direct source root containing app startup and configuration.
  * `Models/`: Strongly-typed model definitions for system objects (e.g., `UserModel`, `OrganizationModel`, `SurveyQuestionModel`).
  * `Services/`: Mock data providers and dependency injection registrations.
  * `ViewModels/`: Page-specific view models representing state, searchable filters, and workspace actions.
  * `Views/`: Standard user interface views:
    * `Shell/`: Sidebar navigation and breadcrumb headers (`SidebarView.xaml`, `TopHeaderView.xaml`).
    * `Shared/`: Standard widgets like `StatusBadgeView`, `MobilePreviewView` (a fully modeled nested phone preview), and property panels.
    * `Organizations/`, `Templates/`, `Variables/`, `Media/`, `Settings/`, `Roles/`, `ActivityLog/`: Feature-specific dashboards and data-table views.
* `Documentation/Concept/`: Original UI design requirements, XAML skeletons, and specifications.

## Build & Restore Workflows
* **Pinned SDK Version**: The repository has `global.json` pinned to the `.NET 9.0.312` SDK and workload versions for strict, reproducible builds across machines.
* **Mac Catalyst Compilation**: For local execution and verification on macOS, build the project with:
  ```bash
  dotnet build SurveyHub/SurveyHub.csproj -f net9.0-maccatalyst
  ```

## Git & Collaboration Standards
* **Commit Attribution**: Always include a co-author attribution line at the bottom of every commit message:
  ```text
  Co-Authored-By: Oz <oz-agent@warp.dev>
  ```
* **XAML Entities Escaping**: Standard XML parser regulations apply to all XAML files. Ensure special characters like `&` are fully escaped (e.g., as `&amp;` inside button or label text) to prevent XAML parse failures (`MAUIG1001`).
