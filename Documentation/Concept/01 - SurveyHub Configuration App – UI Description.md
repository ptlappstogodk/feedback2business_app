SurveyHub Configuration App – UI Description
Overview
This UI represents a desktop administrative configuration application called SurveyHub, used to manage organizations, users, roles, templates, variables, media assets, application settings, and activity logs related to survey-based workflows.
The application follows a dashboard/admin panel pattern with:
•	a persistent left navigation sidebar
•	a top breadcrumb/header
•	a main content area that changes based on the selected section
•	list/detail editing patterns
•	tables for management pages
•	form-based configuration pages
•	one advanced survey builder page with mobile preview
The UI is intended for internal administrators managing survey programs across organizations and brands.

Global Application Structure
Layout
All pages share the same base layout:
1.	Fixed left sidebar
2.	Top breadcrumb/header row
3.	Main page content area
4.	Optional page-specific actions in the top-right
5.	White/light content panels with subtle borders and rounded corners
Visual Style
•	Desktop-first web admin interface
•	Dark navy sidebar
•	White content surfaces
•	Light gray borders and separators
•	Blue primary buttons and selection states
•	Green used for active statuses
•	Clean typography with strong spacing and hierarchy
•	Rounded controls and cards

Global Sidebar Navigation
The left sidebar is persistent across all pages.
Top
Contains:
•	SurveyHub logo/icon
•	title: SurveyHub
•	subtitle: Konfiguration
Navigation Items
The app contains the following main sections:
•	Overblik
•	Organisationer
•	Brugere
•	Roller & rettigheder
•	Skabeloner
•	Variabler
•	Mediebibliotek
•	Indstillinger
•	Aktivitetslog
Active State
The selected navigation item is highlighted with a brighter/different blue background.
Bottom User Area
Shows current user profile summary:
•	avatar or initials
•	user name
•	role
•	optional menu/chevron

Common Header Pattern
Most pages include:
Breadcrumb
Top-left breadcrumb showing hierarchy such as:
•	Organisationer
•	
•	Retail Group A
Some top-level pages may simply use the page name as the current item.
Top-right Utility Icons
Standard window/app control icons are shown at the far right.

Page 1: Organizations Overview
Purpose
A management page for viewing and creating organizations.
Header
Page title:
•	Organisationer
Subtitle/description:
•	administrative overview of organizations, brands, and surveys
Primary Action
Top-right primary button:
•	+ Opret organisation
Main Content
A searchable table listing organizations.
Controls
•	search input for filtering organizations
Table Columns
Likely columns:
•	Name
•	Brands
•	Surveys
•	Brugere / users
•	Last updated / created date
Example Rows
•	Retail Group A
•	FoodCo Holding
•	Service Partners
•	Nordic Retail
•	Build Group
Row Behavior
Selecting a row likely opens the organization detail context.
Footer
Pagination and visible count text, e.g.:
•	“Viser 1-5 af 5 organisationer”

Page 2: Organization Detail – Brands / Survey Builder
This is the previously described advanced page and remains the most feature-rich screen.
Purpose
Manage one organization and configure brand-specific surveys.
Header Area
•	Organization icon tile
•	title: Retail Group A
•	green status badge: Aktiv
•	top tabs:
•	Overblik
•	Brands
•	Brugere
•	Indstillinger
•	App-udgivelse
The active tab is Brands.
Actions
•	Gem ændringer
•	Flere handlinger
Main Layout
Three functional areas:
1.	Brand list
2.	Survey list + mobile preview
3.	Survey editor + question property panel
Included Features
•	brand selection
•	survey selection
•	survey section/question editor
•	mobile app preview
•	question settings panel
•	logic/version/translation tabs
This page should still be implemented exactly as described in the previous specification, but now considered one subpage within the larger app.

Page 3: Organization Detail – Users
Purpose
Manage users belonging to the selected organization.
Header
Page title:
•	Brugere
Subtitle:
•	administer users in the organization
Sub-tabs
Near the top of the content:
•	Brugere
•	Inviterede
The active tab in the screenshot is Brugere.
Primary Action
Top-right button:
•	+ Inviter bruger
Filters / Controls
A toolbar above the table containing:
•	search field
•	role filter dropdown
•	status filter dropdown
Main Table
A user management table.
Columns
Likely:
•	Name
•	Email
•	Role
•	Status
•	Last active
Example Roles
•	Admin
•	Manager
•	Editor
•	Viewer
Status
Uses small status badges such as:
•	Aktiv
Pagination
Bottom pagination control and item count text.
Behavior
•	Search and filter users
•	Invite new user
•	Possibly open/edit user on row click
•	Switch to invited users list via sub-tab

Page 4: Templates
Purpose
Manage reusable survey templates and checklists.
Header
Page title:
•	Skabeloner
Subtitle:
•	create and manage templates for surveys and checklists
Sub-tabs
•	Skabeloner
•	Gruppering
The active tab is Skabeloner.
Primary Action
Top-right button:
•	+ Ny skabelon
Main Content
Searchable table of templates.
Controls
•	search field
Table Columns
Likely:
•	Name
•	Type
•	Description
•	Last updated
Example Rows
•	Butiksinspektion (Inspection)
•	HACCP Tjekliste (Checklist)
•	Kampagneevaluering (Survey)
•	Sikkerhedsrundering (Checklist)
•	Åbning af ny butik (Survey)
Behavior
•	view/edit template
•	create template
•	group templates through the second tab

Page 5: Variables
Purpose
Manage reusable variables that can be referenced dynamically in surveys.
Header
Page title:
•	Variabler
Subtitle:
•	create variables that can be used dynamically in questions
Sub-tabs
•	Variabler
•	Variabelsæt
The active tab is Variabler.
Primary Action
Top-right:
•	+ Ny variabel
Main Table
A searchable or browsable list of variables.
Likely Columns
•	Name
•	Key
•	Type
•	Standardværdi / default value
•	Usage count / used in
Example Variable Types
•	Text
•	Dato / date
•	Image
•	Color / brand color
Example Rows
Variables like:
•	organizations name
•	inspection name
•	date
•	location
•	brand color
Behavior
•	create/edit variables
•	organize variable sets via second tab
•	show how many surveys use each variable

Page 6: Media Library
Purpose
Manage uploaded media used in surveys and app experiences.
Header
Page title:
•	Mediebibliotek
Subtitle:
•	upload and administer images and files
Sub-tabs
•	Filer
•	Mapper
The active tab is Filer.
Toolbar
Contains:
•	search field
•	type filter dropdown
•	upload button
Primary action:
•	Upload fil
Main Content
A card/grid-based media browser.
Media Cards
Each card includes:
•	preview thumbnail or file icon
•	filename
•	file metadata such as size or date
Supported File Types Seen
•	images
•	PDF
•	DOCX
•	XLSX
Example Uses
•	storefront photos
•	brand logo
•	manuals
•	product documents
Footer
Pagination controls and item count text.
Behavior
•	search media
•	filter by type
•	upload files
•	switch between file and folder views
•	open/select media assets for survey use

Page 7: Settings – General
Purpose
Configure organization-level settings.
Context
This page lives under the Indstillinger main sidebar item and uses an internal settings navigation on the left side of the main content.
Internal Settings Navigation
A vertical sub-navigation with items such as:
•	Generelt
•	App-indstillinger
•	Sikkerhed
•	Notifikationer
•	API & integrationer
•	Udseende
The active subpage in this screenshot is Generelt.
Top-right Actions
•	Gem ændringer
•	Flere handlinger
Main Form
A white form card with organization settings fields.
Fields Seen
•	Organisationsnavn
•	Primær sprog
•	Tidszone
•	Dashboard / default view
•	Date format
•	Logo upload
Logo Area
Contains:
•	blue upload placeholder tile
•	upload/change and remove buttons
Behavior
•	edit organization metadata
•	upload logo
•	save changes

Page 8: Settings – App Settings
Purpose
Configure app behavior and field/mobile behavior for the organization.
Internal Settings Navigation
Same settings sub-navigation as above.
The active item is:
•	App-indstillinger
Top-right Action
•	Gem ændringer
Main Form Sections
The page appears to be structured into grouped settings sections.
Example Sections
Offline
Options such as:
•	allow offline data collection
•	sync automatically when connectivity is available
Positioning / Location
Options such as:
•	require location validation at survey start
•	use location for attached evidence / registration
Media
Options such as:
•	compress images before upload
•	maximum file size dropdown/value
Navigation
Options such as:
•	show progress in survey
•	allow skipping ahead in survey
Controls
Mostly checkbox toggles and dropdowns.
Behavior
Used to configure runtime behavior of the survey app.

Page 9: Roles & Permissions
Purpose
Manage access roles and the permissions associated with each role.
Header
Page title:
•	Roller & rettigheder
Subtitle:
•	define roles and what users can access and edit
Sub-tabs
•	Roller
•	Rettigheder
The active tab is Roller.
Primary Action
Top-right:
•	+ Ny rolle
Main Content
Two-panel layout.
Left Panel
List/table of roles.
Columns
•	Role name
•	Description
Example Roles
•	Admin
•	Manager
•	Editor
•	Viewer
Right Panel
Permissions for the selected role.
Selected Role
Likely Admin
Permission Groups
Checkbox groups such as:
•	Organisations
•	Surveys
•	Reports
Example Permissions
•	administer organization
•	invite users
•	administer users
•	create surveys
•	edit surveys
•	see reports
•	export data
Behavior
•	select role to inspect permissions
•	create new role
•	toggle permission sets

Page 10: Activity Log
Purpose
Show an audit trail of changes and administrative actions.
Header
Page title:
•	Aktivitetslog
Subtitle:
•	show changes and activities in the organization
Toolbar / Filters
A filter row with:
•	search input
•	action type dropdown
•	user dropdown
•	period dropdown
Main Table
Audit/event list.
Likely Columns
•	Timestamp
•	Handling / action
•	Bruger / user
•	Details / object / additional info
Example Events
•	updated survey
•	created user
•	uploaded file
•	changed settings
•	exported report
Pagination
Footer with count text and pagination.
Behavior
•	filter by event type, user, time period
•	search historical events
•	inspect admin actions for traceability

Cross-Page Patterns
Tables
Most management pages use:
•	search bar
•	optional dropdown filters
•	table with pagination
•	top-right primary create/invite button
Settings Pages
Settings pages use:
•	left sub-navigation
•	form-style fields
•	save button in the top-right
•	grouped sections with toggles, dropdowns, and file upload
Builder Pages
Complex builder pages use:
•	hierarchical lists
•	property inspector panel
•	live preview
•	explicit save action

Suggested Information Architecture
An implementation agent should treat this as a multi-page admin product with these top-level routes:
ts
/routes /overview /organizations /organizations/:organizationId /organizations/:organizationId/brands /organizations/:organizationId/users /organizations/:organizationId/settings /organizations/:organizationId/publish /users /roles /templates /variables /media /settings/general /settings/app /settings/security /settings/notifications /settings/integrations /settings/appearance /activity-log 

Suggested Page-Level Component Model
ts
AppShell SidebarNavigation TopBreadcrumbBar PageContainer PageHeader PageActions PageToolbar ContentCard / DataTable / FormPanel / SplitView 
Reusable Components
•	SidebarNavigation
•	Breadcrumbs
•	PageHeader
•	PrimaryButton
•	SecondaryDropdownButton
•	SearchInput
•	FilterDropdown
•	DataTable
•	Pagination
•	StatusBadge
•	Tabs
•	SubTabs
•	PropertyPanel
•	FormSection
•	CheckboxField
•	RadioGroup
•	SelectField
•	TextField
•	TextArea
•	UploadField
•	CardGrid
•	MobilePreview

Updated Domain Model
ts
type AppState = { currentUser: User; organizations: Organization[]; templates: Template[]; variables: Variable[]; mediaAssets: MediaAsset[]; roles: Role[]; activityEvents: ActivityEvent[]; settings: AppSettings; }; type Organization = { id: string; name: string; status: "active" | "inactive"; brands: Brand[]; users: User[]; settings: OrganizationSettings; }; type User = { id: string; name: string; email: string; roleId: string; status: "active" | "invited" | "disabled"; lastActiveAt?: string; }; type Role = { id: string; name: string; description: string; permissions: PermissionGroup[]; }; type PermissionGroup = { name: string; permissions: Permission[]; }; type Permission = { id: string; label: string; enabled: boolean; }; type Template = { id: string; name: string; type: "survey" | "checklist" | "inspection"; description?: string; updatedAt: string; }; type Variable = { id: string; name: string; key: string; type: "text" | "date" | "image" | "color" | "number" | "boolean"; defaultValue?: string; usageCount: number; }; type MediaAsset = { id: string; name: string; type: "image" | "pdf" | "docx" | "xlsx" | "other"; url: string; thumbnailUrl?: string; sizeLabel?: string; uploadedAt?: string; }; type ActivityEvent = { id: string; timestamp: string; action: string; userId: string; details: string; }; type OrganizationSettings = { name: string; language: string; timezone: string; dateFormat: string; logoUrl?: string; }; type AppSettings = { offlineEnabled: boolean; autoSync: boolean; requireLocationAtStart: boolean; compressImages: boolean; maxUploadSizeMb: number; showSurveyProgress: boolean; }; 

Short Updated Implementation Prompt
Use this if you want a concise implementation brief for an AI coding agent:
Build a desktop admin web app called SurveyHub for configuring survey-based programs across organizations. Use a shared shell with a dark navy left sidebar and a white main content area. Include top-level navigation for Overview, Organizations, Users, Roles & Permissions, Templates, Variables, Media Library, Settings, and Activity Log. Implement the following page types:
•	Organizations list page with searchable table and create button
•	Organization detail pages with top tabs: Overview, Brands, Users, Settings, App Publishing
•	Brands page with brand list, survey list, mobile preview, and survey builder/property panel
•	Users page with user table, filters, sub-tabs, and invite button
•	Templates page with template table and create button
•	Variables page with variable table and create button
•	Media library page with file grid, search, filters, upload action, and folder/file tabs
•	Settings pages with left sub-navigation and editable forms
•	Roles & Permissions page with role list and permission editor
•	Activity Log page with filters and paginated audit table Use a clean enterprise UI style with rounded panels, subtle borders, blue primary actions, and green status badges.

If you want, I can next turn this into either:
1.	a complete product requirements style spec,
2.	a JSON UI schema, or
3.	a React/TypeScript route + component blueprint.

convert it into a MAUI/XAML screen description.

Below is a MAUI/XAML-oriented screen description of the complete SurveyHub configuration app based on the images. It is written so an AI coding agent can translate it into .NET MAUI XAML + MVVM structure.

SurveyHub Configuration App – MAUI/XAML Screen Description
Goal
Implement a desktop-first administrative app UI in .NET MAUI using XAML and MVVM. The UI should resemble a modern web-style admin dashboard, but built with MAUI layouts and controls.
The app contains:
•	a persistent left sidebar
•	a top breadcrumb/header area
•	a main content host that changes by page
•	list pages with tables
•	settings pages with forms
•	one advanced survey builder page with split panes and mobile preview

1. Application Shell Structure
Recommended MAUI Structure
Use:
•	AppShell.xaml for global navigation
•	a custom DashboardShellPage.xaml or equivalent container page
•	routed content pages for each major section
•	MVVM for state, commands, and selected items
Because the UI behaves like a desktop admin app with a persistent sidebar, it should be implemented more like a single shell layout than a phone-style tab app.
Root Visual Layout
Use a root Grid with:
•	Column 0: fixed-width left sidebar
•	Column 1: main content area
Example conceptual layout:
xml
<Grid ColumnDefinitions="260,*"> <views:SidebarView Grid.Column="0" /> <Grid Grid.Column="1" RowDefinitions="Auto,*"> <views:TopHeaderView Grid.Row="0" /> <ContentPresenter Grid.Row="1" /> </Grid> </Grid> 

2. Global Sidebar
Component
Create a reusable view:
•	SidebarView.xaml
Layout
Use a vertical layout with 3 sections:
1.	Logo/app title area
2.	Navigation menu
3.	Current user footer
Suggested MAUI Structure
•	Root: Grid or VerticalStackLayout
•	Background: dark navy
•	Fixed width: around 240–260
•	Padding: 16
Top Branding Area
Contains:
•	app logo/icon
•	Label for SurveyHub
•	smaller Label for Konfiguration
Use:
•	Image
•	Label
•	VerticalStackLayout
Navigation Items
Use a CollectionView bound to a list of navigation items.
Each item should contain:
•	icon
•	label
•	visual active state
Recommended item template:
•	outer Border with rounded corners
•	Grid with icon column + text column
•	active item has lighter blue background
Suggested navigation items:
•	Overview
•	Organisations
•	Users
•	Roles & permissions
•	Templates
•	Variables
•	Media library
•	Settings
•	Activity log
Bottom User Card
Docked to bottom using a parent Grid with RowDefinitions="Auto,*,Auto".
Contains:
•	avatar circle or initials badge
•	user name
•	role
•	optional chevron icon
Use:
•	Border
•	Grid
•	Label
•	Image

3. Global Top Header
Component
Create:
•	TopHeaderView.xaml
Purpose
Displays:
•	breadcrumb
•	optional page title context
•	optional action buttons
•	window control icons if needed
Layout
Use a horizontal Grid:
•	left side: breadcrumb
•	right side: page actions / utility controls
Example:
xml
<Grid ColumnDefinitions="*,Auto" Padding="24,16"> <HorizontalStackLayout Grid.Column="0" Spacing="8"> <!-- Breadcrumbs --> </HorizontalStackLayout> <HorizontalStackLayout Grid.Column="1" Spacing="12"> <!-- Actions --> </HorizontalStackLayout> </Grid> 
Breadcrumb
Use a bindable breadcrumb item collection or just inline labels for first version.
Example:
•	Organisationer
•	
•	Retail Group A

4. Shared Page Container Pattern
All pages should use a common page container:
•	white background
•	outer padding
•	page title and subtitle
•	optional tab row
•	optional toolbar row
•	content panel
Recommended page structure:
xml
<Grid RowDefinitions="Auto,Auto,Auto,*" Padding="24"> <views:PageHeaderView Grid.Row="0" /> <views:TabsView Grid.Row="1" /> <views:ToolbarView Grid.Row="2" /> <Border Grid.Row="3" ...> <!-- page content --> </Border> </Grid> 

5. Reusable MAUI Components
Create reusable XAML components for consistency.
Recommended Reusable Views
•	SidebarView
•	TopHeaderView
•	PageHeaderView
•	PrimaryButtonView
•	SecondaryButtonView
•	SearchBarView
•	FilterDropdownView
•	StatusBadgeView
•	TabBarView
•	SectionCardView
•	QuestionRowView
•	PropertyPanelView
•	DataTableView
•	PaginationView
•	SettingsNavView
•	MediaCardView
•	MobilePreviewView

6. Organizations List Page
Page
OrganizationsPage.xaml
Purpose
Show a searchable list of organizations.
Structure
Use a root grid with rows:
•	page title row
•	toolbar row
•	content row
•	pagination/footer row
Header
Title:
•	Organisationer
Subtitle:
•	admin overview of organizations, brands, and surveys
Top-right action:
•	+ Opret organisation
Use:
•	Grid with left title/subtitle and right action button
Toolbar
Contains:
•	search field
Use:
•	SearchBar or styled Entry inside Border
Main Content
Use a desktop-style table.
MAUI Table Recommendation
Since MAUI does not provide a rich data grid by default, implement using:
•	Grid header row
•	CollectionView for table rows
•	each row is a Grid with matching columns
Suggested columns:
•	Name
•	Brands
•	Surveys
•	Users
•	Last updated
Row Template
Each row:
•	white background
•	bottom border or separator
•	hover/selection styling if supported
Footer
Pagination control aligned right or centered.
Use:
•	HorizontalStackLayout with page number buttons

7. Organization Detail Container
Page
OrganizationDetailPage.xaml
This page can host sub-tabs:
•	Overview
•	Brands
•	Users
•	Settings
•	App publishing
Header
Top section contains:
•	square icon tile
•	organization name: Retail Group A
•	active badge
•	top-right action buttons
Under it:
•	horizontal tab strip
Use:
•	Grid with columns for icon, text, actions
•	HorizontalStackLayout for tabs

8. Brands / Survey Builder Page
Page
OrganizationBrandsPage.xaml
This is the most complex page.
Main Layout
Use a 3-column grid:
•	Column 0: brands list
•	Column 1: surveys list + app preview
•	Column 2: survey workspace
Because the survey workspace itself includes a properties panel, column 2 can be another nested grid.
Suggested structure:
xml
<Grid ColumnDefinitions="300,360,*" ColumnSpacing="0"> <!-- Brands --> <!-- Surveys + preview --> <!-- Builder area --> </Grid> 
More precisely, the builder area should be:
xml
<Grid ColumnDefinitions="*,320"> <!-- Survey sections/questions --> <!-- Property panel --> </Grid> 
8.1 Brands Panel
Use a Border with:
•	title row
•	plus button
•	CollectionView of brands
Each brand item:
•	logo/icon
•	name
•	survey count
•	selected background
•	left accent bar for selected state
8.2 Surveys Panel
Use a Border with:
•	title: Surveys for Coffee House
•	action text/button: + Opret survey
•	CollectionView of surveys
Each survey item:
•	icon
•	title
•	version
•	number of questions
•	selected state
8.3 App Preview
Below the survey list in same column.
Use:
•	Label header App preview
•	centered phone mockup
Implementation Suggestion
Make a reusable MobilePreviewView.xaml:
•	outer container centered
•	phone frame can be:
•	an image background, or
•	a rounded Border simulating a phone
•	inside use labels/buttons to render current question preview
Contents:
•	survey title
•	progress info
•	current question title
•	description
•	answer buttons
•	bottom nav actions

9. Survey Workspace
Layout
Right side split into:
•	left: survey structure editor
•	right: question property panel
Use:
xml
<Grid ColumnDefinitions="*,320" RowDefinitions="Auto,Auto,Auto,*"> <!-- survey header --> <!-- primary tabs --> <!-- build mode tabs --> <!-- content --> </Grid> 
9.1 Survey Summary Header
Contains:
•	icon tile
•	survey title: Butiksinspektion
•	version badge
•	subtitle/description
9.2 Primary Editor Tabs
Horizontal tabs:
•	Byg
•	Logik
•	Indstillinger
•	Oversættelser
•	Versioner
Use CollectionView horizontally or HorizontalStackLayout.
9.3 Secondary Build Tabs
A segmented control style row:
•	Section
•	Spørgsmål
•	Indhold
•	Side
Implement with:
•	horizontal layout of bordered tab buttons
9.4 Survey Structure Panel
Use a ScrollView with vertical stack of expandable section cards.
Section Card
Each section should be a reusable component:
•	section header with expand/collapse icon
•	title text
•	optional reorder handle
•	overflow menu button
•	inner list of questions
Use:
•	Expander if available in your MAUI toolkit
•	otherwise custom collapsed state with IsVisible
Question Row
Each row contains:
•	question number
•	title
•	answer type aligned right
•	selected highlight
Use:
•	Grid ColumnDefinitions="Auto,*,Auto"
Example rows:
•	2.1 Facade ren og vedligeholdt? — Ja/Nej
•	2.2 Skiltning intakt og synlig? — Ja/Nej
•	2.3 Vinduer rene — Ja/Nej
•	2.4 Billede af facade — Billede
Add Section Button
At bottom:
•	full-width border/button with + Tilføj sektion her

10. Question Property Panel
Component
QuestionPropertyPanel.xaml
Purpose
Show editable properties for selected survey question.
Layout
Use a vertical form inside a bordered panel.
Suggested controls:
•	Picker for question type
•	Entry for title
•	Editor for description
•	CheckBox for required
•	radio button group for app display mode
•	text buttons for logic configuration
•	Entry for variable name
•	destructive button for delete
Structure
Use:
xml
<ScrollView> <VerticalStackLayout Spacing="16" Padding="16"> <!-- field groups --> </VerticalStackLayout> </ScrollView> 
Fields
Question Type
•	Label: Spørgsmålstype
•	Control: Picker
Title
•	Label: Titel
•	Control: Entry
Description
•	Label: Beskrivelse
•	Control: Editor
Required
•	CheckBox + label Påkrævet
Display in App
•	label Visning i app
•	two radio buttons:
•	Standard
•	Kompakt
Conditional Logic
•	text link/button + Tilføj betingelse
Display Logic
•	text link/button + Tilføj regel
Variable Name
•	Entry
Delete Button
•	red text/button:
•	Slet spørgsmål

11. Users Page
Page
OrganizationUsersPage.xaml
Structure
Rows:
•	page header
•	sub-tab row
•	toolbar row
•	table row
•	pagination row
Sub-tabs
•	Brugere
•	Inviterede
Toolbar
Contains:
•	search field
•	role filter
•	status filter
•	invite button
Main Table
Use CollectionView with a header row.
Columns:
•	Name
•	Email
•	Role
•	Status
•	Last active
Status should use reusable StatusBadgeView.

12. Templates Page
Page
TemplatesPage.xaml
Structure
Similar to organizations/users table pages.
Header
•	title Skabeloner
•	subtitle
•	action + Ny skabelon
Sub-tabs
•	Skabeloner
•	Gruppering
Table
Columns:
•	Name
•	Type
•	Description
•	Last updated

13. Variables Page
Page
VariablesPage.xaml
Header
•	title Variabler
•	subtitle
•	action + Ny variabel
Sub-tabs
•	Variabler
•	Variabelsæt
Table
Columns:
•	Name
•	Key
•	Type
•	Standardværdi
•	Bruges i

14. Media Library Page
Page
MediaLibraryPage.xaml
Layout
Rows:
•	header
•	sub-tabs
•	toolbar
•	media grid
•	pagination
Sub-tabs
•	Filer
•	Mapper
Toolbar
•	search field
•	type filter
•	upload button
Main Content
Use CollectionView with ItemsLayout as a grid.
Example:
xml
<CollectionView ItemsLayout="VerticalGrid, 4"> 
Each media card:
•	preview image or file icon
•	filename
•	metadata
Use reusable MediaCardView.

15. Settings Root Page Pattern
Pages
•	SettingsGeneralPage.xaml
•	SettingsAppPage.xaml
•	SettingsSecurityPage.xaml
•	SettingsNotificationsPage.xaml
•	SettingsIntegrationsPage.xaml
•	SettingsAppearancePage.xaml
Layout
Use a two-column grid:
•	left: settings sub-navigation
•	right: form panel
Example:
xml
<Grid ColumnDefinitions="220,*"> <views:SettingsNavView Grid.Column="0" /> <Border Grid.Column="1" Padding="24"> <!-- settings form --> </Border> </Grid> 
Settings Navigation
Vertical list:
•	Generelt
•	App-indstillinger
•	Sikkerhed
•	Notifikationer
•	API & integrationer
•	Udseende
Selected item highlighted.

16. Settings – General Page
Purpose
Organization-level configuration form.
Fields
•	Organization name
•	Primary language
•	Timezone
•	Dashboard/default view
•	Date format
•	Logo upload
Logo Upload
Use:
•	Border
•	image placeholder/icon
•	upload and remove buttons
Top-right Actions
•	Save changes
•	More actions

17. Settings – App Settings Page
Purpose
Configure runtime app behavior.
Form Structure
Use grouped sections inside a ScrollView.
Section: Offline
Checkboxes:
•	allow offline data collection
•	auto sync when connection is available
Section: Positioning
Checkboxes:
•	require location validation at survey start
•	attach location to observations/evidence
Section: Media
Checkboxes and dropdown:
•	compress images before upload
•	max file size
Section: Navigation
Checkboxes:
•	show survey progress
•	allow skipping forward
Use each section as a Border or grouped VerticalStackLayout.

18. Roles & Permissions Page
Page
RolesPermissionsPage.xaml
Layout
Two-column grid:
•	left: list of roles
•	right: permissions editor
Example:
xml
<Grid ColumnDefinitions="320,*"> <!-- roles list --> <!-- permissions details --> </Grid> 
Left Panel
Roles list table:
•	Role name
•	Description
Right Panel
Permission groups with checkboxes.
Suggested groups:
•	Organisations
•	Surveys
•	Reports
Each group contains stacked checkbox items.

19. Activity Log Page
Page
ActivityLogPage.xaml
Structure
Rows:
•	header
•	toolbar with filters
•	table
•	pagination
Toolbar
Contains:
•	search field
•	action filter
•	user filter
•	period filter
Table
Columns:
•	Timestamp
•	Action
•	User
•	Details
Use CollectionView row template.

20. Recommended MVVM ViewModel Structure
Global
csharp
MainShellViewModel 
Properties:
•	CurrentPageTitle
•	CurrentBreadcrumbs
•	NavigationItems
•	CurrentUser
Organizations
csharp
OrganizationsViewModel 
Properties:
•	SearchText
•	Organizations
•	SelectedOrganization
Commands:
•	CreateOrganizationCommand
•	OpenOrganizationCommand
Organization Detail
csharp
OrganizationDetailViewModel 
Properties:
•	Organization
•	SelectedTab
Brands / Builder
csharp
OrganizationBrandsViewModel 
Properties:
•	Brands
•	SelectedBrand
•	Surveys
•	SelectedSurvey
•	Sections
•	SelectedQuestion
Commands:
•	SaveChangesCommand
•	AddBrandCommand
•	AddSurveyCommand
•	AddSectionCommand
•	DeleteQuestionCommand
Users
csharp
OrganizationUsersViewModel 
Properties:
•	Users
•	SearchText
•	SelectedRoleFilter
•	SelectedStatusFilter
Commands:
•	InviteUserCommand
Templates
csharp
TemplatesViewModel 
Variables
csharp
VariablesViewModel 
Media
csharp
MediaLibraryViewModel 
Settings
csharp
SettingsGeneralViewModel SettingsAppViewModel 
Roles
csharp
RolesPermissionsViewModel 
Activity Log
csharp
ActivityLogViewModel 

21. Recommended MAUI Styling
Resource Dictionary
Create shared styles in Resources/Styles.xaml.
Include:
•	page background color
•	sidebar background color
•	primary blue
•	border gray
•	selected row color
•	badge colors
•	typography styles
•	button styles
Suggested Named Colors
•	SidebarBackground
•	PageBackground
•	CardBackground
•	PrimaryBlue
•	PrimaryBlueLight
•	BorderColor
•	TextPrimary
•	TextSecondary
•	SuccessGreen
•	DangerRed
Suggested Shared Styles
•	SidebarNavItemStyle
•	PrimaryButtonStyle
•	SecondaryButtonStyle
•	PageTitleStyle
•	PageSubtitleStyle
•	SectionHeaderStyle
•	DataTableHeaderStyle
•	FormLabelStyle
•	InputBorderStyle

22. Recommended Control Mapping
Here is a practical mapping from the UI to MAUI controls.
UI Element
MAUI Control
Sidebar
Grid, CollectionView, Border
Breadcrumb
HorizontalStackLayout, Label
Tabs
CollectionView horizontal or HorizontalStackLayout
Search box
Entry inside Border
Table
Grid header + CollectionView rows
Status badge
Border + Label
Forms
VerticalStackLayout, Entry, Editor, Picker, CheckBox, RadioButton
Property panel
ScrollView + stacked controls
Media grid
CollectionView with grid layout
Split panels
nested Grids
Expandable sections
Expander or custom collapsible ContentView
Mobile preview
custom ContentView

23. Suggested File/Folder Structure
text
Views/ Shell/ DashboardShellPage.xaml SidebarView.xaml TopHeaderView.xaml Shared/ PageHeaderView.xaml StatusBadgeView.xaml SearchBarView.xaml PaginationView.xaml SettingsNavView.xaml MobilePreviewView.xaml Organizations/ OrganizationsPage.xaml OrganizationDetailPage.xaml OrganizationBrandsPage.xaml OrganizationUsersPage.xaml Templates/ TemplatesPage.xaml Variables/ VariablesPage.xaml Media/ MediaLibraryPage.xaml MediaCardView.xaml Settings/ SettingsGeneralPage.xaml SettingsAppPage.xaml SettingsSecurityPage.xaml SettingsNotificationsPage.xaml SettingsIntegrationsPage.xaml SettingsAppearancePage.xaml Roles/ RolesPermissionsPage.xaml ActivityLog/ ActivityLogPage.xaml 
text
ViewModels/ MainShellViewModel.cs OrganizationsViewModel.cs OrganizationDetailViewModel.cs OrganizationBrandsViewModel.cs OrganizationUsersViewModel.cs TemplatesViewModel.cs VariablesViewModel.cs MediaLibraryViewModel.cs SettingsGeneralViewModel.cs SettingsAppViewModel.cs RolesPermissionsViewModel.cs ActivityLogViewModel.cs 

24. Short MAUI/XAML Implementation Prompt
You can give this directly to an AI coding agent:
Build a desktop-style .NET MAUI admin application called SurveyHub using XAML and MVVM. Use a root two-column shell layout with a fixed dark navy sidebar on the left and a dynamic content area on the right. Implement reusable XAML views for sidebar, top breadcrumb/header, page headers, tabs, search bars, tables, status badges, settings navigation, media cards, and mobile survey preview. Create pages for:
•	Organizations list
•	Organization Brands/Survey Builder
•	Organization Users
•	Templates
•	Variables
•	Media Library
•	Settings General
•	Settings App
•	Roles & Permissions
•	Activity Log
The Brands/Survey Builder page should use nested grids: brand list, survey list with mobile preview, and survey editor with section/question list and right-side property panel. Use CollectionView-based tables, Borders for cards, Pickers/Entries/Editors for forms, and ScrollViews for long content. Follow a clean white/light admin style with blue accents, green status badges, and rounded bordered panels.

