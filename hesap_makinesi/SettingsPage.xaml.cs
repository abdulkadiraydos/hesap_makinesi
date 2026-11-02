namespace hesap_makinesi;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();

        
        string savedTheme = Preferences.Get("AppTheme", "Sistem");
        if (savedTheme == "Açık") ThemePicker.SelectedIndex = 0;
        else if (savedTheme == "Koyu") ThemePicker.SelectedIndex = 1;
        else ThemePicker.SelectedIndex = 2;
    }

    private void OnThemePickerChanged(object sender, EventArgs e)
    {
        if (ThemePicker.SelectedIndex == 0)
        {
            Application.Current.UserAppTheme = AppTheme.Light;
            Preferences.Set("AppTheme", "Açık");
        }
        else if (ThemePicker.SelectedIndex == 1)
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
            Preferences.Set("AppTheme", "Koyu");
        }
        else
        {
            Application.Current.UserAppTheme = AppTheme.Unspecified;
            Preferences.Set("AppTheme", "Sistem");
        }
    }
}