using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Stubble.Core;
using Stubble.Core.Settings;

namespace IonicTest.Components;

public partial class FlowObjectPanel : ComponentBase 
{

    private static readonly Regex GridLengthRegex = new("^((?<percent>(?<value>\\d+(.?\\d+)?)\\%)|(?<star>(?<value>\\d+(.?\\d+)?)(\\*|star))|(?<abs>(?<value>\\d+(.?\\d+)?))|(?<auto>(?<value>(\\d+)?(.?\\d+)?)auto))$", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

    private ObjectFieldOptionsCollection _options;
    private object _dataSource;
    private bool _isVisible = true;
    private string? _cssClass;

    public RenderFragment Content { get; set; }

    //[Inject] IScriptEngineFactory ScriptEngineFactory { get; set; }

    [Parameter]
    public bool IsVisible {
        get => _isVisible;
        set {
            _isVisible = value;
            ReBuildLayout();
        }
    }

    [EditorRequired]
    [Parameter]
    public ObjectFieldOptionsCollection Options {
        get => _options;
        set {
            _options = value;
            ReBuildLayout();
        }
    }

    [Parameter]
    public object DataSource {
        get => _dataSource;
        set {
            _dataSource = value;
            ReBuildLayout();
        }
    }

    [Parameter]
    public string? CssClass {
        get => _cssClass;
        set {
            _cssClass = value;
            ReBuildLayout();
        }
    }

    public FlowObjectPanel() {
        ReBuildLayout();
    }

    /*protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        switch (propertyName)
        {
            case nameof(DataSource):
            case nameof(Options):
                ReBuildLayout();
                break;
        }
    }*/


    /*protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);
    }*/

    private void ReBuildLayout() {
        if (IsVisible == false) { return; }

        if (Options?.Any() != true) {
            MissingConfigurationContent();
        }
        else {
            RenderContent(Options);

            /*var result = new Grid()
            {
                //Background = Brush.Transparent,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                ColumnDefinitions = GetColumnDefinitions(Options),
                RowDefinitions = GetRowDefinitions(Options),
                RowSpacing = 2d,
                ColumnSpacing = 2d
                //HeightRequest = 100
            };

            BuildContent(result, Options);

            Content = result;*/
        }
    }

    protected override void OnInitialized() {
        base.OnInitialized();
    }

    private void MissingConfigurationContent() {

        Content = builder => {
            builder.OpenElement(0, "div");

            builder.AddAttribute(1, "class", $"missing-config {_cssClass}".TrimEnd());

            builder.OpenElement(2, "div");
            builder.AddAttribute(3, "class", "missing-config__image");
            builder.CloseElement();

            builder.OpenElement(4, "h5");
            builder.AddAttribute(5, "class", "missing-config__text");
            builder.AddContent(6, "Missing Configuration");
            builder.CloseElement();


            builder.CloseElement();
        };

        /*return new Frame
        {
            Background = Brush.Transparent,
            Content = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Base64Image()
                    {
                        Base64Source =
                            "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAABmJLR0QA/wD/AP+gvaeTAAAF20lEQVR4nO1ZbYsVZRi+7jlnXvbVlaXMfAtFLLU0lgqiEhFZtTehiLTIDxlhFGFEZBolGJmaFWbqHyiwT4LunnPW8EsEkqCCLUJFRLmp9WFx091zzs5z9WHfzpyZOWdmzpxjxVxfduZ5nrnnuq5z3/c8MwskSJAgQYIECRIkaCzsnLVT5cwrKmdesbPW+zeLh9yMm7LP3ELiCwcRwSuyOn+o0VwabgAzzTOp2f0AOsqmrklKWyyrhi81ko/WyJsBADX7c7jFA0Q7R9VnjebT0AxgzlxH4IR7YupQyPWypnCsUZwaZgCzaKGYFwDc4ZxwLf1NRvJL5EkMNYJXw0pAwdyF6uIBYI6yzJ0NoASgQRnArN5F0U4DSE0N+q4GACXgg9JdPF1vbnXPAB5FipI6guDiAUAj5AhPIV1fdo0ogQ7zdYBdk+fVxU9gGQrma3ViNYm6lgD7muaS6gcArf7CgQqu3BBoS6V75JfYyY2jrhlAqgOILh4Amgl1MFZSZaibAcwaTwN4ogbxE1jLjPFULKQ8UJcSYA/amTb7Qczymv/pSisGBi2QYwaQxNzOYcy/9YZfyMtiFO6SlRiMm2tduqzSzQ9FeYsngK1fLUe+KCAJklBKoc0q4tjW7/1C3qby+i6g+GrcXGMvAWb0+0XhZZ9ZCIhRe0r8RBbYqnIyisgWZtMPxs03VgN4CmmK5nzmT81OHhlp2zVrplW18BqhHeYZ6DWRLA8aZzAUjDcBLHdPOJudkbInf3lgrAeYutsUD9yNv4w3auJYhtgM4AlrHiE7PGZcIxO/dmkJWMEMAAXvscdcEJ2pE/EZoPMggJayUc+1RnrU0QNIwvQoCx80UWNse4NYDGDG2Aji0bJR3/UTJVBaBgF6QCm62WtsCMfSGzUbwD5Mo8i+khFU2+CYuu3YA4TKgPHQFH7C45gegbIDNRugbHMfgJljZ4F2djBSyl0CQXqAI7zMUGnzo7B8y1GTAcxYD4vgxfGzwNeZadtlgKVXKQGP8AJuZsZaGYKyC5EN4FEY1HgYgIQRD0yVQGkfqFgC/uGFUIfYAzMUgRJEz4B2420Qi8OKB5xNcKoH+GRA9fCLoBlvhSYxjkgGMGcupMi2KOKBsY7vKgHDw4CA4QlsZ9a4MwqX0AaQECocAmhFuSEwthUuFa+Ucm6Pqz9IymEScpgM/3YbPgP69E0Qrgp9XQks3eMpMGFAtKQCyBXI6S+EvSyUATyJTlLbE/Ym5dDHM0Ap5ewBUcVP8KN8zB7cEuaaUAYo29gPMNQNvGCVPAUmTGjy6gHh0anE2BvmgsA1w1x6BamdCnONH/4Y1LH/+CwMFzSQRKtlY9v6AXQ0j9YaGgAg0FbLmpGTwdYGAHtgMmWcAxCp01YOHntEAPhRzMI9shIj1RYGKgGVMnbgvyMeABeqvPFOkJVVM4C95iJqPA9E322VQymg52wHzvzcDEWga/4NPN41CC2WT7STrhZEyb2yrtBfaXXFW5IQ5vRvAKlpv+0MCmz7chYy59sdb4Tdy4aw5/mB2oM78a10Fx8R8c+1yiWQ01+KW/zFAQu959ocj0CS6D3bgouXakkyT40PIatvrnSVrwE82TKDkN01MCoLOPbnz2sph/jS46vXPL6lBgrs30xI7GWu+Xa/eV8DlF38FKj9g8MYi6nDpXPyaDGdGyGlFMy0jSWz89ED+09PU3Zxn98STwPYZ3YL8GxINpVITGJ6yyh2P3cZ05qKk+JbrVF8sPEqOtsCfxd0B64wLcAGZlOPeS1zNUF+hyYOGRcAzA/BpiqJclzPa+j/3YRSxOLZBbQ1hdkJBhdfgl9FKy6RblwvHXRlgBoy3kWt4gO8zbWYCvctGMYDC0caIR4A5illbC8fdBkgQOg3qoAEYkBk8QAAgdpUPubVA6J/JfoXi/db5ZUBBwIyikIgYuAKwUN8PBG6tXnuBJkxnlHAWpHy//Q0GlWUBWwdJP7WRHplbeHr2jklSJAgQYIECRL8T/APIry4uMsYxTkAAAAASUVORK5CYII="
                    },
                    new Label()
                    {
                        //TextColor = Color.Default, //TODO @wdcossey
                        Text = "Missing Configuration",
                        VerticalOptions = LayoutOptions.Center,
                    }
                },
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            },
            HorizontalOptions = LayoutOptions.CenterAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            //HeightRequest = 100
        };*/
    }

    private void RenderContent(IList<ObjectFieldOptions> options) {

        Content = builder => {
            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, new Dictionary<string, object>()
            {
                { "class", $"grid-container {_cssClass}".TrimEnd() },
                { "style", $"grid-template-columns: {GetColumnDefinitions(options)}; grid-template-rows: {GetRowDefinitions(options)}" }
            });
            builder.AddContent(3, RenderGridContent(options));
            builder.AddElementReferenceCapture(4, _ => { });
            builder.CloseElement();
        };
    }

    private RenderFragment RenderGridContent(IList<ObjectFieldOptions> options) {
        var fragment = new RenderFragment(builder => {
            /*builder.OpenElement(0, "div");

            builder.AddAttribute(1, "class", "container");
            builder.AddAttribute(2, "style",
                $"grid-template-columns: {GetColumnDefinitions(options)}; grid-template-rows: {GetRowDefinitions(options)}");*/

            foreach (var option in options) {
                var lazyBackgroundColor = new Lazy<string?>(() => ExecuteScript(option.BackgroundColorScript, this.DataSource, () => option?.BackgroundColor));
                var lazyForegroundColor = new Lazy<string?>(() => ExecuteScript(option.ForegroundColorScript ?? option.TextColorScript, this.DataSource, () => option.ForegroundColor ?? option.TextColor));
                //TODO: @wdcossey - this is from the Tasnee merge
                //`lazyImageName` seems odd, use css class?
                var lazyImageName = new Lazy<string?>(() => ExecuteScript(option.ImageNameScript, this.DataSource, () => option.ImageName));
                var lazyClass = new Lazy<string?>(() => ExecuteScript(option.CssClassScript, this.DataSource, () => option.CssClass));

                if (option.Items?.Any() == true) {
                    var styles = option.CssStyle?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) ?? new Dictionary<string, string>();

                    styles.Add("background-color", lazyBackgroundColor.Value);
                    styles.Add("grid-template-columns", GetColumnDefinitions(option.Items, () => option.Width));
                    styles.Add("grid-template-rows", GetRowDefinitions(option.Items, () => option.Height));
                    styles.Add("height", CreateGridLength(option.Height, null));
                    styles.Add("width", CreateGridLength(option.Width, null));


                    var classDict = new List<string?>()
                    {
                        "grid-container",
                        lazyClass.Value
                    };

                    builder.OpenElement(0, "div");
                    builder.AddAttribute(1, "class", string.Join(" ", classDict.Where(w => string.IsNullOrWhiteSpace(w) is not true)));
                    builder.AddAttribute(2, "style", string.Join(";", styles.Where(w => w.Value is not null).Select(s => $"{s.Key}:{s.Value}")));

                    /*child = new Grid()
                    {
                        BackgroundColor = lazyBackgroundColor.Value,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        ColumnDefinitions = GetColumnDefinitions(option.Items, () => option.Width),
                        RowDefinitions = GetRowDefinitions(option.Items, () => option.Height),
                        //Padding = new Thickness(0, 0),
                        //Margin = new Thickness(0, 0),
                    };

                    /*var subChild = RenderLabel(option, lazyBackgroundColor, lazyTextColor);
                    ((Grid)child).Children.Add(subChild);
                    Grid.SetColumn(subChild, option.Column);
                    Grid.SetRow(subChild, option.Row);
                    Grid.SetRowSpan(subChild, option.RowSpan ?? 1);
                    Grid.SetColumnSpan(subChild, option.ColumnSpan ?? 1);*/

                    builder.AddContent(4, RenderGridContent(option.Items));

                    builder.AddElementReferenceCapture(5, _ => { });
                    builder.CloseElement();
                }
                else {
                    builder.AddContent(1, RenderItem(option, lazyClass, lazyBackgroundColor, lazyForegroundColor, lazyImageName));
                    //child = RenderLabel(option, lazyBackgroundColor, lazyTextColor);
                }
            }


            //builder.OpenElement(3, "div");
            //builder.AddAttribute(4, "class", "missing-config__image");
            //builder.CloseElement();


            //builder.AddElementReferenceCapture(5, _ => { });
            //builder.CloseElement();
        });


        return fragment;
    }

    /*private void BuildContent(Grid parent, IList<ObjectFieldOptions> options)
    {
        foreach (var option in options)
        {
            View child;

            var lazyBackgroundColor = new Lazy<Color>(() => ExecuteColorScript(option.BackgroundColorScript, this.DataSource,
                        () => string.IsNullOrWhiteSpace(option.BackgroundColor)
                            ? Colors.Transparent
                            : Color.FromArgb(option.BackgroundColor)));

            var lazyTextColor = new Lazy<Color>(() => ExecuteColorScript(option.TextColorScript, this.DataSource,
                () => string.IsNullOrWhiteSpace(option.TextColor)
                    ? (Label.TextColorProperty.DefaultValue as Color)!
                    : Color.FromArgb(option.TextColor)));

            if (option.Items?.Any() == true)
            {
                child = new Grid()
                {
                    BackgroundColor = lazyBackgroundColor.Value,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    ColumnDefinitions = GetColumnDefinitions(option.Items, () => option.Width),
                    RowDefinitions = GetRowDefinitions(option.Items, () => option.Height),
                    //Padding = new Thickness(0, 0),
                    //Margin = new Thickness(0, 0),
                };

                /*var subChild = RenderLabel(option, lazyBackgroundColor, lazyTextColor);
                ((Grid)child).Children.Add(subChild);
                Grid.SetColumn(subChild, option.Column);
                Grid.SetRow(subChild, option.Row);
                Grid.SetRowSpan(subChild, option.RowSpan ?? 1);
                Grid.SetColumnSpan(subChild, option.ColumnSpan ?? 1);#1#


                BuildContent((Grid)child, option.Items);
            }
            else
            {

                child = RenderLabel(option, lazyBackgroundColor, lazyForegroundColor);

            }

            parent.Children.Add(child);
            Grid.SetColumn(child, option.Column);
            Grid.SetRow(child, option.Row);
            Grid.SetRowSpan(child, option.RowSpan ?? 1);
            Grid.SetColumnSpan(child, option.ColumnSpan ?? 1);
        }
    }*/

    private RenderFragment RenderItem(
        ObjectFieldOptions option,
        Lazy<string?> lazyClass,
        Lazy<string?> lazyBackgroundColor,
        Lazy<string?> lazyForegroundColor,
        Lazy<string?> lazyImageName) {
        return new RenderFragment(builder => {
            switch (option.FieldStyle) {
                case FieldStyle.Text:
                case null:
                    builder.AddContent(1, RenderText(option, lazyClass, lazyBackgroundColor, lazyForegroundColor));
                    break;
                case FieldStyle.Chip:
                    builder.AddContent(1, RenderChip(option, lazyClass, lazyBackgroundColor, lazyForegroundColor));
                    break;
                case FieldStyle.Image:
                    builder.AddContent(1, RenderImage(option, lazyClass, lazyBackgroundColor, lazyForegroundColor));
                    break;
                //TODO: @wdcossey - this is from the Tasnee merge
                //Don't like the look of `FieldStyle.SVG`, seems hacky.
                case FieldStyle.SVG:
                    builder.AddContent(1, RenderSVG(option, lazyClass, lazyBackgroundColor, lazyForegroundColor, lazyImageName));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });
    }

    private RenderFragment RenderText(
        ObjectFieldOptions option,
        Lazy<string?> lazyClass,
        Lazy<string?> lazyBackgroundColor,
        Lazy<string?> lazyForegroundColor) {
        return new RenderFragment(builder => {
            //We want a copy of the original dictionary w/o modifying it
            var styles = option.CssStyle?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) ?? new Dictionary<string, string>();

            styles.TryAdd("color", $"{lazyForegroundColor.Value}");
            styles.TryAdd("background-color", $"{lazyBackgroundColor.Value}");
            styles.TryAdd("grid-column", $"{option.Column + 1} / {option.Column + 1 + (option.ColumnSpan ?? 1)}");
            styles.TryAdd("grid-row", $"{option.Row + 1} / {option.Row + 1 + (option.RowSpan ?? 1)}");

            builder.OpenElement(1, "div");


            switch (option.FontStyles) {
                case FontStyles.Bold:
                    styles.TryAdd("font-weight", "bold");
                    break;
                case FontStyles.Italic:
                    styles.TryAdd("font-style", "italic");
                    break;
            }

            builder.AddMultipleAttributes(2, new Dictionary<string, object>()
            {
                { "data-col", option.Column + 1 },
                { "data-row", option.Row + 1 },
                { "data-col-span", option.ColumnSpan ?? 1 },
                { "data-row-span", option.RowSpan ?? 1 },
            });

            if (option.FieldStyle is null || option.FieldStyle?.Equals(FieldStyle.Text) == true) {
                builder.AddAttribute(3, "class", $"flow-obj-text {lazyClass.Value}".Trim());

                switch (option.HorizontalAlignment) {
                    case AlignmentOptions.Start:
                        styles.TryAdd("text-align", "start");
                        break;
                    case AlignmentOptions.Center:
                        styles.TryAdd("text-align", "center");
                        break;
                    case AlignmentOptions.End:
                        styles.TryAdd("text-align", "end");
                        break;
                }

                //if (!string.IsNullOrWhiteSpace(option.FontSize))
            }

            if (option.FieldStyle?.Equals(FieldStyle.Image) == true) {
                builder.AddAttribute(4, "class", $"flow-obj-image {lazyClass.Value}".Trim());
            }

            /*if (option.FieldStyle?.Equals(FieldStyle.Chip) == true)
            {
                builder.AddAttribute(2, "class", "flow-obj-chip");
            }*/

            if (option.FieldStyle is null && string.IsNullOrWhiteSpace(option.Template)) {
                builder.AddAttribute(5, "class", $"flow-obj-line {lazyClass.Value}".Trim());
                //builder.AddAttribute(6, "style", "width: 100%; height: 100%");
            }

            var margin = CssHelper.CreateThickness(option.Margin);
            if (!string.IsNullOrWhiteSpace(margin))
                styles.TryAdd("margin", $"{margin}");

            var padding = CssHelper.CreateThickness(option.Padding);
            if (!string.IsNullOrWhiteSpace(padding))
                styles.TryAdd("padding", $"{padding}");

            styles.TryAdd("display", "grid");
            styles.TryAdd("height", "100%");
            styles.TryAdd("align-content", "center");

            builder.AddAttribute(7, "style", string.Join(";", styles.Where(w => string.IsNullOrWhiteSpace(w.Value) is not true).Select(s => $"{s.Key}:{s.Value}")));

            builder.AddAttribute(8, "data-option-template", $"{option.Template}");

            builder.AddContent(9, StringHelpers.RenderMustache(this.DataSource, option.Template));


            /*
            Grid.SetColumn(child, option.Column);
            Grid.SetRow(child, option.Row);
            Grid.SetRowSpan(child, option.RowSpan ?? 1);
            Grid.SetColumnSpan(child, option.ColumnSpan ?? 1);
             */

            builder.CloseElement();
        });
    }

    private RenderFragment RenderChip(ObjectFieldOptions option, Lazy<string?> lazyClass, Lazy<string?> lazyBackgroundColor, Lazy<string?> lazyTextColor) {
        return new RenderFragment(builder => {
            var styles = new Dictionary<string, string?>()
            {
                //$"color: {lazyTextColor.Value}",
                //$"background-color:{lazyBackgroundColor.Value}",
                { "grid-column", $"{option.Column + 1} / {option.Column + 1 + (option.ColumnSpan ?? 1)}" },
                { "grid-row", $"{option.Row + 1} / {option.Row + 1 + (option.RowSpan ?? 1)}" },
                { "font-size", $"{option.FontSize}px" }
            };

            var chipStyle = new Dictionary<string, string?>()
            {
                { "color", $"{lazyTextColor.Value}" },
                { "background-color", $"{lazyBackgroundColor.Value}" },
                { "padding-left", $"{(string.IsNullOrWhiteSpace(option.ImageName) ? "15px" : null)}" },
            };

            builder.OpenElement(1, "div");
            builder.AddAttribute(2, "style", $"{string.Join(";", styles.Where(w => string.IsNullOrWhiteSpace(w.Value) is not true).Select(s => $"{s.Key}:{s.Value}"))}");

            builder.OpenElement(3, "a");
            builder.AddAttribute(4, "class", $"flow-obj-chip chip chip-s {lazyClass.Value}".Trim());
            builder.AddAttribute(5, "style", $"{string.Join(";", chipStyle.Where(w => string.IsNullOrWhiteSpace(w.Value) is not true).Select(s => $"{s.Key}:{s.Value}"))}");

            if (!string.IsNullOrWhiteSpace(option.ImageName)) {
                builder.AddContent(6, RenderFontawesome(option, lazyBackgroundColor, lazyTextColor));
            }

            builder.OpenElement(7, "span");
            builder.AddContent(8, StringHelpers.RenderMustache(this.DataSource, option.Template));
            builder.CloseElement(); // span

            builder.CloseElement(); // a

            builder.CloseElement(); // div

        });
    }

    private RenderFragment RenderFontawesome(ObjectFieldOptions option, Lazy<string?> lazyBackgroundColor, Lazy<string?> lazyTextColor) {
        return new RenderFragment(builder => {
            builder.OpenElement(1, "i");
            builder.AddAttribute(2, "class", $"fa {option.ImageName}");
            builder.AddAttribute(3, "style", $"color:{lazyTextColor.Value};background-color:{lazyBackgroundColor.Value}");
            builder.CloseElement(); // i
        });

    }


    private RenderFragment RenderImage(ObjectFieldOptions option, Lazy<string?> lazyClass, Lazy<string?> lazyBackgroundColor, Lazy<string?> lazyTextColor) {
        return new RenderFragment(builder => {
            //We want a copy of the original dictionary w/o modifying it
            var styles = option.CssStyle?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) ?? new Dictionary<string, string>();

            //$"color: {lazyTextColor.Value}",
            //$"background-color:{lazyBackgroundColor.Value}",
            styles.TryAdd("grid-column", $"{option.Column + 1} / {option.Column + 1 + (option.ColumnSpan ?? 1)}");
            styles.TryAdd("grid-row", $"{option.Row + 1} / {option.Row + 1 + (option.RowSpan ?? 1)}");
            styles.TryAdd("font-size", $"{option.FontSize}px");


            builder.OpenElement(1, "div");
            builder.AddAttribute(2, "class", $"flow-obj-image {lazyClass.Value}".Trim());

            var margin = CssHelper.CreateThickness(option.Margin);
            if (!string.IsNullOrWhiteSpace(margin))
                styles.TryAdd("margin", $"{margin}");

            var padding = CssHelper.CreateThickness(option.Padding);
            if (!string.IsNullOrWhiteSpace(padding))
                styles.TryAdd("padding", $"{padding}");

            builder.AddAttribute(3, "style", $"{string.Join(";", styles.Where(w => string.IsNullOrWhiteSpace(w.Value) is not true).Select(s => $"{s.Key}:{s.Value}"))}");

            builder.OpenElement(4, "i");
            builder.AddAttribute(5, "class", $"fa {option.ImageName}");
            builder.AddAttribute(6, "style", $"color:{lazyTextColor.Value};background-color:{lazyBackgroundColor.Value}");
            builder.CloseElement(); // i

            builder.CloseElement(); // div
        });
    }

    //TODO: @wdcossey - this is from the Tasnee merge
    //Seems like a hacky implementation.
    private RenderFragment RenderSVG(ObjectFieldOptions option, Lazy<string?> lazyClass, Lazy<string?> lazyBackgroundColor, Lazy<string?> lazyTextColor, Lazy<string?> lazyImageName) {
        return new RenderFragment(builder => {
            //We want a copy of the original dictionary w/o modifying it
            var styles = option.CssStyle?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) ?? new Dictionary<string, string>();

            styles.TryAdd("grid-column", $"{option.Column + 1} / {option.Column + 1 + (option.ColumnSpan ?? 1)}");
            styles.TryAdd("grid-row", $"{option.Row + 1} / {option.Row + 1 + (option.RowSpan ?? 1)}");

            builder.OpenElement(1, "div");

            var margin = CssHelper.CreateThickness(option.Margin);
            if (!string.IsNullOrWhiteSpace(margin))
                styles.TryAdd("margin", $"{margin}");

            var padding = CssHelper.CreateThickness(option.Padding);
            if (!string.IsNullOrWhiteSpace(padding))
                styles.TryAdd("padding", $"{padding}");

            if (string.IsNullOrWhiteSpace(lazyImageName.Value) == false) {
                styles.TryAdd("background-image", $"url(images/{lazyImageName.Value}.svg)");
                styles.TryAdd("background-repeat", "no-repeat");
                styles.TryAdd("background-position-x", "right");
                styles.TryAdd("background-size", "contain");
                styles.TryAdd("height", $"{option.Height ?? "20"}px");
            }

            builder.AddAttribute(3, "style", $"{string.Join(";", styles.Where(w => string.IsNullOrWhiteSpace(w.Value) is not true).Select(s => $"{s.Key}:{s.Value}"))}");

            builder.CloseElement(); // div
        });
    }

    /*private View RenderLabel(ObjectFieldOptions option, Lazy<Color> lazyBackgroundColor, Lazy<Color> lazyTextColor)
    {

        var result = new Frame()
        {
            HorizontalOptions = GetLayoutOptions(option.HorizontalAlignment),
            VerticalOptions = GetLayoutOptions(option.VerticalAlignment),
            BackgroundColor = lazyBackgroundColor.Value,
            CornerRadius = option.FieldStyle == FieldStyle.Chip ? 10f : 0,
            //IsClippedToBounds = true,
            //HasShadow = false,
            Padding = option.FieldStyle == FieldStyle.Chip ? new Thickness(8, 4) : (option.FieldStyle == FieldStyle.Image ? new Thickness(4, 4) : Thickness.Zero),
            BorderColor = Colors.Transparent,

            //MinimumWidthRequest = option.Style == StyleAttributes.Empty ? CreateGridLength(option.Width).Value : 0,
        };

        if (option.FieldStyle?.HasFlag(FieldStyle.Image) is not true)
        {
            var label = new Label()
            {
                FontSize = option.FontSize,
                BindingContext = this.DataSource,
                BackgroundColor = lazyBackgroundColor.Value,
                FontAttributes = option.FontStyles.HasValue ? (FontAttributes)option.FontStyles : FontAttributes.None,
                TextColor = lazyTextColor.Value,
                HorizontalTextAlignment = GetTextAlignment(option.HorizontalAlignment),
                VerticalTextAlignment = GetTextAlignment(option.VerticalAlignment),
                HorizontalOptions = LayoutOptions.Fill,
                Padding = CssHelper.CreateThickness(option.Padding),
                Margin = CssHelper.CreateThickness(option.Margin),

                MinimumWidthRequest = CreateGridLength(option.Width).Value,
                //MinimumHeightRequest = CreateGridLength(option.Height).Value,
            };

            if (string.IsNullOrWhiteSpace(option.FontFamily) == false)
            {
                label.FontFamily = option.FontFamily;
            }

            label.Behaviors.Add(new MustacheLabelBehavior() { Template = option.Template });

            result.Content = label;
        }

        if (option.FieldStyle?.HasFlag(FieldStyle.Image) is true)
        {
            var image = new SvgImage()
            {
                //BackgroundColor = lazyBackgroundColor.Value,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = CssHelper.CreateThickness(option.Padding),
                Margin = CssHelper.CreateThickness(option.Margin),
                ImageName = option.Template,
                ImageColor = lazyTextColor.Value,
                MinimumWidthRequest = CreateGridLength(option.Width).Value,
                //MinimumHeightRequest = CreateGridLength(option.Height).Value,
            };

            result .Content = image;
        }

        return result;
    }*/

    /*private static LayoutOptions GetLayoutOptions(AlignmentOptions? options)
    {
        return options switch
        {
            AlignmentOptions.Start => LayoutOptions.Start,
            AlignmentOptions.Center => LayoutOptions.Center,
            AlignmentOptions.End => LayoutOptions.End,
            AlignmentOptions.Fill => LayoutOptions.Fill,
            _ => LayoutOptions.Fill
        };
    }*/

    /*private static TextAlignment GetTextAlignment(AlignmentOptions? options)
    {
        return options switch
        {
            AlignmentOptions.Start => TextAlignment.Start,
            AlignmentOptions.Center => TextAlignment.Center,
            AlignmentOptions.End => TextAlignment.End,
            _ => TextAlignment.Start
        };
    }*/

    private static string? GetRowDefinitions(IList<ObjectFieldOptions>? options, Func<string?>? fallbackHeightFunc = null) {
        var rowCount = (options?.Max(opt => opt.Row) ?? 0) + 1;
        var rowDefinitions = new StringBuilder();

        //var result = string.Join(" ", options?.Select(s => CreateGridLength(s?.Height)).ToArray() ?? Array.Empty<string>());

        for (var i = 0; i < rowCount; i++) {
            var height = CreateGridLength(options?.FirstOrDefault(fd => fd.Row == i)?.Height);
            rowDefinitions.Append($"{height} ");
        }

        return rowDefinitions.ToString();
    }

    private static string? GetColumnDefinitions(IList<ObjectFieldOptions>? options, Func<string?>? fallbackWithFunc = null) {
        var columnCount = (options?.Max(opt => opt.Column) ?? 0) + 1;
        var columnDefinitions = new StringBuilder();

        for (var i = 0; i < columnCount; i++) {
            var width = CreateGridLength(options?.FirstOrDefault(fd => fd.Column == i)?.Width);
            columnDefinitions.Append($"{width} ");
        }

        return columnDefinitions.ToString();
    }

    private static string? CreateGridLength(string? input, string? @default = "auto") {
        var match = GridLengthRegex.Match(input ?? string.Empty);

        if (!match.Success)
            return @default;

        if (!double.TryParse(match.Groups["value"].Value, out var value))
            value = 1;

        return match.Groups["star"].Success
            ? $"{value}fr"
            : match.Groups["abs"].Success
                ? $"{value}px"
                : match.Groups["percent"].Success
                    ? $"{value}%"
                    : "auto";
        //percent
    }

    private string? ExecuteScript(string? script, object? dataSource, Func<string?> @default) {
        return @default.Invoke();
        //if (dataSource is null || string.IsNullOrWhiteSpace(script))
        //    return @default.Invoke();

        //try {
        //    var completionValue = ScriptEngineFactory
        //        .Create()
        //        .WithValue("obj", dataSource)
        //        .Execute(script)
        //        .ResultAsString();
        //
        //    return $"{@default.Invoke()} {completionValue}".Trim();
        //}
        //catch {
        //    return @default.Invoke();
        //}
    }

    
}

public record ObjectFieldOptions : INotifyPropertyChanged {
    /// <summary>
    /// Mustache Template
    /// </summary>
    public string? Template { get; set; }

    public string? ImageName { get; set; }

    public string? ImageNameScript { get; set; }

    public FieldStyle? FieldStyle { get; set; }

    public FontStyles? FontStyles { get; set; }

    public string? Width { get; set; }

    public string? Height { get; set; }

    public string? Padding { get; set; }

    /// <summary>
    /// Top,Right,Bottom,Left
    /// </summary>
    public string? Margin { get; set; }

    public double FontSize { get; set; } = 14;
    public string? FontFamily { get; set; }

    public int Column { get; set; }
    public int? ColumnSpan { get; set; }

    public int Row { get; set; }

    public int? RowSpan { get; set; }

    [Obsolete("Use `ForegroundColor`", false)]
    public string? TextColor { get => ForegroundColor; set => ForegroundColor = value; }

    [Obsolete("Use `ForegroundColorScript`", false)]
    public string? TextColorScript { get => ForegroundColorScript; set => ForegroundColorScript = value; }

    /// <summary>
    /// RRGGBBAA
    /// </summary>
    public string? ForegroundColor { get; set; }

    /// <summary>
    /// RRGGBBAA
    /// </summary>
    public string? ForegroundColorScript { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Obsolete("Renamed to `CssClass`", true)]
    public string? Class { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Obsolete("Renamed to `CssClassScript`", true)]
    public string? ClassScript { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? CssClassScript { get; set; }

    /// <summary>
    /// RRGGBBAA
    /// </summary>
    public string? BackgroundColor { get; set; }

    /// <summary>
    /// RRGGBBAA
    /// </summary>
    public string? BackgroundColorScript { get; set; }

    public string? BorderColor { get; set; }
    public string? BorderColorScript { get; set; }

    public AlignmentOptions? VerticalAlignment { get; set; }

    public AlignmentOptions? HorizontalAlignment { get; set; }

    public IList<ObjectFieldOptions> Items { get; set; } = new List<ObjectFieldOptions>();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Optional CSS style(s) to pass to the renderer.
    /// </summary>
    public Dictionary<string, string>? CssStyle { get; set; }
}

public class ObjectFieldOptionsCollection : ObservableCollection<ObjectFieldOptions> {
    //TODO DG Rework to use Resources.Styles/Colors
    public static ObjectFieldOptions BasicLabelBackgroundOptions => new() {
        BackgroundColor = "#615f85"
    };

    public static ObjectFieldOptions BasicLabelOptions => new() {
        Width = "auto",
        Height = "auto",
        FontStyles = FontStyles.Bold,
        HorizontalAlignment = AlignmentOptions.Fill,
        VerticalAlignment = AlignmentOptions.Fill,
        Margin = "0,0,4,0",
        Padding = "0"
    };

    //TODO DG Rework to use Resources.Styles/Colors
    public static ObjectFieldOptions BasicFieldOptions => new() {
        Width = "1*",
        Height = "1auto",
        HorizontalAlignment = AlignmentOptions.Start,
        VerticalAlignment = AlignmentOptions.Center,
        Margin = "4,0,0,0",
        FontSize = 19
    };

    /// <summary>
    /// Creates a <see cref="ObjectFieldOptions"/> entry for each entry <paramref name="fieldNames"/> based on the passed Label and Field template. 
    /// If no templates are passed, <see cref="BasicFieldOptions"/> will be used
    /// </summary>
    /// <param name="fieldNames">Names of public fields and properties to be displayed</param>
    /// <param name="labelOptions">Provide a format template to be used to wrap every displayable property/field name. If omitted the template in <see cref="BasicLabelOptions"/> will be used</param>
    /// <param name="fieldOptions">Provide a format template to be used to wrap every displayable property/field value. If omitted the template in <see cref="BasicFieldOptions"/> will be used</param>
    /// <returns></returns>
    public static ObjectFieldOptionsCollection CreateFromList(IEnumerable<string> fieldNames,
                                                              ObjectFieldOptions? labelOptions = null,
                                                              ObjectFieldOptions? fieldOptions = null) {
        var options = new ObjectFieldOptionsCollection();

        var useBasicTemplate = labelOptions == null;

        labelOptions ??= BasicLabelOptions;
        fieldOptions ??= BasicFieldOptions;

        var row = 0;

        foreach (var name in fieldNames) {
            options.Add(labelOptions with { Template = name, Column = 0, Row = row });
            options.Add(fieldOptions with { Template = $"{{{{{name}}}}}", Column = 1, Row = row++ });
        }

        //DG Temp hack to add a background behind labels
        if (useBasicTemplate) {
            options.Insert(0, new ObjectFieldOptions() {
                BackgroundColor = "#615f85",
                Row = 0,
                RowSpan = options.Count,
                Column = 0,
            });
        }

        return options;
    }

    /// <summary>
    /// Creates a <see cref="ObjectFieldOptions"/> entry for each entry <paramref name="fieldNames"/> based on the passed Label and Field template. 
    /// If no templates are passed, <see cref="BasicFieldOptions"/> will be used
    /// </summary>
    /// <param name="fieldNames">Names of public fields and properties to be displayed</param>
    /// <param name="labelOptions">Provide a format template to be used to wrap every displayable property/field name. If omitted the template in <see cref="BasicLabelOptions"/> will be used</param>
    /// <param name="fieldOptions">Provide a format template to be used to wrap every displayable property/field value. If omitted the template in <see cref="BasicFieldOptions"/> will be used</param>
    /// <returns></returns>
    public static ObjectFieldOptionsCollection CreateFromList(List<KeyValuePair<string, string?>> fieldNames,
                                                              ObjectFieldOptions? labelOptions = null,
                                                              ObjectFieldOptions? fieldOptions = null) {
        var options = new ObjectFieldOptionsCollection();

        var useBasicTemplate = labelOptions == null;

        labelOptions ??= BasicLabelOptions;
        fieldOptions ??= BasicFieldOptions;

        var row = 0;

        foreach (var fieldName in fieldNames) {
            options.Add(labelOptions with {
                Padding = "8",
                HorizontalAlignment = AlignmentOptions.Fill,
                Template = fieldName.Key,
                Column = 0,
                Row = row,
                ColumnSpan = String.IsNullOrEmpty(fieldName.Value) ? 2 : 1,
            });

            if (!String.IsNullOrEmpty(fieldName.Value)) {
                options.Add(fieldOptions with {
                    BorderColor = "#AAA",
                    Padding = "8",
                    HorizontalAlignment = AlignmentOptions.Fill,
                    Template = fieldName.Value,
                    Column = 1,
                    Row = row
                });
            }

            row++;
        }

        //DG Temp hack to add a background behind labels
        if (useBasicTemplate) {
            options.Insert(0, new ObjectFieldOptions() {
                BackgroundColor = "#615f85",
                Row = 0,
                RowSpan = options.Count,
                Column = 0,
            });
        }

        return options;
    }

    /// <summary>
    /// Creates a layout that contains all displayable properties and fields for the specified <paramref name="objectType"/>
    /// </summary>
    /// <param name="objectType"></param>
    /// <param name="labelOptions">Provide a format template to be used to wrap every displayable property/field name. If omitted the template in <see cref="BasicLabelOptions"/> will be used</param>
    /// <param name="fieldOptions">Provide a format template to be used to wrap every displayable property/field value. If omitted the template in <see cref="BasicFieldOptions"/> will be used</param>
    /// <returns></returns>
    public static ObjectFieldOptionsCollection CreateFromType(Type objectType,
                                                              ObjectFieldOptions? labelOptions = null,
                                                              ObjectFieldOptions? fieldOptions = null) {
        var fieldNames = GetDisplayMembers(objectType).Select(m => m.Name);

        return CreateFromList(fieldNames, labelOptions, fieldOptions);
    }

    /// <summary>
    /// Returns list of Properties and Fields with full expanded name paths: . 
    /// </summary>
    /// <param name="objectType"></param>
    /// <param name="objName"></param>
    /// <returns></returns>
    static List<(Type MemberType, string Name)> GetDisplayMembers(Type objectType, string objName = "") {
        List<(Type MemberType, string Name)> members = new();

        if (objName.Count(c => c == '.') >= 3)
            return members; //Limit levels

        var properties = objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
            .Where(x => x.CanRead)
            .Select(p => (MemberType: p.PropertyType, Name: p.Name));

        var fields = objectType.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
            .Select(f => (MemberType: f.FieldType, Name: f.Name));

        foreach (var (MemberType, Name) in properties.Concat(fields)) {
            if (MemberType == typeof(Type)) continue; //Ignore
            if (MemberType != typeof(String) && typeof(IEnumerable).IsAssignableFrom(MemberType)) continue; //Ignore lists/arrays
            if (MemberType.Name.StartsWith("ValueTuple`")) continue; //Ignore Tuples

            if (typeof(IComparable).IsAssignableFrom(MemberType) || MemberType.IsPrimitive || MemberType.IsValueType) {
                members.Add(new(MemberType, objName + (objName == "" ? "" : ".") + Name));
            }
            else {
                members.AddRange(GetDisplayMembers(MemberType, objName + (objName == "" ? "" : ".") + MemberType.Name));
            }
        }

        return members;
    }

    /// <summary>
    /// Creates an ObjectFieldOptionsCollection to display fields as a list
    /// Optional leading bar                  Optional trailing text
    ///   vvv                                          vvv
    /// +-----+-------------------------------------+-------+
    /// |     |                                     |       |
    /// |     |   Fields specified by fieldNames    |       |
    /// |     |                                     |       |
    /// +-----+-------------------------------------+-------+
    /// </summary>
    /// <returns></returns>
    public static ObjectFieldOptionsCollection CreateListOptions(List<string> fieldNames, string? leadingBarClassScript = null, string? trailingBarTemplate = null) {
        var options = new ObjectFieldOptionsCollection();

        var hasLeadingBar = !string.IsNullOrWhiteSpace(leadingBarClassScript);
        if (hasLeadingBar) {
            options.Add(new ObjectFieldOptions() {
                Width = "7",
                Height = "1*",
                CssClassScript = leadingBarClassScript,
                Column = 0,
                Row = 0,
            });
        }

        options.Add(new ObjectFieldOptions() {
            Width = "1*",
            Height = "1*",
            Margin = "5",
            Column = hasLeadingBar ? 1 : 0,
            Row = 0,
            Items = fieldNames
                .Select((fieldName) => BasicFieldOptions with {
                    Margin = "5",
                    Template = $"{{{{{fieldName}}}}}",
                    Row = fieldNames.IndexOf(fieldName)
                })
                .ToList()
        });

        if (!String.IsNullOrEmpty(trailingBarTemplate)) {
            options.Add(new ObjectFieldOptions() {
                Width = "75",
                Height = "100%",
                Column = hasLeadingBar ? 2 : 1,
                BackgroundColor = "#615f8580",
                Items = new List<ObjectFieldOptions>()
                {
                        new()
                        {
                            Row = 0,
                            Height = "1*",
                        },
                        new()
                        {
                            Row = 1,
                            HorizontalAlignment = AlignmentOptions.Center,
                            Height = "auto",
                            FontStyles = FontStyles.Bold,
                            ForegroundColor = "#FFF",
                            Template = trailingBarTemplate,
                            Margin = "10",
                        },
                        new()
                        {
                            Row = 2,
                            Height = "1*"
                        },
                    }
            });
        }

        return options;
    }
}

[Flags]
public enum AlignmentOptions {
    Start = 1,
    Center = 2,
    End = 4,
    Fill = 8
}

[Flags]
public enum FontStyles {
    /// <summary>The font is unmodified.</summary>
    None = 0,
    /// <summary>The font is bold.</summary>
    Bold = 1,
    /// <summary>The font is italic.</summary>
    Italic = 2,
}

public enum FieldStyle {
    Text,
    Chip,
    Image,
    SVG
}

internal static class CssHelper {
    //TODO: Add UoM to RegEx
    private static readonly Regex ThicknessRegex = new("^(?:(?<value>\\-?\\d+)\\s*?\\,?\\s*?){1,4}$", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);


    internal static string? CreateThickness(string? input, string? uom = "px") {
        var match = ThicknessRegex.Match(input ?? string.Empty);

        if (!match.Success || match.Groups["value"].Captures.Count <= 0)
            return null;

        var captures = match.Groups["value"].Captures.Select(capture => double.Parse(capture.Value)).ToArray();

        return captures.Length switch {
            1 => $"{captures[0]}{uom}",
            2 => $"{captures[0]}{uom} {captures[1]}{uom}",
            3 => $"{captures[0]}{uom} {captures[1]}{uom} {captures[2]}{uom} {captures[1]}{uom}",
            4 => $"{captures[0]}{uom} {captures[1]}{uom} {captures[2]}{uom} {captures[3]}{uom}",
            _ => null
        };
    }
}

public static class StringHelpers
{
    public static string? RenderMustache(object data, string? template)
    {
        var settings = RenderSettings.GetDefaultRenderSettings();
        settings.SkipHtmlEncoding = true;
        return RenderMustache(data, template, settings);
    }


    public static string? RenderMustache(object data, string? template, RenderSettings settings)
    {
        if (string.IsNullOrWhiteSpace(template))
            return null;

        var stubble = StaticStubbleRenderer.Instance;
        return stubble.Render(template, data, settings);
    }
}