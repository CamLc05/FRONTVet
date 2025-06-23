using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Veterinaria.Converters;

namespace Veterinaria.Controls
{
    public partial class PasswordEntryControl : ContentView
    {
        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(PasswordEntryControl), string.Empty);

        public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(PasswordEntryControl), Colors.Gray);

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(PasswordEntryControl), Colors.Black);

        public static readonly BindableProperty PasswordTextProperty =
            BindableProperty.Create(nameof(PasswordText), typeof(string), typeof(PasswordEntryControl), string.Empty, BindingMode.TwoWay);

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public string PasswordText
        {
            get => (string)GetValue(PasswordTextProperty);
            set => SetValue(PasswordTextProperty, value);
        }

        public bool IsPasswordVisible { get; set; } = false;

        public ImageSource EyeIconSource => IsPasswordVisible ? "abierto.png" : "cerrado.png"; // Asegúrate de tener estas imágenes

        public PasswordEntryControl()
        {
            InitializeComponent();
            passwordEntry.IsPassword = !IsPasswordVisible;
            UpdateEyeIcon(); // Inicializa el ícono
        }

        private void OnToggleVisibilityClicked(object sender, EventArgs e)
        {
            IsPasswordVisible = !IsPasswordVisible;
            passwordEntry.IsPassword = !IsPasswordVisible; // Actualiza la propiedad IsPassword del Entry interno
            UpdateEyeIcon();
        }

        private void UpdateEyeIcon()
        {
            toggleVisibilityImage.Source = EyeIconSource;
            toggleVisibilityImage.WidthRequest = 30;
            toggleVisibilityImage.HeightRequest = 30;
        }

    }
}

	