using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LanguageSwitchSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_en_Click(object sender, RoutedEventArgs e)
        {
            ResourceDictionary langRd = null;
            string langFile = @"lang/english.xaml";

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
            ResourceDictionary langRd = null;
            string langFile = @"lang/japanses.xaml";

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
    }
}
