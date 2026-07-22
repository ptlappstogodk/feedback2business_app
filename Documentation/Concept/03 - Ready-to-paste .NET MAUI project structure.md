Below are all 3 deliverables:
1.	MAUI XAML page skeletons
2.	Control hierarchy per page
3.	C# ViewModel and model scaffolding
I am keeping the code at a practical starter level so an AI agent can continue implementation cleanly.
 
1. MAUI XAML Page Skeletons
1.1 AppShell.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <Shell x:Class="Feedback2Business.AppShell" xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" xmlns:views="clr-namespace:Feedback2Business.Views" FlyoutBehavior="Disabled"> <ShellContent Title="Feedback2Business" ContentTemplate="{DataTemplate views:DashboardShellPage}" /> </Shell> 
 
1.2 DashboardShellPage.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <ContentPage x:Class="Feedback2Business.Views.DashboardShellPage" xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" xmlns:shell="clr-namespace:Feedback2Business.Views.Shell" xmlns:org="clr-namespace:Feedback2Business.Views.Organizations" BackgroundColor="{StaticResource PageBackground}"> <Grid ColumnDefinitions="250,*"> <shell:SidebarView Grid.Column="0" /> <Grid Grid.Column="1" RowDefinitions="Auto,*"> <shell:TopHeaderView Grid.Row="0" /> <ContentView Grid.Row="1" Padding="0"> <!-- Replace with routed content host or dynamic ContentPresenter --> <org:OrganizationBrandsPage /> </ContentView> </Grid> </Grid> </ContentPage> 
 
1.3 SidebarView.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <ContentView x:Class="Feedback2Business.Views.Shell.SidebarView" xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"> <Grid BackgroundColor="{StaticResource SidebarBackground}" Padding="16" RowDefinitions="Auto,*,Auto"> <VerticalStackLayout Spacing="6"> <Label Text="Feedback2Business" TextColor="White" FontSize="20" FontAttributes="Bold" /> <Label Text="Konfiguration" TextColor="#B8C7E0" FontSize="12" /> </VerticalStackLayout> <CollectionView Grid.Row="1" Margin="0,20,0,20" ItemsSource="{Binding NavigationItems}" SelectionMode="Single" SelectedItem="{Binding SelectedNavigationItem}"> <CollectionView.ItemTemplate> <DataTemplate> <Border Margin="0,4" Padding="12" StrokeThickness="0" BackgroundColor="{Binding BackgroundColor}"> <Grid ColumnDefinitions="24,*"> <Label Grid.Column="1" Text="{Binding Title}" TextColor="White" VerticalOptions="Center" /> </Grid> </Border> </DataTemplate> </CollectionView.ItemTemplate> </CollectionView> <Border Grid.Row="2" Padding="12" Stroke="#1C3A69" BackgroundColor="#0A2444"> <Grid ColumnDefinitions="Auto,*,Auto"> <Border WidthRequest="32" HeightRequest="32" StrokeThickness="0" BackgroundColor="#2F4B73" StrokeShape="RoundRectangle 16"> <Label Text="AK" HorizontalOptions="Center" VerticalOptions="Center" TextColor="White" FontSize="12" /> </Border> <VerticalStackLayout Grid.Column="1" Margin="10,0,0,0" Spacing="0"> <Label Text="{Binding CurrentUser.Name}" TextColor="White" FontSize="13" /> <Label Text="{Binding CurrentUser.Role}" TextColor="#B8C7E0" FontSize="11" /> </VerticalStackLayout> <Label Grid.Column="2" Text="›" TextColor="White" VerticalOptions="Center" /> </Grid> </Border> </Grid> </ContentView> 
 
1.4 TopHeaderView.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <ContentView x:Class="Feedback2Business.Views.Shell.TopHeaderView" xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"> <Grid Padding="24,16" ColumnDefinitions="*,Auto" BackgroundColor="White"> <HorizontalStackLayout Spacing="8"> <Label Text="{Binding BreadcrumbPrimary}" TextColor="{StaticResource TextSecondary}" FontSize="12" /> <Label Text=">" TextColor="{StaticResource TextSecondary}" FontSize="12" /> <Label Text="{Binding BreadcrumbSecondary}" TextColor="{StaticResource TextPrimary}" FontSize="12" FontAttributes="Bold" /> </HorizontalStackLayout> <HorizontalStackLayout Grid.Column="1" Spacing="16"> <Label Text="—" TextColor="{StaticResource TextSecondary}" /> <Label Text="□" TextColor="{StaticResource TextSecondary}" /> <Label Text="✕" TextColor="{StaticResource TextSecondary}" /> </HorizontalStackLayout> </Grid> </ContentView> 
 
1.5 OrganizationBrandsPage.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <ContentPage x:Class="Feedback2Business.Views.Organizations.OrganizationBrandsPage" xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" xmlns:shared="clr-namespace:Feedback2Business.Views.Shared" Title="Brands" BackgroundColor="{StaticResource PageBackground}"> <Grid RowDefinitions="Auto,*" Padding="24"> <!-- Header --> <Grid RowDefinitions="Auto,Auto" Grid.Row="0"> <Grid ColumnDefinitions="Auto,*,Auto" ColumnSpacing="16"> <Border WidthRequest="56" HeightRequest="56" BackgroundColor="{StaticResource PrimaryBlue}" StrokeThickness="0" StrokeShape="RoundRectangle 12" /> <VerticalStackLayout Grid.Column="1" Spacing="6"> <HorizontalStackLayout Spacing="10"> <Label Text="Retail Group A" Style="{StaticResource PageTitleStyle}" /> <shared:StatusBadgeView Text="Aktiv" BadgeColor="#D9F5E5" TextColor="#1E8E5A" /> </HorizontalStackLayout> <HorizontalStackLayout Spacing="24"> <Label Text="Overblik" Style="{StaticResource TabTextStyle}" /> <Label Text="Brands" Style="{StaticResource ActiveTabTextStyle}" /> <Label Text="Brugere" Style="{StaticResource TabTextStyle}" /> <Label Text="Indstillinger" Style="{StaticResource TabTextStyle}" /> <Label Text="App-udgivelse" Style="{StaticResource TabTextStyle}" /> </HorizontalStackLayout> </VerticalStackLayout> <HorizontalStackLayout Grid.Column="2" Spacing="12"> <Button Text="Gem ændringer" Style="{StaticResource PrimaryButtonStyle}" /> <Button Text="Flere handlinger" Style="{StaticResource SecondaryButtonStyle}" /> </HorizontalStackLayout> </Grid> </Grid> <!-- Content --> <Grid Grid.Row="1" Margin="0,20,0,0" ColumnDefinitions="300,360,*" ColumnSpacing="16"> <!-- Brands --> <Border Grid.Column="0" Style="{StaticResource CardStyle}"> <Grid RowDefinitions="Auto,*"> <Grid Padding="16" ColumnDefinitions="*,Auto"> <Label Text="Brands" Style="{StaticResource SectionHeaderStyle}" /> <Button Grid.Column="1" Text="+" WidthRequest="32" HeightRequest="32" /> </Grid> <CollectionView Grid.Row="1" ItemsSource="{Binding Brands}" SelectedItem="{Binding SelectedBrand}"> <CollectionView.ItemTemplate> <DataTemplate> <Grid Padding="12" ColumnDefinitions="Auto,*,Auto"> <BoxView WidthRequest="4" Color="{StaticResource PrimaryBlue}" IsVisible="{Binding IsSelected}" /> <Border Grid.Column="1" Padding="12" Margin="8,0,0,0" BackgroundColor="{Binding ItemBackground}" StrokeShape="RoundRectangle 8"> <VerticalStackLayout> <Label Text="{Binding Name}" FontAttributes="Bold" /> <Label Text="{Binding SurveyCountText}" FontSize="12" TextColor="{StaticResource TextSecondary}" /> </VerticalStackLayout> </Border> </Grid> </DataTemplate> </CollectionView.ItemTemplate> </CollectionView> </Grid> </Border> <!-- Surveys + Preview --> <Border Grid.Column="1" Style="{StaticResource CardStyle}"> <Grid RowDefinitions="Auto,*,Auto"> <Grid Padding="16" ColumnDefinitions="*,Auto"> <Label Text="Surveys for Coffee House" Style="{StaticResource SectionHeaderStyle}" /> <Button Grid.Column="1" Text="+ Opret survey" /> </Grid> <CollectionView Grid.Row="1" ItemsSource="{Binding Surveys}" SelectedItem="{Binding SelectedSurvey}"> <CollectionView.ItemTemplate> <DataTemplate> <Grid Padding="12" ColumnDefinitions="Auto,*"> <BoxView WidthRequest="4" Color="{StaticResource PrimaryBlue}" IsVisible="{Binding IsSelected}" /> <Border Grid.Column="1" Padding="12" Margin="8,0,0,0" BackgroundColor="{Binding ItemBackground}" StrokeShape="RoundRectangle 8"> <VerticalStackLayout Spacing="4"> <Label Text="{Binding Name}" FontAttributes="Bold" /> <Label Text="{Binding VersionText}" FontSize="12" TextColor="{StaticResource TextSecondary}" /> </VerticalStackLayout> </Border> </Grid> </DataTemplate> </CollectionView.ItemTemplate> </CollectionView> <shared:MobilePreviewView Grid.Row="2" Margin="16" BindingContext="{Binding SelectedQuestionPreview}" /> </Grid> </Border> <!-- Builder --> <Border Grid.Column="2" Style="{StaticResource CardStyle}"> <Grid RowDefinitions="Auto,Auto,Auto,*"> <!-- Survey header --> <Grid Padding="16" ColumnDefinitions="Auto,*"> <Border WidthRequest="40" HeightRequest="40" BackgroundColor="#EEF4FF" StrokeThickness="0" StrokeShape="RoundRectangle 10" /> <VerticalStackLayout Grid.Column="1" Margin="12,0,0,0" Spacing="4"> <HorizontalStackLayout Spacing="10"> <Label Text="Butiksinspektion" FontSize="20" FontAttributes="Bold" /> <shared:StatusBadgeView Text="Version 3 (Aktiv)" BadgeColor="#D9F5E5" TextColor="#1E8E5A" /> </HorizontalStackLayout> <Label Text="Standard inspection of store operations and presentation." TextColor="{StaticResource TextSecondary}" FontSize="12" /> </VerticalStackLayout> </Grid> <!-- Primary tabs --> <HorizontalStackLayout Grid.Row="1" Spacing="24" Padding="16,0"> <Label Text="Byg" Style="{StaticResource ActiveTabTextStyle}" /> <Label Text="Logik" Style="{StaticResource TabTextStyle}" /> <Label Text="Indstillinger" Style="{StaticResource TabTextStyle}" /> <Label Text="Oversættelser" Style="{StaticResource TabTextStyle}" /> <Label Text="Versioner" Style="{StaticResource TabTextStyle}" /> </HorizontalStackLayout> <!-- Secondary tabs --> <HorizontalStackLayout Grid.Row="2" Spacing="8" Padding="16,12"> <Button Text="Section" /> <Button Text="Spørgsmål" /> <Button Text="Indhold" /> <Button Text="Side" /> </HorizontalStackLayout> <!-- Main body --> <Grid Grid.Row="3" ColumnDefinitions="*,320" ColumnSpacing="16" Padding="16"> <!-- Section list --> <ScrollView Grid.Column="0"> <VerticalStackLayout Spacing="12"> <shared:SectionCardView Title="1. Butiksinformation" ItemsSource="{Binding Section1Questions}" /> <shared:SectionCardView Title="2. Eksteriør" ItemsSource="{Binding Section2Questions}" /> <shared:SectionCardView Title="3. Indretning &amp; præsentation" ItemsSource="{Binding Section3Questions}" /> <Button Text="+ Tilføj sektion her" /> </VerticalStackLayout> </ScrollView> <!-- Properties --> <shared:QuestionPropertyPanel Grid.Column="1" BindingContext="{Binding SelectedQuestion}" /> </Grid> </Grid> </Border> </Grid> </Grid> </ContentPage> 
 
1.6 MobilePreviewView.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <ContentView x:Class="Feedback2Business.Views.Shared.MobilePreviewView" xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"> <VerticalStackLayout Spacing="12"> <HorizontalStackLayout Spacing="6"> <Label Text="App preview" FontAttributes="Bold" /> <Label Text="ⓘ" FontSize="12" TextColor="{StaticResource TextSecondary}" /> </HorizontalStackLayout> <Grid HorizontalOptions="Center"> <Border WidthRequest="220" HeightRequest="450" Stroke="#1A1A1A" StrokeThickness="2" BackgroundColor="White" StrokeShape="RoundRectangle 28" Padding="16"> <VerticalStackLayout Spacing="10"> <Label Text="{Binding SurveyTitle}" HorizontalOptions="Center" FontAttributes="Bold" /> <Grid ColumnDefinitions="*,Auto"> <Label Text="{Binding ProgressText}" FontSize="12" /> <Label Grid.Column="1" Text="{Binding PercentText}" FontSize="12" /> </Grid> <BoxView HeightRequest="4" Color="#2BB673" /> <Label Text="{Binding QuestionNumberTitle}" FontAttributes="Bold" TextColor="#1C7D4D" /> <Label Text="{Binding Description}" FontSize="12" TextColor="{StaticResource TextSecondary}" /> <Button Text="Ja" /> <Button Text="Nej" /> <Grid Margin="0,20,0,0" ColumnDefinitions="*,*"> <Button Text="Forrige" /> <Button Grid.Column="1" Text="Næste" /> </Grid> </VerticalStackLayout> </Border> </Grid> </VerticalStackLayout> </ContentView> 
 
1.7 SectionCardView.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <ContentView x:Class="Feedback2Business.Views.Shared.SectionCardView" xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Name="This"> <Border Style="{StaticResource InnerCardStyle}"> <VerticalStackLayout> <Grid Padding="12" ColumnDefinitions="Auto,*,Auto"> <Label Text="⌄" /> <Label Grid.Column="1" Text="{Binding Title, Source={x:Reference This}}" FontAttributes="Bold" /> <Label Grid.Column="2" Text="⋮" /> </Grid> <CollectionView ItemsSource="{Binding ItemsSource, Source={x:Reference This}}" SelectionMode="Single"> <CollectionView.ItemTemplate> <DataTemplate> <Grid Padding="10,8" ColumnDefinitions="*,Auto"> <Label Text="{Binding DisplayTitle}" /> <Label Grid.Column="1" Text="{Binding TypeLabel}" TextColor="{StaticResource TextSecondary}" FontSize="12" /> </Grid> </DataTemplate> </CollectionView.ItemTemplate> </CollectionView> </VerticalStackLayout> </Border> </ContentView> 
Code-behind bindable properties will be added later in the scaffolding section.
 
1.8 QuestionPropertyPanel.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <ContentView x:Class="Feedback2Business.Views.Shared.QuestionPropertyPanel" xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"> <Border Style="{StaticResource InnerCardStyle}"> <ScrollView> <VerticalStackLayout Padding="16" Spacing="14"> <Label Text="Spørgsmålstype" Style="{StaticResource FormLabelStyle}" /> <Picker ItemsSource="{Binding QuestionTypes}" SelectedItem="{Binding SelectedQuestionType}" /> <Label Text="Titel" Style="{StaticResource FormLabelStyle}" /> <Entry Text="{Binding Title}" /> <Label Text="Beskrivelse" Style="{StaticResource FormLabelStyle}" /> <Editor Text="{Binding Description}" HeightRequest="100" /> <HorizontalStackLayout Spacing="8"> <CheckBox IsChecked="{Binding IsRequired}" /> <Label Text="Påkrævet" VerticalOptions="Center" /> </HorizontalStackLayout> <Label Text="Visning i app" Style="{StaticResource FormLabelStyle}" /> <HorizontalStackLayout Spacing="16"> <HorizontalStackLayout Spacing="6"> <RadioButton IsChecked="{Binding IsStandardDisplay}" GroupName="DisplayMode" /> <Label Text="Standard" VerticalOptions="Center" /> </HorizontalStackLayout> <HorizontalStackLayout Spacing="6"> <RadioButton IsChecked="{Binding IsCompactDisplay}" GroupName="DisplayMode" /> <Label Text="Kompakt" VerticalOptions="Center" /> </HorizontalStackLayout> </HorizontalStackLayout> <Label Text="Betinget logik" Style="{StaticResource FormLabelStyle}" /> <Button Text="+ Tilføj betingelse" Style="{StaticResource LinkButtonStyle}" /> <Label Text="Visningslogik" Style="{StaticResource FormLabelStyle}" /> <Button Text="+ Tilføj regel" Style="{StaticResource LinkButtonStyle}" /> <Label Text="Variabelnavn (valgfri)" Style="{StaticResource FormLabelStyle}" /> <Entry Text="{Binding VariableName}" /> <Button Text="Slet spørgsmål" TextColor="{StaticResource DangerRed}" BackgroundColor="Transparent" /> </VerticalStackLayout> </ScrollView> </Border> </ContentView> 
 
1.9 OrganizationsPage.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <ContentPage x:Class="Feedback2Business.Views.Organizations.OrganizationsPage" xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" BackgroundColor="{StaticResource PageBackground}"> <Grid RowDefinitions="Auto,Auto,*,Auto" Padding="24"> <Grid ColumnDefinitions="*,Auto"> <VerticalStackLayout> <Label Text="Organisationer" Style="{StaticResource PageTitleStyle}" /> <Label Text="Administrative oversigt over organisationer, brands og deres surveys." Style="{StaticResource PageSubtitleStyle}" /> </VerticalStackLayout> <Button Grid.Column="1" Text="+ Opret organisation" Style="{StaticResource PrimaryButtonStyle}" /> </Grid> <Border Grid.Row="1" Margin="0,20,0,16" Style="{StaticResource CardStyle}" Padding="12"> <Entry Placeholder="Søg organisationer..." Text="{Binding SearchText}" /> </Border> <Border Grid.Row="2" Style="{StaticResource CardStyle}"> <Grid RowDefinitions="Auto,*"> <Grid Padding="16" ColumnDefinitions="3*,*,*,*,*"> <Label Text="Navn" Style="{StaticResource DataTableHeaderStyle}" /> <Label Grid.Column="1" Text="Brands" Style="{StaticResource DataTableHeaderStyle}" /> <Label Grid.Column="2" Text="Surveys" Style="{StaticResource DataTableHeaderStyle}" /> <Label Grid.Column="3" Text="Brugere" Style="{StaticResource DataTableHeaderStyle}" /> <Label Grid.Column="4" Text="Sidst opdateret" Style="{StaticResource DataTableHeaderStyle}" /> </Grid> <CollectionView Grid.Row="1" ItemsSource="{Binding Organizations}"> <CollectionView.ItemTemplate> <DataTemplate> <Grid Padding="16" ColumnDefinitions="3*,*,*,*,*"> <Label Text="{Binding Name}" /> <Label Grid.Column="1" Text="{Binding BrandCount}" /> <Label Grid.Column="2" Text="{Binding SurveyCount}" /> <Label Grid.Column="3" Text="{Binding UserCount}" /> <Label Grid.Column="4" Text="{Binding UpdatedText}" /> </Grid> </DataTemplate> </CollectionView.ItemTemplate> </CollectionView> </Grid> </Border> <HorizontalStackLayout Grid.Row="3" Margin="0,12,0,0" HorizontalOptions="End" Spacing="8"> <Button Text="1" /> <Button Text="2" /> <Button Text="3" /> </HorizontalStackLayout> </Grid> </ContentPage> 
 
1.10 OrganizationUsersPage.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <ContentPage x:Class="Feedback2Business.Views.Organizations.OrganizationUsersPage" xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" xmlns:shared="clr-namespace:Feedback2Business.Views.Shared" BackgroundColor="{StaticResource PageBackground}"> <Grid RowDefinitions="Auto,Auto,Auto,*,Auto" Padding="24"> <VerticalStackLayout> <Label Text="Brugere" Style="{StaticResource PageTitleStyle}" /> <Label Text="Administrer brugere i organisationen." Style="{StaticResource PageSubtitleStyle}" /> </VerticalStackLayout> <HorizontalStackLayout Grid.Row="1" Spacing="24" Margin="0,16,0,0"> <Label Text="Brugere" Style="{StaticResource ActiveTabTextStyle}" /> <Label Text="Inviterede" Style="{StaticResource TabTextStyle}" /> </HorizontalStackLayout> <Grid Grid.Row="2" Margin="0,16,0,16" ColumnDefinitions="*,160,160,Auto" ColumnSpacing="12"> <Entry Placeholder="Søg brugere..." Text="{Binding SearchText}" /> <Picker Grid.Column="1" Title="Rolle" ItemsSource="{Binding Roles}" /> <Picker Grid.Column="2" Title="Status" ItemsSource="{Binding Statuses}" /> <Button Grid.Column="3" Text="+ Inviter bruger" Style="{StaticResource PrimaryButtonStyle}" /> </Grid> <Border Grid.Row="3" Style="{StaticResource CardStyle}"> <Grid RowDefinitions="Auto,*"> <Grid Padding="16" ColumnDefinitions="2*,3*,*,*,*"> <Label Text="Navn" Style="{StaticResource DataTableHeaderStyle}" /> <Label Grid.Column="1" Text="Email" Style="{StaticResource DataTableHeaderStyle}" /> <Label Grid.Column="2" Text="Rolle" Style="{StaticResource DataTableHeaderStyle}" /> <Label Grid.Column="3" Text="Status" Style="{StaticResource DataTableHeaderStyle}" /> <Label Grid.Column="4" Text="Sidst aktiv" Style="{StaticResource DataTableHeaderStyle}" /> </Grid> <CollectionView Grid.Row="1" ItemsSource="{Binding Users}"> <CollectionView.ItemTemplate> <DataTemplate> <Grid Padding="16" ColumnDefinitions="2*,3*,*,*,*"> <Label Text="{Binding Name}" /> <Label Grid.Column="1" Text="{Binding Email}" /> <Label Grid.Column="2" Text="{Binding Role}" /> <shared:StatusBadgeView Grid.Column="3" Text="{Binding Status}" BadgeColor="#D9F5E5" TextColor="#1E8E5A" /> <Label Grid.Column="4" Text="{Binding LastActiveText}" /> </Grid> </DataTemplate> </CollectionView.ItemTemplate> </CollectionView> </Grid> </Border> <HorizontalStackLayout Grid.Row="4" HorizontalOptions="End" Spacing="8" Margin="0,12,0,0"> <Button Text="1" /> <Button Text="2" /> <Button Text="3" /> </HorizontalStackLayout> </Grid> </ContentPage> 
 
1.11 TemplatesPage.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <ContentPage x:Class="Feedback2Business.Views.Templates.TemplatesPage" xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" BackgroundColor="{StaticResource PageBackground}"> <Grid RowDefinitions="Auto,Auto,Auto,*,Auto" Padding="24"> <Grid ColumnDefinitions="*,Auto"> <VerticalStackLayout> <Label Text="Skabeloner" Style="{StaticResource PageTitleStyle}" /> <Label Text="Opret og administrer skabeloner til spørgeskemaer og tjeklister." Style="{StaticResource PageSubtitleStyle}" /> </VerticalStackLayout> <Button Grid.Column="1" Text="+ Ny skabelon" Style="{StaticResource PrimaryButtonStyle}" /> </Grid> <HorizontalStackLayout Grid.Row="1" Margin="0,16,0,0" Spacing="24"> <Label Text="Skabeloner" Style="{StaticResource ActiveTabTextStyle}" /> <Label Text="Gruppering" Style="{StaticResource TabTextStyle}" /> </HorizontalStackLayout> <Border Grid.Row="2" Margin="0,16,0,16" Style="{StaticResource CardStyle}" Padding="12"> <Entry Placeholder="Søg skabeloner..." Text="{Binding SearchText}" /> </Border> <Border Grid.Row="3" Style="{StaticResource CardStyle}"> <Grid RowDefinitions="Auto,*"> <Grid Padding="16" ColumnDefinitions="2*,*,3*,*"> <Label Text="Navn" Style="{StaticResource DataTableHeaderStyle}" /> <Label Grid.Column="1" Text="Type" Style="{StaticResource DataTableHeaderStyle}" /> <Label Grid.Column="2" Text="Beskrivelse" Style="{StaticResource DataTableHeaderStyle}" /> <Label Grid.Column="3" Text="Sidst opdateret" Style="{StaticResource DataTableHeaderStyle}" /> </Grid> <CollectionView Grid.Row="1" ItemsSource="{Binding Templates}"> <CollectionView.ItemTemplate> <DataTemplate> <Grid Padding="16" ColumnDefinitions="2*,*,3*,*"> <Label Text="{Binding Name}" /> <Label Grid.Column="1" Text="{Binding Type}" /> <Label Grid.Column="2" Text="{Binding Description}" /> <Label Grid.Column="3" Text="{Binding UpdatedText}" /> </Grid> </DataTemplate> </CollectionView.ItemTemplate> </CollectionView> </Grid> </Border> </Grid> </ContentPage> 
 
1.12 VariablesPage.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <ContentPage x:Class="Feedback2Business.Views.Variables.VariablesPage" xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" BackgroundColor="{StaticResource PageBackground}"> <Grid RowDefinitions="Auto,Auto,*,Auto" Padding="24"> <Grid ColumnDefinitions="*,Auto"> <VerticalStackLayout> <Label Text="Variabler" Style="{StaticResource PageTitleStyle}" /> <Label Text="Opret variabler som kan bruges dynamisk i spørgsmål." Style="{StaticResource PageSubtitleStyle}" /> </VerticalStackLayout> <Button Grid.Column="1" Text="+ Ny variabel" Style="{StaticResource PrimaryButtonStyle}" /> </Grid> <HorizontalStackLayout Grid.Row="1" Margin="0,16,0,16" Spacing="24"> <Label Text="Variabler" Style="{StaticResource ActiveTabTextStyle}" /> <Label Text="Variabelsæt" Style="{StaticResource TabTextStyle}" /> </HorizontalStackLayout> <Border Grid.Row="2" Style="{StaticResource CardStyle}"> <Grid RowDefinitions="Auto,*"> <Grid Padding="16" ColumnDefinitions="2*,2*,*,*,*"> <Label Text="Navn" Style="{StaticResource DataTableHeaderStyle}" /> <Label Grid.Column="1" Text="Nøgle" Style="{StaticResource DataTableHeaderStyle}" /> <Label Grid.Column="2" Text="Type" Style="{StaticResource DataTableHeaderStyle}" /> <Label Grid.Column="3" Text="Standardværdi" Style="{StaticResource DataTableHeaderStyle}" /> <Label Grid.Column="4" Text="Bruges i" Style="{StaticResource DataTableHeaderStyle}" /> </Grid> <CollectionView Grid.Row="1" ItemsSource="{Binding Variables}"> <CollectionView.ItemTemplate> <DataTemplate> <Grid Padding="16" ColumnDefinitions="2*,2*,*,*,*"> <Label Text="{Binding Name}" /> <Label Grid.Column="1" Text="{Binding Key}" /> <Label Grid.Column="2" Text="{Binding Type}" /> <Label Grid.Column="3" Text="{Binding DefaultValue}" /> <Label Grid.Column="4" Text="{Binding UsageCountText}" /> </Grid> </DataTemplate> </CollectionView.ItemTemplate> </CollectionView> </Grid> </Border> </Grid> </ContentPage> 
 
1.13 MediaLibraryPage.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <ContentPage x:Class="Feedback2Business.Views.Media.MediaLibraryPage" xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" xmlns:media="clr-namespace:Feedback2Business.Views.Media" BackgroundColor="{StaticResource PageBackground}"> <Grid RowDefinitions="Auto,Auto,Auto,*,Auto" Padding="24"> <Grid ColumnDefinitions="*,Auto"> <VerticalStackLayout> <Label Text="Mediebibliotek" Style="{StaticResource PageTitleStyle}" /> <Label Text="Upload og administrer billeder og filer." Style="{StaticResource PageSubtitleStyle}" /> </VerticalStackLayout> <Button Grid.Column="1" Text="Upload fil" Style="{StaticResource PrimaryButtonStyle}" /> </Grid> <HorizontalStackLayout Grid.Row="1" Margin="0,16,0,0" Spacing="24"> <Label Text="Filer" Style="{StaticResource ActiveTabTextStyle}" /> <Label Text="Mapper" Style="{StaticResource TabTextStyle}" /> </HorizontalStackLayout> <Grid Grid.Row="2" Margin="0,16,0,16" ColumnDefinitions="*,180,Auto" ColumnSpacing="12"> <Entry Placeholder="Søg filer..." Text="{Binding SearchText}" /> <Picker Grid.Column="1" Title="Type" ItemsSource="{Binding FileTypes}" /> <Button Grid.Column="2" Text="Upload fil" Style="{StaticResource PrimaryButtonStyle}" /> </Grid> <CollectionView Grid.Row="3" ItemsSource="{Binding MediaItems}"> <CollectionView.ItemsLayout> <GridItemsLayout Orientation="Vertical" Span="4" /> </CollectionView.ItemsLayout> <CollectionView.ItemTemplate> <DataTemplate> <media:MediaCardView /> </DataTemplate> </CollectionView.ItemTemplate> </CollectionView> <HorizontalStackLayout Grid.Row="4" HorizontalOptions="End" Spacing="8"> <Button Text="1" /> <Button Text="2" /> <Button Text="3" /> </HorizontalStackLayout> </Grid> </ContentPage> 
 
1.14 MediaCardView.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <ContentView x:Class="Feedback2Business.Views.Media.MediaCardView" xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"> <Border Style="{StaticResource InnerCardStyle}" Padding="8" Margin="6"> <VerticalStackLayout Spacing="8"> <Border HeightRequest="110" BackgroundColor="#F3F6FB" StrokeThickness="0" StrokeShape="RoundRectangle 8"> <Image Source="{Binding ThumbnailUrl}" Aspect="AspectFill" /> </Border> <Label Text="{Binding Name}" FontAttributes="Bold" LineBreakMode="TailTruncation" /> <Label Text="{Binding MetaText}" FontSize="12" TextColor="{StaticResource TextSecondary}" /> </VerticalStackLayout> </Border> </ContentView> 
 
1.15 SettingsGeneralPage.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <ContentPage x:Class="Feedback2Business.Views.Settings.SettingsGeneralPage" xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" xmlns:shared="clr-namespace:Feedback2Business.Views.Shared" BackgroundColor="{StaticResource PageBackground}"> <Grid ColumnDefinitions="220,*" Padding="24" ColumnSpacing="16"> <shared:SettingsNavView Grid.Column="0" SelectedKey="General" /> <Grid Grid.Column="1" RowDefinitions="Auto,*"> <Grid ColumnDefinitions="*,Auto" Margin="0,0,0,16"> <Label Text="Indstillinger" Style="{StaticResource PageTitleStyle}" /> <HorizontalStackLayout Grid.Column="1" Spacing="12"> <Button Text="Gem ændringer" Style="{StaticResource PrimaryButtonStyle}" /> <Button Text="Flere handlinger" Style="{StaticResource SecondaryButtonStyle}" /> </HorizontalStackLayout> </Grid> <Border Grid.Row="1" Style="{StaticResource CardStyle}"> <ScrollView> <VerticalStackLayout Padding="24" Spacing="16"> <Label Text="Organisationsnavn" Style="{StaticResource FormLabelStyle}" /> <Entry Text="{Binding OrganizationName}" /> <Label Text="Primært sprog" Style="{StaticResource FormLabelStyle}" /> <Picker ItemsSource="{Binding Languages}" SelectedItem="{Binding SelectedLanguage}" /> <Label Text="Tidszone" Style="{StaticResource FormLabelStyle}" /> <Picker ItemsSource="{Binding Timezones}" SelectedItem="{Binding SelectedTimezone}" /> <Label Text="Dashboard" Style="{StaticResource FormLabelStyle}" /> <Entry Text="{Binding DashboardName}" /> <Label Text="Datoformat" Style="{StaticResource FormLabelStyle}" /> <Entry Text="{Binding DateFormat}" /> <Label Text="Logo" Style="{StaticResource FormLabelStyle}" /> <HorizontalStackLayout Spacing="16"> <Border WidthRequest="96" HeightRequest="96" BackgroundColor="{StaticResource PrimaryBlue}" StrokeThickness="0" StrokeShape="RoundRectangle 12" /> <VerticalStackLayout> <Button Text="Skift logo" /> <Button Text="Fjern" /> </VerticalStackLayout> </HorizontalStackLayout> </VerticalStackLayout> </ScrollView> </Border> </Grid> </Grid> </ContentPage> 
 
1.16 SettingsAppPage.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <ContentPage x:Class="Feedback2Business.Views.Settings.SettingsAppPage" xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" xmlns:shared="clr-namespace:Feedback2Business.Views.Shared" BackgroundColor="{StaticResource PageBackground}"> <Grid ColumnDefinitions="220,*" Padding="24" ColumnSpacing="16"> <shared:SettingsNavView Grid.Column="0" SelectedKey="AppSettings" /> <Grid Grid.Column="1" RowDefinitions="Auto,*"> <Grid ColumnDefinitions="*,Auto" Margin="0,0,0,16"> <Label Text="App-indstillinger" Style="{StaticResource PageTitleStyle}" /> <Button Grid.Column="1" Text="Gem ændringer" Style="{StaticResource PrimaryButtonStyle}" /> </Grid> <Border Grid.Row="1" Style="{StaticResource CardStyle}"> <ScrollView> <VerticalStackLayout Padding="24" Spacing="18"> <Border Style="{StaticResource InnerCardStyle}" Padding="16"> <VerticalStackLayout Spacing="10"> <Label Text="Offline" FontAttributes="Bold" /> <HorizontalStackLayout> <CheckBox IsChecked="{Binding OfflineEnabled}" /> <Label Text="Tillad offline udfyldelse" VerticalOptions="Center" /> </HorizontalStackLayout> <HorizontalStackLayout> <CheckBox IsChecked="{Binding AutoSyncEnabled}" /> <Label Text="Synkroniser automatisk når forbindelse er tilgængelig" VerticalOptions="Center" /> </HorizontalStackLayout> </VerticalStackLayout> </Border> <Border Style="{StaticResource InnerCardStyle}" Padding="16"> <VerticalStackLayout Spacing="10"> <Label Text="Placering" FontAttributes="Bold" /> <HorizontalStackLayout> <CheckBox IsChecked="{Binding RequireLocationAtStart}" /> <Label Text="Kræv lokationsdata ved start af survey" VerticalOptions="Center" /> </HorizontalStackLayout> <HorizontalStackLayout> <CheckBox IsChecked="{Binding UseLocationOnObservations}" /> <Label Text="Brug lokation til geotagging af besvarelser" VerticalOptions="Center" /> </HorizontalStackLayout> </VerticalStackLayout> </Border> <Border Style="{StaticResource InnerCardStyle}" Padding="16"> <VerticalStackLayout Spacing="10"> <Label Text="Medier" FontAttributes="Bold" /> <HorizontalStackLayout> <CheckBox IsChecked="{Binding CompressImages}" /> <Label Text="Komprimer billeder før upload" VerticalOptions="Center" /> </HorizontalStackLayout> <Label Text="Maks. billedstørrelse" Style="{StaticResource FormLabelStyle}" /> <Picker ItemsSource="{Binding MaxFileSizeOptions}" SelectedItem="{Binding SelectedMaxFileSize}" /> </VerticalStackLayout> </Border> <Border Style="{StaticResource InnerCardStyle}" Padding="16"> <VerticalStackLayout Spacing="10"> <Label Text="Navigation" FontAttributes="Bold" /> <HorizontalStackLayout> <CheckBox IsChecked="{Binding ShowSurveyProgress}" /> <Label Text="Vis survey fremdrift" VerticalOptions="Center" /> </HorizontalStackLayout> <HorizontalStackLayout> <CheckBox IsChecked="{Binding AllowSkipForward}" /> <Label Text="Tillad spring fremad i survey" VerticalOptions="Center" /> </HorizontalStackLayout> </VerticalStackLayout> </Border> </VerticalStackLayout> </ScrollView> </Border> </Grid> </Grid> </ContentPage> 
 
1.17 RolesPermissionsPage.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <ContentPage x:Class="Feedback2Business.Views.Roles.RolesPermissionsPage" xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" BackgroundColor="{StaticResource PageBackground}"> <Grid RowDefinitions="Auto,Auto,*" Padding="24"> <Grid ColumnDefinitions="*,Auto"> <VerticalStackLayout> <Label Text="Roller &amp; rettigheder" Style="{StaticResource PageTitleStyle}" /> <Label Text="Definér roller og hvad brugere har adgang til." Style="{StaticResource PageSubtitleStyle}" /> </VerticalStackLayout> <Button Grid.Column="1" Text="+ Ny rolle" Style="{StaticResource PrimaryButtonStyle}" /> </Grid> <HorizontalStackLayout Grid.Row="1" Margin="0,16,0,16" Spacing="24"> <Label Text="Roller" Style="{StaticResource ActiveTabTextStyle}" /> <Label Text="Rettigheder" Style="{StaticResource TabTextStyle}" /> </HorizontalStackLayout> <Grid Grid.Row="2" ColumnDefinitions="320,*" ColumnSpacing="16"> <Border Grid.Column="0" Style="{StaticResource CardStyle}"> <Grid RowDefinitions="Auto,*"> <Grid Padding="16" ColumnDefinitions="*,*"> <Label Text="Rollenavn" Style="{StaticResource DataTableHeaderStyle}" /> <Label Grid.Column="1" Text="Beskrivelse" Style="{StaticResource DataTableHeaderStyle}" /> </Grid> <CollectionView Grid.Row="1" ItemsSource="{Binding Roles}" SelectedItem="{Binding SelectedRole}"> <CollectionView.ItemTemplate> <DataTemplate> <Grid Padding="16" ColumnDefinitions="*,*"> <Label Text="{Binding Name}" /> <Label Grid.Column="1" Text="{Binding Description}" /> </Grid> </DataTemplate> </CollectionView.ItemTemplate> </CollectionView> </Grid> </Border> <Border Grid.Column="1" Style="{StaticResource CardStyle}"> <ScrollView> <VerticalStackLayout Padding="24" Spacing="16"> <Label Text="{Binding SelectedRole.Name}" FontAttributes="Bold" FontSize="18" /> <VerticalStackLayout BindableLayout.ItemsSource="{Binding PermissionGroups}"> <BindableLayout.ItemTemplate> <DataTemplate> <Border Style="{StaticResource InnerCardStyle}" Padding="16" Margin="0,0,0,8"> <VerticalStackLayout Spacing="10"> <Label Text="{Binding Name}" FontAttributes="Bold" /> <VerticalStackLayout BindableLayout.ItemsSource="{Binding Permissions}"> <BindableLayout.ItemTemplate> <DataTemplate> <HorizontalStackLayout Spacing="8"> <CheckBox IsChecked="{Binding IsEnabled}" /> <Label Text="{Binding Label}" VerticalOptions="Center" /> </HorizontalStackLayout> </DataTemplate> </BindableLayout.ItemTemplate> </VerticalStackLayout> </VerticalStackLayout> </Border> </DataTemplate> </BindableLayout.ItemTemplate> </VerticalStackLayout> </VerticalStackLayout> </ScrollView> </Border> </Grid> </Grid> </ContentPage> 
 
1.18 ActivityLogPage.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <ContentPage x:Class="Feedback2Business.Views.ActivityLog.ActivityLogPage" xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" BackgroundColor="{StaticResource PageBackground}"> <Grid RowDefinitions="Auto,Auto,*,Auto" Padding="24"> <VerticalStackLayout> <Label Text="Aktivitetslog" Style="{StaticResource PageTitleStyle}" /> <Label Text="Se ændringer og aktiviteter i organisationen." Style="{StaticResource PageSubtitleStyle}" /> </VerticalStackLayout> <Grid Grid.Row="1" Margin="0,16,0,16" ColumnDefinitions="*,160,160,160" ColumnSpacing="12"> <Entry Placeholder="Søg log..." Text="{Binding SearchText}" /> <Picker Grid.Column="1" Title="Handling" ItemsSource="{Binding Actions}" /> <Picker Grid.Column="2" Title="Bruger" ItemsSource="{Binding Users}" /> <Picker Grid.Column="3" Title="Periode" ItemsSource="{Binding Periods}" /> </Grid> <Border Grid.Row="2" Style="{StaticResource CardStyle}"> <Grid RowDefinitions="Auto,*"> <Grid Padding="16" ColumnDefinitions="2*,2*,*,3*"> <Label Text="Tidspunkt" Style="{StaticResource DataTableHeaderStyle}" /> <Label Grid.Column="1" Text="Handling" Style="{StaticResource DataTableHeaderStyle}" /> <Label Grid.Column="2" Text="Bruger" Style="{StaticResource DataTableHeaderStyle}" /> <Label Grid.Column="3" Text="Detaljer" Style="{StaticResource DataTableHeaderStyle}" /> </Grid> <CollectionView Grid.Row="1" ItemsSource="{Binding ActivityEvents}"> <CollectionView.ItemTemplate> <DataTemplate> <Grid Padding="16" ColumnDefinitions="2*,2*,*,3*"> <Label Text="{Binding TimestampText}" /> <Label Grid.Column="1" Text="{Binding Action}" /> <Label Grid.Column="2" Text="{Binding UserName}" /> <Label Grid.Column="3" Text="{Binding Details}" /> </Grid> </DataTemplate> </CollectionView.ItemTemplate> </CollectionView> </Grid> </Border> <HorizontalStackLayout Grid.Row="3" HorizontalOptions="End" Spacing="8" Margin="0,12,0,0"> <Button Text="1" /> <Button Text="2" /> <Button Text="3" /> </HorizontalStackLayout> </Grid> </ContentPage> 
 
2. Control Hierarchy Per Page
Below is a practical hierarchy for implementation.
 
2.1 DashboardShellPage
•	ContentPage
•	Grid
	Column 0: SidebarView
	Column 1: Grid
	Row 0: TopHeaderView
	Row 1: dynamic content host
	current page view
 
2.2 SidebarView
•	ContentView
•	Grid
	Row 0: branding
	VerticalStackLayout
	app title label
	subtitle label
	Row 1: nav list
	CollectionView
	item template
	Border
	Grid
	icon
	title
	Row 2: current user card
	Border
	Grid
	initials badge
	user info stack
	chevron
 
2.3 TopHeaderView
•	ContentView
•	Grid
	left breadcrumb stack
	primary crumb
	separator
	secondary crumb
	right utility/action area
	utility icons or page actions
 
2.4 OrganizationsPage
•	ContentPage
•	Grid
	Row 0: page header
	title/subtitle
	create button
	Row 1: search card
	Entry
	Row 2: table card
	Grid
	table header row
	CollectionView rows
	Row 3: pagination
 
2.5 OrganizationBrandsPage
•	ContentPage
•	Grid
	Row 0: organization header
	icon tile
	title/status/tabs
	actions
	Row 1: 3-column main layout
	Column 0: brands panel
	title row
	add button
	brands CollectionView
	Column 1: surveys/preview panel
	title row
	add button
	surveys CollectionView
	MobilePreviewView
	Column 2: builder panel
	survey summary header
	primary editor tabs
	secondary build tabs
	nested Grid
	left: sections ScrollView
	section cards
	add section button
	right: QuestionPropertyPanel
 
2.6 SectionCardView
•	ContentView
•	Border
	VerticalStackLayout
	section header Grid
	expand icon
	title
	menu icon
	question CollectionView
	question row
	title
	type label
 
2.7 QuestionPropertyPanel
•	ContentView
•	Border
	ScrollView
	VerticalStackLayout
	type label + Picker
	title label + Entry
	description label + Editor
	required checkbox row
	display mode radio rows
	conditional logic button
	display logic button
	variable name entry
	delete button
 
2.8 OrganizationUsersPage
•	ContentPage
•	Grid
	title/subtitle
	sub-tabs row
	toolbar row
	search entry
	role picker
	status picker
	invite button
	table card
	header row
	CollectionView
	pagination row
 
2.9 TemplatesPage
•	ContentPage
•	Grid
	title/subtitle + create button
	sub-tabs row
	search row
	table card
	header row
	CollectionView
 
2.10 VariablesPage
•	ContentPage
•	Grid
	title/subtitle + create button
	sub-tabs row
	table card
	header row
	CollectionView
 
2.11 MediaLibraryPage
•	ContentPage
•	Grid
	title/subtitle + upload button
	sub-tabs row
	toolbar row
	search
	type filter
	upload button
	CollectionView
	grid layout
	MediaCardView
	pagination
 
2.12 MediaCardView
•	ContentView
•	Border
	VerticalStackLayout
	preview border/image
	file name label
	metadata label
 
2.13 SettingsGeneralPage
•	ContentPage
•	Grid
	Column 0: SettingsNavView
	Column 1: main settings area
	title + actions
	form card
	ScrollView
	stacked form fields
	logo upload section
 
2.14 SettingsAppPage
•	ContentPage
•	Grid
	Column 0: SettingsNavView
	Column 1:
	title + save button
	scrollable grouped setting cards
	Offline
	Location
	Media
	Navigation
 
2.15 RolesPermissionsPage
•	ContentPage
•	Grid
	title/subtitle + new role button
	sub-tabs row
	2-column content
	left role list card
	right permission editor card
	selected role title
	permission group cards
	group name
	permission checkboxes
 
2.16 ActivityLogPage
•	ContentPage
•	Grid
	title/subtitle
	filter toolbar
	table card
	header row
	CollectionView
	pagination row
 
3. C# ViewModel and Model Scaffolding
Below is starter scaffolding for MVVM.
 
3.1 Base Classes
ObservableObject.cs
csharp
using System.ComponentModel; using System.Runtime.CompilerServices; namespace Feedback2Business.ViewModels; public abstract class ObservableObject : INotifyPropertyChanged { public event PropertyChangedEventHandler? PropertyChanged; protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string? propertyName = null) { if (EqualityComparer<T>.Default.Equals(backingStore, value)) return false; backingStore = value; OnPropertyChanged(propertyName); return true; } protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); } } 
RelayCommand.cs
csharp
using System.Windows.Input; namespace Feedback2Business.ViewModels; public class RelayCommand : ICommand { private readonly Action _execute; private readonly Func<bool>? _canExecute; public RelayCommand(Action execute, Func<bool>? canExecute = null) { _execute = execute; _canExecute = canExecute; } public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true; public void Execute(object? parameter) => _execute(); public event EventHandler? CanExecuteChanged; public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty); } public class RelayCommand<T> : ICommand { private readonly Action<T?> _execute; private readonly Func<T?, bool>? _canExecute; public RelayCommand(Action<T?> execute, Func<T?, bool>? canExecute = null) { _execute = execute; _canExecute = canExecute; } public bool CanExecute(object? parameter) => _canExecute?.Invoke((T?)parameter) ?? true; public void Execute(object? parameter) => _execute((T?)parameter); public event EventHandler? CanExecuteChanged; public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty); } 
 
3.2 Models
NavigationItem.cs
csharp
namespace Feedback2Business.Models; public class NavigationItem { public string Key { get; set; } = string.Empty; public string Title { get; set; } = string.Empty; public string IconGlyph { get; set; } = string.Empty; public bool IsSelected { get; set; } public string BackgroundColor => IsSelected ? "#12345F" : "Transparent"; } 
UserModel.cs
csharp
namespace Feedback2Business.Models; public class UserModel { public string Id { get; set; } = Guid.NewGuid().ToString(); public string Name { get; set; } = string.Empty; public string Email { get; set; } = string.Empty; public string Role { get; set; } = string.Empty; public string Status { get; set; } = string.Empty; public DateTime? LastActiveAt { get; set; } public string LastActiveText => LastActiveAt?.ToString("dd. MM yy") ?? "-"; } 
OrganizationModel.cs
csharp
namespace Feedback2Business.Models; public class OrganizationModel { public string Id { get; set; } = Guid.NewGuid().ToString(); public string Name { get; set; } = string.Empty; public int BrandCount { get; set; } public int SurveyCount { get; set; } public int UserCount { get; set; } public DateTime UpdatedAt { get; set; } public string UpdatedText => UpdatedAt.ToString("dd. MMM yyyy"); } 
BrandModel.cs
csharp
namespace Feedback2Business.Models; public class BrandModel { public string Id { get; set; } = Guid.NewGuid().ToString(); public string Name { get; set; } = string.Empty; public string LogoUrl { get; set; } = string.Empty; public int SurveyCount { get; set; } public bool IsSelected { get; set; } public string SurveyCountText => $"{SurveyCount} surveys"; public string ItemBackground => IsSelected ? "#EEF4FF" : "Transparent"; } 
SurveyModel.cs
csharp
namespace Feedback2Business.Models; public class SurveyModel { public string Id { get; set; } = Guid.NewGuid().ToString(); public string Name { get; set; } = string.Empty; public int Version { get; set; } public int QuestionCount { get; set; } public bool IsSelected { get; set; } public string VersionText => $"Version {Version} · {QuestionCount} spørgsmål"; public string ItemBackground => IsSelected ? "#EEF4FF" : "Transparent"; } 
SurveySectionModel.cs
csharp
namespace Feedback2Business.Models; public class SurveySectionModel { public string Id { get; set; } = Guid.NewGuid().ToString(); public string Title { get; set; } = string.Empty; public bool IsExpanded { get; set; } = true; public List<SurveyQuestionModel> Questions { get; set; } = new(); } 
SurveyQuestionModel.cs
csharp
namespace Feedback2Business.Models; public class SurveyQuestionModel { public string Id { get; set; } = Guid.NewGuid().ToString(); public string NumberLabel { get; set; } = string.Empty; public string Title { get; set; } = string.Empty; public string Description { get; set; } = string.Empty; public string Type { get; set; } = string.Empty; public bool IsRequired { get; set; } public string VariableName { get; set; } = string.Empty; public string DisplayMode { get; set; } = "Standard"; public string DisplayTitle => $"{NumberLabel} {Title}"; public string TypeLabel => Type; } 
TemplateModel.cs
csharp
namespace Feedback2Business.Models; public class TemplateModel { public string Id { get; set; } = Guid.NewGuid().ToString(); public string Name { get; set; } = string.Empty; public string Type { get; set; } = string.Empty; public string Description { get; set; } = string.Empty; public DateTime UpdatedAt { get; set; } public string UpdatedText => UpdatedAt.ToString("dd. MMM yyyy"); } 
VariableModel.cs
csharp
namespace Feedback2Business.Models; public class VariableModel { public string Id { get; set; } = Guid.NewGuid().ToString(); public string Name { get; set; } = string.Empty; public string Key { get; set; } = string.Empty; public string Type { get; set; } = string.Empty; public string DefaultValue { get; set; } = string.Empty; public int UsageCount { get; set; } public string UsageCountText => $"{UsageCount} surveys"; } 
MediaItemModel.cs
csharp
namespace Feedback2Business.Models; public class MediaItemModel { public string Id { get; set; } = Guid.NewGuid().ToString(); public string Name { get; set; } = string.Empty; public string ThumbnailUrl { get; set; } = string.Empty; public string FileType { get; set; } = string.Empty; public string MetaText { get; set; } = string.Empty; } 
Permission Models
csharp
namespace Feedback2Business.Models; public class PermissionModel { public string Id { get; set; } = Guid.NewGuid().ToString(); public string Label { get; set; } = string.Empty; public bool IsEnabled { get; set; } } public class PermissionGroupModel { public string Name { get; set; } = string.Empty; public List<PermissionModel> Permissions { get; set; } = new(); } public class RoleModel { public string Id { get; set; } = Guid.NewGuid().ToString(); public string Name { get; set; } = string.Empty; public string Description { get; set; } = string.Empty; public List<PermissionGroupModel> PermissionGroups { get; set; } = new(); } 
ActivityEventModel.cs
csharp
namespace Feedback2Business.Models; public class ActivityEventModel { public string Id { get; set; } = Guid.NewGuid().ToString(); public DateTime Timestamp { get; set; } public string Action { get; set; } = string.Empty; public string UserName { get; set; } = string.Empty; public string Details { get; set; } = string.Empty; public string TimestampText => Timestamp.ToString("dd. MMM yyyy, HH:mm"); } 
MobilePreviewModel.cs
csharp
namespace Feedback2Business.Models; public class MobilePreviewModel { public string SurveyTitle { get; set; } = string.Empty; public string ProgressText { get; set; } = string.Empty; public string PercentText { get; set; } = string.Empty; public string QuestionNumberTitle { get; set; } = string.Empty; public string Description { get; set; } = string.Empty; } 
 
3.3 ViewModels
MainShellViewModel.cs
csharp
using System.Collections.ObjectModel; using Feedback2Business.Models; namespace Feedback2Business.ViewModels; public class MainShellViewModel : ObservableObject { private NavigationItem? _selectedNavigationItem; public ObservableCollection<NavigationItem> NavigationItems { get; } = new(); public UserModel CurrentUser { get; } = new() { Name = "Anders Kirk", Role = "Admin" }; public string BreadcrumbPrimary { get; set; } = "Organisationer"; public string BreadcrumbSecondary { get; set; } = "Retail Group A"; public NavigationItem? SelectedNavigationItem { get => _selectedNavigationItem; set { if (SetProperty(ref _selectedNavigationItem, value)) { foreach (var item in NavigationItems) item.IsSelected = item == value; OnPropertyChanged(nameof(NavigationItems)); } } } public MainShellViewModel() { NavigationItems.Add(new NavigationItem { Key = "Overview", Title = "Overblik" }); NavigationItems.Add(new NavigationItem { Key = "Organizations", Title = "Organisationer", IsSelected = true }); NavigationItems.Add(new NavigationItem { Key = "Users", Title = "Brugere" }); NavigationItems.Add(new NavigationItem { Key = "Roles", Title = "Roller & rettigheder" }); NavigationItems.Add(new NavigationItem { Key = "Templates", Title = "Skabeloner" }); NavigationItems.Add(new NavigationItem { Key = "Variables", Title = "Variabler" }); NavigationItems.Add(new NavigationItem { Key = "Media", Title = "Mediebibliotek" }); NavigationItems.Add(new NavigationItem { Key = "Settings", Title = "Indstillinger" }); NavigationItems.Add(new NavigationItem { Key = "ActivityLog", Title = "Aktivitetslog" }); SelectedNavigationItem = NavigationItems.FirstOrDefault(i => i.IsSelected); } } 
OrganizationsViewModel.cs
csharp
using System.Collections.ObjectModel; using Feedback2Business.Models; namespace Feedback2Business.ViewModels; public class OrganizationsViewModel : ObservableObject { private string _searchText = string.Empty; public string SearchText { get => _searchText; set => SetProperty(ref _searchText, value); } public ObservableCollection<OrganizationModel> Organizations { get; } = new(); public RelayCommand CreateOrganizationCommand { get; } public OrganizationsViewModel() { CreateOrganizationCommand = new RelayCommand(() => { }); Organizations.Add(new OrganizationModel { Name = "Retail Group A", BrandCount = 3, SurveyCount = 20, UserCount = 5, UpdatedAt = DateTime.Today.AddDays(-2) }); Organizations.Add(new OrganizationModel { Name = "FoodCo Holding", BrandCount = 2, SurveyCount = 14, UserCount = 3, UpdatedAt = DateTime.Today.AddDays(-5) }); Organizations.Add(new OrganizationModel { Name = "Service Partners", BrandCount = 4, SurveyCount = 34, UserCount = 8, UpdatedAt = DateTime.Today.AddDays(-10) }); } } 
OrganizationBrandsViewModel.cs
csharp
using System.Collections.ObjectModel; using Feedback2Business.Models; namespace Feedback2Business.ViewModels; public class OrganizationBrandsViewModel : ObservableObject { private BrandModel? _selectedBrand; private SurveyModel? _selectedSurvey; private SurveyQuestionEditorViewModel? _selectedQuestion; public ObservableCollection<BrandModel> Brands { get; } = new(); public ObservableCollection<SurveyModel> Surveys { get; } = new(); public ObservableCollection<SurveyQuestionModel> Section1Questions { get; } = new(); public ObservableCollection<SurveyQuestionModel> Section2Questions { get; } = new(); public ObservableCollection<SurveyQuestionModel> Section3Questions { get; } = new(); public MobilePreviewModel SelectedQuestionPreview { get; } = new() { SurveyTitle = "Butiksinspektion", ProgressText = "2 af 24", PercentText = "8%", QuestionNumberTitle = "2.1 Facade ren og vedligeholdt?", Description = "Angiv om facaden fremstår ren og uden skader." }; public BrandModel? SelectedBrand { get => _selectedBrand; set { if (SetProperty(ref _selectedBrand, value)) { foreach (var item in Brands) item.IsSelected = item == value; OnPropertyChanged(nameof(Brands)); } } } public SurveyModel? SelectedSurvey { get => _selectedSurvey; set { if (SetProperty(ref _selectedSurvey, value)) { foreach (var item in Surveys) item.IsSelected = item == value; OnPropertyChanged(nameof(Surveys)); } } } public SurveyQuestionEditorViewModel? SelectedQuestion { get => _selectedQuestion; set => SetProperty(ref _selectedQuestion, value); } public RelayCommand SaveChangesCommand { get; } public RelayCommand AddBrandCommand { get; } public RelayCommand AddSurveyCommand { get; } public RelayCommand AddSectionCommand { get; } public OrganizationBrandsViewModel() { SaveChangesCommand = new RelayCommand(() => { }); AddBrandCommand = new RelayCommand(() => { }); AddSurveyCommand = new RelayCommand(() => { }); AddSectionCommand = new RelayCommand(() => { }); Brands.Add(new BrandModel { Name = "Coffee House", SurveyCount = 3, IsSelected = true }); Brands.Add(new BrandModel { Name = "GreenFuel", SurveyCount = 2 }); Brands.Add(new BrandModel { Name = "Urban Eats", SurveyCount = 4 }); SelectedBrand = Brands[0]; Surveys.Add(new SurveyModel { Name = "Butiksinspektion", Version = 3, QuestionCount = 24, IsSelected = true }); Surveys.Add(new SurveyModel { Name = "HACCP Tjekliste", Version = 2, QuestionCount = 18 }); Surveys.Add(new SurveyModel { Name = "Kampagneevaluering", Version = 1, QuestionCount = 12 }); SelectedSurvey = Surveys[0]; Section1Questions.Add(new SurveyQuestionModel { NumberLabel = "1.1", Title = "Dato og tidspunkt", Type = "Dato & tid" }); Section1Questions.Add(new SurveyQuestionModel { NumberLabel = "1.2", Title = "Butik", Type = "Sted (fra app)" }); Section1Questions.Add(new SurveyQuestionModel { NumberLabel = "1.3", Title = "Inspektør", Type = "Bruger (fra app)" }); Section2Questions.Add(new SurveyQuestionModel { NumberLabel = "2.1", Title = "Facade ren og vedligeholdt?", Type = "Ja / Nej", Description = "Angiv om facaden fremstår ren og uden skader.", IsRequired = true, VariableName = "facade_ren", DisplayMode = "Standard" }); Section2Questions.Add(new SurveyQuestionModel { NumberLabel = "2.2", Title = "Skiltning intakt og synlig?", Type = "Ja / Nej" }); Section2Questions.Add(new SurveyQuestionModel { NumberLabel = "2.3", Title = "Vinduer rene", Type = "Ja / Nej" }); Section2Questions.Add(new SurveyQuestionModel { NumberLabel = "2.4", Title = "Billede af facade", Type = "Billede" }); Section3Questions.Add(new SurveyQuestionModel { NumberLabel = "3.1", Title = "Butikken fremstår ryddelig", Type = "Skala 1-5" }); Section3Questions.Add(new SurveyQuestionModel { NumberLabel = "3.2", Title = "Produkter korrekt prissat", Type = "Ja / Nej" }); Section3Questions.Add(new SurveyQuestionModel { NumberLabel = "3.3", Title = "Kampagnemateriale på plads", Type = "Ja / Nej" }); Section3Questions.Add(new SurveyQuestionModel { NumberLabel = "3.4", Title = "Billede af kampagne", Type = "Billede" }); var q = Section2Questions[0]; SelectedQuestion = new SurveyQuestionEditorViewModel(q); } } 
SurveyQuestionEditorViewModel.cs
csharp
using System.Collections.ObjectModel; using Feedback2Business.Models; namespace Feedback2Business.ViewModels; public class SurveyQuestionEditorViewModel : ObservableObject { private string _selectedQuestionType = "Ja / Nej"; private string _title = string.Empty; private string _description = string.Empty; private bool _isRequired; private string _variableName = string.Empty; private bool _isStandardDisplay = true; private bool _isCompactDisplay; public ObservableCollection<string> QuestionTypes { get; } = new() { "Ja / Nej", "Skala 1-5", "Billede", "Tekst", "Single select" }; public string SelectedQuestionType { get => _selectedQuestionType; set => SetProperty(ref _selectedQuestionType, value); } public string Title { get => _title; set => SetProperty(ref _title, value); } public string Description { get => _description; set => SetProperty(ref _description, value); } public bool IsRequired { get => _isRequired; set => SetProperty(ref _isRequired, value); } public string VariableName { get => _variableName; set => SetProperty(ref _variableName, value); } public bool IsStandardDisplay { get => _isStandardDisplay; set { if (SetProperty(ref _isStandardDisplay, value) && value) IsCompactDisplay = false; } } public bool IsCompactDisplay { get => _isCompactDisplay; set { if (SetProperty(ref _isCompactDisplay, value) && value) IsStandardDisplay = false; } } public RelayCommand AddConditionCommand { get; } public RelayCommand AddRuleCommand { get; } public RelayCommand DeleteQuestionCommand { get; } public SurveyQuestionEditorViewModel(SurveyQuestionModel source) { _selectedQuestionType = source.Type; _title = source.Title; _description = source.Description; _isRequired = source.IsRequired; _variableName = source.VariableName; _isStandardDisplay = source.DisplayMode == "Standard"; _isCompactDisplay = source.DisplayMode == "Kompakt"; AddConditionCommand = new RelayCommand(() => { }); AddRuleCommand = new RelayCommand(() => { }); DeleteQuestionCommand = new RelayCommand(() => { }); } } 
OrganizationUsersViewModel.cs
csharp
using System.Collections.ObjectModel; using Feedback2Business.Models; namespace Feedback2Business.ViewModels; public class OrganizationUsersViewModel : ObservableObject { private string _searchText = string.Empty; public string SearchText { get => _searchText; set => SetProperty(ref _searchText, value); } public ObservableCollection<string> Roles { get; } = new() { "Alle", "Admin", "Manager", "Editor", "Viewer" }; public ObservableCollection<string> Statuses { get; } = new() { "Alle", "Aktiv", "Inviteret", "Deaktiveret" }; public ObservableCollection<UserModel> Users { get; } = new(); public RelayCommand InviteUserCommand { get; } public OrganizationUsersViewModel() { InviteUserCommand = new RelayCommand(() => { }); Users.Add(new UserModel { Name = "Anders Kirk", Email = "anders.kirk@email.com", Role = "Admin", Status = "Aktiv", LastActiveAt = DateTime.Now.AddDays(-1) }); Users.Add(new UserModel { Name = "Maria Jensen", Email = "maria.jensen@email.com", Role = "Manager", Status = "Aktiv", LastActiveAt = DateTime.Now.AddDays(-2) }); Users.Add(new UserModel { Name = "Lars Petersen", Email = "lars.petersen@email.com", Role = "Editor", Status = "Aktiv", LastActiveAt = DateTime.Now.AddDays(-5) }); } } 
TemplatesViewModel.cs
csharp
using System.Collections.ObjectModel; using Feedback2Business.Models; namespace Feedback2Business.ViewModels; public class TemplatesViewModel : ObservableObject { private string _searchText = string.Empty; public string SearchText { get => _searchText; set => SetProperty(ref _searchText, value); } public ObservableCollection<TemplateModel> Templates { get; } = new(); public TemplatesViewModel() { Templates.Add(new TemplateModel { Name = "Butiksinspektion", Type = "Inspektion", Description = "Standard skabelon til butiksinspektioner", UpdatedAt = DateTime.Today.AddDays(-3) }); Templates.Add(new TemplateModel { Name = "HACCP Tjekliste", Type = "Tjekliste", Description = "Fødevarekontrol og HACCP", UpdatedAt = DateTime.Today.AddDays(-10) }); Templates.Add(new TemplateModel { Name = "Kampagneevaluering", Type = "Survey", Description = "Evaluering af kampagner i butik", UpdatedAt = DateTime.Today.AddDays(-14) }); } } 
VariablesViewModel.cs
csharp
using System.Collections.ObjectModel; using Feedback2Business.Models; namespace Feedback2Business.ViewModels; public class VariablesViewModel : ObservableObject { public ObservableCollection<VariableModel> Variables { get; } = new(); public VariablesViewModel() { Variables.Add(new VariableModel { Name = "Organisationsnavn", Key = "org_name", Type = "Tekst", DefaultValue = "Retail Group A", UsageCount = 5 }); Variables.Add(new VariableModel { Name = "Inspektørens navn", Key = "inspector_name", Type = "Tekst", DefaultValue = "", UsageCount = 9 }); Variables.Add(new VariableModel { Name = "Dato", Key = "today", Type = "Dato", DefaultValue = "I dag", UsageCount = 9 }); Variables.Add(new VariableModel { Name = "Lokation", Key = "location_name", Type = "Tekst", DefaultValue = "", UsageCount = 7 }); } } 
MediaLibraryViewModel.cs
csharp
using System.Collections.ObjectModel; using Feedback2Business.Models; namespace Feedback2Business.ViewModels; public class MediaLibraryViewModel : ObservableObject { private string _searchText = string.Empty; public string SearchText { get => _searchText; set => SetProperty(ref _searchText, value); } public ObservableCollection<string> FileTypes { get; } = new() { "Alle", "Billeder", "PDF", "DOCX", "XLSX" }; public ObservableCollection<MediaItemModel> MediaItems { get; } = new(); public MediaLibraryViewModel() { MediaItems.Add(new MediaItemModel { Name = "facade_eksempel.jpg", ThumbnailUrl = "dotnet_bot.png", FileType = "Image", MetaText = "Billede · 1.2 MB" }); MediaItems.Add(new MediaItemModel { Name = "indgang_eksempel.jpg", ThumbnailUrl = "dotnet_bot.png", FileType = "Image", MetaText = "Billede · 980 KB" }); MediaItems.Add(new MediaItemModel { Name = "sikkerhed.pdf", ThumbnailUrl = "", FileType = "PDF", MetaText = "PDF · 2.3 MB" }); MediaItems.Add(new MediaItemModel { Name = "brand_logo.png", ThumbnailUrl = "dotnet_bot.png", FileType = "Image", MetaText = "Billede · 250 KB" }); } } 
SettingsGeneralViewModel.cs
csharp
using System.Collections.ObjectModel; namespace Feedback2Business.ViewModels; public class SettingsGeneralViewModel : ObservableObject { private string _organizationName = "Retail Group A"; private string _selectedLanguage = "Dansk"; private string _selectedTimezone = "UTC+01:00 København"; private string _dashboardName = "Dashboard"; private string _dateFormat = "DD-MM-YYYY"; public string OrganizationName { get => _organizationName; set => SetProperty(ref _organizationName, value); } public ObservableCollection<string> Languages { get; } = new() { "Dansk", "English", "German" }; public ObservableCollection<string> Timezones { get; } = new() { "UTC+01:00 København", "UTC+00:00 London" }; public string SelectedLanguage { get => _selectedLanguage; set => SetProperty(ref _selectedLanguage, value); } public string SelectedTimezone { get => _selectedTimezone; set => SetProperty(ref _selectedTimezone, value); } public string DashboardName { get => _dashboardName; set => SetProperty(ref _dashboardName, value); } public string DateFormat { get => _dateFormat; set => SetProperty(ref _dateFormat, value); } } 
SettingsAppViewModel.cs
csharp
using System.Collections.ObjectModel; namespace Feedback2Business.ViewModels; public class SettingsAppViewModel : ObservableObject { private bool _offlineEnabled = true; private bool _autoSyncEnabled = true; private bool _requireLocationAtStart = true; private bool _useLocationOnObservations = false; private bool _compressImages = true; private string _selectedMaxFileSize = "2 MB"; private bool _showSurveyProgress = true; private bool _allowSkipForward = false; public bool OfflineEnabled { get => _offlineEnabled; set => SetProperty(ref _offlineEnabled, value); } public bool AutoSyncEnabled { get => _autoSyncEnabled; set => SetProperty(ref _autoSyncEnabled, value); } public bool RequireLocationAtStart { get => _requireLocationAtStart; set => SetProperty(ref _requireLocationAtStart, value); } public bool UseLocationOnObservations { get => _useLocationOnObservations; set => SetProperty(ref _useLocationOnObservations, value); } public bool CompressImages { get => _compressImages; set => SetProperty(ref _compressImages, value); } public ObservableCollection<string> MaxFileSizeOptions { get; } = new() { "1 MB", "2 MB", "5 MB", "10 MB" }; public string SelectedMaxFileSize { get => _selectedMaxFileSize; set => SetProperty(ref _selectedMaxFileSize, value); } public bool ShowSurveyProgress { get => _showSurveyProgress; set => SetProperty(ref _showSurveyProgress, value); } public bool AllowSkipForward { get => _allowSkipForward; set => SetProperty(ref _allowSkipForward, value); } } 
RolesPermissionsViewModel.cs
csharp
using System.Collections.ObjectModel; using Feedback2Business.Models; namespace Feedback2Business.ViewModels; public class RolesPermissionsViewModel : ObservableObject { private RoleModel _selectedRole = new(); public ObservableCollection<RoleModel> Roles { get; } = new(); public RoleModel SelectedRole { get => _selectedRole; set { if (SetProperty(ref _selectedRole, value)) OnPropertyChanged(nameof(PermissionGroups)); } } public List<PermissionGroupModel> PermissionGroups => SelectedRole.PermissionGroups; public RolesPermissionsViewModel() { var admin = new RoleModel { Name = "Admin", Description = "Fuldt overblik og adgang til alle funktioner", PermissionGroups = new List<PermissionGroupModel> { new() { Name = "Organisation", Permissions = new List<PermissionModel> { new() { Label = "Administrer organisation", IsEnabled = true }, new() { Label = "Inviter brugere", IsEnabled = true }, new() { Label = "Administrer brugere", IsEnabled = true } } }, new() { Name = "Surveys", Permissions = new List<PermissionModel> { new() { Label = "Opret surveys", IsEnabled = true }, new() { Label = "Rediger surveys", IsEnabled = true }, new() { Label = "Se besvarelser", IsEnabled = true } } }, new() { Name = "Rapporter", Permissions = new List<PermissionModel> { new() { Label = "Se rapporter", IsEnabled = true }, new() { Label = "Eksportér data", IsEnabled = true } } } } }; var manager = new RoleModel { Name = "Manager", Description = "Kan administrere surveys og se rapporter" }; var editor = new RoleModel { Name = "Editor", Description = "Kan oprette og redigere surveys" }; var viewer = new RoleModel { Name = "Viewer", Description = "Kan udfylde surveys og se egne data" }; Roles.Add(admin); Roles.Add(manager); Roles.Add(editor); Roles.Add(viewer); SelectedRole = admin; } } 
ActivityLogViewModel.cs
csharp
using System.Collections.ObjectModel; using Feedback2Business.Models; namespace Feedback2Business.ViewModels; public class ActivityLogViewModel : ObservableObject { private string _searchText = string.Empty; public string SearchText { get => _searchText; set => SetProperty(ref _searchText, value); } public ObservableCollection<string> Actions { get; } = new() { "Alle", "Opdateret", "Oprettet", "Uploadet", "Eksporteret" }; public ObservableCollection<string> Users { get; } = new() { "Alle", "Anders Kirk", "Maria Jensen", "Lars Petersen" }; public ObservableCollection<string> Periods { get; } = new() { "7 dage", "30 dage", "90 dage" }; public ObservableCollection<ActivityEventModel> ActivityEvents { get; } = new(); public ActivityLogViewModel() { ActivityEvents.Add(new ActivityEventModel { Timestamp = DateTime.Now.AddHours(-2), Action = "Opdaterede survey", UserName = "Anders Kirk", Details = "Butiksinspektion v3" }); ActivityEvents.Add(new ActivityEventModel { Timestamp = DateTime.Now.AddHours(-5), Action = "Inviterede bruger", UserName = "Maria Jensen", Details = "maria.jensen@email.com" }); ActivityEvents.Add(new ActivityEventModel { Timestamp = DateTime.Now.AddDays(-1), Action = "Uploadede fil", UserName = "Anders Kirk", Details = "brand_logo.png" }); } } 
 
3.4 Shared View Code-Behind Scaffolding
StatusBadgeView.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <ContentView x:Class="Feedback2Business.Views.Shared.StatusBadgeView" xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Name="This"> <Border BackgroundColor="{Binding BadgeColor, Source={x:Reference This}}" StrokeThickness="0" Padding="8,4" StrokeShape="RoundRectangle 12"> <Label Text="{Binding Text, Source={x:Reference This}}" TextColor="{Binding TextColor, Source={x:Reference This}}" FontSize="11" /> </Border> </ContentView> 
StatusBadgeView.xaml.cs
csharp
namespace Feedback2Business.Views.Shared; public partial class StatusBadgeView : ContentView { public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(StatusBadgeView), string.Empty); public static readonly BindableProperty BadgeColorProperty = BindableProperty.Create(nameof(BadgeColor), typeof(Color), typeof(StatusBadgeView), Colors.LightGray); public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(StatusBadgeView), Colors.Black); public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); } public Color BadgeColor { get => (Color)GetValue(BadgeColorProperty); set => SetValue(BadgeColorProperty, value); } public Color TextColor { get => (Color)GetValue(TextColorProperty); set => SetValue(TextColorProperty, value); } public StatusBadgeView() { InitializeComponent(); } } 
SectionCardView.xaml.cs
csharp
using System.Collections; namespace Feedback2Business.Views.Shared; public partial class SectionCardView : ContentView { public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(SectionCardView), string.Empty); public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(SectionCardView), null); public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); } public IEnumerable ItemsSource { get => (IEnumerable)GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value); } public SectionCardView() { InitializeComponent(); } } 
SettingsNavView.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <ContentView x:Class="Feedback2Business.Views.Shared.SettingsNavView" xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Name="This"> <Border Style="{StaticResource CardStyle}"> <VerticalStackLayout Padding="12" Spacing="8"> <Label Text="Generelt" /> <Label Text="App-indstillinger" /> <Label Text="Sikkerhed" /> <Label Text="Notifikationer" /> <Label Text="API &amp; integrationer" /> <Label Text="Udseende" /> </VerticalStackLayout> </Border> </ContentView> 
SettingsNavView.xaml.cs
csharp
namespace Feedback2Business.Views.Shared; public partial class SettingsNavView : ContentView { public static readonly BindableProperty SelectedKeyProperty = BindableProperty.Create(nameof(SelectedKey), typeof(string), typeof(SettingsNavView), string.Empty); public string SelectedKey { get => (string)GetValue(SelectedKeyProperty); set => SetValue(SelectedKeyProperty, value); } public SettingsNavView() { InitializeComponent(); } } 
 
3.5 Sample Styles.xaml
xml
<?xml version="1.0" encoding="utf-8" ?> <ResourceDictionary xmlns="http://schemas.microsoft.com/dotnet/2021/maui" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"> <Color x:Key="SidebarBackground">#041B36</Color> <Color x:Key="PageBackground">#F5F7FB</Color> <Color x:Key="CardBackground">#FFFFFF</Color> <Color x:Key="PrimaryBlue">#1F6BFF</Color> <Color x:Key="BorderColor">#E2E8F0</Color> <Color x:Key="TextPrimary">#1B2430</Color> <Color x:Key="TextSecondary">#6B7280</Color> <Color x:Key="DangerRed">#D14343</Color> <Style x:Key="PageTitleStyle" TargetType="Label"> <Setter Property="FontSize" Value="24" /> <Setter Property="FontAttributes" Value="Bold" /> <Setter Property="TextColor" Value="{StaticResource TextPrimary}" /> </Style> <Style x:Key="PageSubtitleStyle" TargetType="Label"> <Setter Property="FontSize" Value="12" /> <Setter Property="TextColor" Value="{StaticResource TextSecondary}" /> </Style> <Style x:Key="SectionHeaderStyle" TargetType="Label"> <Setter Property="FontSize" Value="16" /> <Setter Property="FontAttributes" Value="Bold" /> <Setter Property="TextColor" Value="{StaticResource TextPrimary}" /> </Style> <Style x:Key="DataTableHeaderStyle" TargetType="Label"> <Setter Property="FontSize" Value="12" /> <Setter Property="FontAttributes" Value="Bold" /> <Setter Property="TextColor" Value="{StaticResource TextSecondary}" /> </Style> <Style x:Key="FormLabelStyle" TargetType="Label"> <Setter Property="FontSize" Value="12" /> <Setter Property="FontAttributes" Value="Bold" /> <Setter Property="TextColor" Value="{StaticResource TextPrimary}" /> </Style> <Style x:Key="TabTextStyle" TargetType="Label"> <Setter Property="FontSize" Value="13" /> <Setter Property="TextColor" Value="{StaticResource TextSecondary}" /> </Style> <Style x:Key="ActiveTabTextStyle" TargetType="Label"> <Setter Property="FontSize" Value="13" /> <Setter Property="TextColor" Value="{StaticResource PrimaryBlue}" /> <Setter Property="FontAttributes" Value="Bold" /> </Style> <Style x:Key="PrimaryButtonStyle" TargetType="Button"> <Setter Property="BackgroundColor" Value="{StaticResource PrimaryBlue}" /> <Setter Property="TextColor" Value="White" /> <Setter Property="CornerRadius" Value="8" /> <Setter Property="Padding" Value="16,10" /> </Style> <Style x:Key="SecondaryButtonStyle" TargetType="Button"> <Setter Property="BackgroundColor" Value="White" /> <Setter Property="TextColor" Value="{StaticResource TextPrimary}" /> <Setter Property="BorderColor" Value="{StaticResource BorderColor}" /> <Setter Property="BorderWidth" Value="1" /> <Setter Property="CornerRadius" Value="8" /> <Setter Property="Padding" Value="16,10" /> </Style> <Style x:Key="LinkButtonStyle" TargetType="Button"> <Setter Property="BackgroundColor" Value="Transparent" /> <Setter Property="TextColor" Value="{StaticResource PrimaryBlue}" /> <Setter Property="Padding" Value="0" /> </Style> <Style x:Key="CardStyle" TargetType="Border"> <Setter Property="BackgroundColor" Value="{StaticResource CardBackground}" /> <Setter Property="Stroke" Value="{StaticResource BorderColor}" /> <Setter Property="StrokeThickness" Value="1" /> <Setter Property="StrokeShape" Value="RoundRectangle 12" /> </Style> <Style x:Key="InnerCardStyle" TargetType="Border"> <Setter Property="BackgroundColor" Value="White" /> <Setter Property="Stroke" Value="{StaticResource BorderColor}" /> <Setter Property="StrokeThickness" Value="1" /> <Setter Property="StrokeShape" Value="RoundRectangle 10" /> </Style> </ResourceDictionary> 
 