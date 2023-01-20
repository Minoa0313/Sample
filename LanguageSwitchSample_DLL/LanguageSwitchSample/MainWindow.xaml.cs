using System;
using System.Windows;
using WpfControlLibrary1;

namespace LanguageSwitchSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Class1 class1;

        public MainWindow()
        {
            InitializeComponent();

            class1 = new Class1(uc1);
        }

        private void Button_en_Click(object sender, RoutedEventArgs e)
        {
            ChangeLang(@"lang/english.xaml");
            class1.LanguageSwitch(true);
        }

        private void ChangeLang(string path)
        {
            ResourceDictionary langRd = null;
            string langFile = path;

            try
            {
                langRd = Application.LoadComponent(new Uri(langFile, UriKind.Relative)) as ResourceDictionary;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (langRd != null)
            {
                if (this.Resources.MergedDictionaries.Count > 0)
                {
                    //一つのリソースディレクトリを使うとき、
                    //クリアした後、追加するのはOK
                    //複数のリソースディレクトリを使うとき、
                    //クリアすると、他のディレクトリがなくなるので
                    //MergedDictionaries[index]を固定して、修正したほうがいい
                    //w.Resources.MergedDictionaries.Clear();
                    this.Resources.MergedDictionaries[0] = langRd;
                }
                else
                {
                    this.Resources.MergedDictionaries.Add(langRd);
                }
            }
        }

        private void Button_ja_Click(object sender, RoutedEventArgs e)
        {
            ChangeLang(@"lang/japanses.xaml");
            class1.LanguageSwitch(false);
        }
    }
}
