namespace hesap_makinesi;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

       
        string savedTheme = Preferences.Get("AppTheme", "Sistem");
        if (savedTheme == "Açık") UserAppTheme = AppTheme.Light;
        else if (savedTheme == "Koyu") UserAppTheme = AppTheme.Dark;
        else UserAppTheme = AppTheme.Unspecified;

        MainPage = new AppShell();
    }
}
